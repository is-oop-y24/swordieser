using System;
using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Exceptions;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Cleaners
{
    public class CombineCleaner : IBackupCleaner
    {
        public CombineCleaner(bool both, int amount, DateTime dateTime)
        {
            BothLimits = both;
            TimeLimit = dateTime;
            if (amount < 1)
                throw new InvalidLimitNumberException();

            NumberLimit = amount;
        }

        private bool BothLimits { get; }

        private DateTime TimeLimit { get; }

        private int NumberLimit { get; }

        public List<RestorePoint> Clear(List<RestorePoint> restorePoints)
        {
            var deleteByNumber = new List<RestorePoint>();
            var deleteByTime = restorePoints.Where(rp => rp.CreationTime < TimeLimit).ToList();
            if (restorePoints.Count > NumberLimit)
                deleteByNumber = restorePoints.Take(restorePoints.Count - NumberLimit).ToList();

            List<RestorePoint> deletePoints = BothLimits
                ? deleteByTime.Intersect(deleteByNumber).ToList()
                : deleteByTime.Union(deleteByNumber).ToList();

            if (deletePoints.Count == 0)
                throw new NoRestorePointsToDelete();

            if (deletePoints.Count == restorePoints.Count)
                throw new DeletingAllRestorePointsException();

            return deletePoints;
        }
    }
}
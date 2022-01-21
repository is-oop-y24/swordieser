using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Exceptions;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Cleaners
{
    public class NumberLimitCleaner : IBackupCleaner
    {
        public NumberLimitCleaner(int amount)
        {
            if (amount < 1)
                throw new InvalidLimitNumberException();

            Limit = amount;
        }

        private int Limit { get; }

        public List<RestorePoint> Clear(List<RestorePoint> restorePoints)
        {
            var deletePoints = new List<RestorePoint>();
            if (restorePoints.Count > Limit)
                deletePoints = restorePoints.Take(restorePoints.Count - Limit).ToList();

            if (deletePoints.Count == 0)
                throw new NoRestorePointsToDeleteException();

            return deletePoints;
        }
    }
}
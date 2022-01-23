using System;
using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Exceptions;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Cleaners
{
    public class DateLimitCleaner : IBackupCleaner
    {
        public DateLimitCleaner(DateTime dateTime)
        {
            Limit = dateTime;
        }

        private DateTime Limit { get; }

        public List<RestorePoint> Clear(List<RestorePoint> restorePoints)
        {
            var deletePoints = restorePoints.Where(rp => rp.CreationTime < Limit).ToList();
            if (deletePoints.Count == 0)
                throw new NoRestorePointsToDeleteException();

            if (deletePoints.Count == restorePoints.Count)
                throw new DeletingAllRestorePointsException();

            return deletePoints;
        }
    }
}
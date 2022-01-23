using System.Collections.Generic;
using Backups;

namespace BackupsExtra.Interfaces
{
    public interface IBackupCleaner
    {
        List<RestorePoint> Clear(List<RestorePoint> restorePoints);
    }
}
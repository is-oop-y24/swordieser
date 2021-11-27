using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;
using NUnit.Framework;

namespace Backups
{
    public class BackupJob
    {
        public BackupJob(string name)
        {
            Name = name;
            RestorePoints = new List<RestorePoint>();
        }

        public string Name { get; }

        private List<RestorePoint> RestorePoints { get; }

        public IReadOnlyList<RestorePoint> GetRestorePoints()
        {
            return RestorePoints.AsReadOnly();
        }

        public RestorePoint AddRestorePoint(string name, StorageType storageType, string backupPath = "")
        {
            var rp = new RestorePoint(name, storageType, backupPath);
            RestorePoints.Add(rp);
            return rp;
        }

        public void CreateLocalBackup(RestorePoint restorePoint, ILocalStorage localStorage, string backupPath, int id)
        {
            localStorage.Save(restorePoint.Name, backupPath, id);
        }

        public void CreateVirtualBackup(IVirtualStorage virtualStorage, List<FileInfo> files, RestorePoint restorePoint, int id)
        {
            virtualStorage.Save(files, restorePoint, id);
        }
    }
}
using System.Collections.Generic;

namespace Backups.Interfaces
{
    public interface IBackupSaver // choose local or virtual
    {
        public void Save(RestorePoint restorePoint, List<Repository> repositories);
    }
}
using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups
{
    public class VirtualSaver : IBackupSaver
    {
        public void Save(RestorePoint restorePoint, List<Repository> repositories)
        {
            restorePoint.AddRepositories(repositories);
        }
    }
}
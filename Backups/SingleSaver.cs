using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;

namespace Backups
{
    public class SingleSaver : IArchiveSaver
    {
        public void Save(List<FileInfo> files, RestorePoint restorePoint, IBackupSaver saver)
        {
            var repo = new Repository();
            repo.AddFiles(files);
            saver.Save(restorePoint, new List<Repository>
            {
                repo,
            });
        }
    }
}
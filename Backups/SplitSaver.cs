using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;

namespace Backups
{
    public class SplitSaver : IArchiveSaver
    {
        public void Save(List<FileInfo> files, RestorePoint restorePoint, IBackupSaver saver)
        {
            var repositories = new List<Repository>();
            foreach (FileInfo fileInfo in files)
            {
                var repo = new Repository();
                repo.AddFiles(new List<FileInfo>
                {
                    fileInfo,
                });
                repositories.Add(repo);
            }

            saver.Save(restorePoint, repositories);
        }
    }
}
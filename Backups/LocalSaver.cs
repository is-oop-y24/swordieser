using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Interfaces;

namespace Backups
{
    public class LocalSaver : IBackupSaver
    {
        public void Save(RestorePoint restorePoint, List<Repository> repositories)
        {
            int id = 1;
            restorePoint.AddRepositories(repositories);
            var directory = new DirectoryInfo(restorePoint.Path);
            directory.Create();
            foreach (Repository repo in repositories)
            {
                var tempDirectory = new DirectoryInfo(Path.Combine(restorePoint.Path, "tempDirectory"));
                tempDirectory.Create();
                foreach (FileInfo fileInfo in repo.GetFiles())
                {
                    fileInfo.CopyTo(tempDirectory.FullName);
                }

                ZipFile.CreateFromDirectory(tempDirectory.FullName, Path.Combine(
                    directory.FullName,
                    $"archive{id++}.zip"));
                tempDirectory.Delete(true);
            }
        }
    }
}
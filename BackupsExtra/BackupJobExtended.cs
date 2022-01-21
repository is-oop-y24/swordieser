using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups;
using Backups.Interfaces;
using BackupsExtra.Interfaces;
using Serilog.Core;

namespace BackupsExtra
{
    public class BackupJobExtended : BackupJob
    {
        public BackupJobExtended(
            string name,
            IArchiveSaver archiveSaver,
            IBackupSaver backupSaver,
            IBackupCleaner backupCleaner,
            IBackupLogger backupLogger)
            : base(name, archiveSaver, backupSaver)
        {
            BackupCleaner = backupCleaner;
            BackupLogger = backupLogger;
            RestorePoints = new List<RestorePoint>();
            BackupSaver = backupSaver;
        }

        private List<RestorePoint> RestorePoints { get; }
        private IBackupSaver BackupSaver { get; }
        private IBackupCleaner BackupCleaner { get; }

        private IBackupLogger BackupLogger { get; }

        public RestorePoint CreateRestorePoint(string path, bool withTimeStamp)
        {
            Logger log = BackupLogger.CreateLog(withTimeStamp, "Created restore point");
            RestorePoint point = CreateRestorePoint(path);
            RestorePoints.Add(point);
            return point;
        }

        public RestorePoint MergeRestorePoints(RestorePoint first, RestorePoint second)
        {
            if (first.GetRepositories().Count > 1 && second.GetRepositories().Count > 1)
            {
                var filesToAdd = new List<Repository>();
                bool exist = false;
                foreach (Repository repo in first.GetRepositories())
                {
                    foreach (FileInfo file in repo.GetFiles())
                    {
                        foreach (Repository repo2 in second.GetRepositories())
                        {
                            foreach (FileInfo file2 in repo2.GetFiles())
                            {
                                if (file2.Name == file.Name)
                                {
                                    exist = true;
                                }
                            }
                        }

                        if (!exist)
                        {
                            filesToAdd.Add(repo);
                        }
                    }
                }

                BackupSaver.Save(second, filesToAdd);
            }

            RestorePoints.Remove(first);
            Directory.Delete(first.Path, true);
            BackupLogger.CreateLog(true, $"Merged restore points {first.Id} and {second.Id}");
            return second;
        }

        public void DeleteRestorePoints()
        {
            List<RestorePoint> deletePoints = BackupCleaner.Clear(RestorePoints);
            foreach (RestorePoint restorePoint in deletePoints)
            {
                RestorePoints.Remove(restorePoint);
                Directory.Delete(restorePoint.Path, true);
                BackupLogger.CreateLog(true, $"Deleted restore points at {restorePoint.Path}");
            }
        }

        public void FileRecoveryFromRestorePoint(string path, RestorePoint restorePoint)
        {
            var directory = new DirectoryInfo(path);
            directory.Create();
            foreach (FileInfo file in new DirectoryInfo(restorePoint.Path).GetFiles())
            {
                ZipFile.ExtractToDirectory(file.FullName, directory.FullName);
                BackupLogger.CreateLog(true, $"Extracted file to {directory.FullName}");
            }
        }
    }
}
using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups
{
    public class BackupJob
    {
        private List<RestorePoint> _restorePoints;

        private IArchiveSaver _archiveSaver;

        private IBackupSaver _backupSaver;

        private List<FileInfo> _files;

        public BackupJob(string name, IArchiveSaver archiveSaver, IBackupSaver backupSaver)
        {
            Name = name;
            _restorePoints = new List<RestorePoint>();
            _archiveSaver = archiveSaver;
            _backupSaver = backupSaver;
            _files = new List<FileInfo>();
        }

        public string Name { get; }

        public IReadOnlyList<RestorePoint> GetRestorePoints()
        {
            return _restorePoints.AsReadOnly();
        }

        public void AddJobObjects(List<FileInfo> files)
        {
            _files.AddRange(files);
        }

        public void DeleteJobObject(FileInfo file)
        {
            if (file == null)
            {
                throw new BackupException("null file");
            }

            _files.Remove(file);
        }

        public RestorePoint CreateRestorePoint(string path)
        {
            var rp = new RestorePoint(path);
            _restorePoints.Add(rp);
            _archiveSaver.Save(_files, rp, _backupSaver);
            return rp;
        }
    }
}
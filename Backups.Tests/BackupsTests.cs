using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Interfaces;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTests
    {
        private IArchiveSaver _archiveSaver;
        private IBackupSaver _backupSaver;

        [Test]
        public void SingleVirtualSave()
        {
            _archiveSaver = new SingleSaver();
            _backupSaver = new VirtualSaver();
            var files = new List<FileInfo>()
            {
                new FileInfo("first"),
                new FileInfo("second")
            };
            var backupJob = new BackupJob("first job", _archiveSaver, _backupSaver);
            backupJob.AddJobObjects(files);
            RestorePoint rp = backupJob.CreateRestorePoint("path");
            backupJob.DeleteJobObject(files[0]);
            RestorePoint rp1 = backupJob.CreateRestorePoint("path1");
            Assert.AreEqual(files, rp.GetRepositories()[0].GetFiles());
            Assert.AreEqual(1, rp1.GetRepositories()[0].GetFiles().Count);
        }

        [Test]
        public void SplitVirtualSave()
        {
            _archiveSaver = new SplitSaver();
            _backupSaver = new VirtualSaver();
            var files = new List<FileInfo>()
            {
                new FileInfo("first"),
                new FileInfo("second")
            };
            var backupJob = new BackupJob("first job", _archiveSaver, _backupSaver);
            backupJob.AddJobObjects(files);
            RestorePoint rp = backupJob.CreateRestorePoint("path");
            backupJob.DeleteJobObject(files[0]);
            RestorePoint rp1 = backupJob.CreateRestorePoint("path1");
            Assert.AreEqual(2, rp.GetRepositories().Count);
            Assert.AreEqual(1, rp1.GetRepositories().Count);
        }
    }
}
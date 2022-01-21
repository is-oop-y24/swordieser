using System.Collections.Generic;
using System.IO;
using Backups;
using Backups.Interfaces;
using BackupsExtra.Cleaners;
using BackupsExtra.Interfaces;
using BackupsExtra.Loggers;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        [Test]
        public void MergeTest()
        {
            string path = Path.Combine("D:", "BackupDirectory");
            Directory.CreateDirectory(path);
            IArchiveSaver splitSaver = new SplitSaver();
            IBackupSaver backupSaver = new VirtualSaver();
            IBackupCleaner backupCleaner = new NumberLimitCleaner(5);
            IBackupLogger backupLogger = new ConsoleLogger();
            var backupJob = new BackupJobExtended("Job", splitSaver, backupSaver, backupCleaner, backupLogger);
            var file1 = new FileInfo("file1");
            var file2 = new FileInfo("file2");
            var file3 = new FileInfo("file3");
            backupJob.AddJobObjects(new List<FileInfo>
            {
                file1,
                file2
            });
            var rp1 = backupJob.CreateRestorePoint(path, true);
            backupJob.DeleteJobObject(file1);
            backupJob.AddJobObjects(new List<FileInfo>
            {
                file3
            });
            var rp2 = backupJob.CreateRestorePoint(path, true);
            var rp3 = backupJob.MergeRestorePoints(rp1, rp2);
            Assert.AreEqual(3, rp3.GetRepositories().Count);
        }
    }
}
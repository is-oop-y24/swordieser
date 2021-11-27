using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTests
    {
        private BackupJob _backup;
        private RestorePoint rp;

        [SetUp]
        public void Setup()
        {
            _backup = new BackupJob("first");
            rp = _backup.AddRestorePoint("first", StorageType.Virtual);
        }

        [Test]
        public void AddFiles()
        {
            var files = new List<FileInfo>()
            {
                new FileInfo("firstFile")
            };

            _backup.CreateVirtualBackup(new SingleVirtual(), files, rp, 1);
            Assert.True(_backup.GetRestorePoints()[0].GetRepositories().Count > 0);
        }
    }
}
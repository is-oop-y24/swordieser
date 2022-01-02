using System.Collections.Generic;
using System.IO;

namespace Backups.Interfaces
{
    public interface IArchiveSaver // choose single or split
    {
        public void Save(List<FileInfo> files, RestorePoint restorePoint, IBackupSaver saver);
    }
}
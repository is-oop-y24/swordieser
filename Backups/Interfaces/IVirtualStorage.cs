using System.Collections.Generic;
using System.IO;

namespace Backups.Interfaces
{
    public interface IVirtualStorage
    {
        public void Save(List<FileInfo> files, RestorePoint restorePoint, int id);
    }
}
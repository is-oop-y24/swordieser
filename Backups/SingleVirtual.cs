using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;

namespace Backups
{
    public class SingleVirtual : IVirtualStorage
    {
        public void Save(List<FileInfo> files, RestorePoint restorePoint, int id)
        {
            Repository repo = restorePoint.AddRepository("Files_" + id);
            repo.AddFiles(files.ToArray());
        }
    }
}
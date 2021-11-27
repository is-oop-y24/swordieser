using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;

namespace Backups
{
    public class SplitVirtual : IVirtualStorage
    {
        public void Save(List<FileInfo> files, RestorePoint restorePoint, int id)
        {
            foreach (FileInfo file in files)
            {
                Repository repo = restorePoint.AddRepository(file.Name + "_" + id);
                repo.AddFiles(file);
            }
        }
    }
}
using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public class Repository
    {
        private List<FileInfo> _files;

        public Repository()
        {
            _files = new List<FileInfo>();
        }

        public IReadOnlyList<FileInfo> GetFiles()
        {
            return _files.AsReadOnly();
        }

        public void AddFiles(List<FileInfo> files)
        {
            _files.AddRange(files);
        }
    }
}
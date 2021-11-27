using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public class Repository
    {
        public Repository(string name)
        {
            Files = new List<FileInfo>();
            Name = name;
        }

        public string Name { get;  }
        private List<FileInfo> Files { get; }

        public IReadOnlyList<FileInfo> GetFiles() { return Files.AsReadOnly(); }

        public void AddFiles(params FileInfo[] files)
        {
            Files.AddRange(files);
        }
    }
}
using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public class RestorePoint
    {
        private static int _id = 1;

        public RestorePoint(string name, StorageType storageType, string backupPath)
        {
            Repositories = new List<Repository>();
            Id = _id++;
            Name = name;
            if (storageType == StorageType.Local)
            {
                var directory = new DirectoryInfo(Path.Combine(backupPath, name + Id));
                if (!directory.Exists)
                {
                    directory.Create();
                }
            }
        }

        public int Id { get; }

        public string Name { get; }
        private List<Repository> Repositories { get; }

        public IReadOnlyList<Repository> GetRepositories() { return Repositories.AsReadOnly(); }

        public Repository AddRepository(string name)
        {
            var repo = new Repository(name);
            Repositories.Add(repo);
            return repo;
        }
    }
}
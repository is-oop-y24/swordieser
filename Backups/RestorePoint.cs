using System;
using System.Collections.Generic;
using Backups.Tools;

namespace Backups
{
    public class RestorePoint
    {
        private static int _id = 1;

        private List<Repository> _repositories;

        public RestorePoint(string backupPath)
        {
            Id = _id++;
            _repositories = new List<Repository>();
            CreationTime = DateTime.Now;
            Path = backupPath;
        }

        public DateTime CreationTime { get; }

        public int Id { get; }

        public string Path { get; }

        public void AddRepositories(List<Repository> repositories)
        {
            _repositories.AddRange(repositories);
        }

        public void RemoveRepository(Repository repo)
        {
            if (repo == null)
            {
                throw new BackupException("null repo");
            }

            _repositories.Remove(repo);
        }

        public IReadOnlyList<Repository> GetRepositories()
        {
            return _repositories.AsReadOnly();
        }
    }
}
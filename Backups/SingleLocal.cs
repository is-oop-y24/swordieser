using System.IO;
using System.IO.Compression;
using Backups.Interfaces;

namespace Backups
{
    public class SingleLocal : ILocalStorage
    {
        public void Save(string restorePointName, string backupPath, int id)
        {
            string restorePointDirectory = restorePointName + id;
            var jobObjectsDirectory = new DirectoryInfo(Path.Combine(backupPath, "Job Objects"));
            string archiveName = "Files_" + id;
            string sourcePath = jobObjectsDirectory.FullName;
            string zipPath = Path.Combine(backupPath, restorePointDirectory, archiveName + ".zip");
            ZipFile.CreateFromDirectory(sourcePath, zipPath);
        }
    }
}
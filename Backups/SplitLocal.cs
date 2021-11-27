using System.IO;
using System.IO.Compression;
using Backups.Interfaces;

namespace Backups
{
    public class SplitLocal : ILocalStorage
    {
        public void Save(string restorePointName, string backupPath, int id)
        {
            string restorePointDirectory = restorePointName + id;
            var jobObjectsDirectory = new DirectoryInfo(Path.Combine(backupPath, "Job Objects"));
            foreach (FileInfo file in jobObjectsDirectory.GetFiles())
            {
                string archiveName = file.Name + "_" + id;
                string filePath = file.FullName;
                string sourcePath = jobObjectsDirectory.FullName;
                string zipPath = Path.Combine(backupPath, restorePointDirectory, archiveName + ".zip");
                using ZipArchive zipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
                zipArchive.CreateEntryFromFile(filePath, file.Name);
            }
        }
    }
}
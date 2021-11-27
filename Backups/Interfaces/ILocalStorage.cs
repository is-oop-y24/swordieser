namespace Backups.Interfaces
{
    public interface ILocalStorage
    {
        public void Save(string restorePointName, string backupPath, int id);
    }
}
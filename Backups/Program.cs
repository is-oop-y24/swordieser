using System.IO;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var rp = new RestorePoint("C:\\Users\\komra\\Desktop\\Nazvanie");
            var dick = new DirectoryInfo(rp.Path);
            dick.Create();
        }
    }
}
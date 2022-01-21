using Serilog.Core;

namespace BackupsExtra.Interfaces
{
    public interface IBackupLogger
    {
        Logger CreateLog(bool withTimeStamp, string message);
    }
}
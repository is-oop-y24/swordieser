using System.IO;
using BackupsExtra.Interfaces;
using Serilog;
using Serilog.Core;

namespace BackupsExtra.Loggers
{
    public class FileLogger : IBackupLogger
    {
        public Logger CreateLog(bool withTimeStamp, string message)
        {
            string template = withTimeStamp
                ? "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message}{NewLine}{Exception}"
                : "[{Level:u3}] {Message}{NewLine}{Exception}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
            Logger logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(path, outputTemplate: template)
                .CreateLogger();
            logger.Information(message);
            return logger;
        }
    }
}
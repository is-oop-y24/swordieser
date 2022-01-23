using BackupsExtra.Interfaces;
using Serilog;
using Serilog.Core;

namespace BackupsExtra.Loggers
{
    public class ConsoleLogger : IBackupLogger
    {
        public Logger CreateLog(bool withTimeStamp, string message)
        {
            string template = withTimeStamp
                ? "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message}{NewLine}{Exception}"
                : "[{Level:u3}] {Message}{NewLine}{Exception}";
            Logger logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: template)
                .CreateLogger();
            logger.Information(message);
            return logger;
        }
    }
}
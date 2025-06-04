using NLog;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoggerManager : ILoggerService
    {
        private readonly ILogger _logger;
        public LoggerManager()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        // Loglamada .txt dosyasına debug logu düşmesini sağlar.
        public Task LogDebug(string message)
        {
            _logger.Debug(message);
            return Task.CompletedTask;
        }

        // Loglamada .txt dosyasına error logu düşmesini sağlar.

        public Task LogError(string message)
        {
            _logger.Error(message);
            return Task.CompletedTask;
        }

        // Loglamada .txt dosyasına info logu düşmesini sağlar.

        public Task LogInfo(string message)
        {
            _logger.Info(message);
            return Task.CompletedTask;
        }

        // Loglamada .txt dosyasına warning logu düşmesini sağlar.

        public Task LogWarning(string message)
        {
            _logger.Warn(message);
            return Task.CompletedTask;
        }

    }
}

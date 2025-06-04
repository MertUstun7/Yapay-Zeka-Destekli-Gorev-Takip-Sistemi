using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ILoggerService
    {
        Task LogInfo(string message);
        Task LogWarning(string message);
        Task LogError(string message);
        Task LogDebug(string message);

    }
}

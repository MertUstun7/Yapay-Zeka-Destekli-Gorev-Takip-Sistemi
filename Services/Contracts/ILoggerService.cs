using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    // Proje genelinde loglama işlemlerini gerçekleştiren servisin arayüz tanımıdır.
    public interface ILoggerService
    {
        // Info seviyesinde log kaydı oluşturan metodun tanımı yapılmıştır.
        Task LogInfo(string message);

        // Warning seviyesinde log kaydı oluşturan metodun tanımı yapılmıştır.
        Task LogWarning(string message);

        // Error seviyesinde log kaydı oluşturan metodun tanımı yapılmıştır.
        Task LogError(string message);

        // Debug seviyesinde log kaydı oluşturan metodun tanımı yapılmıştır.
        Task LogDebug(string message);

    }
}

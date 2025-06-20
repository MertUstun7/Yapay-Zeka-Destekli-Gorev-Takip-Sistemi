using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    // Tüm servisleri tek bir yapıda toplayan arayüz tanımı yapılmıştır.
    public interface IServiceManager
    {
        // Kullanıcı işlemlerine ait servisin tanımı yapılmıştır.
        IUserService UserService { get; }
        
        // Görev işlemlerine ait servisin tanımı yapılmıştır.
        ITaskService TaskItemService { get; }

        // Rapor işlemlerine ait servisin tanımı yapılmıştır.

        IReportService ReportService { get; }

        // Kimlik doğrulama işlemlerine ait servisin tanımı yapılmıştır.

        IAuthenticationService AuthenticationService { get; }
    }
}



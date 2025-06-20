using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    // Presentation katmanında servis katmanına erişimi yöneten sınıftır.
    // Tüm servisler merkezi bir yapıda toplanmıştır.
    public class ServiceManager : IServiceManager
    {
        // Kullanıcı işlemlerine ait servisin tanımı yapılmıştır.
        public IUserService UserService { get; }

        // Görev işlemlerine ait servisin tanımı yapılmıştır.
        public ITaskService TaskItemService { get; }

        // Kimlik doğrulama işlemlerine ait servisin tanımı yapılmıştır.
        public IAuthenticationService AuthenticationService { get; }

        // Rapor işlemlerine ait servisin tanımı yapılmıştır.
        public IReportService ReportService { get; }

        // Servislerin bağımlılıklarını DI (Dependency Injection) yöntemiyle almayı sağlayan constructor'ın tanımı yapılmıştır
        public ServiceManager(ITaskService taskService, IUserService userService, IAuthenticationService authenticationService, IReportService reportService)
        {
            UserService = userService;

            AuthenticationService = authenticationService;

            TaskItemService = taskService;
            ReportService = reportService;
        }

    }
}

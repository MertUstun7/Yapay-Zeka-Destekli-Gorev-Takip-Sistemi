using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    // Presentation katmanında servislere ulaşmamızı sağlayan yapıdır.
    public class ServiceManager : IServiceManager
    {
        public IUserService UserService { get; }

        public ITaskService TaskItemService { get; }
        public IAuthenticationService AuthenticationService { get; }

        public IReportService ReportService { get; }

        public ServiceManager(ITaskService taskService, IUserService userService, IAuthenticationService authenticationService, IReportService reportService)
        {
            UserService = userService;

            AuthenticationService = authenticationService;

            TaskItemService = taskService;
            ReportService = reportService;
        }

    }
}

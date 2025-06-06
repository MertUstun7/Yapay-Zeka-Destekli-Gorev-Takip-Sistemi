using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        
        ITaskService TaskItemService { get; }
        IReportService ReportService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}

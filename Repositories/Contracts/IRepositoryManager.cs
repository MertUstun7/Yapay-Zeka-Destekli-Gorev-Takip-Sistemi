using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    //Repository manager interface oluşturularak servis içerisinden ilgili entityler üzerinde işlemler yapabilmemiz sağlanmıştır.
    public interface IRepositoryManager
    {
        IUserRepository User { get; }

        ICompanyRepository Company { get; }

        ITaskItemRepository TaskItem { get; }

        IReportRepository Report { get; }
        Task SaveAsync();
    }
}

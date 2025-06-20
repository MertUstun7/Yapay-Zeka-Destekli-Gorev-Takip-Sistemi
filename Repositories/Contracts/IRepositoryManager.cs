using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    //Repository manager interface'i oluşturularak servis içerisinden ilgili entityler üzerinde işlemler yapabilmemiz sağlanmıştır.
    public interface IRepositoryManager
    {
        // Kullanıcı ile ilgili yapılacak işlemleri gerçekleştirmek için User repository'sine erişim sağlar.
        IUserRepository User { get; }

        // Şirket ile ilgili yapılacak işlemleri gerçekleştirmek için Company repository'sine erişim sağlar.
        ICompanyRepository Company { get; }

        // Görev ile ilgili yapılacak işlemleri gerçekleştirmek için TaskItem repository'sine erişim sağlar.
        ITaskItemRepository TaskItem { get; }
        
        // Rapor ile ilgili yapılacak işlemleri gerçekleştirmek için Report repository'sine erişim sağlar.
        IReportRepository Report { get; }

        // Yapılan değişikliklerin veri tabanına yansımasını asenkron olarak sağlar.
        Task SaveAsync();
    }
}

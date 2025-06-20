using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IUserRepository : IGTSBase<User>
    {
        // trackChanges parametresi ile EF Core'un değişiklikleri izleyip izlemeyeceği belirtilir.

        // İlgili şirket bünyesinde çalışan tüm personelleri getiren asenkron metot tanımıdır.
        Task<IEnumerable<User>> GetUsersByCompanyIdAsync(Guid companyId, bool trackChanges);

        // Ad ve soyad verilerine göre kullanıcı araması yapmasını sağlayan asenkron metot tanımıdır.
        Task<IEnumerable<User>> SearchByNameAsync(string name, bool trackChanges);

    }
}





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
        // Şirkete göre kullanıcıları getir
        Task<IEnumerable<User>> GetUsersByCompanyIdAsync(Guid companyId, bool trackChanges);

        // İsme göre arama (ad veya soyad)
        Task<IEnumerable<User>> SearchByNameAsync(string name, bool trackChanges);

        // Kullanıcıyı şirket bilgisiyle birlikte getir
        Task<User> GetUserWithCompanyAsync(string userId, bool trackChanges);


    }
}

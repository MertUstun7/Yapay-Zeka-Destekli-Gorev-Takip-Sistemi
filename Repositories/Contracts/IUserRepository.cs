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
        Task<IEnumerable<User>> GetUsersByCompanyIdAsync(Guid companyId, bool trackChanges);
        Task<IEnumerable<User>> SearchByNameAsync(string name, bool trackChanges);
        Task<User> GetUserWithCompanyAsync(string userId, bool trackChanges);

    }
}

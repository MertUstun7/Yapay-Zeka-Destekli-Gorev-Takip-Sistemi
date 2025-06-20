using Repositories.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICompanyRepository:IGTSBase<Company>
    {
        // Belirtilen şirket ID'sine sahip şirketi ve şirket bünyesindeki çalışanları getiren asenkron metodun interface tanımlamasıdır.
        Task<Company> GetWithUsersAsync(Guid companyId, bool trackChanges);

        // Belirtilen ID'ye sahip şirket ile ilgili tüm bilgileri getiren asenkron metodun interface tanımlamasıdır.
        Task<Company> GetByIdAsync(Guid id, bool trackChanges);
    }
}




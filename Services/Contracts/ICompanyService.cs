using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<CompanyDto> GetByIdAsync(Guid id);
        Task<CompanyDto> CreateAsync(CompanyDtoForCreate request);
        Task UpdateAsync(Guid id, CompanyDtoForUpdate request);
        Task DeleteAsync(Guid id);
        Task AssignUserAsync(Guid companyId, string userId, string roleName);

        Task<IEnumerable<UserDto>> GetUsersByCompanyAsync(Guid companyId);

        Task<IEnumerable<UserDto>> GetManagersAsync(Guid companyId);

        Task<IEnumerable<UserDto>> GetWorkersAsync(Guid companyId);

        Task RemoveUserAsync(Guid companyId, string userId);
    }
}

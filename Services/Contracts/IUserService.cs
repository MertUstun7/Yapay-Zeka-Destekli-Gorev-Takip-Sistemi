using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Contracts
{
    using Microsoft.AspNetCore.Identity;
    

    public interface IUserService
    {
        // CREATE
        Task<UserDtoForGet> CreateAsync(UserDtoForCreate dto);

        // READ
        Task<UserDtoForGet> GetByIdAsync(string userId);
        Task<IEnumerable<UserDtoForGet>> GetAllAsync();
        Task<IEnumerable<UserDtoForGet>> GetByCompanyIdAsync(Guid companyId);
        Task<IEnumerable<UserDtoForGet>> SearchByNameAsync(string name);

        // UPDATE (PUT)
        Task UpdateAsync(string userId, UserDtoForUpdate dto);

        // PATCH
        Task PatchAsync(string userId, JsonPatchDocument<UserDtoForUpdate> patchDoc);

        // DELETE
        Task DeleteAsync(string userId);

        // PASSWORD
        Task<IdentityResult> UpdatePasswordAsync(string userId, UserPasswordUpdateDto dto);
    }

}

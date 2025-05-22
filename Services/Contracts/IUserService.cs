using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<bool> EmailExistsAsync(string email);

        Task<List<UserDtoForGet>> GetActiveUsersAsync();
        Task<List<Assignment>> GetUserAssignmentsAsync(string userId);
        Task<List<ReportDtoForGet>> GetUserReportsAsync(string userId);
        Task<UserDtoForGet> GetUserWithDetailsAsync(string userId);
        Task<List<UserDtoForGet>> SearchUsersAsync(string searchTerm);
        Task DeactivateUserAsync(string userId);
        Task ActivateUserAsync(string userId);
        Task<User> RegisterUserAsync(UserDtoForCreate userDto);   
        Task UpdateUserProfileAsync(UserDtoForUpdate user,string userId);

        Task DeleteUserProfileAsync(string userId);
        Task<List<UserDtoForGet>> GetAllUser();

        Task<bool> CheckUser(string userId);
        
    }
}

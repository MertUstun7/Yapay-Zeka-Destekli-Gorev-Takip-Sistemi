using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _repoManager;
        private readonly UserManager<User> _userManager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public UserManager(IRepositoryManager repoManager, ILoggerService logger, IMapper mapper, UserManager<User> userManager)
        {
            _repoManager = repoManager;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDtoForGet> CreateAsync(UserDtoForCreate dto)
        {
            var allowedRoles = new[] { "worker", "manager" };

            if (!allowedRoles.Contains(dto.Role?.ToLower()))
            {
                throw new ArgumentException("Invalid role");
            }
                var user = _mapper.Map<User>(dto);
                var result = await _userManager.CreateAsync(user, dto.Password);
                await _userManager.AddToRoleAsync(user, dto.Role);
                if (!result.Succeeded)
                {
                await _logger.LogError($"{dto.Email} kullanıcısı veri tabanına eklenirken hata meydana geldi.");
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                
                    

                return _mapper.Map<UserDtoForGet>(user);

            
           
        }

        public async Task DeleteAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");
            await _logger.LogWarning($"{userId} veri tabanından silindi");
            await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<UserDtoForGet>> GetAllAsync()
        {
            var users = await _repoManager.User.FindAllAsync(false);
            await _logger.LogWarning($"Kullanıcılar listelendi");

            return users.Select(u => _mapper.Map<UserDtoForGet>(u));
        }

        public async Task<IEnumerable<UserDtoForGet>> GetByCompanyIdAsync(Guid companyId)
        {
            var users = await _repoManager.User.GetUsersByCompanyIdAsync(companyId, false);
            await _logger.LogWarning($"Kullanıcılar listelendi");

            return users.Select(u => _mapper.Map<UserDtoForGet>(u));
        }

        public async Task<UserDtoForGet> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)

                ?? throw new KeyNotFoundException("User not found");
            await _logger.LogWarning($"Kullanıcı listelendi.");

            return _mapper.Map<UserDtoForGet>(user);
        }

        public async Task PatchAsync(string userId, JsonPatchDocument<UserDtoForUpdate> patchDoc)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");

            var userDto = _mapper.Map<UserDtoForUpdate>(user);
            patchDoc.ApplyTo(userDto);
            _mapper.Map(userDto, user);
            await _logger.LogWarning($"Kullanıcı güncellendi.");

            await _userManager.UpdateAsync(user);
        }

        public async Task<IEnumerable<UserDtoForGet>> SearchByNameAsync(string name)
        {
            var users = await _repoManager.User.SearchByNameAsync(name, false);
            return users.Select(u => _mapper.Map<UserDtoForGet>(u));
        }

        public async Task UpdateAsync(string userId, UserDtoForUpdate dto)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");

            _mapper.Map(dto, user);
            await _logger.LogWarning($"Kullanıcı güncellendi.");

            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdatePasswordAsync(string userId, UserPasswordUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");
            await _logger.LogWarning($"{userId} kullanıcısının şifresi güncellendi.");

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            return result;
        }
    }
}

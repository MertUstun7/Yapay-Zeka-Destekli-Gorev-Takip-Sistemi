using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _repoManager;
        private readonly UserManager<User> _userManager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserManager(IRepositoryManager repoManager, ILoggerService logger, IMapper mapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _repoManager = repoManager;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }
        // Worker, Manager, CompanyOwner rolünde kullanıcı oluşturulmasını sağlar ve veri tabanına ekler.
        public async Task<UserDtoForGet> CreateAsync(UserDtoForCreate dto)
        {
            var allowedRoles = new[] { "worker", "manager","companyowner" };

            if (!allowedRoles.Contains(dto.Role?.ToLower()))
            {
                throw new ArgumentException("Invalid role");
            }
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value
                ?? throw new UnauthorizedAccessException("Kullanıcı bilgisi alınamadı.");

            var creator = await _userManager.FindByEmailAsync(email)
                ?? throw new Exception("Creator bulunamadı.");

            var user = _mapper.Map<User>(dto);
                var result = await _userManager.CreateAsync(user, dto.Password);
                user.CompanyId = creator.CompanyId;
            await _userManager.AddToRoleAsync(user, dto.Role);
                if (!result.Succeeded)
                {
                await _logger.LogError($"{dto.Email} kullanıcısı veri tabanına eklenirken hata meydana geldi.");
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                
                    

                return _mapper.Map<UserDtoForGet>(user);
        }

        // Kullanıcı siler.
        public async Task DeleteAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new KeyNotFoundException("Kullanıcı bulunamadı");
            await _logger.LogWarning($"{userId} veri tabanından silindi");
            await _userManager.DeleteAsync(user);
        }

        // Veri tabanında yer alan tüm kullanıcıları listeler.
        public async Task<IEnumerable<UserDtoForGet>> GetAllAsync()
        {
            var users = await _repoManager.User.FindAllAsync(false);
            await _logger.LogWarning($"Kullanıcılar listelendi");

            return users.Select(u => _mapper.Map<UserDtoForGet>(u));
        }

        // Giriş yapan kişinin şirketinde yer alan çalışanları listeler (company owner rolü hariç).
        public async Task<IEnumerable<UserDtoForGet>> GetByCompanyIdAsync()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value
                ?? throw new UnauthorizedAccessException("Kullanıcı doğrulanamadı.");

            var currentUser = await _userManager.FindByEmailAsync(email)
                ?? throw new KeyNotFoundException("Kullanıcı bulunamadı");

            var users = await _repoManager.User.GetUsersByCompanyIdAsync(currentUser.CompanyId.Value, false);
            await _logger.LogWarning($"Kullanıcılar listelendi");

            var filtereduser=new List<User>();

            foreach (var user in users)
            {
                var roles=await _userManager.GetRolesAsync(user);
                if (!roles.Contains("CompanyOwner"))
                {
                    filtereduser.Add(user);
                }
            }

            return filtereduser.Select(u => _mapper.Map<UserDtoForGet>(u));
        }

        // Kullanıcının ID'si ile bilgilerini getirir.
        public async Task<UserDtoForGet> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)

                ?? throw new KeyNotFoundException("User not found");
            await _logger.LogWarning($"Kullanıcı listelendi.");

            return _mapper.Map<UserDtoForGet>(user);
        }

        // Kullanıcı bilgilerinden spesifik bir alanı günceller.
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

        // İsme göre kullanıcıyı arar.
        public async Task<IEnumerable<UserDtoForGet>> SearchByNameAsync(string name)
        {
            var users = await _repoManager.User.SearchByNameAsync(name, false);
            return users.Select(u => _mapper.Map<UserDtoForGet>(u));
        }

        // Kullanıcı bilgilerini günceller.
        public async Task UpdateAsync(string userId, UserDtoForUpdate dto)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");

            _mapper.Map(dto, user);

           
            await _logger.LogWarning($"Kullanıcı güncellendi.");

            await _userManager.UpdateAsync(user);
        }

        // Kullanıcı şifresini günceller.
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

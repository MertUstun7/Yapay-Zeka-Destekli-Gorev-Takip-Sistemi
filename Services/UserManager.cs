using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _userManager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public UserManager(IRepositoryManager userManager, ILoggerService logger, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        

        public async Task<bool> EmailExistsAsync(string email)
        {
            await _logger.LogInfo($"{email} maili için kontrol sağlama işlemi başlatıldı.");
            return await _userManager.User.EmailExistsAsync(email);
        }

        

        

        

        public async Task<List<UserDtoForGet>> GetActiveUsersAsync()
        {
            await _logger.LogInfo($"GetActiveUsersAsync işlemi başlatıldı");

            var activeuser = await _userManager.User.GetActiveUsersAsync();
            var entity = _mapper.Map<List<UserDtoForGet>>(activeuser);
            if (entity is null)
            {
                string message = $"Aktif kullanıcı yok.";
                await _logger.LogInfo(message);
                throw new Exception(message);
            }
            return entity;

        }

       


        public async Task<List<UserDtoForGet>> SearchUsersAsync(string searchTerm)
        {
            await _logger.LogInfo($"SearchUsersAsync işlemi başlatıldı");

            var users= await _userManager.User.SearchUsersAsync(searchTerm);
            var entity = _mapper.Map<List<UserDtoForGet>>(users);
            return entity;
        }

        public async Task DeactivateUserAsync(string userId)
        {
            await _logger.LogInfo($"DeactivateUserAsync işlemi başlatıldı");
            try
            {
                await _userManager.User.DeactivateUserAsync(userId);
                
                await _userManager.SaveAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
            
        }
        public async Task ActivateUserAsync(string userId)
        {
            await _logger.LogInfo($"ActivateUserAsync işlemi başlatıldı");
            try
            {
                await _userManager.User.ActivateUserAsync(userId);

                await _userManager.SaveAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        public async Task<User> RegisterUserAsync(UserDtoForCreate userDto)
        {
            await _logger.LogInfo($"RegisterUserAsync işlemi başlatıldı");

            if (await _userManager.User.EmailExistsAsync(userDto.Email))
                throw new Exception("Bu email zaten kayıtlı.");

            var entity=_mapper.Map<User>(userDto);

            await _userManager.User.CreateAsync(entity);
            await _userManager.SaveAsync();

            await _logger.LogInfo($"{userDto} kullanıcısı veri tabanına eklenildi.");


            return entity;
        }

        

        public async Task<List<UserDtoForGet>> GetAllUser()
        {
            await _logger.LogInfo($"GetAllUser işlemi başladı");
            var users=await _userManager.User.FindAllAsync(false);
            var entity = _mapper.Map<List<UserDtoForGet>>(users);
            return entity;
        }

        public async Task<bool> CheckUser(string userId)
        {
            return await _userManager.User.CheckUserId(userId);
        }

        public async Task DeleteUserProfileAsync(string userId)
        {
            await _userManager.User.DeleteUserAsync(userId);
        }
    }
}

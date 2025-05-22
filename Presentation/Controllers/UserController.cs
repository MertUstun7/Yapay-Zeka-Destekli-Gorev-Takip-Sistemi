using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController:ControllerBase
    {
      
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;


        public UserController(IServiceManager serviceManager, ILoggerService logger)
        {
            _manager = serviceManager;
            _logger = logger;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                await _logger.LogInfo("Tüm kullanıcılar listelenme işlemi gerçekleşiyor...");

                var users = await _manager.UserService.GetAllUser();
                
                await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");

                return Ok(users);
            }
            catch (Exception ex)
            {
               await _logger.LogError($"Tüm kullanıcılar getirilirken bir hata meydana geldi. Error:{ex} ");
                throw;
            }
         
            
        }
     
        [HttpGet("active-users")]

        public async Task<IActionResult> GetActiveUsers()
        {
            try
            {

                await _logger.LogInfo("Aktif kullanıcılar ekranda listeleme işlemi gerçekleşiyor...");

                var users = await _manager.UserService.GetActiveUsersAsync();
                
                await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");

                return Ok(users);
            }
            catch (Exception ex)
            {
                await _logger.LogError($"Tüm aktif kullanıcılar getirilirken bir hata meydana geldi. Error:{ex} ");

                throw;
            }


        }

        [HttpGet("{userId}")]

        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {
                await _logger.LogInfo($"{userId} kullanıcısı ekranda gösteriliyor...");

                var users = await _manager.UserService.GetUserWithDetailsAsync(userId);
                
                await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");

                return Ok(users);
            }
            catch (Exception ex)
            {
                await _logger.LogError($"{userId} kullanıcısı getirilirken bir hata meydana geldi. Error:{ex} ");
                throw;
            }
            

        }

        [HttpGet("{userId}/assignments")]
        public async Task<ActionResult<List<Assignment>>> GetUserAssignments(string userId)
        {
            await _logger.LogInfo($"{userId} no'lu çalışan için görevleri getirme işlemi gerçekleşiyor...");

            var user_valid = await _manager.UserService.CheckUser(userId);

            if (user_valid)
            {
                var assignments = await _manager.UserService.GetUserAssignmentsAsync(userId);
                if (assignments == null || assignments.Count == 0)
                {
                    throw new AssignmentNotFoundException(userId);
                }
                await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");
                return Ok(assignments);

            }
            else
            {
                await _logger.LogError($"{userId} kullanıcısı getirilirken bir hata meydana geldi.");

                throw new UserNotFoundException(userId);
            }

        }

        [HttpGet("{userId}/reports")]
        public async Task<ActionResult<List<Report>>> GetReport(string userId)
        {
            await _logger.LogInfo($"{userId} numaralı kullanıcı için raporları getirme işlemi başlatılıyor...");
            var user_valid = await _manager.UserService.CheckUser(userId);
            if (user_valid)
            {
                var reports = await _manager.UserService.GetUserReportsAsync(userId);
                if (reports == null || reports.Count == 0)
                {
                    throw new ReportNotFoundException(userId);
                }
                await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");

                return Ok(reports);
            }
            else
            {
                throw new UserNotFoundException(userId);
            }
        }
        [HttpGet("user-searching")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            try
            {
                await _logger.LogInfo($"{searchTerm} kullanıcı için raporları getirme işlemi başlatılıyor...");

                var users = await _manager.UserService.SearchUsersAsync(searchTerm);
                if (users == null || users.Count == 0)
                {
                    throw new UserNotFoundException(searchTerm);

                }
                await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");

                return Ok(users);
            }
            catch (Exception ex)
            {
                await _logger.LogError($"{searchTerm} kullanıcısı getirilirken bir hata meydana geldi.");

                throw;
            }


        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] UserDtoForCreate userDto)
        {
            await _logger.LogInfo($"Kullanıcı kayıt işlemi başlatılıyor...");

            if (userDto == null)
                return BadRequest("Kullanıcı bilgisi eksik.");

            try
            {
                var user = await _manager.UserService.RegisterUserAsync(userDto);
                await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");

                return CreatedAtAction(nameof(GetUser), new {userId=user.Id},new {Message="Kullanıcı oluşturuldu."});
            }
            catch (Exception ex)
            {
                await _logger.LogError($"Kullanıcı kayıdı esnasında bir hata meydana geldi.");

                throw;
            }
        }

        

        [HttpPut("{userId}/deactivation")]

        public async Task<IActionResult> Deactive(string userId)
        {
            await _logger.LogInfo($"{userId} kullanıcısı için deaktif etme işlemi başlatılıyor...");


            var user_valid = await _manager.UserService.CheckUser(userId);
            if (!user_valid)
                throw new UserNotFoundException(userId);

            await _manager.UserService.DeactivateUserAsync(userId);
            await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");
            return NoContent();
            

        }

        [HttpPut("{userId}/activation")]
        public async Task<IActionResult> Active(string userId)
        {
            await _logger.LogInfo($"{userId} kullanıcısı için aktif etme işlemi başlatılıyor...");


            var user_valid = await _manager.UserService.CheckUser(userId);
            if (!user_valid)
                throw new UserNotFoundException(userId);

            await _logger.LogInfo($"{userId} numaralı kullanıcı için aktif etme işlemi gerçekleşiyor.");
            await _manager.UserService.ActivateUserAsync(userId);
            await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");
            return NoContent();


        }

        [HttpPut("{userId}")]
        public async Task <IActionResult> UpdateUser([FromBody] UserDtoForUpdate userInformation, string userId)
        {
            await _logger.LogInfo($"{userId} kullanıcısı için güncelleme işlemi başlatılıyor...");

            var user_valid = await _manager.UserService.CheckUser(userId);
            if (!user_valid)
                throw new UserNotFoundException(userId);
            await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");

            await _manager.UserService.UpdateUserProfileAsync(userInformation,userId);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task <IActionResult> DeleteUser(string userId)
        {
            await _logger.LogInfo($"{userId} kullanıcısı için silme işlemi başlatılıyor...");

            await _logger.LogInfo($"{userId} için silme işlemi gerçekleşiyor");
            var user_valid = await _manager.UserService.CheckUser(userId);
            if (!user_valid)
                throw new UserNotFoundException(userId);
            await _manager.UserService.DeleteUserProfileAsync(userId);
            await _logger.LogInfo("İşlem başarılı bir şekilde gerçekleşti.");

            return NoContent();
        }

    }


}


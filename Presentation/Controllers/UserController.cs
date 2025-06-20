using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
      
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerService _logger;


        public UserController(IServiceManager serviceManager, ILoggerService logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

    

        [HttpPost]
        [Authorize(Roles = "Admin,CompanyOwner,Manager")]

        public async Task<IActionResult> CreateUser([FromBody] UserDtoForCreate dto)
        {
            await _logger.LogInfo($" [POST] api/user/{dto.Email} veri tabanına ekleniyor...");
            var createdUser = await _serviceManager.UserService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, createdUser);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            //Tüm kullanıcıları listelememizi sağlar.
            await _logger.LogInfo("[GET] api/user isteği gönderildi.");
            var users = await _serviceManager.UserService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin,Manager,CompanyOwner")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            // Id'de yer alan kullanıcı bilgilerini getirmemizi sağlar.
            await _logger.LogInfo("[GET] api/user/{userId} isteği gönderildi.");
            var user = await _serviceManager.UserService.GetByIdAsync(userId);
            return Ok(user);
        }

        [HttpGet("company")]
        [Authorize(Roles = "Admin,Manager,CompanyOwner")]
        public async Task<IActionResult> GetUsersByCompany()
        {
            //Şirket içerisinde yer alan kişileri getirmemizi sağlar.
            await _logger.LogInfo("[GET] api/user/company isteği gönderildi.");
            var users = await _serviceManager.UserService.GetByCompanyIdAsync();
            return Ok(users);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin,Manager,CompanyOwner,Worker")]
        public async Task<IActionResult> SearchUsers([FromQuery] string name)
        {
            // Kullanıcı isim ve soyisme göre arama yapmamızı sağlar.
            await _logger.LogInfo("[GET] api/user/search isteği gönderildi.");
            var users = await _serviceManager.UserService.SearchByNameAsync(name);
            return Ok(users);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin,Manager,CompanyOwner,Worker")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserDtoForUpdate dto)
        {
            //Kullanıcı bilgilerini güncellememizi sağlar.
            await _logger.LogInfo("[PUT] api/user/{userId} isteği gönderildi.");
            await _serviceManager.UserService.UpdateAsync(userId, dto);
            return NoContent();
        }

        [HttpPatch("{userId}")]
        [Authorize(Roles = "Admin,Manager,CompanyOwner,Worker")]
        public async Task<IActionResult> PatchUser(string userId, [FromBody] JsonPatchDocument<UserDtoForUpdate> patchDoc)
        {
            //Kullanıcı bilgilerini güncellememizi sağlar.
            await _logger.LogInfo("[PATCH] api/user/{userId} isteği gönderildi.");
            await _serviceManager.UserService.PatchAsync(userId, patchDoc);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin,Manager,CompanyOwner")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            // Girilen ID'deki kullanıcıyı silmemizi sağlar.
            await _logger.LogInfo("[DELETE] api/user/{userId} isteği gönderildi.");
            await _serviceManager.UserService.DeleteAsync(userId);
            return NoContent();
        }

        [HttpPut("{userId}/update-password")]
        [Authorize(Roles = "Admin,Manager,CompanyOwner,Worker")]
        public async Task<IActionResult> UpdatePassword(string userId, [FromBody] UserPasswordUpdateDto dto)
        {
            // Girilen ID'deki kullanıcı şifresini güncellememizi sağlar.
            await _logger.LogInfo("[PUT] api/user/{userId}/update-password isteği gönderildi.");
            var result = await _serviceManager.UserService.UpdatePasswordAsync(userId, dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }








    }


}


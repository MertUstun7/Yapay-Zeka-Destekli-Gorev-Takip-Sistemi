using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController:ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCompanyOwner([FromBody] CompanyOwnerRegistrationDto companyOwnerRegistrationDto)
        {
            var result=await _service.
                AuthenticationService.
                RegisterCompanyOwner(companyOwnerRegistrationDto);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);


        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized();

            var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);

            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        
        public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
        {
            if (string.IsNullOrWhiteSpace(tokenDto.AccessToken) || string.IsNullOrWhiteSpace(tokenDto.RefreshToken))
                return BadRequest("accessToken ve refreshToken zorunludur.");

            var refreshtoken =await _service.AuthenticationService.RefreshToken(tokenDto);
            return Ok(refreshtoken);
        }
    }


}

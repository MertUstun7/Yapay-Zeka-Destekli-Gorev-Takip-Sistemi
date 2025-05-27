using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ICompanyService _companyManager;
        private readonly IConfiguration _configuration;
        private User? _user;
        public AuthenticationManager(ILoggerService logger,ICompanyService companyManager, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _companyManager = companyManager;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signinCredentials = GetSinginCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

            var refrehToken = GenerateRefreshToken();
            _user.RefreshToken= refrehToken;
            
            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(2);

            await _userManager.UpdateAsync(_user);

            var accessToken= new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refrehToken,

            };

        }



        public async Task<IdentityResult> RegisterCompanyOwner(CompanyOwnerRegistrationDto companyOwnerRegistrationDto)
        {
            try
            {
                await _logger.LogInfo("Şirket hesabı oluşturuluyor...");

                var user = _mapper.Map<User>(companyOwnerRegistrationDto);
                user.UserDescription = "Şirket Sahibi";
                user.Department = "Şirket Sahibi";
                var result = await _userManager.CreateAsync(user, companyOwnerRegistrationDto.Password);

                if (result.Succeeded)
                {
                    await _logger.LogInfo($"Vergi numarası: {companyOwnerRegistrationDto.TaxNumber} için  kayıt oluşturuldu");
                    await _userManager.AddToRoleAsync(user, "CompanyOwner");
                    var company = _mapper.Map<Company>(companyOwnerRegistrationDto);
                    //company.OwnerId = user.Id;
                    
                    var companyCreateDto = new CompanyDtoForCreate
                    {
                        Name = companyOwnerRegistrationDto.CompanyName,
                        TaxNumber = companyOwnerRegistrationDto.TaxNumber,
                        OwnerId = user.Id
                    };
                    string name = companyCreateDto.Name;
                    // Bu satır, companies tablosuna yeni kaydı ekler
                    var companyDto = await _companyManager.CreateAsync(companyCreateDto);
                    await _logger.LogInfo($"Şirket kaydı tamamlandı: {companyDto.Id}");

                    // 4) Kullanıcının CompanyId alanını güncelle
                    user.CompanyId = companyDto.Id;
                    await _userManager.UpdateAsync(user);

                    return result;

                }

                else
                {
                    await _logger.LogError($"Vergi no: {companyOwnerRegistrationDto.TaxNumber} için hesap oluşturulamadı ");
                    return result;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogError($"Vergi no: {companyOwnerRegistrationDto.TaxNumber} için hesap oluşturulamadı");
                throw new Exception(ex.Message);

            }

        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userFourAuthDto)
        {
            _user = await _userManager.FindByEmailAsync(userFourAuthDto.Mail);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userFourAuthDto.Password));

            if (!result)
            {
                await _logger.LogWarning($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password.");
            }
            return result;
        }
        private SigningCredentials GetSinginCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        }

        private async Task<List<Claim>> GetClaims()
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,_user.Id.ToString()),
                new Claim(ClaimTypes.Name, _user.UserName),                
                new Claim(ClaimTypes.GivenName, _user.FirstName),          
                new Claim(ClaimTypes.Surname, _user.LastName),       
        
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles) {

                claims.Add(new Claim(ClaimTypes.Role, role));

            }

            return claims;


        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signinCredentials
                );
            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rndnumber = RandomNumberGenerator.Create())
            {
                rndnumber.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];
            var tokenValidation = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                RoleClaimType = ClaimTypes.Role,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidation, out securityToken);

            var jwtSecurityToken=securityToken as JwtSecurityToken;

            if(jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token.");
            }
            return principal;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal=GetPrincipalFromExpiredToken(tokenDto.AccessToken);

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null|| user.RefreshToken!=tokenDto.RefreshToken||user.RefreshTokenExpiryTime<=DateTime.Now) {

                throw new RefreshTokenBadRequestException();
            }


            _user = user;
            return await CreateToken(populateExp: false);

        }
    }
}

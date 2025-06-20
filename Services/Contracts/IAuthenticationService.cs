using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    // Kullanıcı kimlik doğrulama işlemlerini gerçekleştiren servisin arayüzünü tanımlamaktadır.
    public interface IAuthenticationService
    {
        // Şirket sahibi olarak olurken kayıt kullanıcının verilerini veri tabanına işlenmesini sağlayan metot tanımı yapılmıştır.
        Task<IdentityResult> RegisterCompanyOwner(CompanyOwnerRegistrationDto companyOwnerRegistrationDto);
        // Kullanıcı siteye giriş yaparken kimlik doğrulama işlemlerini gerçekleştirilmesini sağlayan metodun tanımı yapılmıştır.
        Task<bool> ValidateUser(UserForAuthenticationDto userFourAuthDto);
        // JWT Token oluşturan metodun tanımı yapılmıştır.
        Task<TokenDto> CreateToken(bool populateExp);
        // Refresh Token oluşturan metodun tanımı yapılmıştır.
        Task<TokenDto> RefreshToken(TokenDto tokenDto);

    }
}

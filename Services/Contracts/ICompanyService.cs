using Entities.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    // Şirket ile ilgili yapılacak işlemleri tanımlayan servis arayüzüdür.
    public interface ICompanyService
    {
        // Tüm şirketleri listeleyen metodun tanımıdır.
        Task<IEnumerable<CompanyDto>> GetAllAsync();

        // Belirtilen ID' ye sahip şirketin verilerini getiren metodun tanımıdır.
        Task<CompanyDto> GetByIdAsync(Guid id);

        // Veri tabanına yeni bir şirket kaydı ekleyen metodun tanımıdır.
        Task<CompanyDto> CreateAsync(CompanyDtoForCreate request);

        // Veri tabanında mevcut bir şirketin bilgilerini güncelleyen metodun tanımıdır.
        Task UpdateAsync(Guid id, CompanyDtoForUpdate request);

        // Veri tabanında mevcut olan bir kaydı silen metodun tanımıdır.
        Task DeleteAsync(Guid id);

        // Kullanıcıyı belirtilen şirkete atayan ve şirketteki rolünü belirleyen metodun tanımıdır.
        Task AssignUserAsync(Guid companyId, string userId, string roleName);

        // Şirket bünyesinde bulunan tüm personelleri listeleyen metodun tanımı yapılmıştır.
        Task<IEnumerable<UserDto>> GetUsersByCompanyAsync(Guid companyId);

        // Şirket bünyesinde bulunan "Manager" rolündeki kullanıcıları listeleyen metodun tanımı yapılmıştır.
        Task<IEnumerable<UserDto>> GetManagersAsync(Guid companyId);

        // Şirkete ait "Worker" rolündeki kullanıcıları listeleyen metodun tanımı yapılmıştır.
        Task<IEnumerable<UserDto>> GetWorkersAsync(Guid companyId);

        // Kullanıcıyı şirket bünyesinden çıkarır ve rolünü kaldıran metodun tanımı yapılmıştır.
        Task RemoveUserAsync(Guid companyId, string userId);

        // Şirket içerisinden spesifik bir alanı günceller.
        Task PatchCompanyAsync(Guid id, JsonPatchDocument<Company> patchDoc);
    }
}

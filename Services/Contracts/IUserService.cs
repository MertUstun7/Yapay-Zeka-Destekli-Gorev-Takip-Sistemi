using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{

    
    // Kullanıcılarla ilgili işlemleri gerçekleştiren servisin arayüzü tanımlanmıştır.
    public interface IUserService
    {
        // Yeni bir kullanıcı kayıdı oluşturan metodun tanımı yapılmıştır.
        Task<UserDtoForGet> CreateAsync(UserDtoForCreate dto);

        // İlgili ID'deki kullanıcıyı getiren metodun tanımı yapılmıştır.
        Task<UserDtoForGet> GetByIdAsync(string userId);

        // Tüm kullanıcıları listeleyen metodun tanımı yapılmıştır.
        Task<IEnumerable<UserDtoForGet>> GetAllAsync();

        // Giriş yapan kullanıcının şirketinde çalışan tüm personelleri (şirket sahibi hariç) listeleyen metodun tanımı yapılmıştır.
        Task<IEnumerable<UserDtoForGet>> GetByCompanyIdAsync();

        // Belirli bir isme göre kullanıcıyı listeleyen metodun tanımı yapılmıştır.
        Task<IEnumerable<UserDtoForGet>> SearchByNameAsync(string name);

        // Kullanıcı bilgilerini güncellemeyi sağlayan metodun tanımı yapılmıştır.
        Task UpdateAsync(string userId, UserDtoForUpdate dto);

        // Kullanıcıya ait verilerin yalnızca belirtilen alanlarını güncelleyen metodun tanımı yapılmıştır.
        Task PatchAsync(string userId, JsonPatchDocument<UserDtoForUpdate> patchDoc);

        // İlgili ID'deki kullanıcıyı silen metodun tanımı yapılmıştır.
        Task DeleteAsync(string userId);

        // Kullanıcının şifresini güncelleyebilmesini sağlayan metodun tanımı yapılmıştır.
        Task<IdentityResult> UpdatePasswordAsync(string userId, UserPasswordUpdateDto dto);
    }

}

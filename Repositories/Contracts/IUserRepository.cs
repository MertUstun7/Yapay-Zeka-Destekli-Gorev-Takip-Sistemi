using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IUserRepository : IGTSBase<User>
    {
        
        Task<bool> EmailExistsAsync(string email);

        // Kullanıcı türüne göre filtreleme fonksiyonlarının tanımı yapılmıştır.

        Task<bool> CheckUserId(string userId);

        // İlişkili verilerle kullanıcı çekme fonksiyonunun tanımı 
        

        // Durum bazlı sorgular fonksiyonunun tanımı yapılmıştır.
        Task<List<User>> GetActiveUsersAsync();

        // Görev ve raporlarla ilgili sorgulama fonksiyonlarının tanımı yapılmıştır.
        
    

        // Arama ve filtreleme fonksiyonunun tanımı yapılmıştır.
        Task<List<User>> SearchUsersAsync(string searchTerm);

        // Soft delete ve durum güncelleme fonksiyonlarının tanımı yapılmıştır.
        Task DeactivateUserAsync(string userId);
        Task ActivateUserAsync(string userId);

        Task DeleteUserAsync(string userId);
        IQueryable<User> GetAllQuery(bool trackChanges); 

    }
}

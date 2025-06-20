using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IReportRepository:IGTSBase<TaskReport>
    {
        // trackChanges parametresi ile EF Core'un değişiklikleri izleyip izlemeyeceği belirtilir.

        // İlgili görev ID'sine ait tüm raporları getiren asenkron metot tanımıdır.
        Task<List<TaskReport>> GetReportByIdAsync(Guid taskId, bool trackChanges);

        // İlgili kullanıcı ID'sine (şirket personeli) sahip kişinin oluşturduğu tüm raporları getiren asenkron metot tanımıdır.
        Task<List<TaskReport>> GetReportByUserIdAsync(string userId, bool trackChanges);

        // Belirtilen şirket ID'sinde bulunan tüm raporları getiren asenkron metot tanımıdır.
        Task<List<TaskReport>> GetReportsByCompanyIdAsync(Guid companyId, bool trackChanges);

    }
}





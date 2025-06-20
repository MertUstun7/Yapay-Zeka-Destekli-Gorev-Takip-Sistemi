using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    // Rapor ile ilgili işlemleri gerçekleştiren servisin arayüz tanımı yapılmıştır.
    public interface IReportService
    {
        // Veri tabanına rapor kaydı ekleyen metodun tanımı yapılmıştır.
        Task<ReportDtoForGet> CreateReportAsync(ReportDtoForCreate dto);

        // Belirtilen göreve ait gönderilmiş tüm raporları listelememizi sağlayan metodun tanımı yapılmıştır.
        Task<IEnumerable<ReportDtoForGet>> GetReportsByTaskIdAsync(Guid taskId);

        // Sisteme giriş yapan kullanıcının oluşturmuş olduğu tüm raporları listeleyen metodun tanımı yapılmıştır.
        Task<IEnumerable<ReportDtoForGet>> GetMyReportsAsync();

        // Şirket bünyesindeki yüklenen tüm raporları listeleyen metodun tanımı yapılmıştır.
        Task<IEnumerable<ReportDtoForGet>> GetReportsByCompanyAsync();

        // Belirtilen rapora ait yüklenen PDF dosyasını getiren metodun tanımı yapılmıştır.
        Task<ReportFileDto> GetReportFileAsync(Guid id);

        // ID'si gönderilen raporu veri tabanında silinmesini sağlayan metodun tanımı yapılmıştır.
        Task DeleteTaskAsync(Guid id);
    }
}



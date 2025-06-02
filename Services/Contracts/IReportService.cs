using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IReportService
    {
        Task<ReportDtoForGet> CreateReportAsync(ReportDtoForCreate dto);
        Task<IEnumerable<ReportDtoForGet>> GetReportsByTaskIdAsync(Guid taskId);
        Task<IEnumerable<ReportDtoForGet>> GetMyReportsAsync();

        Task<IEnumerable<ReportDtoForGet>> GetReportsByCompanyAsync();

        Task<ReportFileDto> GetReportFileAsync(Guid id);

        Task DeleteTaskAsync(Guid id);
    }
}

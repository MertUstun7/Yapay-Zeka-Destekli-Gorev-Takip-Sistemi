using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;      
        private readonly ILoggerService _logger;
        public ReportController(ILoggerService logger,IServiceManager serviceManager)
        {       
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Authorize(Roles ="Admin,CompanyOwner,Worker,Manager")]
        //Rapor oluşturmamızı sağlar.
        public async Task<IActionResult> CreateReport([FromForm] ReportDtoForCreate reportDto)
        {  
            await _logger.LogInfo("[POST]api/reports isteği geldi.");
            var result = await _serviceManager.ReportService.CreateReportAsync(reportDto);
            return Ok();
        }
        [HttpGet("task/{taskId}")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager")]

        public async Task<IActionResult> GetReportsById(Guid taskId)
        {
            // İlgili ID'deki raporu getirmemizi sağlar.
            await _logger.LogInfo($"[GET]api/reports/task/{taskId} isteği geldi.");

            var reports = await _serviceManager.ReportService.GetReportsByTaskIdAsync(taskId);
            return Ok(reports);
        }
        [HttpGet("my-reports")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager,Worker")]
        public async Task<IActionResult> GetMyReports()
        {  
            //Kişinin oluşturduğu raporları getirmemizi sağlar.
            await _logger.LogInfo($"[GET]api/reports/my-reports isteği geldi.");       
            var reports= await _serviceManager.ReportService.GetMyReportsAsync();
            return Ok(reports);
        }

        [HttpGet("company")]
        [Authorize(Roles ="Admin,CompanyOwner,Manager")]
        public async Task<IActionResult> GetCompanyReports()
        {
            //İlgili şirkette yer alan tüm raporları getirmemizi sağlar
            await _logger.LogInfo($"[GET]api/reports/company isteği geldi.");
            var reports = await _serviceManager.ReportService.GetReportsByCompanyAsync();
            return Ok(reports);
        }
        [HttpGet("download/{id}")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager")]
        public async Task<IActionResult> DownloadReportPdf(Guid id)
        {
            //Rapor içerisinde PDF dosyası gönderilmişse kendi localimizde indirmemizi sağlar.
            await _logger.LogInfo($"[GET]api/reports/download/{id} isteği geldi.");
            var report = await _serviceManager.ReportService.GetReportFileAsync(id);

            if (report == null || report.PdfFileData == null)
                return NotFound();

            return File(report.PdfFileData, "application/pdf", report.PdfFileName);
        }
        [HttpDelete("{reportId}")]
        [Authorize(Roles = "Admin,CompanyOwner")]
        public async Task<IActionResult> DeleteReport(Guid reportId)
        {
            //Rapor silmemizi sağlar.
            await _logger.LogInfo($"[DELETE]api/reports/{reportId} isteği geldi.");
            await _serviceManager.ReportService.DeleteTaskAsync(reportId);
            return NoContent();
        }

    }
}

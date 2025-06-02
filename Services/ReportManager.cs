using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReportManager : IReportService
    {
        private readonly IRepositoryManager _repoManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        
        public ReportManager(IRepositoryManager repoManager, IHttpContextAccessor httpContextAccessor, ILoggerService logger, IMapper mapper, UserManager<User> userManager)
        {
            _repoManager = repoManager;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        // Yeni rapor oluşturur. Giriş yapan kişinin bilgisi jwt ile alınır.
        public async Task<ReportDtoForGet> CreateReportAsync(ReportDtoForCreate dto)
        {
            //Kullanıcı e-posta adresi HTTP context üzerinden alınıyor.
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var user=await _userManager.FindByEmailAsync(userEmail);

            var entity = _mapper.Map<TaskReport>(dto);
            entity.CreatedById = user.Id;
            //Yüklenen PDF dosyası byte dizisine çevirip mssql de kayıt ediyoruz.
            if (dto.PdfFile != null)
            {
                using var ms=new MemoryStream();
                await dto.PdfFile.CopyToAsync(ms);
                entity.PdfFileData=ms.ToArray();
                entity.PdfFileName=dto.PdfFile.FileName;
                entity.PdfContentType=dto.PdfFile.ContentType;
            }
            await _repoManager.Report.CreateAsync(entity);
            await _repoManager.SaveAsync();
            return _mapper.Map<ReportDtoForGet>(entity);
        }

        // Giriş yapan kişinin tüm raporlarını getirir.
        public async Task<IEnumerable<ReportDtoForGet>> GetMyReportsAsync()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var reports = await _repoManager.Report.GetReportByUserIdAsync(user.Id, false);
            return reports.Select(r => _mapper.Map<ReportDtoForGet>(r));
        }

        // TaskId ye atanmış raporları getirir.
        public async Task<IEnumerable<ReportDtoForGet>> GetReportsByTaskIdAsync(Guid taskId)
        {
            var reports = await _repoManager.Report.GetReportByIdAsync(taskId, false);
            return reports.Select(r => _mapper.Map<ReportDtoForGet>(r));
        }
        // Giriş yapan şirket sahibinin şirketine ait raporları getirir.
        public async Task<IEnumerable<ReportDtoForGet>> GetReportsByCompanyAsync()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail)
                ?? throw new Exception("Kullanıcı bulunamadı");

            var reports = await _repoManager.Report.GetReportsByCompanyIdAsync(user.CompanyId.Value, false);
            
            // Her bir rapor için ilgili görev başlığı ve PDF indirme linkini atar.
            var result = reports.Select(r =>
            {
                var dto = _mapper.Map<ReportDtoForGet>(r);
                dto.TaskTitle = r.TaskItem?.Title ?? "Başlık bulunamadı";
                dto.DownloadUrl = $"/api/reports/{r.Id}/pdf";
                return dto;
            });
      
            return result;

        }


        // İlgili raporun PDF dosyasını döner. Yoksa null döner. 

        public async Task<ReportFileDto> GetReportFileAsync(Guid id)
        {
            var report = await _repoManager.Report.GetByIdAsync(id, false);

            if (report == null || report.PdfFileData == null)
                return null;

            return new ReportFileDto
            {
                PdfFileData = report.PdfFileData,
                PdfFileName = report.PdfFileName
            };
        }

        // İlgili ID'ye ait raporu veri tabanından siler.
        public async Task DeleteTaskAsync(Guid id)
        {
            var report=await _repoManager.Report.GetByIdAsync(id, true);
            await _repoManager.Report.DeleteAsync(report);
            await _repoManager.SaveAsync();
        }
    }
}

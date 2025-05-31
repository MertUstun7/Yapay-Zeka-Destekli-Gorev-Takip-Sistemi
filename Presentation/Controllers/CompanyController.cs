using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILoggerService _logger;
        private readonly IRepositoryManager _repoManager;

        public CompanyController(ICompanyService companyService, ILoggerService logger, IRepositoryManager repoManager)
        {
            _companyService = companyService;
            _logger = logger;
            _repoManager = repoManager;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll()
        {
            // Tüm şirketleri getirir.
            await _logger.LogInfo("[GET] api/company isteği geldi.");
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,CompanyOwner")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // İlgili id'deki şirket verilerini getirir.
            await _logger.LogInfo("[GET] /api/Company/{id} isteği geldi.");
            var company = await _companyService.GetByIdAsync(id);
            return Ok(company);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CompanyDtoForCreate request)
        {
            //Veri tabanında yer alan bir kullanıcıya şirket ataması yapmayı sağlar.

            await _logger.LogInfo("[POST] api/company isteği geldi.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _companyService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,CompanyOwner")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CompanyDtoForUpdate request)
        {
            //İlgili id'deki şirket verilerini günceller.
            await _logger.LogInfo("[PUT] api/company/{id:guid} isteği geldi.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _companyService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,CompanyOwner")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // İlgili id'deki şirketi veri tabanından siler.
            await _logger.LogInfo("[DELETE] api/company/{id:guid} isteği geldi.");
            await _companyService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{companyId:guid}/assign-user")]
        [Authorize(Roles = "Admin,CompanyOwner")]
        public async Task<IActionResult> AssignUser(Guid companyId, [FromQuery] string userId, [FromQuery] string role)
        {
            //Şirket içerisine eleman eklememizi sağlar.
            await _logger.LogInfo("[POST] api/company/{companyId:guid}/assign-user isteği geldi.");
            await _companyService.AssignUserAsync(companyId, userId, role);
            return NoContent();
        }

        [HttpPost("{companyId:guid}/remove-user")]
        [Authorize(Roles = "Admin,CompanyOwner")]
        public async Task<IActionResult> RemoveUser(Guid companyId, [FromQuery] string userId)
        {
            // Şirket içerisinden eleman çıkarmamızı sağlar.
            await _logger.LogInfo("[POST] api/company/{companyId:guid}/remove-user isteği geldi.");
            await _companyService.RemoveUserAsync(companyId, userId);
            return NoContent();
        }

        [HttpGet("{companyId:guid}/users")]
        [Authorize(Roles = "Admin,CompanyOwner")]
        public async Task<IActionResult> GetUsers(Guid companyId)
        {
            //Şirket içerisinde yer alan kişileri getirir.
            await _logger.LogInfo("[GET] api/company/{companyId:guid}/users isteği geldi.");
            var users = await _companyService.GetUsersByCompanyAsync(companyId);
            return Ok(users);
        }

        [HttpGet("{companyId:guid}/managers")]
        [Authorize(Roles = "Admin,CompanyOwner")]
        public async Task<IActionResult> GetManagers(Guid companyId)
        {
            // Şirket içerisindeki manager rolündeki kişileri getirir.
            await _logger.LogInfo("[GET] api/company/{companyId:guid}/managers isteği geldi.");
            var managers = await _companyService.GetManagersAsync(companyId);
            return Ok(managers);
        }

        [HttpGet("{companyId:guid}/workers")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager")]
        public async Task<IActionResult> GetWorkers(Guid companyId)
        {
            //Şirket içerisindeki worker rolündeki kişileri getirir
            await _logger.LogInfo("[GET] api/company/{companyId:guid}/workers isteği geldi.");
            var workers = await _companyService.GetWorkersAsync(companyId);
            return Ok(workers);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin,CompanyOwner")]
        public async Task<IActionResult> PatchCompany(Guid id, [FromBody] JsonPatchDocument<Company> patchDoc)
        {
            //Şirket bilgilerinden spesifik olarak bir alanı güncellememizi sağlar.
            await _logger.LogInfo("[PATCH] api/company/{id} isteği geldi.");
            if (patchDoc is null)
                return BadRequest();

            var company = await _repoManager.Company.GetByIdAsync(id, trackChanges: true);
            if (company is null)
                return NotFound();

            patchDoc.ApplyTo(company);

            await _repoManager.SaveAsync();

            return NoContent();
        }
    }
}

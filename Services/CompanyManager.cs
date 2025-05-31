using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CompanyManager : ICompanyService
    {
       
                
        private readonly IRepositoryManager _repoManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CompanyManager(
            IRepositoryManager repoManager,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _repoManager = repoManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        // Veri tabanında kayıtlı tüm şirketleri getirir.
        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            var companies = await _repoManager.Company.FindAllAsync(trackChanges: false);
            return companies.Select(c => _mapper.Map<CompanyDto>(c));
        }

        // ID'si verilen şirkete ait bilgileri (kullanıcılarla birlikte) getirir.
        public async Task<CompanyDto> GetByIdAsync(Guid id)
        {
            var company = await _repoManager.Company.GetWithUsersAsync(id, trackChanges: false);
            return _mapper.Map<CompanyDto>(company);
        }

        // Veri tabanına yeni şirket kayıt eder.
        public async Task<CompanyDto> CreateAsync(CompanyDtoForCreate request)
        {
            var entity = _mapper.Map<Company>(request);
            await _repoManager.Company.CreateAsync(entity);
            await _repoManager.SaveAsync();
            return _mapper.Map<CompanyDto>(entity);
        }

        // Veri tabanında var olan bir şirketi günceller.
        public async Task UpdateAsync(Guid id, CompanyDtoForUpdate request)
        {
            var entity = await _repoManager.Company.GetByIdAsync(id, trackChanges: true);
            _mapper.Map(request, entity);
            await _repoManager.Company.UpdateAsync(entity);
            await _repoManager.SaveAsync();
        }

        // ID'de yer alan şirketi siler.
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repoManager.Company.GetByIdAsync(id, trackChanges: false);
            await _repoManager.Company.DeleteAsync(entity);
            await _repoManager.SaveAsync();
        }

        // Kullanıcıyı şirket bünyesine atar ve rolü belirlenir.
        public async Task AssignUserAsync(Guid companyId, string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new KeyNotFoundException($"User '{userId}' not found.");

            user.CompanyId = companyId;
            await _userManager.UpdateAsync(user);

            if (!await _userManager.IsInRoleAsync(user, roleName))
                await _userManager.AddToRoleAsync(user, roleName);
        }

        // Kullanıcıyı şirket bünyesinden çıkarır ve tüm rolleri kaldırılır.

        public async Task RemoveUserAsync(Guid companyId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new KeyNotFoundException($"User '{userId}' not found.");

            if (user.CompanyId != companyId)
                throw new InvalidOperationException("User is not part of the specified company.");

            user.CompanyId = null;

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var r in roles)
                await _userManager.RemoveFromRoleAsync(user, r);

            await _userManager.UpdateAsync(user);
        }

        // Şirket bünyesinde çalışanları listeler.
        public async Task<IEnumerable<UserDto>> GetUsersByCompanyAsync(Guid companyId)
        {
            var company = await _repoManager.Company.GetWithUsersAsync(companyId, trackChanges: false);
            var users = company.Users;
            var userDtos = new List<UserDto>();
            foreach (var u in users)
            {
                var dto = _mapper.Map<UserDto>(u);
                dto.Roles = await _userManager.GetRolesAsync(u);
                userDtos.Add(dto);
            }

            return userDtos;
        }

        // Şirket bünyesinde çalışan manager rolündeki kişileri getirir.
        public async Task<IEnumerable<UserDto>> GetManagersAsync(Guid companyId)
        {
            var allUsers = await GetUsersByCompanyAsync(companyId);
            return allUsers.Where(u => u.Roles.Contains("Manager"));
        }

        // Şirketteki "worker" rolüne sahip kullanıcıları getirir.
        public async Task<IEnumerable<UserDto>> GetWorkersAsync(Guid companyId)
        {

            var allUsers = await GetUsersByCompanyAsync(companyId);
            return allUsers.Where(u => u.Roles.Contains("Worker"));
        }
    }
}
    


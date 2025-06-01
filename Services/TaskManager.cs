using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

    public class TaskManager : ITaskService
    {
        private readonly IRepositoryManager _repoManager;
        private readonly UserManager<User> _userManager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TaskManager(IRepositoryManager repoManager, ILoggerService logger, IMapper mapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _repoManager = repoManager;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }

        // Yeni görev oluşturur ve kullanıcıya atamasını yapar.
        public async Task<TaskItemForGetDto> CreateTaskAsync(TaskItemDtoForCreate taskDto)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            var task = _mapper.Map<TaskItem>(taskDto);
            task.CreatedById = user.Id;
            task.CompanyId = user.CompanyId.Value;
            task.CreatedByFullName=user.FirstName+" "+user.LastName;
            task.Assignments.Add(new TaskAssignment
            {
                AssignedToId = taskDto.AssignedToId,
                AssignedById = user.Id,
                AssignedAt = DateTime.UtcNow
            });

            await _repoManager.TaskItem.CreateAsync(task);
            await _repoManager.SaveAsync();

            return _mapper.Map<TaskItemForGetDto>(task);
        }

        // Görevin durum güncellemesini sağlar.
        public async Task UpdateTaskStatusAsync(Guid taskId, string status)
        {
            var task = await _repoManager.TaskItem.GetByIdAsync(taskId, true);
            task.Status = status;
            await _repoManager.TaskItem.UpdateAsync(task);
            await _repoManager.SaveAsync();
        }

        // Görevi veri tabanından siler.
        public async Task DeleteTaskAsync(Guid taskId)
        {
            var task = await _repoManager.TaskItem.GetByIdAsync(taskId, false);
            await _repoManager.TaskItem.DeleteAsync(task);
            await _repoManager.SaveAsync();
        }
        // Veri tabanında yer alan tüm görevleri listeler.
        public async Task<IEnumerable<TaskItemForGetDto>> GetAllAsync()
        {
            var tasks = await _repoManager.TaskItem.FindAllAsync(false);
            return tasks.Select(t => _mapper.Map<TaskItemForGetDto>(t));
        }

        // Sisteme giriş yapan kişinin görevlerini listeler.
        public async Task<IEnumerable<TaskItemForGetDto>> GetAssignedToCurrentUserAsync()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            var tasks = await _repoManager.TaskItem.GetAssignedToUserAsync(user.Id, false);
            return tasks.Select(t => _mapper.Map<TaskItemForGetDto>(t));
        }
        // Bir göreve atanmış tüm kişileri listeler.
        public async Task<IEnumerable<UserDtoForGet>> GetAssigneesAsync(Guid taskId)
        {
            var task = await _repoManager.TaskItem.GetByIdAsync(taskId, false);
            var users = task.Assignments.Select(a => a.AssignedTo);
            return users.Select(u => _mapper.Map<UserDtoForGet>(u));
        }

        // Şirkette bulunun tüm görevleri listeler.
        public async Task<IEnumerable<TaskItemForGetDto>> GetByCompanyAsync()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            var tasks = await _repoManager.TaskItem.GetByCompanyIdAsync(user.CompanyId.Value, false);
            return tasks.Select(t => _mapper.Map<TaskItemForGetDto>(t));
        }
        //  Giriş yapan kullanıcının oluşturduğu görevleri getirir.
        public async Task<IEnumerable<TaskItemForGetDto>> GetByCreatorAsync()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            var tasks = await _repoManager.TaskItem.GetByCreatorIdAsync(user.Id, false);
            return tasks.Select(t => _mapper.Map<TaskItemForGetDto>(t));
        }

        // İlgili ID'deki görev detaylarını getirir.
        public async Task<TaskItemForGetDto> GetTaskByIdAsync(Guid taskId)
        {
            var task = await _repoManager.TaskItem.GetByIdAsync(taskId, false);
            return _mapper.Map<TaskItemForGetDto>(task);
        }

        // Verilen duruma sahip görevleri listeler.
        public async Task<IEnumerable<TaskItemForGetDto>> GetByStatusAsync(string status)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            var tasks = await _repoManager.TaskItem.GetByStatusAsync(status, user.CompanyId.Value, false);
            return tasks.Select(t => _mapper.Map<TaskItemForGetDto>(t));
        }

        // Görevi günceller.
        public async Task UpdateTaskAsync(Guid taskId, TaskItemForUpdateDto taskDto)
        {
            var task = await _repoManager.TaskItem.GetByIdAsync(taskId, true);
            _mapper.Map(taskDto, task);
            await _repoManager.TaskItem.UpdateAsync(task);
            await _repoManager.SaveAsync();
        }
    }
}

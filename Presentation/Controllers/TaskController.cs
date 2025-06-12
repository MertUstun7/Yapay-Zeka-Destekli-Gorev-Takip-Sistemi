using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerService _logger;
        private readonly IRepositoryManager _repoManager;


        public TaskController(IServiceManager serviceManager, ILoggerService logger, IRepositoryManager repoManager)
        {
            _serviceManager = serviceManager;
            _logger = logger;
            _repoManager = repoManager;
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin,CompanyOwner,Manager")]

        public async Task<IActionResult> CreateTask([FromBody] TaskItemDtoForCreate taskDto)
        {
            //Görevleri oluşturmak için hazırlanmış POST isteğidir.
            await _logger.LogInfo($"[POST] api/task  isteği {taskDto.AssignedToId} için geldi.");
            var created = await _serviceManager.TaskItemService.CreateTaskAsync(taskDto);
            return Ok();
        }
        

        [HttpPut("{taskId}")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager")]

        public async Task<IActionResult> UpdateTask(Guid taskId, [FromBody] TaskItemForUpdateDto taskDto)
        {
            //Görevleri güncellemek için hazırlanmış bir PUT isteğidir.
            await _logger.LogInfo($"[PUT] api/task {taskId} isteği geldi.");
            await _serviceManager.TaskItemService.UpdateTaskAsync(taskId, taskDto);
            return NoContent();
        }
 

        [HttpDelete("{taskId}")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            //Görevleri silmek için hazırlanmış bir DELETE isteğidir.
            await _logger.LogInfo($"[DELETE] UpdateTask isteği {taskId} geldi.");
            await _serviceManager.TaskItemService.DeleteTaskAsync(taskId);
            return NoContent();
        }
        //Görevlerin durumunu güncellemek için hazırlanmış bir PATCH isteğidir.

        [HttpPatch("{taskId}")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager,Worker")]
        public async Task<IActionResult> UpdateStatus(Guid taskId, [FromBody] JsonPatchDocument<TaskItem> taskDto)
        {
            //Şirket bilgilerinden spesifik olarak bir alanı güncellememizi sağlar.
            await _logger.LogInfo("[PATCH] api/task/{taskId}/status isteği geldi.");
            if (taskDto is null)
                return BadRequest();
            var taskitem = await _repoManager.TaskItem.GetByIdAsync(taskId, trackChanges: true);
            if (taskitem is null)
                return NotFound();
            taskDto.ApplyTo(taskitem);
            await _repoManager.SaveAsync();

            return NoContent();
        }

       


        [HttpGet("{taskId}")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager,Worker")]
        public async Task<IActionResult> GetById(Guid taskId)
        {  // ID ile görev getirmek için tasarlanmış GET isteğidir.
            await _logger.LogInfo($"[GET] api/task/{taskId}  görevi istendi.");
            var task = await _serviceManager.TaskItemService.GetTaskByIdAsync(taskId);
            return Ok(task);
        }


        [HttpGet("all")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager,Worker")]
        public async Task<IActionResult> GetAll()
        {        // Site sahibinin tüm görevleri görebileceği bir GET isteğidir.
            await _logger.LogInfo($"[GET] api/task/all ile tüm görevler getirildi.");

            var tasks = await _serviceManager.TaskItemService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("user-created")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager,Worker")]
        public async Task<IActionResult> GetCreatedTaskByMe()
        {
            // Kullanıcının hazırladığı görevleri gösteren bir GET isteğidir.
            await _logger.LogInfo($"[GET] api/task/user-created ile görevler getirildi.");

            var tasks = await _serviceManager.TaskItemService.GetByCreatorAsync();
            return Ok(tasks);
        }
        [HttpGet("user-asigned")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager,Worker")]
        public async Task<IActionResult> GetAssignedTaskByMe()
        {
            // Kullanıcının atamış olduğu görevleri görmesini sağlayan bir GET isteğidir.

            await _logger.LogInfo($"[GET] api/task/user-asigned ile görevler getirildi.");

            var tasks = await _serviceManager.TaskItemService.GetAssignedToCurrentUserAsync();
            return Ok(tasks);
        }


        [HttpGet("company-tasks")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager")]
        public async Task<IActionResult> GetCompanyTasks()
        {
            // Şirket görevlerini listelememizi sağlayan bir GET isteğidir.
            await _logger.LogInfo($"[GET] GetCompanyTasks ile görevler getirildi.");
            var tasks = await _serviceManager.TaskItemService.GetByCompanyAsync();
            return Ok(tasks);
        }

        [HttpGet("status/{status}")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager,Worker")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            // Durumlara göre görevleri getirmemizi sağlayan bir GET isteğidir.
            await _logger.LogInfo($"[GET] api/task/status/{status} ile görevler getirildi.");
            var tasks = await _serviceManager.TaskItemService.GetByStatusAsync(status);
            return Ok(tasks);
        }

        [HttpGet("{taskId}/users")]
        [Authorize(Roles = "Admin,CompanyOwner,Manager,Worker")]
        public async Task<IActionResult> GetTasks(Guid taskId)
        {   
            // Göreve atanmış kişileri getirmesini sağlayan bir GET isteğidir.
            await _logger.LogInfo($"[GET] api/task/{taskId}/users ile görevler getirildi.");
            var users = await _serviceManager.TaskItemService.GetAssigneesAsync(taskId);
            return Ok(users);
        }
    }   
}

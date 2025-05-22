using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [Route("api/worker")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;


        public WorkerController(IServiceManager serviceManager, ILoggerService logger)
        {
            _manager = serviceManager;
            _logger = logger;
        }

        [HttpGet("active-worker")]
        public async Task<IActionResult> GetActiveWorker()
        {
            try
            {
                await _logger.LogInfo("Aktif çalışanları getirme işlemi başlatılıyor...");
                var active_workers = await _manager.WorkerService.GetActiveWorkerAsync();
                await _logger.LogInfo("Çalışanlar başarılı bir şekilde getirildi.");
                return Ok(active_workers);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPut("active-user")]
        public async Task<IActionResult> ActivedWorker(string userId)
        {
            try
            {
                await _logger.LogInfo($"{userId} için aktif etme işlemi başlatılıyor....");
                await _manager.WorkerService.ActiveWorkerAsync(userId);
                await _logger.LogInfo($"{userId} numaralı çalışan aktif edildi.");
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost("worker")]
        public async Task<IActionResult> CreateWorker(WorkerDtoForCreate worker)
        {
            try
            {
                await _logger.LogInfo($"Çalışan oluşturma işlemi başlatılıyor....");
                await _manager.WorkerService.CreateWorkerAsync(worker);
                await _logger.LogInfo($"{worker.WorkerId} numaralı çalışan veri tabanına eklenildi.");
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }
        }
    }


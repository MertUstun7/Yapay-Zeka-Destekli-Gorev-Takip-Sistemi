using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WorkerManager : IWorkerService
    {
        private readonly IRepositoryManager _workerManager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public WorkerManager(IRepositoryManager workerManager, ILoggerService logger, IMapper mapper)
        {
            _workerManager = workerManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task ActiveWorkerAsync(string workerId)
        {
            try
            {
                await _workerManager.Worker.ActivateWorkerAsync(workerId);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task ChangeWorkerManagerAsync(string workerId, string newManagerId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateWorkerAsync(WorkerDtoForCreate workerDto)
        {
            try
            {
                
                var entity = _mapper.Map<Worker>(workerDto);
                await _workerManager.Worker.CreateAsync(entity);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        public Task DeactiveWorkerAsync(string workerId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorkerAsync(string workerId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WorkerDtoForList>> GetActiveWorkerAsync()
        {
            try
            {
                var active_workers = await _workerManager.Worker.GetActiveWorkerListAsync();

                var dto_workers = _mapper.Map<List<WorkerDtoForList>>(active_workers);

                return dto_workers;
            }
            catch (Exception ex)
            {
                await _logger.LogError("Aktif kullanıcılar getirilirken bir hata meydana geldi.");
                throw new Exception( ex.Message);
            }

        }

        public Task<List<WorkerDtoForList>> GetAllWorkersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkerDtoForList>> GetInactiveWorkerAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WorkerDtoForDetails> GetWorkerByIdAsync(string workerID)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkerDtoForList>> GetWorkersByJobTitleAsync(string jobTitle)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkerDtoForList>> GetWorkersByManagerAsync(string managerId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkerDtoForDetails> GetWorkerWithDetailAsync(string workerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkerDtoForList>> SearchWorkerAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWorkerAsync(string workerId, WorkerDtoForUpdate workerDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> WorkerExistAsync(string workerId)
        {
            throw new NotImplementedException();
        }

        Task<List<WorkerDtoForList>> IWorkerService.DeactiveWorkerAsync(string workerId)
        {
            throw new NotImplementedException();
        }
    }
}

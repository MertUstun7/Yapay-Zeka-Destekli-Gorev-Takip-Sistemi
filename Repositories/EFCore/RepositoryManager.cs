using Entities;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly GTSDbContext _context;

        private readonly IUserRepository _userRepository;
        
        private readonly ICompanyRepository _companyRepository;

        private ITaskItemRepository _taskRepository;

        private IReportRepository _reportRepository;
        public RepositoryManager(
            GTSDbContext context,

            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            ITaskItemRepository taskRepository,
            IReportRepository reportRepository
            )
            
        {
            _context = context;

            _userRepository = userRepository;
            _companyRepository=companyRepository;
            _taskRepository = taskRepository;
            _reportRepository = reportRepository;

        }

        // Presentation katmanında repositoryde tanımlamış olduğumuz fonksiyonlara erişimi sağlar.
        public IUserRepository User => _userRepository;

        public ICompanyRepository Company => _companyRepository;

        public ITaskItemRepository TaskItem => _taskRepository;

        public IReportRepository Report => _reportRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

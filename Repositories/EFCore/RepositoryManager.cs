using Entities;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    // Projede bulunan tüm repository'ler merkezi bir yapıda toplanılmasını ve erişilmesini sağlayan sınıftır.
    public class RepositoryManager : IRepositoryManager
    {
        // Veri tabanı işlemlerini gerçekleştirmek üzere kullanılan EF Core DbContext nesnesinin tanımı yapılmıştır.
        private readonly GTSDbContext _context;

        // Kullanıcılarla ilgili yapılacak veri tabanı işlemlerini yöneten repository nesnesinin tanımı yapılmıştır.
        private readonly IUserRepository _userRepository;

        // Şirketle ilgili yapılacak veri tabanı işlemlerini yöneten repository nesnesinin tanımı yapılmıştır.
        private readonly ICompanyRepository _companyRepository;

        // Görevle ilgili veri tabanı işlemlerini yöneten repository nesnesinin tanımı yapılmıştır.
        private ITaskItemRepository _taskRepository;

        // Raporla ilgili veri tabanı işlemlerini yöneten repository nesnesinin tanımı yapılmıştır.
        private IReportRepository _reportRepository;

        // Dependency Injection (DI) ile gelen repository örneklerinin atama işlemleri gerçekleştirilmiştir.
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

        // User repository'sine erişim sağlar.
        public IUserRepository User => _userRepository;
        // Company repository'sine erişim sağlar.
        public ICompanyRepository Company => _companyRepository;
        // TaskItem repository'sine erişim sağlar.
        public ITaskItemRepository TaskItem => _taskRepository;

        // Report repository'sine erişim sağlar.
        public IReportRepository Report => _reportRepository;

        // Yapılan değişiklikleri (işaretlenen değişiklikleri) veri tabanına yansıtılmasını sağlar.
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

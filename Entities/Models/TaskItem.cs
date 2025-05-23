using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? Deadline { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;

        // Şirket ve oluşturan kullanıcı
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public string CreatedById { get; set; }
        public User CreatedBy { get; set; }

        // Atamalar
        public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();
    }



}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TaskReport
    {
        public Guid Id { get; set; }

        public Guid TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }

        public string CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public TaskStatus StatusAtReportTime { get; set; }
    }


}

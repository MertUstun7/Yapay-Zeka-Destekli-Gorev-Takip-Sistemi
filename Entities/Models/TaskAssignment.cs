using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TaskAssignment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }

        public string? AssignedToId { get; set; }
        public User AssignedTo { get; set; }

        public string AssignedById { get; set; }
        public User AssignedBy { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }


}

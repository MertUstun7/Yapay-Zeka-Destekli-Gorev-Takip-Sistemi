using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class TaskItemForUpdateDto
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? Deadline { get; set; }

        
        public string? Priority { get; set; }

       
        public string? Status { get; set; }
    }
}

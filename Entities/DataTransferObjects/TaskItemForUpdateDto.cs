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
        // Güncellenecek görevin başlığını string türünden almak için tanımlanmıştır.
        [Required]
        public string Title { get; set; }

        // Güncellenecek açıklamayı string türünden güncellemek için tanımlanmıştır.
        public string Description { get; set; }

        // Görevin teslim tarihini DateTime türünden güncellemek için tanımlanmıştır.
        public DateTime? Deadline { get; set; }

        // Görevin önceliğini string türünde güncellemek için tanımlanmıştır.
        public string? Priority { get; set; }

        // Görevin durumunu string türünden güncellemek için tanımlanmıştır. 
        public string? Status { get; set; }
    }
}

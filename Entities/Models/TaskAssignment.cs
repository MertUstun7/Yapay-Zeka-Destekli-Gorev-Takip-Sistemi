using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TaskAssignment
    {
        // Veri tabanında görevlerin benzersiz kimliğini temsil eden parametredir.
        public Guid Id { get; set; } = Guid.NewGuid();

        // Atanan görevin ID bilgisini temsil eden parametredir (Foreign Key).
        public Guid TaskItemId { get; set; }

        // Atanan görev ile olan ilişkiyi tanımlayan parametredir.
        public TaskItem TaskItem { get; set; }

        // Görevin atandığı kişinin ID numarasını temsil eden parametredir.
        public string? AssignedToId { get; set; }

        // Görevin atandığı kişiyle olan ilişkiyi tanımlayan parametredir.
        public User AssignedTo { get; set; }

        // Görevi atayan kişinin ID numarasını temsil eden parametredir.
        public string AssignedById { get; set; }

        // Görevi atayan kişiyle olan ilişkiyi tanımlayan parametredir.
        public User AssignedBy { get; set; }

        // Görevin atandığı tarih ve saat bilgisini temsil eden parametredir. Default olarak atandığı tarih ve saat verilir.
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TaskItem
    {
        // Veri tabanında GUID formatında ID numarasını temsil eden parametredir. Değeri default olarak verilmektedir.
        public Guid Id { get; set; } = Guid.NewGuid();

        // Veri tabanında görev başlığını temsil eden parametredir.
        public string Title { get; set; }

        // Veri tabanında görev açıklamasını temsil eden parametredir.
        public string Description { get; set; }

        // Veri tabanında görevin oluşturulma tarihini temsil eden parametredir. Default olarak oluşturulduğu günün tarih ve saati verilir.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Veri tabanında görevin son teslim tarihini temsil eden parametredir.
        public DateTime? Deadline { get; set; }

        // Veri tabanında görevin öncelik durumunu temsil eden parametredir.
        public string? Priority { get; set; }

        // Veri tabanında görevin hangi durumda olduğunu temsil eden parametredir.
        public string? Status { get; set; }

        // Veri tabanında görevi oluşturan kişinin isim ve soyismini tutan parametredir.
        public string CreatedByFullName { get; set; }

        // Veri tabanında görevin hangi şirkete ait olduğunu temsil eden parametredir.
        public Guid CompanyId { get; set; }

        // Görevin şirket ile olan ilişkisini temsil eden parametredir.
        public Company Company { get; set; }

        // Görevi oluşturan kişinin ID numarasını temsil eden parametredir.
        public string CreatedById { get; set; }

        // Görevi oluşturan kişi ile olan ilişkiyi temsil eden parametredir.
        public User CreatedBy { get; set; }

        // Görevle ilgili olan tüm atamaları temsil eden koleksiyondur.
        public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();
    }



}

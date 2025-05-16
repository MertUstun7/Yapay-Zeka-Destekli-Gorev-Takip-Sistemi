using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    //Görevlere ait olan entity sınıfım
    public class Assignment
    {
        //Görevin id numarasını temsil eden parametredir.
        
        [Key]
        public int TaskId { get; set; }
        
        // Görevin başlığını temsil eden parametredir
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        //Görev ile ilgili açıklamanın yapıldığı parametredir
        public string Description { get; set; }
       //Görevin atandığı çalışanın kimlik numarasını temsil eden parametredir.
        [Required]
        public int AssignedTo { get; set; }
        //Görevi atayan kişinin kimlik numarasını temsil eden parametredir.
        [Required]
        public int AssignedBy { get; set; }
      //Görevin mevcut durumunu temsil eden parametredir.
        [StringLength(50)]
        public string Status { get; set; } = "Pending";
        //Görevin oluşturulma zamanını temsil eden parametredir.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //Görevin bitiş tarihini temsil eden parametredir.
        public DateTime? DueDate { get; set; }

        // Aşağıda ilişkisel verileri tutan parametreler bulunmaktadır.
        //Worker tablosuna bağlıdır 
        public Worker AssignedWorker { get; set; }
        //Manager tablosuna bağlıdır
        public Manager AssignedManager { get; set; }
       //Bu görev ile ilgili raporları temsil eden parametredir.
        public ICollection<Report> Reports { get; set; }
    }
}

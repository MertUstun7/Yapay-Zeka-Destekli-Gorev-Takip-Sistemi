using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    // Çalışanları temsil eden entitiy sınıfımdır
    public class Worker
    {
        //Çalışanın id numarasını temsil eden parametredir.
        [Key]
        [ForeignKey("User")]
        public string WorkerId { get; set; }
        //Çalışanın bağlı olduğu yöneticiyi temsil eden parametredir.
        [Required]
        public string ManagerId { get; set; }
        //Çalışanın görev unvanını temsil eden parametredir.
        [StringLength(100)]
        public string JobTitle { get; set; }

        // Aşağıda ilişkisel verileri tutan parametreler bulunmaktadır.
        public User Users { get; set; }
        //Çalışanın bağlı olduğu yöneticiyi temsil eden parametredir.
        public Manager Manager { get; set; }
        //Çalışana atanan görevleri temsil eden parametredir.
        public ICollection<Assignment> Tasks { get; set; }
    }
}

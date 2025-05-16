using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
   // Yöneticiye ait olan entity sınıfım
    public class Manager
    {
        //Yöneticiye ait olan kimlik numarasını temsil eden parametredir. Ayrıca useer tablosu ile birebir ilişkilidir
        [Key]
        [ForeignKey("User")]
        public int ManagerId { get; set; }
        //Yöneticinin departmanını temsil eden parametredir.
        [StringLength(100)]
        public string Department { get; set; }

        // Aşağıda ilişkisel verileri tutan parametreler bulunmaktadır.
        //User nesnesiyle olan birebir ilişkiyi temsil eden parametredir.
        public User User { get; set; }
        //Yöneticinin sorumlu olduğu çalışanları temsil eden parametredir.
        public ICollection<Worker> Workers { get; set; }
        //Yöneticinin sistemde atadığı görevlerin listesini tutan parametredir.
        public ICollection<Assignment> AssignedTasks { get; set; }
    }
}

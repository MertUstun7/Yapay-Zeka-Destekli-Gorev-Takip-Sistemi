using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    //Kullanıcı bilgilerini temsil eden entity sınıfım.
    public class User:IdentityUser
    {


        //Kullanıcı adını temsil eden parametredir.
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        //Kullanıcı soyadını temsil eden parametredir.
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string UserType { get; set; } // Manager veya Worker
        //Kullanıcının sisteme kayıt olduğu tarih ve saati temsil eden parametredir.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //Kullanıcının sistemde aktif olup olmadığını temsil eden parametredir.
        public bool IsActive { get; set; } = true;

        // Aşağıda ilişkisel verileri tutan parametreler bulunmaktadır.

        //Kullanıcı yönetici ise bu ilişki yönetici nesnesini temsil eder.
        public Manager Manager { get; set; }
        //Kullanıcı çalışan ise bu ilişki worker nesnesini temsil eder.
        public Worker Worker { get; set; }
        //Kullanıcının yüklediği raporları listeleyen parametredir.
        public ICollection<Report> Reports { get; set; }
    }
}
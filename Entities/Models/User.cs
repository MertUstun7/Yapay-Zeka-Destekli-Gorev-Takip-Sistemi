using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{   /*
    ** Kullanıcı bilgilerini temsil eden entity sınıfımdır.
    ** ASP.NET Core Identity'nin sunduğu kimlik doğrulama özelliklerini 
       kullanmak amacıyla IdentityUser sınıfından kalıtım alınmıştır.
       Şifre yönetimini hashleme, mail gibi parametreleri default olarak gelmiştir.
    */
    public class User : IdentityUser
    {
        // Kullanıcı adını temsil eden parametremdir.
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        // Kullanıcı soyadını temsil eden parametremdir.
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        // Kullanıcı telefon numarasını temsil eden parametremdir.
        [Phone]
        public override string PhoneNumber { get; set; }

        // Kullanıcının sisteme kayıt olduğu tarih ve saati temsil eden parametremdir.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Kullanıcı için yapılan açıklamayı temsil eden parametremdir.
        public string? UserDescription { get; set; }

        // Kullanıcının çalıştığı departmanı temsil eden parametremdir.
        public string? Department { get; set; }

        // Kullanıcının oturum yenileme işlemlerinde kullanılacak refresh token'ı temsil eden parametremdir.
        public string? RefreshToken { get; set; }

        // Refresh token'ın geçerlilik süresini temsil eden parametremdir.
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // Kullanıcının çalıştığı şirket'in ID'sini temsil eden parametremdir.
        public Guid? CompanyId { get; set; }

        // Kullanıcının çalıştığı şirket ile olan ilişkiyi tanımlayan parametremdir.
        public Company? Company { get; set; }

        // Kullanıcıya atanmış görevleri temsil eden koleksiyondur.
        public ICollection<TaskAssignment> AssignedTasks { get; set; } = new List<TaskAssignment>();

        // Kullanıcının atamış olduğu görevleri temsil eden koleksiyondur.
        public ICollection<TaskAssignment> GivenTasks { get; set; } = new List<TaskAssignment>();

        // Kullanıcının oluşturduğu görev raporlarını temsil eden koleksiyondur.
        public ICollection<TaskReport> TaskReports { get; set; } = new List<TaskReport>();

        // Kullanıcının oluşturduğu görevleri temsil eden parametremdir.
        public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();
    }


}



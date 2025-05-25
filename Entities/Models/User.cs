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
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Phone]
        public override string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UserDescription { get; set; }

        public string? Department { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // Şirket bağlantısı
        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }

        // İlişkiler
        public ICollection<TaskAssignment> AssignedTasks { get; set; } = new List<TaskAssignment>();
        public ICollection<TaskAssignment> GivenTasks { get; set; } = new List<TaskAssignment>();
        public ICollection<TaskReport> TaskReports { get; set; } = new List<TaskReport>();
        public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();
    }


}
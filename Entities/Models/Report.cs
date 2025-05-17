using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    // Raporlara ait olan entity sınıfım
    public class Report
    {
        //Raporların id numaraların temsil eden parametredir.
        [Key]
        public string ReportId { get; set; }
        //Rapor başlığını temsil eden parametredir
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        //Raporun ayrıntılı açıklamasını temsil eden parametredir.
        [Required]
        [StringLength(500)]
        public string Detail { get; set; }
        //Raporu yükleyen kullanıcının id numarasını temsil eden parametredir.
        [Required]
        public string UploadedBy { get; set; }
        //Bu raporun bağlı olduğu görevin id numarasını temsil eden parametredir.
        public string? TaskId { get; set; }
        //Raporun sisteme yüklendiği zamanı temsil eden parametredir.
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // İlişkileri temsil eden parametredir.
        //Raporu yükleyen kullanıcıyı temsil eden parametredir.
        public User Uploader { get; set; }
        //Raporun bağlı olduğu görevi temsil eden parametredir.
        public Assignment Task { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TaskReport
    {
        // Raporun kimliğini temsil eden parametredir.
        public Guid Id { get; set; } = Guid.NewGuid();

        // Raporun ait olduğu görevin ID'sini temsil eden parametredir (Foreign Key)
        public Guid TaskItemId { get; set; }

        // Raporun ilişkili olduğu görev nesnesiyle olan bağlantıyı temsil eden parametredir.
        public TaskItem TaskItem { get; set; }

        // Raporu oluşturan kullanıcının ID bilgisini temsil eder.
        public string CreatedById { get; set; }

        //Raporu oluşturan kullanıcıyla olan ilişkiyi tanımlayan parametredir.
        public User CreatedBy { get; set; }

        // Raporun içerik metnini temsil eden parametredir.
        public string Content { get; set; }

        // Raporun oluşturulduğu tarih ve saat bilgisini temsil eder. Default olarak oluşturulduğu günün tarih ve saati atanır.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Rapor oluşturulurken görevin o anki durumunu temsil eden parametredir.
        public string? StatusAtReportTime { get; set; }

        // Yüklenen PDF dosyasının adını temsil eder.
        public string? PdfFileName { get; set; }

        // PDF dosyasının MIME türünü belirten parametredir.
        public string? PdfContentType { get; set; }

        // PDF dosyasının içerik verisini byte formatında tutar.
        public byte[]? PdfFileData { get; set; }

      
    }


}

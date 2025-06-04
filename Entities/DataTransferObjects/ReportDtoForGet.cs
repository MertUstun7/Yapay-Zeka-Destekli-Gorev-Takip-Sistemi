using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ReportDtoForGet
    {
        // Kullanıcıya ilgili raporun ID'sinin Guid türünden gösterilmesi için tanımlanmıştır.
        public Guid Id { get; init; }
        // Kullanıcıya ilgili raporun isminin string türünden gösterilmesi için tanımlanmıştır.
        public string? TaskTitle { get; set; }
        // Kullanıcıya ilgili raporun isminin string türünden gösterilmesi için tanımlanmıştır.
        public string? Content { get; init; }
        // Kullanıcıya ilgili raporun oluşturlma tarihi DateTime türünden gösterilmesi için tanımlanmıştır.
        public DateTime CreatedAt { get; init; }
        // Kullanıcıya ilgili raporun durumunun string türünden gösterilmesi için tanımlanmıştır.
        public string? StatusAtReportTime { get; init; }
        // Kullanıcıya ilgili raporun oluşturan kişinin tam isminin string türünden gösterilmesi için tanımlanmıştır.
        public string? CreatedByFullName { get; init; }
        // Kullanıcıya ilgili rapor için yüklenen dosyanın isminin string türünden gösterilmesi için tanımlanmıştır.
        public string? PdfFileName { get; init; }
        // Kullanıcıya ilgili raporun arayüzden indirmesi için bir url tanımlaması yapılması amacıyla tanımlanmıştır.
        public string? DownloadUrl { get; set; }

    }
}


using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ReportDtoForCreate
    {
        // Raporun yazıldığı görevin ID'si Guid türünden alınması için tanımlanmıştır.
        public Guid TaskItemId { get; init; }

        // Rapor içeriğinin string türünden alınması  için tanımlanmıştır.
        public string Content {  get; init; }

        // İlgili görevin durumunu string türünden alınması için tanımlanmıştır.
        public string? StatusAtReportTime { get; init; }

        // Rapor içeriğine PDF türünden dosya alınması için tanımlanmıştır.
        public IFormFile? PdfFile { get; init; }
    }
}

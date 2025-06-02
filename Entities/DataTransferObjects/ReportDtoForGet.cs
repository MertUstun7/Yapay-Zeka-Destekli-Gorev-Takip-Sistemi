using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ReportDtoForGet
    {
        public Guid Id { get; init; }

        public string? TaskTitle { get; set; }
        public string? Content { get; init; }

        public DateTime CreatedAt { get; init; }

        public string? StatusAtReportTime { get; init; }

        public string? CreatedByFullName { get; init; }

        public string? PdfFileName { get; init; }

        public string? DownloadUrl { get; set; }

    }
}

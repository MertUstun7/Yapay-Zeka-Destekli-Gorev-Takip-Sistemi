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
        public Guid TaskItemId { get; init; }
        public string Content {  get; init; }

        public string? StatusAtReportTime { get; init; }

        public IFormFile? PdfFile { get; init; }
    }
}

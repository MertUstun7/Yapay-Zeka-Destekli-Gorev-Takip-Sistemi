using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ReportFileDto
    { 
        public byte[]? PdfFileData { get; set; }

        public string ?PdfFileName { get; set; }
    }
}

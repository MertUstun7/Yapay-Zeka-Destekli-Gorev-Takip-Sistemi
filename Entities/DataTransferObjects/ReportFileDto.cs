using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ReportFileDto
    { 
        // Veri tabanında yer alan PDF dosyasının içeriğinin byte türünden alınması için tanımlanmıştır.
        public byte[]? PdfFileData { get; set; }

        // Veri tabanında yer alan PDF dosyasının isminin string türünden alınması için tanımlanmıştır.
        public string ?PdfFileName { get; set; }
    }
}

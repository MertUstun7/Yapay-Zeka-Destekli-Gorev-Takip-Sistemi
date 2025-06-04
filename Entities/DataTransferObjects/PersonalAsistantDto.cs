using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class PersonalAsistantDto
    {
        
        // Yapay zeka asistanına sorunun string türünden alınması için tanımlanmıştır.
        public string? Query { get; set; }

        // Yapay zekaya istek atan kullanıcının ID'si string türünden alınması için tanımlanmıştır.
        public string? UserId { get; set; }

        // Yapay zeka asistanına PDF formatında bir dosya alınması için tanımlanmıştır.
        public IFormFile? File { get; set; }
    }
}

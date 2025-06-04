using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.ErrorModel
{
    public  class ErrorDetails
    {
        // Hata kodunu tutabilmemiz için integer türünde tanımlanmış bir parametredir.
        public int StatusCode {  get; set; }

        // Hata mesajı için tanımlanmış string türünden bir parametredir.
        public string? Message { get; set; }

        // Hatayı izlemini yapabilmek için tanımlanmış string türünden bir parametredir.
        public string? TraceId { get; set; }

      // Error modelimi JSON formatında string türünde kullanıcıya göstermek için tanımlanan ve override edilmiş bir metottur.
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}

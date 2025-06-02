using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ReportNotFoundException : NotFoundException
    {
        public ReportNotFoundException(string id) : base($"{id} numaralı kullanıcı için herhangi bir rapor bulunamadı.")
        {
        }
    }
}

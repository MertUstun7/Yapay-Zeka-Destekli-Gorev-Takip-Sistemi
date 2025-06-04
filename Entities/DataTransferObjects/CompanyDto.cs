using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record CompanyDto
    {
        // Guid türündeki şirket ID'yi göstermek için tanımlanmıştır.
        public Guid Id { get; init; }
        //Şirket ismini string türünde göstermek için tanımlanmıştır.
        public string Name { get; init; }
        // Şirket vergi numarasını string türünde göstermek için tanımlanmıştır.
        public string TaxNumber { get; init; }

        // Şirket sahibinin ID' sini göstermek için tanımlanmıştır.
        public Guid OwnerId { get; init; }
    }
}

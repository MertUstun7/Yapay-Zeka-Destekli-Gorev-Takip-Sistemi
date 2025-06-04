using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record CompanyDtoForCreate
    {
        // Kullanıcı arayüzünden şirket ismini string türünden almak için tanımlanmıştır.
        public string Name { get; init; }
        // Kullanıcı arayüzünde şirket vergi numarasını string türünden almak için tanımlanmıştır.
        public string TaxNumber { get; init; }

        // Kullanıcı arayüzünden şirket sahibinin ID'sini string türünden almak için tanımlanmıştır.
        public string OwnerId { get; init; }
    }
}


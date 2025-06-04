using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserDtoForUpdate
    {
        // Kullanıcı adının string türünden güncellenmesi için tanımlanmış parametredir.
        public string FirstName { get; init; }

        // Kullanıcı soyisminin string türünden güncellenmesi için tanımlanmış parametredir.
        public string LastName { get; init; }

        // Kullanıcı e-mailinin string türünden güncellenmesi için tanımlanmış parametredir.
        public string Email { get; init; }

        // Kullanıcı telefon numarasının string türünden güncellenmesi için tanımlanmış parametredir.
        public string? PhoneNumber { get; init; }
        // Kullanıcı departmanının string türünden güncellenmesi için tanımlanmış parametredir.
        public string? Department { get; init; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserDtoForGet
    {
        // Kullanıcı ID'sini string türünden göstermek için tanımlanmış parametredir.

        public string Id { get; init; }
        // Kullanıcı adını string türünden göstermek için tanımlanmış parametredir.
        public string FirstName { get; init; }

        // Kullanıcı soyadını string türünden göstermek için tanımlanmış parametredir.
        public string LastName { get; init; }

        // Kullanıcı emailini string türünden göstermek için tanımlanmış parametredir.
        public string Email { get; init; }

        // Kullanıcı telefon numarasını string türünden göstermek için tanımlanmış parametredir.
        public string? PhoneNumber { get; init; }

        // Kullanıcı departmanını string türünden göstermek için tanımlanmış parametredir.
        public string? Department { get; init; }

        // Kullanıcı açıklamasını string türünden göstermek için tanımlanmış parametredir.
        public string? UserDescription { get; init; }

        // Kullanıcının bağlı olduğu şirketin ID'sini Guid türünden göstermek için tanımlanmış parametredir.
        public Guid? CompanyId { get; init; }

        // Kullanıcı hesabının oluşturulma tarihini DateTime türünden göstermek için tanımlanmış parametredir.
        public DateTime CreatedAt { get; init; }
    }
}

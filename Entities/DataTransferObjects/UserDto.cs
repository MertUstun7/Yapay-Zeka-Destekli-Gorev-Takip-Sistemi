using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        // Kullanıcı ID'sini string türünde temsil eden parametredir.
        public string? Id { get; init; }
        
        // Kullanıcı adını string türünde temsil eden parametredir.
        public string? UserName { get; init; }

        // Kullanıcı e-mailini string türünde temsil eden parametredir.
        public string? Email { get; init; }
        // Kullanıcı adını string türünde temsil eden parametredir.
        public string? FirstName { get; init; }

        // Kullanıcı soyismini string türünde temsil eden parametredir.
        public string? LastName { get; init; }

        // Kullanıcı telefon numarasını string türünde temsil eden parametredir.
        public string? PhoneNumber { get; init; }

        // Kullanıcı departmanını string türünde temsil eden parametredir.
        public string? Department { get; init; }

        // Kullanıcı ile ilgili yapılan açıklamayı string türünde temsil eden parametredir.
        public string? UserDescription { get; init; }

        // Kullanıcının ilişkili olduğu şirketi ID'sini Guid türünde temsil eden parametredir.
        public Guid? CompanyId { get; init; }

        // Kullanıcının şirket içerisindeki rollerini liste türünde temsil eden parametredir.
        public IList<string> Roles { get; set; } = new List<string>();

        // Kullanıcı hesabının oluşturulma tarihini DateTime türünde temsil eden parametredir.
        public DateTime? CreatedAt { get; init; }
    }

}

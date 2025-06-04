using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record UserDtoForCreate
    {
        // Kullanıcı oluşturmak için adını string türünden temsil eden parametredir.
        public string FirstName { get; init; }
        // Kullanıcı oluşturmak için soyadını string türünden temsil eden parametredir.
        public string LastName { get; init; }

        // Kullanıcı oluşturmak için e-mailini string türünden temsil eden parametredir.
        public string Email { get; init; }

        // Kullanıcı oluşturmak için şifresini string türünden temsil eden parametredir.
        public string Password { get; init; }

        // Kullanıcı oluşturmak için telefon numarasını string türünden temsil eden parametredir.
        public string? PhoneNumber { get; init; }

        // Kullanıcı oluşturmak için departmanını string türünden temsil eden parametredir.
        public string? Department { get; init; }

        // Kullanıcı oluşturmak için rolünü string türünden temsil eden parametredir.
        public string? Role { get; init; }

        // Kullanıcı oluşturmak için kullanıcı açıklamasını string türünden temsil eden parametredir.
        public string? UserDescription { get; init; }
    }
    
}

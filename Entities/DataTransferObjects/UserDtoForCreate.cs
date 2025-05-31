using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    

    public record UserDtoForCreate
    {

            public string FirstName { get; init; }
            public string LastName { get; init; }
            public string Email { get; init; }
            public string Password { get; init; }
            public string? PhoneNumber { get; init; }
            public string? Department { get; init; }
            public string? Role { get; init; }
            public string? UserDescription { get; init; }
        }



    
}

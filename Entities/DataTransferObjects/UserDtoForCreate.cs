using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record UserDtoForCreate
    {
        public string Id { get; init; }

        public string FirstName {  get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }

        public string PasswordHash{ get; init; }

        public string PhoneNumber { get; init; }

        public bool IsActive { get; init; }

        public string UserType {  get; init; }


    }
}

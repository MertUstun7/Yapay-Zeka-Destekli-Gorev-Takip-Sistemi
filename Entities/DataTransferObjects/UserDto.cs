using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public string? Id { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Department { get; init; }
        public string? UserDescription { get; init; }
        public Guid? CompanyId { get; init; }
        public IList<string> Roles { get; set; } = new List<string>();
        public DateTime? CreatedAt { get; init; }
    }

}

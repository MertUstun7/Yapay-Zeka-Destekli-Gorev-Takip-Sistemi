using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record CompanyDtoForUpdate
    {
        public string? Name { get; init; }
        public string? TaxNumber { get; init; }
        public Guid? OwnerId { get; init; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record CompanyDtoForCreate
    {
        public string Name { get; init; }

        public string TaxNumber { get; init; }
        public string OwnerId { get; init; }
    }
}

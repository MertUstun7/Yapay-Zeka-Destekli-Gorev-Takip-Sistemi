using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WorkerDtoForList
    {
        public string WorkerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string JobTitle { get; set; }

        public string ManagerId { get; set; }

        public string ManagerName { get; set; }

        public bool IsActive { get; set; }
    }
}

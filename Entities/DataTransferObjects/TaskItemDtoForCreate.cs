using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class TaskItemDtoForCreate
    {
        public string? Title { get; init; }
        public string? Description { get; init; }
        public DateTime? Deadline { get; init; }
        public string? Priority { get; init; }

        public string? AssignedToId { get; init; }

    }

}

using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class TaskItemForGetDto
    {
            public Guid Id { get; init; }
            public string Title { get; init; }
            public string Description { get; init; }
            public DateTime CreatedAt { get; init; }
            public DateTime? Deadline { get; init; }
            public string? Priority { get; init; }
            public string? Status { get; init; }
            public string? Assigned { get; init; }
            public string CreatedByFullName { get; init; }
        

    }
}

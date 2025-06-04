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
        // Görev başlığının string türünden arayüzden alınması için tanımlanmıştır.
        public string? Title { get; init; }

        // Görev açıklamasının string türünden arayüzden alınması için tanımlanmıştır.
        public string? Description { get; init; }

        // Görev'in son tarihinin DateTime türünden arayüzden alınması için tanımlanmıştır.
        public DateTime? Deadline { get; init; }

        // Görevin önceliğinin string türünden arayüzden alınması için tanımlanmıştır.
        public string? Priority { get; init; }

        // Görevin kime atandığını string türünden arayüzden alınması için tanımlanmıştır.
        public string? AssignedToId { get; init; }

    }

}

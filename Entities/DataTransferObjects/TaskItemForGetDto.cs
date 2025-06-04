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
        // Görev ID'sinin Guid türünden kullanıcıya gösterilmesi için tanımlanmıştır.
        public Guid Id { get; init; }

        // Görev başlığının string türünden kullanıcıya gösterilmesi için tanımlanmıştır.
        public string Title { get; init; }
        // Görev açıklamasının string türünden kullanıcıya gösterilmesi için tanımlanmıştır.
        public string Description { get; init; }

        // Görev oluşturulma tarihinin DateTime türünden kullanıcıya gösterilmesi için tanımlanmıştır.
        public DateTime CreatedAt { get; init; }

        // Görev bitiş süresinin DateTime türünden kullanıcıya gösterilmesi için tanımlanmıştır.
        public DateTime? Deadline { get; init; }

        // Görev önceliğinin string türünden kullanıcıya gösterilmesi için tanımlanmıştır.
        public string? Priority { get; init; }

        // Görev durumunun string türünden kullanıcıya gösterilmesi için tanımlanmıştır.
        public string? Status { get; init; }

        // Görev'in atandığı kişi ile ilgili bilginin string türünden kullanıcıya gösterilmesi için tanımlanmıştır.
        public string? Assigned { get; init; }
        // Görev'i atayan kişinin tam isminin string türünden kullanıcıya gösterilmesi için tanımlanmıştır.

        public string CreatedByFullName { get; init; }   

    }
}

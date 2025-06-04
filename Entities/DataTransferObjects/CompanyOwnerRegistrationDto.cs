using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record CompanyOwnerRegistrationDto
    {
        // Şirket sahibinin string türünden adının alınması için tanımlanmıştır.
        [Required]
        public string FirstName { get; init; }

        // Şirket sahibinin soyismi string türünden alınması için tanumlanmıştır.
        [Required]
        public string LastName { get; init; }

        // Şirket sahibinin string türünden e-mail adresinin alınması için tanımlanmıştır.
        [Required]
        public string Email { get; init; }

        // Şirket hesabının şifresinin string türünden alınması için tanımlanmıştır.
        [Required(ErrorMessage ="Şifre doldurulması zorunludur.")]
        public string Password { get; init; }

        // Şirket sahibinin telefon numarasının string türünden alınması için tanımlanmıştır.
        [Phone]
        public string PhoneNumber { get; init; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Vergi numarası 10 haneli sayılardan oluşmalıdır.")]

        // Şirket'in vergi numarasının string türünden alınması için tanımlanmıştır.
        public string TaxNumber { get; init; }

        // Şirket isminin string türünden alınması için tanımlanmıştır.

        [Required(ErrorMessage ="Şirket isminin doldurulması zorunludur.")]
        public string CompanyName { get; init; }     

    }
}

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
        [Required]
        public string FirstName { get; init; }

        [Required]
        public string LastName { get; init; }

        [Required]
        public string Email { get; init; }


        [Required(ErrorMessage ="Şifre doldurulması zorunludur.")]
        public string Password { get; init; }

        [Phone]
        public string PhoneNumber { get; init; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Vergi numarası 10 haneli sayılardan oluşmalıdır.")]
        public string TaxNumber { get; init; }

        [Required(ErrorMessage ="Şirket isminin doldurulması zorunludur.")]
        public string CompanyName { get; init; }

        

    }
}

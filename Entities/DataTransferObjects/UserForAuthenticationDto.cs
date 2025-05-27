using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage ="Mail girilmesi zorunludur.")]
        public string? Mail { get; init; }

        [Required(ErrorMessage ="Şifre girilmesi zorunludur.")]

        public string? Password { get; init; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserPasswordUpdateDto
    {
        // Kullanıcı şifresi ve güncellenecek şifrenin alınmasını sağlayan DTO modelidir.

        [Required(ErrorMessage = "Mevcut şifre zorunludur.")]
        public string CurrentPassword { get; init; }

        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Yeni şifre en az 6 karakter olmalıdır.")]
        public string NewPassword { get; init; }

        [Required(ErrorMessage = "Yeni şifre tekrarı zorunludur.")]
        [Compare("NewPassword", ErrorMessage = "Yeni şifre ve tekrarı eşleşmiyor.")]
        public string ConfirmNewPassword { get; init; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record TokenDto
    {
        // Kullanıcıya verilen token'ı temsil etmektedir.
        public String AccessToken { get; init; }
        // Süresi dolan token için refresh token'ı almak için tanımlanmıştır.
        public String RefreshToken { get; init; }
    }
}

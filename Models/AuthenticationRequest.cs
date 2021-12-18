using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace onlyarts.Models
{
    [SwaggerSchema(Description = "Модель запроса прохождения аутентификации")]
    public class AuthenticationRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

using Swashbuckle.AspNetCore.Annotations;

namespace onlyarts.Models
{
    [SwaggerSchema(Description = "Модель регистрации нового пользователя")]
    public class RegistrationRequest
    {   
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string LinkToAvatar { get; set; }
    }
}

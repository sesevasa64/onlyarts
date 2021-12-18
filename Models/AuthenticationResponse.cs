using Swashbuckle.AspNetCore.Annotations;

namespace onlyarts.Models
{
    [SwaggerSchema(Description = "Модель ответа на запрос прохождения аутентификации")]
    public class AuthenticationResponse
    {
        public string AuthToken { get; set; }
        // Пока что будем юзать только обычные токены
        // public string RefreshToken { get; set; }
    }
}

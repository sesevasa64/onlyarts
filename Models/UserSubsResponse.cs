using Swashbuckle.AspNetCore.Annotations;

namespace onlyarts.Models
{
    [SwaggerSchema(Description = "Модель ответа на запрос информации о подписчиках автора")]
    public class UserSubsResponse
    {
        public UserSubsResponse(string login, string nickname, string linkToAvatar)
        {
            Login = login;
            Nickname = nickname;
            LinkToAvatar = linkToAvatar;
        }
        public string Login { get; set; }
        public string Nickname { get; set; }
        public string LinkToAvatar { get; set; }
    }
}

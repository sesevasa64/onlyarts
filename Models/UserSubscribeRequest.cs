using Swashbuckle.AspNetCore.Annotations;

namespace onlyarts.Models
{
    [SwaggerSchema(Description = "Модель запроса добавления новой подписки")]
    public class UserSubscribeRequest
    {
        public int AuthorId { get; set; }
        public int SubUserId { get; set; }
        public int SubTypeId { get; set; }
    }
}
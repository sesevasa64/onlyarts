using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace onlyarts.Models
{
    [SwaggerSchema(Description = "Модель запроса обновления информации о пользователе")]
    public class UserUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public string Info { get; set; }
    }
}

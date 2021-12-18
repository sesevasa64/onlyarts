using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace onlyarts.Models
{
    [SwaggerSchema(Description = "Модель запроса обновления контента")]
    public class ContentUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

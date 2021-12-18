using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace onlyarts.Models
{
    [SwaggerSchema(Description = "Модель запроса добавления нового контента")]
    public class ContentRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string LinkToPreview { get; set; }
        public string LinkToBlur { get; set; }
        public int UserID { get; set; }
        public int SubTypeID { get; set; }
        public List<string> Images { get; set; }
    }
}
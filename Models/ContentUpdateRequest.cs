using System.ComponentModel.DataAnnotations;

namespace onlyarts.Models
{
    public class ContentUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

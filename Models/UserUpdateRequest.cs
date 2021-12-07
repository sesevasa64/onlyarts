using System.ComponentModel.DataAnnotations;

namespace onlyarts.Models
{
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

using System.ComponentModel.DataAnnotations;

namespace onlyarts.Models
{
    public class UserSubscribeRequest
    {
        public int AuthorId { get; set; }
        public int SubUserId { get; set; }
        public int SubTypeId { get; set; }
    }
}
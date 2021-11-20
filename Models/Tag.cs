using onlyarts.Interfaces;

namespace onlyarts.Models
{
    public class Tag : IEntity
    {
        public int Id { get; set; }
        public string TagName{ get; set; }
    }
}
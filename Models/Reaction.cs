using onlyarts.Interfaces;

namespace onlyarts.Models
{
    public class Reaction : IEntity
    {
        public int Id { get; set; }
        public bool Type { get; set; } // 0 - лайк 1 - дизлайк
        public User User { get; set; }  //соединяемся с User
        public Content Content {get; set; } //соединяемся с Content
    }
}
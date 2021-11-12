namespace onlyarts.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public bool Type { get; set; }

        public User User { get; set; }  //соединяемся с User
        public Content Content {get; set; } //соединяемся с Content
    }
}
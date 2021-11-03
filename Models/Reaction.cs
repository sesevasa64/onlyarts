namespace onlyarts.Models
{
    public class Reaction
    {
        public Reaction(int id) {
            Id = id;
       }
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Type { get; set; }
    }
}

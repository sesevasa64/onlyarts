namespace onlyarts.Models
{
    public class LinkTag
    {
        public int Id { get; set; }
        
        public Tag Tag { get; set; }
        public Content Content { get; set; }
    }
}
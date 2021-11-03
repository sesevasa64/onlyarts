namespace onlyarts.Models
{
    public class Content
    {
        public Content(int id) {
            Id = id;
       }
        public int Id { get; set; }
        public int SubTypeId { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string LinkToContent { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public User User { get; set; }  //соединяемся с User через UserID
    }
}

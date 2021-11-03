namespace onlyarts.Models
{
    public class Content
    {
        public int Id { get; set; }
        public int SubTypeId { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string LinkToContent { get; set; }
        public int LikesCount { get; set; }     //сделать вычисляемымым
        public int DislikesCount { get; set; }  //сделать вычисляемымым
        public User User { get; set; }  //соединяемся с User через UserID
    }
}

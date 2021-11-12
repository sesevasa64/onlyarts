namespace onlyarts.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string LinkToContent { get; set; }
        public int LikesCount { get; set; }     //сделать вычисляемымым
        public int DislikesCount { get; set; }  //сделать вычисляемымым
        public int ViewCount { get; set; } 

        public User User { get; set; } 
        public SubType SubType { get; set; }
    }
}
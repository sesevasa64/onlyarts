using onlyarts.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace onlyarts.Models
{
    public class Content : IEntity 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }     //pic или vid
        public string LinkToPreview { get; set; }   //ссылка на превью
        public string LinkToBlur { get; set; }  //ссылка на размытое превью
        //public int LikesCount { get; private set; }     //сделать вычисляемымым
        //public int DislikesCount { get; set; }  //сделать вычисляемымым
        public int ViewCount { get; set; } 

        public User User { get; set; } 
        public SubType SubType { get; set; }
    }
}
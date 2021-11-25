using onlyarts.Interfaces;

namespace onlyarts.Models
{
    public class Image : IEntity
    {
        public int Id { get; set; }
        public string LinkToImage { get; set; } //ссылка на изображение
        public Content Content { get; set; }
    }
}
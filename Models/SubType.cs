using onlyarts.Interfaces;

namespace onlyarts.Models
{
    public class SubType : IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }
        public int Duration { get; set; }
        // 0 - бесплатно, 1 - платно и т.д. и все что больше видит все что меньше
        public byte SubLevel { get; set; } 
    }
}
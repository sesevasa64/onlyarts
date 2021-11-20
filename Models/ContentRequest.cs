namespace onlyarts.Models
{
    public class ContentRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string LinkToPreview { get; set; }
        public string LinkToBlur { get; set; }
        public int UserID { get; set; }
        public int SubTypeID { get; set; }
    }
}
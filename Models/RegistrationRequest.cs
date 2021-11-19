namespace onlyarts.Models
{
    public class RegistrationRequest
    {   
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string LinkToAvatar { get; set; }
    }
}

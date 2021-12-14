namespace onlyarts.Models
{
    public class UserSubsResponse
    {
        public UserSubsResponse(string login, string nickname, string linkToAvatar)
        {
            Login = login;
            Nickname = nickname;
            LinkToAvatar = linkToAvatar;
        }
        public string Login { get; set; }
        public string Nickname { get; set; }
        public string LinkToAvatar { get; set; }
    }
}

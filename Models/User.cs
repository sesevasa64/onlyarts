namespace onlyarts.Models
{
    public class User
    {
        public User(int id, string login) {
            Id = id;
            Login = login;
        }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nickname {get; set; }
        //public DateTime RegisDate = new DateTime() { get; set;}
        public uint Money {get; set;}
    }
}

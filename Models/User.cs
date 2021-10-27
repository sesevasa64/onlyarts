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
    }
}

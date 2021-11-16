namespace onlyarts.Models
{
    public class AuthenticationResponse
    {
        public string AuthToken { get; set; }
        // Пока что будем юзать только обычные токены
        // public string RefreshToken { get; set; }
    }
}

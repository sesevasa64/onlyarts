using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace onlyarts
{
    class AuthOptions
    {
        private static string key;
        public static double Lifetime {get; set; }
        public static void Init(IConfiguration configuration)
        {
            key = configuration.GetSection("Token")["Key"];
            Lifetime = Convert.ToDouble(configuration.GetSection("Token")["Lifetime"]);
        }
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}

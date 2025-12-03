using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace FitSammen_API.Security
{
    public class SecurityHelper
    {
        private const int KeySize = 32;
        private readonly IConfiguration _configuration;

        public SecurityHelper(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
        }

        public SymmetricSecurityKey? GetSecurityKey()
        {
            SymmetricSecurityKey? SIGNING_KEY = null;
            string? SECRET_KEY = _configuration["JwtSettings:SecretKey"];
            if (!string.IsNullOrEmpty(SECRET_KEY))
            {
                SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            }
            return SIGNING_KEY;
        }


        public byte[] HashPassword(string password, byte[] salt)
        {
            
            using var hashedPassword = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] foundHash = hashedPassword.GetBytes(KeySize);
            return foundHash;
        }
    }
}

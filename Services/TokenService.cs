using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JPS.Data.Entities;
using JPS.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JPS.Services
{
    /// <summary>
    /// Token service class to handle JWT token
    /// </summary>
    public class TokenService : ITokenService
    {
        /// <summary>
        /// Security To create Token
        /// </summary>
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        private readonly string _refreshTokenSalt;
        /// <summary>
        /// Constructor Init Key
        /// </summary>
        /// <param name="config"></param>
        /// <param name="userManager"></param>
        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _refreshTokenSalt = config["TokenKey"];
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _userManager = userManager;
        }

        /// <summary>
        /// Create Refresh Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Rendom generated refresh token string</returns>
        public Task<string> CreateRefreshToken(AppUser user)
        {
            // Get salt from app settings
            var salt = _refreshTokenSalt;
            if (string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("Missing RefreshTokenSalt in app settings");
            }

            // Generate random value
            var randomBytes = new byte[32]; // Adjust size as needed (32 bytes for 256-bit random value)
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(randomBytes);
            }
            var randomString = Convert.ToBase64String(randomBytes);

            // Combine username, salt, and random value
            var combinedString = $"{user.UserName}{salt}{randomString}";

            // Hash using SHA512
            using (var sha512 = SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
                    return Task.FromResult( Convert.ToBase64String(hashedBytes)); 
            }
        }

        /// <summary>
        /// This function creates a JWT token for authentication in an API using a user's username as a
        /// claim.
        /// </summary>
        /// <param name="user">AppUser is a custom class representing a user in the application. It
        /// contains properties such as UserName, Email, Password, etc.</param>
        /// <returns>
        /// The method is returning a JWT token as a string.
        /// </returns>
        public async Task<string> CreateToken(AppUser user)
        {
            var claim = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };
            //Adding role in token service
            var roles = await _userManager.GetRolesAsync(user);
            
            claim.AddRange(roles.Select(role=> new Claim(ClaimTypes.Role, role)));

            var creds= new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials= creds
            };

            var tokenHandlar = new JwtSecurityTokenHandler();
            var token = tokenHandlar.CreateToken(tokenDescriptor);
            return tokenHandlar.WriteToken(token);
        }
    }
}
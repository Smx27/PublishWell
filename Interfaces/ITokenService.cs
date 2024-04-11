using JPS.Data.Entities;

namespace JPS.Interfaces
{
    /// <summary>
    /// Interface for Token service which will handle jwt tokens
    /// </summary>
    public interface ITokenService
    {

        /// <summary>
        /// This function creates a JWT token for authentication in an API using a user's username as a
        /// claim.
        /// </summary>
        /// <param name="user">AppUser is a custom class representing a user in the application. It
        /// contains properties such as UserName, Email, Password, etc.</param>
        /// <returns>
        /// The method is returning a JWT token as a string.
        /// </returns>
        Task<string> CreateToken(AppUser user);
        
        /// <summary>
        /// This function creates a JWT refresh token for authentication in an API using a user's username as a
        /// claim.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Jwt refresh token as a string</returns>
        Task<string> CreateRefreshToken(AppUser user);

    }
}
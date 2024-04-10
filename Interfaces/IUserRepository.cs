using JPS.Data.Entities;
using PublishWell.Controllers.Users.DTO;

namespace JPS.Interfaces
{
    /// <summary>
    /// Interface for user repository, providing methods to retrieve user data.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user by their ID asynchronously.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        Task<UserDataDTO> GetUserByID(int userID);

        /// <summary>
        /// Gets a user by their username asynchronously. (There seems to be a typo in the method name, assuming it should be GetUserByName)
        /// </summary>
        /// <param name="userName">The username of the user (likely a typo, should be string name instead of int userID).</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        Task<UserDataDTO> GetUserByName(string userName);

        /// <summary>
        /// Gets a user by their email address asynchronously. (There seems to be a typo in the method name, assuming it should be GetUserByEmail)
        /// </summary>
        /// <param name="email">The email address of the user (likely a typo, should be string email instead of int userID).</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        Task<UserDataDTO> GetUserByEmail(string email);

        /// <summary>
        /// Update user object
        /// </summary>
        /// <param name="userData"></param>
        void Update(UserDataDTO userData);

        /// <summary>
        /// Create User Data
        /// </summary>
        /// <param name="user"></param>
        void Create(UserDataDTO user);

        /// <summary>
        /// Delete User Data
        /// </summary>
        /// <param name="userID"></param>
        void Delete(int userID );
        /// <summary>
        /// Check if the user exist or not
        /// </summary>
        /// <param name="userID"></param>
        bool IsExist(int userID);
    }
}

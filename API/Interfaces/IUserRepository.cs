using PublishWell.API.Controllers.Users.DTO;

namespace JPS.Interfaces
{
    /// <summary>
    /// Interface for user repository, providing methods to retrieve user data.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all users asynchronously.
        /// </summary>
        /// <returns>A task that resolves to a list of `AppUser` objects representing all retrieved users.</returns>
        Task<List<UserDataDTO>> GetAllUsers();
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
        Task UpdateAsync(UserDataDTO userData);

        /// <summary>
        /// Create User Data
        /// </summary>
        /// <param name="user"></param>
        Task CreateAsync(UserDataDTO user);

        /// <summary>
        /// Delete User Data
        /// </summary>
        /// <param name="userID"></param>
        Task DeleteAsync(int userID );
        /// <summary>
        /// Check if the user exist or not
        /// </summary>
        /// <param name="userID"></param>
        bool IsExist(int userID);
    }
}

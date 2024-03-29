using JPS.Data.Entities;

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
        Task<AppUser> GetUserByID(int userID);

        /// <summary>
        /// Gets a user by their username asynchronously. (There seems to be a typo in the method name, assuming it should be GetUserByName)
        /// </summary>
        /// <param name="userID">The username of the user (likely a typo, should be string name instead of int userID).</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        Task<AppUser> GetUserByName(int userID); // Likely a typo, consider changing parameter to string name

        /// <summary>
        /// Gets a user by their email address asynchronously. (There seems to be a typo in the method name, assuming it should be GetUserByEmail)
        /// </summary>
        /// <param name="userID">The email address of the user (likely a typo, should be string email instead of int userID).</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        Task<AppUser> GetUserByEmail(int userID); // Likely a typo, consider changing parameter to string email
    }
}

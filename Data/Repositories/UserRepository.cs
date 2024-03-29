using JPS.Data.Entities;
using JPS.Interfaces;

namespace JPS.Data.Repositories
{
    /// <summary>
    /// User repository to handle User interactions, providing data access for retrieving user information.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Database context used for accessing user data.
        /// </summary>
        public DataContext _context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class with a specified data context.
        /// </summary>
        /// <param name="context">The data context to use for data access.</param>
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a user by their ID asynchronously.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        public Task<AppUser> GetUserByID(int userID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a user by their username asynchronously, but this method has a potential typo in its name and parameter.
        /// </summary>
        /// <param name="userID">The username of the user (likely a typo, should be string name instead of int userID).</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        public Task<AppUser> GetUserByName(int userID)  // Potential typo: Consider renaming to GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a user by their email address asynchronously, but this method has a potential typo in its name and parameter.
        /// </summary>
        /// <param name="userID">The email address of the user (likely a typo, should be string email instead of int userID).</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        public Task<AppUser> GetUserByEmail(int userID)  // Potential typo: Consider renaming to GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}

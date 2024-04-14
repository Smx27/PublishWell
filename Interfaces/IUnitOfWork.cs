using JPS.Interfaces;

namespace PublishWell.Interfaces
{
    /// <summary>
    /// Interface representing a Unit of Work pattern.
    /// This interface provides access to repositories for various data access operations
    /// and allows for committing changes made through these repositories in a single transaction.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Provides access to the IUserRepository instance for user-related data access operations.
        /// </summary>
        public IUserRepository userRepository { get; }

        /// <summary>
        /// Provides access to the IMailRepository instance for mail template-related data access operations.
        /// </summary>
        public IMailRepository mailRepository { get; }

        /// <summary>
        /// Provides access to the IExceptionLogRepository instance for exception logging operations.
        /// </summary>
        public IExceptionLogRepository exceptionLogRepository { get; }

        /// <summary>
        /// Commits all changes made through the repositories exposed by this UnitOfWork instance
        /// in a single database transaction.
        /// </summary>
        /// <returns>A Task indicating completion of the commit operation. 
        /// The returned boolean value indicates success (true) or failure (false).</returns>
        Task<bool> Complete();

        /// <summary>
        /// Checks whether any changes have been made through the repositories exposed by this UnitOfWork instance.
        /// </summary>
        /// <returns>True if there are changes to be committed, false otherwise.</returns>
        bool HasChanges();
    }
}

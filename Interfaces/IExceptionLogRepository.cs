using PublishWell.Data.Entities;

namespace PublishWell.Interfaces
{
    /// <summary>
    /// ception log repository interface.
    /// </summary>
    public interface IExceptionLogRepository
    {
        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        void LogException(Exception ex);

        /// <summary>
        /// Gets all exception logs.
        /// </summary>
        /// <returns>A list of exception logs.</returns>
        Task<List<ExceptionLog>> GetExceptionLogs();
    }
}

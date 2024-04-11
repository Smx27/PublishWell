using JPS.Data;
using Microsoft.EntityFrameworkCore;
using PublishWell.Data.Entities;
using PublishWell.Interfaces;

namespace PublishWell.Data.Repositories
{
    /// <summary>
    /// Repository for managing exception logs.
    /// </summary>
    public class ExceptionLogRepository : IExceptionLogRepository
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionLogRepository"/> class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public ExceptionLogRepository(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets all exception logs.
        /// </summary>
        /// <returns>A list of exception logs.</returns>
        public Task<List<ExceptionLog>> GetExceptionLogs()
        {
            return _context.ExceptionLogs
            .OrderByDescending(x=> x.Id)
            .ToListAsync();
        }

        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        public void LogException(Exception ex)
        {
            if(ex == null) return;
            ExceptionLog log = new ExceptionLog
            {
                InnerException = ex.InnerException?.ToString(),
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace,
                TargetSite = ex.TargetSite?.ToString()
            };
            _context.ExceptionLogs.Add(log);
            _context.SaveChanges();
        }
    }
}
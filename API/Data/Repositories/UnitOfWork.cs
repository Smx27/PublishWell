using AutoMapper;
using JPS.Data;
using JPS.Data.Repositories;
using JPS.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using PublishWell.API.Interfaces;

namespace PublishWell.API.Data.Repositories
{
    /// <summary>
    /// Concrete implementation of the IUnitOfWork interface.
    /// This class manages a set of repositories and provides a way to commit changes 
    /// made through them in a single transaction.
    /// 
    /// It leverages dependency injection to receive dependencies like DataContext, 
    /// AutoMapper instance, and ILogger.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly IDistributedCache _cache;
        /// <summary>
        /// Injecting the repository into the unit of work constructor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        /// <param name="http"></param>
        /// <param name="cache"></param>
        public UnitOfWork(DataContext context, IMapper mapper, IHttpContextAccessor http, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _http = http;
            _cache = cache;
        }

        /// <summary>
        /// Provides access to the IUserRepository instance for user-related data access operations.
        /// This instance is created with the injected DataContext and AutoMapper for potential object mapping needs.
        /// </summary>
        public IUserRepository userRepository => new UserRepository(_context, _mapper, _cache);

        /// <summary>
        /// Provides access to the IMailRepository instance for mail template-related data access operations.
        /// This instance is created with the injected DataContext and AutoMapper for potential object mapping needs.
        /// </summary>
        public IMailRepository mailRepository => new MailRepository(_context, _mapper);

        /// <summary>
        /// Provides access to the IExceptionLogRepository instance for exception logging operations.
        /// This instance is created directly with the injected DataContext.
        /// (Consider if AutoMapper mapping is needed for exception logging)
        /// </summary>
        public IExceptionLogRepository exceptionLogRepository => new ExceptionLogRepository(_context);

        /// <summary>
        ///  Provides access to the IPublicationsRepository instance.
        /// </summary>
        /// <returns>publicationsRepository object</returns>
        public IPublicationsRepository publicationsRepository => new PublicationRepository(_context, _mapper, _http, _cache);

        /// <summary>
        /// Commits all changes made through the repositories exposed by this UnitOfWork instance
        /// in a single database transaction.
        /// 
        /// It calls SaveChangesAsync on the injected DataContext and returns true if any changes were saved,
        /// indicating a successful commit.
        /// </summary>
        /// <returns>A Task indicating completion of the commit operation. 
        /// The returned boolean value indicates success (true) or failure (false).</returns>
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Checks whether any changes have been made through the repositories exposed by this UnitOfWork instance.
        /// 
        /// It uses the ChangeTracker property of the DataContext to determine if there are any unsaved modifications.
        /// </summary>
        /// <returns>True if there are changes to be committed, false otherwise.</returns>
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}

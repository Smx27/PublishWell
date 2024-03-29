using JPS.Data;
using Microsoft.EntityFrameworkCore;

namespace JPS.Extension
{
    /// <summary>
    /// Extension class of IServiceCollection 
    /// </summary>
    public static class ApplicationServiceExtension
    {
        /// <summary>
        /// Extension Method of IServiceCollection
        /// </summary>
        /// <param name="service"></param>
        /// <param name="config"></param>
        /// <returns>Application Service Collection</returns>
        public static IServiceCollection ApplicationService(this IServiceCollection service, IConfiguration config)
        {
            // Added database Services
            service.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("SqliteCS"));
            });
            //TODO: add CORS
            return service;
        }
    }
}
using JPS.Data;
using JPS.Data.Repositories;
using JPS.Interfaces;
using JPS.Services;
using Microsoft.EntityFrameworkCore;
using PublishWell.API.Common.Helper;
using PublishWell.API.Data.Repositories;
using PublishWell.API.Interfaces;

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
            service.AddCors(o=> o.AddPolicy(name: "CORS" , builder=> {
                builder.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
            }));

            //Added token service 
            service.AddScoped<ITokenService, TokenService>();
            //Adding exception log repo
            service.AddScoped<IExceptionLogRepository, ExceptionLogRepository>();
            //Adding UserRepository 
            service.AddScoped<IUserRepository, UserRepository>();
            // Adding Mail repository
            service.AddScoped<IMailRepository, MailRepository>();
            //Adding unit of work
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            // Adding Email Service 
            service.AddScoped<IEmailService, EmailService>();
            // Adding Publication Repository
            service.AddScoped<IPublicationsRepository, PublicationRepository>();
            //adding automapper
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Adding Email service
            // service.AddFluentEmail("info@sp.com")
            //     .AddSmtpSender("localhost", 2500);
            //Adding SMTP Setup class 
            service.Configure<SmtpSetup>(config.GetSection("SmtpSetup"));
            //Adding ApplicationInformation
            service.Configure<AppInfo>(config.GetSection("AppDeploymentInfo"));


            return service;
        }
    }
}
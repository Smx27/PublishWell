using System.Text;
using JPS.Data;
using JPS.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JPS.Extension
{
    /// <summary>
    /// Identity Service Extension Class
    /// </summary>
    public static class IdentityServicesExtension
    {
        /// <summary>
        /// Extension method to add Identity Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns>Identity service Collection</returns>
        public static IServiceCollection IdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //Added Microsoft Identity service
            services.AddIdentityCore<AppUser>(opt=>{
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddUserManager<UserManager<AppUser>>()
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddEntityFrameworkStores<DataContext>();
            //Barrier config
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    options.Events = new JwtBearerEvents{
                        OnMessageReceived = context => {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;

                            if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs")){
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                }
            );

            //Adding policy to the application
            services.AddAuthorization(opt=>{
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin","Moderate"));
            });
            return services;
        }
    }
}
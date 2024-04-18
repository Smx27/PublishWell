using System.Text;
using JPS.Data;
using JPS.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager()
            .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider)
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

            //adding httpcontext accessor
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
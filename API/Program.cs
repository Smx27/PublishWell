using API.Middleware;
using JPS.Data;
using JPS.Data.Entities;
using JPS.Data.Seeder;
using JPS.Extension;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Added application services through extension
builder.Services.ApplicationService(builder.Configuration);

// Added Identity Services through Extension class
builder.Services.IdentityServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseKestrel();
//Added Swagger 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PublishWell",
        Description = "An ASP.NET Core Web API for managing PublishWell api's",
        TermsOfService = new Uri("https://github.com/Smx27/PublishWell/blob/master/terms.txt"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://github.com/Smx27")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://github.com/Smx27/PublishWell/blob/master/LICENSE.txt")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});


// Adding redis caching 
builder.Services.AddStackExchangeRedisCache(o=>{
    o.Configuration = builder.Configuration.GetConnectionString("Redis");
    o.InstanceName = "Publishwell_";
});


var app = builder.Build();
//Adding exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
//Added cors
app.UseCors("CORS");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Seeding default Data
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
try
{
    var context = service.GetRequiredService<DataContext>();
    var userManager = service.GetRequiredService<UserManager<AppUser>>();
    var roleManager = service.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await SeedData.SeedUsers(userManager, roleManager);
}
catch(Exception ex)
{
    var logger  = service.GetService<ILogger<Program>>();
    logger.LogError(ex,"An error occur while seeding data/ Migration");
}

app.Run();
using JPS.Extension;
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
});


var app = builder.Build();

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

app.UseAuthorization();

app.MapControllers();

//Seeding default Data
// using var scope = app.Services.CreateScope();
// var service = scope.ServiceProvider;
// try
// {
//     var context = service.GetRequiredService<DataContext>();
//     var userManager = service.GetRequiredService<UserManager<AppUser>>();
//     var roleManager = service.GetRequiredService<RoleManager<AppRole>>();
//     await context.Database.MigrateAsync();
//     await SeedData.SeedUsers(userManager, roleManager);
// }
// catch(Exception ex)
// {
//     var logger  = service.GetService<ILogger<Program>>();
//     logger.LogError(ex,"An error occur while seeding data/ Migration");
// }

app.Run();

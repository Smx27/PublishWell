using JPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JPS.Data.Seeder
{
    /// <summary>
    /// This will seed some pre defined data to the db 
    /// </summary>
    public class SeedData
    {
        /// <summary>
        /// Seed user Data to db
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns>Predefined Data</returns>
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;
            // TODO: Will be added after adding user properties
            // var userData = await File.ReadAllTextAsync("Data/UserDataSeed.json");

            // var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);
            var users = new List<AppUser>{
                new AppUser{
                    UserName = "TestUser",
                    Created = DateTime.Now,
                    Email = "testuser@jps.com",
                    LastActive = DateTime.Now
                },
                new AppUser{
                    UserName = "Member",
                    Created = DateTime.Now,
                    Email = "member@jps.com",
                    LastActive = DateTime.Now
                },
                new AppUser{
                    UserName = "Moderator",
                    Created = DateTime.Now,
                    Email = "moderator@jps.com",
                    LastActive = DateTime.Now
                }
            };
            var roles = new List<AppRole>{
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                user.Created = DateTime.SpecifyKind(user.Created, DateTimeKind.Utc);
                user.LastActive = DateTime.SpecifyKind(user.LastActive, DateTimeKind.Utc);
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@jps.com",
                LastActive = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
        }

    }
}
using JPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Faker;
using PublishWell.Data.Enums;
using PublishWell.Data.Entities;

namespace JPS.Data.Seeder
{
    /// <summary>
    /// This will seed some pre defined data to the db 
    /// </summary>
    public class SeedData
    {
        /// <summary>
        /// Get Randomly Generated User
        /// </summary>
        /// <param name="userCount"></param>
        /// <returns></returns>
        public static List<AppUser> GetUsers(int userCount)
        {
            // var faker = new Faker.TextFaker();
            var users = new List<AppUser>();

            for (int i = 0; i < userCount; i++)
            {
                users.Add(new AppUser
                {
                    FirstName = NameFaker.FirstName(),
                    LastName = NameFaker.LastName(),
                    Email = InternetFaker.Email(),
                    UserName = $"{NameFaker.FirstName()}Gen{NameFaker.LastName()}", // Consider validation for uniqueness
                    PhotoUrl = string.Empty,  // Replace with placeholder image if needed
                    Website = InternetFaker.Url(),
                    Location = LocationFaker.City() + ", " + LocationFaker.Country(),
                    Bio = TextFaker.Sentences(5),  // Generate a sentence with 5 words
                    IsActive = BooleanFaker.Boolean(),
                    NotificationsEnabled = BooleanFaker.Boolean(),
                    Created = DateTime.UtcNow.AddDays(-NumberFaker.Number(30)), // Random past date within 30 days
                    LastActive = DateTime.UtcNow.AddDays(-NumberFaker.Number(7)),  // Random past date within 7 days
                    IsDeleted = false,
                    PhoneNumber = PhoneFaker.Phone(),
                    Theme = EnumFaker.SelectFrom<Theme>(),
                    SocialMediaLinks = new SocialMediaLinks{
                        Facebook = "www.facebook.com/" + NameFaker.Name().Replace(" ", "_"),
                        Twitter = "www.twitter.com/" + NameFaker.Name().Replace(" ", "_"),
                        Instagram = "www.instagram.com/" + NameFaker.Name().Replace(" ", "_"),
                        LinkedIn = "www.linkedin.com/" + NameFaker.Name().Replace(" ", "_"),
                        GitHub = "www.github.com/" + NameFaker.Name().Replace(" ", "_"),
                        Pinterest = "www.pinterest.com/" + NameFaker.Name().Replace(" ", "_")
                    }
                });
            }

            return users;
        }
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
            var users = GetUsers(10);
            var roles = new List<AppRole>{
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
                new AppRole{Name = "Reviewer"},
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
                var Data = await userManager.FindByNameAsync(user.UserName.ToLower());
                Data.UserRoles = new List<AppUserRole>(){
                    new AppUserRole {
                        Role = await roleManager.FindByNameAsync("Member"),
                        RoleId = roleManager.FindByNameAsync("Member").Result.Id,
                        User = Data,
                        UserId = Data.Id
                    }
                };
                await userManager.UpdateAsync(Data);
                // await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin".ToLower(),
                FirstName = "Admin",
                LastName = "User",
                Bio = "This is a generated admin user which will be replace latter on",
                IsActive = true,
                IsDeleted = false,
                NotificationsEnabled = true,
                Theme = Theme.Light,
                Email = "admin@jps.com",
                LastActive = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            var userData = await userManager.FindByNameAsync(admin.UserName.ToLower());
                userData.UserRoles = new List<AppUserRole>(){
                    new AppUserRole {
                        Role = await roleManager.FindByNameAsync("Admin"),
                        RoleId = roleManager.FindByNameAsync("Admin").Result.Id,
                        User = userData,
                        UserId = userData.Id
                    },                    
                    new AppUserRole {
                        Role = await roleManager.FindByNameAsync("Moderator"),
                        RoleId = roleManager.FindByNameAsync("Moderator").Result.Id,
                        User = userData,
                        UserId = userData.Id
                    }

                };
            var result = await userManager.UpdateAsync(userData);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            // await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
        }

    }
}
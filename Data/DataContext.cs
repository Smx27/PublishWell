using JPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JPS.Data
{
    /// <summary>
    /// Data context class which will handle migrations
    /// </summary>
    public class DataContext : IdentityDbContext<AppUser,AppRole, int,
    IdentityUserClaim<int>, AppUserRole, 
    IdentityUserLogin<int>, IdentityRoleClaim<int>,
    IdentityUserToken<int>>
    {
        /// <summary>
        /// Constructor for DataContext class to init base class
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public DataContext(DbContextOptions options) : base(options){}

        /// <summary>
        /// The OnModelCreating function is used to configure the relationships and constraints between
        /// entities in the database model.
        /// </summary>
        /// <param name="builder">builder is a class provided by Entity Framework Core that is
        /// used to configure the database model. It is used to define the shape of the entities and
        /// their relationships in the database.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<AppUser>()
            .HasMany(ur=> ur.UserRoles)
            .WithOne(u=> u.User)
            .HasForeignKey(ur=> ur.UserId)
            .IsRequired();

            builder.Entity<AppRole>()
            .HasMany(ur=> ur.UserRoles)
            .WithOne(u=> u.Role)
            .HasForeignKey(ur=> ur.RoleId)
            .IsRequired();
        }
    }
}
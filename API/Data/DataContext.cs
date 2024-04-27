using JPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PublishWell.API.Data.Entities;

namespace JPS.Data;

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
    /// Exception Log Table which will store all the exceptions
    /// </summary>
    public DbSet<ExceptionLog> ExceptionLogs { get; set; }

    /// <summary>
    /// Mail Template Table which will store all the mail templates
    /// </summary>
    public DbSet<MailTemplate> MailTemplates { get; set; }
    /// <summary>
    /// Mail Template type Table which will store all the mail templates Types.
    /// </summary>
    public DbSet<MailTemplateType> MailTemplateTypes { get; set; }

    /// <summary>
    /// Publication Table which will store all the publications.
    /// </summary>
    public DbSet<Publication> Publications { get; set; }
    
    /// <summary>
    /// Categorie Table which will store all the categories for publication.
    /// </summary>
    public DbSet<Categorie> Categories { get; set; }
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

        builder.Entity<MailTemplateType>()
        .HasMany(mt=> mt.Template)
        .WithOne(mtt=> mtt.Type);

        builder.Entity<AppUser>()
        .HasMany(p=> p.Publications)
        .WithOne(u=> u.PublicationAuthor)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
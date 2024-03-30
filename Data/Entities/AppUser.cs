using Microsoft.AspNetCore.Identity;

namespace JPS.Data.Entities
{
    /// <summary>
    /// User Entity class 
    /// </summary>
    public class AppUser : IdentityUser<int>
    {
        /// <summary>
        /// The date and time the user was created.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date and time the user was last active.
        /// </summary>
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Roles for user
        /// </summary>
        /// <value></value>
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}

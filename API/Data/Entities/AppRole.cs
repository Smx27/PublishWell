using Microsoft.AspNetCore.Identity;

namespace JPS.Data.Entities
{
    /// <summary>
    /// Entity class for User role
    /// </summary>
    public class AppRole: IdentityRole<int>
    {
        /// <summary>
        /// Navigation property for Users roles
        /// </summary>
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
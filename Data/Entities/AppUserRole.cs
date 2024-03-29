using Microsoft.AspNetCore.Identity;

namespace JPS.Data.Entities
{
    /// <summary>
    /// This class represents the relationship between a user and a role.
    /// </summary>
    public class AppUserRole : IdentityUserRole<int>
    {
        /// <summary>
        /// The user associated with this role assignment.
        /// </summary>
        public AppUser User { get; set; }

        /// <summary>
        /// The role associated with this role assignment.
        /// </summary>
        public AppRole Role { get; set; }
    }
}

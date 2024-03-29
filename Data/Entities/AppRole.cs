using Microsoft.AspNetCore.Identity;

namespace JPS.Data.Entities
{
    /// <summary>
    /// Entity class for User role
    /// </summary>
    public class AppRole: IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
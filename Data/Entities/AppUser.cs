using Microsoft.AspNetCore.Identity;

namespace JPS.Data.Entities
{
    /// <summary>
    /// User Entity class 
    /// </summary>
    public class AppUser : IdentityUser<int>
    {
        /// <summary>
        /// The user's first name.
        /// </summary>
        /// <value>John</value>
        public string FirstName { get; set; }

        /// <summary>
        /// The App users last name.
        /// </summary>
        /// <value>Doe</value>
        public string LastName { get; set; }
        /// <summary>
        /// Is User Deleted Or not 
        /// </summary>
        /// <value></value>
        public bool IsDeleted { get; set; } = false;
        
        /// <summary>
        /// Is User Active Or not 
        /// </summary>
        /// <value></value>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// The user's photo URL.
        /// </summary>
        /// <value>https://www.example.com/profile-photo.jpg</value>
        public string PhotoUrl { get; set; }
        
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

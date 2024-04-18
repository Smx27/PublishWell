using Microsoft.AspNetCore.Identity;
using PublishWell.Data.Entities;
using PublishWell.Data.Enums;

namespace JPS.Data.Entities;

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

    /// <summary>
    /// (Optional) The user's website URL.
    /// </summary>
    public string Website { get; set; }

    /// <summary>
    /// (Optional) The user's location (city, country). Consider privacy implications before storing.
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// (Optional) A short biography of the user.
    /// </summary>
    public string Bio { get; set; }

    // Social Information (consider privacy implications and potential misuse)
    /// <summary>
    /// (Optional) A dictionary or string containing links to the user's social media profiles.
    /// </summary>
    public SocialMediaLinks SocialMediaLinks { get; set; }

    // Preferences
    /// <summary>
    /// (Optional) The user's preferred language for the application interface.
    /// </summary>
    public string PreferredLanguage { get; set; }

    /// <summary>
    /// A flag indicating whether the user wants to receive notifications.
    /// </summary>
    public bool NotificationsEnabled { get; set; } = true;

    // Advanced Preferences (consider complexity and potential for future changes)
    /// <summary>
    /// (Optional) The user's preferred theme for the application interface (e.g., light, dark).
    /// </summary>
    public Theme Theme { get; set; }

    /// <summary>
    /// (Optional) A complex object or string storing the user's preferences for content filtering or display.
    /// </summary>
    public string ContentPreferences { get; set; }

    // Security Enhancements (implement securely with proper expiration)
    /// <summary>
    /// (Optional) A temporary token used for password reset functionality.
    /// </summary>
    public string PasswordResetToken { get; set; }

    /// <summary>
    /// (Optional) The IP address of the user's last login attempt (for security monitoring).
    /// </summary>
    public string LastLoginIp { get; set; }

    /// <summary>
    /// Refresh Token Of the user
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// Refresh Token Expiry Time Of the user
    /// </summary>
    public DateTime RefreshTokenExpiryTime { get; set; }

    // Relationships (implement as needed)

    /// <summary>
    /// Users Publications 
    /// </summary>
    /// <value></value>
    public ICollection<Publication> Publications { get; set; }

    /// <summary>
    /// Collection of posts created by the user (one-to-many relationship with Post entity).
    /// </summary>
    //public ICollection<Post> Posts { get; set; }

    /// <summary>
    /// Collection of subscriptions the user has (one-to-many relationship with Subscription entity).
    /// </summary>
    //public ICollection<Subscription> Subscriptions { get; set; }

    /// <summary>
    /// Collection of comments the user has made (one-to-many relationship with Comment entity).
    /// </summary>
    //public ICollection<Comment> Comments { get; set; }

    /// <summary>
    /// Collection of ratings the user has given (one-to-many relationship with Rating entity).
    /// </summary>
    //public ICollection<Rating> Ratings { get; set; }
}
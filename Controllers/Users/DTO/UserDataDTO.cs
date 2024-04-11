using PublishWell.Data.Entities;
using PublishWell.Data.Enums;

namespace PublishWell.Controllers.Users.DTO;

/// <summary>
/// Data transfer object for user data.
/// </summary>
public class UserDataDTO
{
    /// <summary>
    /// The user's first name.
    /// </summary>
    /// <value>John</value>
    public string FirstName { get; set; }

    /// <summary>
    /// Is User Deleted Or not 
    /// </summary>
    /// <value></value>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Is User Active Or not 
    /// </summary>
    /// <value></value>
    public bool IsActive { get; set; }
    /// <summary>
    /// The user's username.
    /// </summary>
    /// <value>johndoe</value>
    public string UserName { get; set; }
    /// <summary>
    /// The App users last name.
    /// </summary>
    /// <value>Doe</value>
    public string LastName { get; set; }

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


}

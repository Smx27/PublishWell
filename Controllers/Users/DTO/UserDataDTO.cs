using JPS.Data.Entities;

namespace PublishWell.Controllers.Users.DTO;

/// <summary>
/// Data transfer object for user data.
/// </summary>
public class UserDataDTO
{
    /// <summary>
    /// The user's unique identifier.
    /// </summary>
    /// <value>1</value>
    public int Id { get; set; }

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
    /// The user's email address.
    /// </summary>
    /// <value>john.doe@example.com</value>
    public string Email { get; set; }

    /// <summary>
    /// The user's phone number.
    /// </summary>
    /// <value>123-456-7890</value>
    public string PhoneNumber { get; set; }

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

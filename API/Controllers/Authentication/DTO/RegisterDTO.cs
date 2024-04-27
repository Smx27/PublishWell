using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PublishWell.API.Controllers.Authentication.DTO
{
    /// <summary>
    ///  Data transfer object for registering a new user.
    /// </summary>
    public class RegisterDTO
    {
        /// <summary>
        /// The username of the new user.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [DefaultValue("TestUser1")]
        public string Username { get; set; }

        /// <summary>
        /// The password of the new user.
        /// </summary>
        [Required]
        [StringLength(8,MinimumLength =4)]
        [DataType(DataType.Password)]
        [DefaultValue("Pa$$w0rd")]
        public string Password { get; set; }

        /// <summary>
        /// The email address of the new user.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [DefaultValue("TestUser1@jps.com")]
        public string Email { get; set; }

        /// <summary>
        /// The user's first name.
        /// </summary>
        /// <value>John</value>
        [Required]
        [DataType(DataType.Text)]
        [DefaultValue("John")]
        public string FirstName { get; set; }
        /// <summary>
        /// The App users last name.
        /// </summary>
        /// <value>Doe</value>
        [Required]
        [DataType(DataType.Text)]
        [DefaultValue("Doe")]
        public string LastName { get; set; }

        /// <summary>
        /// The user's phone number.
        /// </summary>
        /// <value>123-456-7890</value>
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DefaultValue("9134567890")]
        public string PhoneNumber { get; set; }

    }
}
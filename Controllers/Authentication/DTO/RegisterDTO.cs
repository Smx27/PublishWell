using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PublishWell.Controllers.Authentication.DTO
{
    public class RegisterDTO
    {
        [Required]
        [DataType(DataType.Text)]
        [DefaultValue("TestUser1")]
        public string Username { get; set; }
        [Required]
        [StringLength(8,MinimumLength =4)]
        [DataType(DataType.Password)]
        [DefaultValue("Pa$$w0rd")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DefaultValue("TestUser1@jps.com")]
        public string Email { get; set; }
    }
}
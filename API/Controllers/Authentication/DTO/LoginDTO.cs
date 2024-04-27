using System.ComponentModel;

namespace PublishWell.API.Controllers.Authentication.DTO
{
    /// <summary>
    /// Login DTO which will be supplied by the frontend
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Username of user the user 
        /// </summary>
        /// <value>Username of the user</value>
        [DefaultValue("TestUser1")]
        public string UserName { get; set; }
        /// <summary>
        /// Password of the user
        /// </summary>
        /// <value>Password of the User</value>
        [DefaultValue("Pa$$w0rd")]
        public string Password { get; set; }
    }
}
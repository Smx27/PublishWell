namespace PublishWell.Controllers.Users.DTO
{
    /// <summary>
    /// User dto which will be sent back to application
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Username of the user
        /// </summary>
        /// <value>SampleUsername</value>
        public string UserName { get; set; }
        /// <summary>
        /// Token of the user
        /// </summary>
        /// <value>SampleJwtToken</value>
        public string Token { get; set; }
        /// <summary>
        /// Email of the user
        /// </summary>
        /// <value>SampleEmail</value>
        public string Email { get; set; }
    }
}
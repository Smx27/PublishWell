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
        public string JWTToken { get; set; }

        /// <summary>
        /// Refresh token of the user
        /// </summary>
        /// <value>Some RandomlyGenerated hashed string</value>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Refresh token expiry date
        /// </summary>
        /// <value>Token expiry date</value>
        public DateTime RefreshTokenExpires { get; set; }
        
        /// <summary>
        /// Email of the user
        /// </summary>
        /// <value>SampleEmail</value>
        public string Email { get; set; }
    }
}
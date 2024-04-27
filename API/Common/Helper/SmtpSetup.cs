namespace PublishWell.API.Common.Helper
{
    /// <summary>
    /// This is a smtp setup class which will pull value from appsettings.
    /// </summary>
    public class SmtpSetup
    {
        /// <summary>
        /// SMTP port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// SMTP host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Using ssl for smtp or not
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// Service name
        /// </summary>
        public string Name { get; set; }
    }
}
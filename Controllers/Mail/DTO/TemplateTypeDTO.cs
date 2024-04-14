using System.ComponentModel;

namespace PublishWell.Controllers.Mail.DTO
{
    /// <summary>
    /// Template type data transfar object 
    /// </summary>
    public class TemplateTypeDTO
    {
        /// <summary>
        /// MailTemplateType Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// MailTemplateType Name
        /// </summary>
        [DefaultValue("UserRegistrationTemplate")]
        public string Name { get; set; }

        /// <summary>
        /// MailTemplateType Description
        /// </summary>
        [DefaultValue("User Registration Template for PublishWell Application")]
        public string Description { get; set; }

    }
}
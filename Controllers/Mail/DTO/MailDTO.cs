namespace PublishWell.Controllers.Mail.DTO
{
    /// <summary>
    /// MailDTO
    /// </summary>
    public class MailDTO
    {
        /// <summary>
        /// Mail Template ID
        /// </summary>
        public int TemplateID { get; set; }
        /// <summary>
        /// Template type id which will indicate what type of mail template is this.
        /// </summary>
        public int TemplateTypeID { get; set; }
        /// <summary>
        /// Template type id which will indicate what type of mail template is this.
        /// </summary>
        public string TemplateTypeName { get; set; }
                
        /// <summary>
        /// MailTemplate Name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Template Content
        /// </summary>
        public string MailTemplateBody { get; set; }

        /// <summary>
        /// Template Created At
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Template Updated At
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Template IsDeleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Template IsDefault or not.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Template Created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Template Updated By
        /// </summary>
        public string UpdatedBy { get; set; }

    }
}
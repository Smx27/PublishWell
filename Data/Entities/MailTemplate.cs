namespace PublishWell.Data.Entities
{
    /// <summary>
    /// MailTemplate entity
    /// </summary>
    public class MailTemplate
    {   
        /// <summary>
        /// MailTemplate Id
        /// </summary>
        public int Id { get; set; }
        
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
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Template IsDefault or not.
        /// </summary>
        public bool IsDefault { get; set; }
        
        /// <summary>
        /// Template Type
        /// </summary>
        public MailTemplateType Type { get; set; }
        /// <summary>
        /// Mail Template type id
        /// </summary>
        public int TypeID { get; set; }

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
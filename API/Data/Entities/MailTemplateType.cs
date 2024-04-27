namespace PublishWell.API.Data.Entities
{
    /// <summary>
    /// MailTemplateType entity
    /// </summary>
    public class MailTemplateType
    {
        /// <summary>
        /// MailTemplateType Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// MailTemplateType Name
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// MailTemplateType Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// MailTemplateType Template
        /// </summary>
        public ICollection<MailTemplate> Template { get; set; }
    }
}
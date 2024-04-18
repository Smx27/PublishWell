using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PublishWell.Controllers.Publications.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for comments associated with publications.
    /// </summary>
    public class CommentDTO
    {
        /// <summary>
        /// The ID of the publication this comment belongs to.
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "'PublicationId' is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "'PublicationId' must be a positive integer.")]
        [DefaultValue(1)]
        public int PublicationId { get; set; }

        /// <summary>
        /// The ID of the viewer who posted the comment (optional).
        /// </summary>
        /// <example>2</example>
        [DefaultValue(1)]
        public int? ViewerId { get; set; }

        /// <summary>
        /// The content of the comment.
        /// </summary>
        /// <example>This is a sample comment.</example>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 5)]
        [DefaultValue("This is a sample comment.")]
        [DisplayName("Comment")]
        [Description("The content of the comment.")]
        public string Comment { get; set; }
    }
}

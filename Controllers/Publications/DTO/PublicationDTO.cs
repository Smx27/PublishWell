using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PublishWell.Data.Entities;
using PublishWell.Data.Enums;

namespace PublishWell.Controllers.Publications.DTO;

/// <summary>
/// Publication data transfar object which will be used by api to transfer data
/// </summary>
public class PublicationDTO
{
    /// <summary>
    /// (Optional) The number of comments left on this publication.
    /// </summary>
    public int CommentCount { get; set; }
    /// <summary>
    /// (Optional) The number of times this publication has been viewed.
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// (Optional) The number of likes this publication has received.
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Id of the viewer who is viewing this publication.
    /// </summary>
    public int ViewerID { get; set; }
    /// <summary>
    /// The unique identifier for the publication.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name or title of the publication.
    /// </summary>
    [Required(ErrorMessage = "Publication name is required.")]
    [MinLength(5)]
    [MaxLength(10000)]
    [DataType(DataType.Text)]
    [DefaultValue("Test Publication title")]
    public string PublicationName { get; set; }
    /// <summary>
    /// A description of the publication content.
    /// </summary>
    [Required(ErrorMessage = "Publication Description is required.")]
    [MinLength(5)]
    [MaxLength(10000)]
    [DataType(DataType.Text)]
    [DefaultValue("This is a test publication to testoout the api if that is working or not.")]
    public string PublicationDescription { get; set; }
    /// <summary>
    /// (Optional) A URL or path to an image representing the publication.
    /// </summary>
    public string PublicationImage { get; set; }
    /// <summary>
    /// The date and time the publication was created.
    /// </summary>
    public DateTime PublicationDate { get; set; }
    /// <summary>
    /// The foreign key representing the author of the publication (linked to the AppUser table).
    /// </summary>
    public int PublicationAuthorId { get; set; }
    /// <summary>
    /// Collection of PublicationTag objects associated with this publication. this sould be a ',' seperated string.
    /// </summary>
    [DefaultValue("test, test1, test again, something new")]
    public string Tags { get; set; }

    /// <summary>
    /// (Optional) The category of the publication (e.g., article, blog post, news item).
    /// </summary>
    [DefaultValue(3)]
    public int CategoryID { get; set; }

    /// <summary>
    /// The content of the publication.
    /// </summary>
    [MinLength(100)]
    [MaxLength(Int32.MaxValue)]
    [DataType(DataType.Html)]
    [Required(ErrorMessage = "Publication content is required.")]
    [DefaultValue(@"Missing Mapping Configuration: There might be missing configuration for mapping the PublicationTags property between PublicationDTO and Publication.
Property Mismatch: The structure of PublicationTags in PublicationDTO might not match the corresponding property in Publication. This could be a difference in data types, presence of nested objects, etc.")]
    public string PublicationContent { get; set; }

    /// <summary>
    /// (Optional) The status of the publication (e.g., active, inactive, deleted).
    /// </summary>
    [DefaultValue(PublicationStatus.Draft)]
    public PublicationStatus Status { get; set; } = PublicationStatus.Draft;
    /// <summary>
    /// (Optional) The date and time the publication was last updated.
    /// </summary>
    public DateTime LastUpdatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// The number of Edits this publication has received.
    /// </summary>
    public int EditCount { get; set; }
    /// <summary>
    /// Any documnet attached into the publications will be availlable here
    /// </summary>
    public List<string> AttachedDocuments { get; set; }
}
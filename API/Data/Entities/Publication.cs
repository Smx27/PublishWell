using System.ComponentModel.DataAnnotations;
using JPS.Data.Entities;
using PublishWell.API.Data.Enums;

namespace PublishWell.API.Data.Entities;

/// <summary>
/// Represents a published work of content within the PublishWell system.
/// </summary>
public class Publication
{
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
    public string PublicationName { get; set; }

    /// <summary>
    /// A description of the publication content.
    /// </summary>
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
    /// The date and time the data was created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// The foreign key representing the author of the publication (linked to the AppUser table).
    /// </summary>
    public int PublicationAuthorId { get; set; }

    /// <summary>
    /// Navigation property representing the author of the publication (AppUser object).
    /// </summary>
    public AppUser PublicationAuthor { get; set; }
    
    //TODO: Impliment Publication Tags

    /// <summary>
    /// Collection of PublicationTag objects associated with this publication.
    /// </summary>
    public ICollection<PublicationTag> PublicationTags { get; set; }

    /// <summary>
    /// Collection of PublicationLike objects representing users who liked this publication. 
    /// </summary>
    public ICollection<PublicationLike> PublicationLikes { get; set; }

    /// <summary>
    /// Collection of PublicationComment objects representing comments left on this publication.
    /// </summary>
    public ICollection<PublicationComment> PublicationComments { get; set; }

    /// <summary>
    /// Collection of PublicationView objects representing users who viewed this publication.
    /// </summary>
    public ICollection<PublicationView> PublicationViews { get; set; }

    /// <summary>
    /// (Optional) The category of the publication (e.g., article, blog post, news item).
    /// </summary>
    public PublicationCategorie PublicationCategory { get; set; }

    /// <summary>
    /// The content of the publication.
    /// </summary>
    [MinLength(100)]
    [MaxLength(Int32.MaxValue)]
    [DataType(DataType.Html)]
    [Required(ErrorMessage = "Publication content is required.")]
    public string PublicationContent { get; set; }
    
    /// <summary>
    /// (Optional) The status of the publication (e.g., active, inactive, deleted).
    /// </summary>
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
    /// A publication must contains one or multiple documnets of reserch files 
    /// </summary>
    public ICollection<string> AttachedDocumets { get; set; }

}

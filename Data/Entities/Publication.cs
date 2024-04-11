using JPS.Data.Entities;

namespace PublishWell.Data.Entities;

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
    // public ICollection<PublicationTag> PublicationTags { get; set; }

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
    public string PublicationCategory { get; set; }

    /// <summary>
    /// (Optional) A flag indicating if the publication is published or a draft.
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// (Optional) The number of comments left on this publication.
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// The content of the publication.
    /// </summary>
    public string PublicationContent { get; set; }
    
    /// <summary>
    /// (Optional) The status of the publication (e.g., active, inactive, deleted).
    /// </summary>
    public string Status { get; set; }
    /// <summary>
    /// (Optional) The number of times this publication has been viewed.
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// (Optional) The number of likes this publication has received.
    /// </summary>
    public int LikeCount { get; set; }
    
    /// <summary>
    /// The number of Edits this publication has received.
    /// </summary>
    public int EditCount { get; set; }

}

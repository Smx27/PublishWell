using JPS.Data.Entities;

namespace PublishWell.API.Data.Entities;

/// <summary>
/// This class represents a record of a user liking a publication.
/// </summary>
public class PublicationLike
{
    /// <summary>
    /// Unique identifier for this PublicationLike record.
    /// </summary>
    public int PublicationLikeId { get; set; }

    /// <summary>
    /// Foreign key referencing the Publication this like is associated with.
    /// </summary>
    public int PublicationId { get; set; }

    /// <summary>
    /// Navigation property referencing the Publication this like is associated with.
    /// </summary>
    public Publication Publication { get; set; }
    
    /// <summary>
    /// Foreign key referencing the AppUser who liked the publication.
    /// </summary>
    public int AppUserId { get; set; }

    /// <summary>
    /// Navigation property referencing the AppUser who liked the publication.
    /// </summary>
    public AppUser User { get; set; }

    /// <summary>
    /// Date and time the like was recorded.
    /// </summary>
    public DateTime LikedAt { get; set; }

    /// <summary>
    /// Flag indicating whether this like record has been marked as deleted (soft deletion).
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}

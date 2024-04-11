namespace PublishWell.Data.Entities;

/// <summary>
/// Publication like table which will count the likes of the publication.
/// </summary>
public class PublicationLike
{   

    /// <summary>
    /// Publication like id
    /// </summary>
    public int PublicationLikeId { get; set; }

    /// <summary>
    /// Publication id
    /// </summary>
    public int PublicationId { get; set; }

    /// <summary>
    /// Appuser id
    /// </summary>
    public int AppUserId { get; set; }
    
    /// <summary>
    /// Publication like date
    /// </summary>
    public DateTime PublicationLikeDate { get; set; }

}
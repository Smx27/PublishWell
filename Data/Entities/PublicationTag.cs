namespace PublishWell.Data.Entities;

/// <summary>
/// This class represents a tag associated with a publication.
/// Tags are used for keyword-based searching and categorization.
/// </summary>
public class PublicationTag
{
    /// <summary>
    /// Unique identifier for this PublicationTag record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Text of the tag (keyword).
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Foreign key referencing the Publication this tag is associated with.
    /// </summary>
    public int PublicationId { get; set; }

    /// <summary>
    /// Navigation property referencing the Publication this tag is associated with.
    /// </summary>
    public Publication Publication { get; set; }
}

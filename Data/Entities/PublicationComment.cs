using JPS.Data.Entities;

namespace PublishWell.Data.Entities;

/// <summary>
/// This is the table which will contains the publications comments for each publication.
/// </summary>
public class PublicationComment
{

    /// <summary>
    /// The primary key of the table.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The publication ID.
    /// </summary>
    public int PublicationID { get; set; }

    /// <summary>
    /// Navigation property to the publication.
    /// </summary>
    public Publication Publication { get; set; }

    /// <summary>
    /// The comment text.
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// The date and time the comment was created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// The date and time the comment was last updated.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Indicates whether the comment has been edited.
    /// </summary>
    public bool IsEdited { get; set; }

    /// <summary>
    /// The user ID of the user who created the comment.
    /// </summary>
    public int UserID { get; set; }

    /// <summary>
    /// Navigation property to the user who created the comment.
    /// </summary>
    public AppUser User { get; set; }
}
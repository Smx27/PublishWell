namespace PublishWell.Controllers.Publications.DTO;

/// <summary>
/// Data transfar object publication comment.
/// </summary>
public class PublicationCommentDTO
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
    public DateTime LastUpdate { get; set; } = DateTime.Now;

    /// <summary>
    /// Indicates whether the comment has been edited.
    /// </summary>
    public bool IsEdited { get; set; }

    /// <summary>
    /// The user ID of the user who created the comment.
    /// </summary>
    public int UserID { get; set; }

    /// <summary>
    /// User name of the commented user.
    /// </summary>
    public string UserName { get; set; }
}
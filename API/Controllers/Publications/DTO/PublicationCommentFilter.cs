using System.ComponentModel.DataAnnotations;
using PublishWell.API.Common.Helper;
using PublishWell.API.Data.Enums;

namespace PublishWell.API.Controllers.Publications.DTO;
/// <summary>
/// Filter for publication comments.
/// </summary>
public class PublicationCommentFilter : PaginationParams
{
    /// <summary>
    /// Publication id of the publication to filter comments for.
    /// </summary>
    [Required]
    public int PublicationID { get; set; }

    /// <summary>
    /// Filter by added to get status.
    /// </summary>
    public FilterByEnum FilterBy { get; set; } 
}
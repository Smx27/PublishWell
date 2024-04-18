using PublishWell.Common.Helper;
using PublishWell.Data.Enums;

namespace PublishWell.Controllers.Publications.DTO;

/// <summary>
/// Publications filter to get publications.
/// </summary>
public class PublicationsFilter : PaginationParams
{
    /// <summary>
    /// Filter by Publication status Published/Draft/Deleted etc
    /// </summary>
    public PublicationStatus? Status { get; set; }

    /// <summary>
    /// Author ID / User id which will be sue to identify the user who created the publication.
    /// </summary>
    public int AuthorID { get; set; }
    /// <summary>
    /// Author name / User name which will be sue to identify the user who created the publication.
    /// </summary>
    public string AuthorName { get; set; }

    /// <summary>
    /// To fetch the Publication from StartDate to end date.
    /// </summary>
    public DateTime? StartDate { get; set; }
    /// <summary>
    /// To fetch the Publication from StartDate to end date.
    /// </summary>
    public DateTime? EndDate { get; set; }
    /// <summary>
    /// To fetch the data by filter by enums
    /// </summary>
    public FilterByEnum FilterBy { get; set; }
    /// <summary>
    /// TODO: To be implimented{Filter by categories}
    /// </summary>
    public string PublicationCategorieName { get; set; }
}
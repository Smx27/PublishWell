using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PublishWell.API.Controllers.Publications.DTO;

/// <summary>
/// data transfar object for uploading files
/// </summary>
public class UploadFileDTO
{
    /// <summary>
    /// List of ifromfiles which will be uploaded onto the server 
    /// </summary>
    [Required(ErrorMessage = "Atlease one file is required")]
    public List<IFormFile> Files { get; set; }


    /// <summary>
    /// Id of the publlication where the data will be saved 
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Publication ID is required")]
    [DefaultValue(1)]
    public int PublicationId { get; set; }
}
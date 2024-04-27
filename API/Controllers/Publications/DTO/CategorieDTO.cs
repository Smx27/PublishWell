using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PublishWell.API.Controllers.Publications.DTO;

/// <summary>
/// categories data transfar object to handle operations
/// </summary>
public class CategorieDTO
{
    /// <summary>
    /// Unique identifier for this Categorie record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the category.
    /// </summary>
    [Required(ErrorMessage = "Categorie name is required")]
    [DefaultValue("Test")]
    public string Name { get; set; }

    /// <summary>
    /// Description of the category (optional).
    /// </summary>
    [DefaultValue("This is a test description")]
    public string Description { get; set; }

    /// <summary>
    /// Url slug for the category (optional).
    /// This can be used for SEO-friendly URLs.
    /// </summary>
    [DefaultValue("https//loalhost:5000/some/url/totest")]
    public string Slug { get; set; }    
}
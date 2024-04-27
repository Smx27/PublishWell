namespace PublishWell.API.Data.Entities;

/// <summary>
/// This class represents a category for publications.
/// </summary>
public class Categorie
{
    /// <summary>
    /// Unique identifier for this Categorie record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the category.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Description of the category (optional).
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Url slug for the category (optional).
    /// This can be used for SEO-friendly URLs.
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    /// A collection of publications associated with this category.
    /// </summary>
    public ICollection<PublicationCategorie> Publications { get; set; }
}

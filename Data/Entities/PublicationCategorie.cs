namespace PublishWell.Data.Entities;

/// <summary>
/// This class represents the relationship between a Publication and a Categorie.
/// It stores the ID of both the Publication and the Categorie, 
/// as well as additional information about the creation and update process.
/// </summary>
public class PublicationCategorie
{
    /// <summary>
    /// Unique identifier for this PublicationCategorie record.
    /// </summary>
    public int PublicationCategorieId { get; set; }

    /// <summary>
    /// Foreign key referencing the Publication table.
    /// </summary>
    public int PublicationId { get; set; }

    /// <summary>
    /// Foreign key referencing the Categorie table.
    /// </summary>
    public int CategorieId { get; set; }

    /// <summary>
    /// Navigation property referencing the Publication this record is associated with.
    /// </summary>
    public Publication Publication { get; set; }

    /// <summary>
    /// Navigation property referencing the Categorie this record is associated with.
    /// </summary>
    public Categorie Categorie { get; set; }

    /// <summary>
    /// Date and time this record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time this record was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Flag indicating whether this record has been marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Username of the user who created this record.
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// Username of the user who last updated this record.
    /// </summary>
    public string UpdatedBy { get; set; }

}

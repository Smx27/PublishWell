using PublishWell.Common.Helper;
using PublishWell.Controllers.Publications.DTO;
using PublishWell.Data.Entities;

namespace PublishWell.Interfaces;

/// <summary>
/// Interface for the Publications repository.
/// This interface defines methods for CRUD (Create, Read, Update, Delete) operations 
/// on Publication entities, as well as filtering and comments management.
/// </summary>
public interface IPublicationsRepository
{
    /// <summary>
    /// Gets a Publication by its ID.
    /// 
    /// This method retrieves a specific Publication from the data store based on its ID.
    /// 
    /// Parameters:
    ///   - id: The ID of the publication to retrieve.
    ///   - viewerID: The ID of the user who is requesting the publication (optional).
    /// 
    /// Returns:
    ///   A Task that resolves to a Publication object if found, or null if not found.
    /// </summary>
    Task<Publication> GetById(int id, int viewerID);

    /// <summary>
    /// Gets all publications.
    /// </summary>
    /// <returns>A collection of Publication objects.</returns>
    Task<IEnumerable<PublicationDTO>> GetAll();

    /// <summary>
    /// Adds a new Publication.
    /// 
    /// This method adds a new Publication to the data store.
    /// 
    /// Parameters:
    ///   - publication: The Publication object to add.
    /// 
    /// Returns:
    ///   A Task that resolves to a PublicationDTO object representing the newly added publication.
    /// </summary>
    Task AddAsync(PublicationDTO publication);

    /// <summary>
    /// Updates an existing Publication.
    /// 
    /// This method updates an existing Publication in the data store.
    /// 
    /// Parameters:
    ///   - publication: The Publication object with updated information.
    /// 
    /// Returns:
    ///   A Task that completes the update operation asynchronously.
    /// </summary>
    Task UpdateAsync(Publication publication);

    /// <summary>
    /// Deletes a Publication.
    /// 
    /// This method deletes a Publication from the data store.
    /// 
    /// Parameters:
    ///   - id: The ID of the publication to delete.
    /// 
    /// Returns:
    ///   A Task that completes the delete operation asynchronously.
    /// </summary>
    Task DeleteAsync(int id);


    /// <summary>
    /// Gets all publications by filter params.
    /// 
    /// This method retrieves publications based on provided filter criteria.
    /// 
    /// Parameters:
    ///   - filter: A PublicationsFilter object containing filtering options (optional).
    /// 
    /// Returns:
    ///   A Task that resolves to a PagedList PublicationDTO object containing the filtered publications.
    /// </summary>
    Task<PagedList<PublicationDTO>> GetAllByFilter(PublicationsFilter filter);

    /// <summary>
    /// Adds a comment to a publication.
    /// 
    /// This method adds a new comment to a specific publication.
    /// 
    /// Parameters:
    ///   - publicationId: The ID of the publication to add the comment to.
    ///   - viewerId: The ID of the user who is adding the comment.
    ///   - comment: The text content of the comment.
    /// 
    /// Returns:
    ///   A Task that completes the comment addition operation asynchronously.
    /// </summary>
    Task AddCommentAsync(int publicationId, int viewerId, string comment);
    /// <summary>
    /// Gets all comments for a publication.
    /// 
    /// This method retrieves all comments associated with a specific publication.
    /// 
    /// Parameters:
    ///   - publicationId: The ID of the publication to get comments for.
    /// 
    /// Returns:
    ///   A Task that resolves to a collection of PublicationComment objects representing the comments.
    /// </summary>
    Task<PagedList<PublicationCommentDTO>> GetCommentsAsync(PublicationCommentFilter filter);
    /// <summary>
    /// Deletes a comment from a publication.
    /// 
    /// This method removes a specific comment associated with a publication.
    /// 
    /// Parameters:
    ///   - publicationId: The ID of the publication that contains the comment.
    ///   - commentId: The unique identifier of the comment to be deleted.
    /// 
    /// Returns:
    ///   A Task that completes the deletion operation asynchronously. Upon successful deletion,
    ///   an empty Task is typically returned. If the comment is not found or cannot be deleted,
    ///   an exception may be thrown.
    /// </summary>
    Task DeleteCommentAsync(int publicationId, int commentId);
    /// <summary>
    /// A user can like a publication.
    /// 
    /// This method removes a specific comment associated with a publication.
    /// 
    /// Parameters:
    ///   - publicationId: The ID of the publication that contains the comment.
    ///   - viewerId: The unique identifier of the viewer to be deleted.
    /// 
    /// Returns:
    ///   A Task that completes the deletion operation asynchronously. Upon successful deletion,
    ///   an empty Task is typically returned. If the comment is not found or cannot be deleted,
    ///   an exception may be thrown.
    /// </summary>
    Task AddLikeAsync(int publicationId, int viewerId);
    /// <summary>
    /// Create a cattegorie whih will be used for publication.
    /// 
    /// This method removes a specific comment associated with a publication.
    /// 
    /// Parameters:
    ///   - dto: Categorie datatransfar object.
    ///   
    /// Returns:
    ///   categorie object which is just created.
    /// </summary>
    Task AddCategorie(CategorieDTO dto);

    /// <summary>
    /// Gets a Categorie by its ID.
    /// 
    /// This method retrieves a specific Categorie object from the data store based on its ID.
    /// 
    /// Parameters:
    ///   - id: The ID of the categorie to retrieve.
    /// 
    /// Returns:
    ///   A Task that resolves to a CategorieDTO object if found, or null if not found.
    /// </summary>
    Task<CategorieDTO> GetCategorieByID(int id);

    /// <summary>
    /// Deletes a Categorie.
    /// 
    /// This method removes a Categorie from the data store.
    /// 
    /// Parameters:
    ///   - id: The ID of the categorie to delete.
    /// 
    /// Returns:
    ///   A Task that completes the delete operation asynchronously. Upon successful deletion,
    ///   an empty Task is typically returned. If the categorie is not found or cannot be deleted,
    ///   an exception may be thrown.
    /// </summary>
    Task DeleteCategorieAsync(int id);

    /// <summary>
    /// Updates an existing Categorie.
    /// 
    /// This method updates an existing Categorie in the data store.
    /// 
    /// Parameters:
    ///   - categorie: The CategorieDTO object with updated information.
    /// 
    /// Returns:
    ///   A Task that completes the update operation asynchronously. Upon successful update,
    ///   a Task containing the updated CategorieDTO object is typically returned. If the categorie
    ///   cannot be updated, an exception may be thrown.
    /// </summary>
    Task UpdateCategorieAsync(CategorieDTO categorie);
    /// <summary>
    /// Gets all Categorie objects.
    /// 
    /// This method retrieves all Categorie entities currently stored in the data store.
    /// 
    /// Returns:
    ///   A Task that resolves to a collection of CategorieDTO objects representing all categories.
    /// </summary>
    Task<IEnumerable<CategorieDTO>> GetAllCategorie();

    /// <summary>
    /// Checks if a Categorie exists in the data store based on its ID.
    /// 
    /// This method uses LINQ to check if any Categorie entity exists in the Categories table
    /// where the ID matches the provided value. It returns true if a Categorie is found,
    /// otherwise false.
    /// </summary>
    /// <param name="id">The ID of the Categorie to check for existence.</param>
    /// <returns>
    ///   An asynchronous Task that resolves to a boolean value indicating whether a Categorie exists.
    /// </returns>
    Task<bool> IsCategorieExists(int id);
}
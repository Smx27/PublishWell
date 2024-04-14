using PublishWell.Controllers.Mail.DTO;
using PublishWell.Data.Entities;

namespace PublishWell.Interfaces;

/// <summary>
/// Interface for a repository that provides access to Mail Templates.
/// This interface defines methods for CRUD (Create, Read, Update, Delete) operations
/// on Mail Templates.
/// </summary>
public interface IMailRepository
{
    /// <summary>
    /// Retrieves a Mail Template by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Mail Template to retrieve.</param>
    /// <returns>An containing either the retrieved MailDTO object (on success) 
    /// or an error message (on failure).</returns>
    Task<MailDTO> GetMailTemplateByIDAsync(int id);

    /// <summary>
    /// Retrieves all available Mail Templates.
    /// </summary>
    /// <returns>An containing a List of MailDTO objects representing all Mail Templates
    /// or an error message (on failure).</returns>
    Task<List<MailDTO>> GetMailTemplatesAsync();

    /// <summary>
    /// Retrieves Mail Templates based on a specific Type ID.
    /// (Consider adding additional details about the type if applicable)
    /// </summary>
    /// <returns>An containing a List of MailDTO objects representing Mail Templates
    /// of the specified type or an error message (on failure).</returns>
    Task<List<MailDTO>> GetMailTemplatesByTypeIDAsync(int TemplateTypeID);

    /// <summary>
    /// Creates a new Mail TemplateType.
    /// </summary>
    /// <param name="mail">The MailDTO object representing the new Mail Template to create.</param>
    /// <returns>An containing the newly created MailDTO object (on success) 
    /// or an error message (on failure).</returns>
    Task<TemplateTypeDTO> CreateMailTemplateTypeAsync(TemplateTypeDTO mail);
    
    /// <summary>
    /// Creates a new Mail Template.
    /// </summary>
    /// <param name="mail">The MailDTO object representing the new Mail Template to create.</param>
    /// <returns>An containing the newly created MailDTO object (on success) 
    /// or an error message (on failure).</returns>
    Task<MailDTO> CreateMailTemplateAsync(MailDTO mail);


    /// <summary>
    /// get all mail template type
    /// </summary>
    /// <returns>all mail templated type</returns>
    Task<List<TemplateTypeDTO>> GetAllMailTemplateTypeAsync();
    
    /// <summary>
    /// Updates an existing Mail Template.
    /// </summary>
    /// <param name="mail">The MailDTO object containing the updated information for the Mail Template.</param>
    /// <returns>An containing the updated MailDTO object (on success) 
    /// or an error message (on failure).</returns>
    Task<MailDTO> UpdateMailTemplateAsync(MailDTO mail);

    /// <summary>
    /// Deletes a Mail Template by its unique identifier.
    /// </summary>  
    /// <param name="id">The unique identifier of the Mail Template to delete.</param>
    /// <returns>An indicating success or failure of the deletion operation.</returns>
    Task<bool> DeleteMailTemplateAsync(int id);
}

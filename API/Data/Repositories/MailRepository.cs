using AutoMapper;
using AutoMapper.QueryableExtensions;
using JPS.Common;
using JPS.Data;
using Microsoft.EntityFrameworkCore;
using PublishWell.API.Controllers.Mail.DTO;
using PublishWell.API.Data.Entities;
using PublishWell.API.Interfaces;

namespace PublishWell.API.Data.Repositories;

/// <summary>
/// Concrete implementation of the IMailRepository interface.
/// This class provides data access logic for Mail Templates using (likely) a specific data storage mechanism 
/// (e.g., database, file system).
/// </summary>
public class MailRepository : IMailRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor for dependencie injections.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public MailRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Implement methods to perform CRUD (Create, Read, Update, Delete) operations on Mail Templates
    /// using the chosen data storage mechanism.
    /// </summary>
    /// <param name="mail"></param>
    /// <returns>MailDTO</returns>
    public async Task<MailDTO> CreateMailTemplateAsync(MailDTO mail)
    {
        var mailTemplate = _mapper.Map<MailTemplate>(mail);

        MailTemplateType type = null;
        // Checking if we have template type id
        if (mail.TemplateTypeID > 0)
            type = await _context.MailTemplateTypes.FindAsync(mail.TemplateTypeID);

        // Checking if we have template type name
        else if(Utilities.IsNotNullOrEmpty(mail.TemplateTypeName))
            type = await _context.MailTemplateTypes.Where(t=> t.Name.Equals( mail.TemplateTypeName)).FirstOrDefaultAsync();

        // Checking if type is not null 
        if (type != null)
            mailTemplate.Type = type;
        else
            return new MailDTO();
        mailTemplate.CreatedAt = DateTime.Now;
        mailTemplate.UpdatedAt = DateTime.Now;
        mailTemplate.IsDeleted = false;
        mailTemplate.IsDefault = await IsFirstMailTemplateInType(mail.TemplateTypeID);
        await _context.MailTemplates.AddAsync(mailTemplate);
        return _mapper.Map<MailDTO>(mailTemplate);

    }
    private async Task<bool> IsFirstMailTemplateInType(int typeID)
    {
        int count = await _context.MailTemplates.CountAsync(t => t.Type.Id == typeID && !t.IsDeleted);
        return count > 1 ? false : true;
    }
    /// <summary>
    /// Deletes a Mail Template by its unique identifier.
    /// (Implementation details will depend on the chosen data storage mechanism)
    /// </summary>
    /// <param name="id">The unique identifier of the Mail Template to delete.</param>
    /// <returns>An  indicating success or failure of the deletion operation.</returns>
    public async Task<bool> DeleteMailTemplateAsync(int id)
    {
        var mail = await _context.MailTemplates.FindAsync(id);
        if (mail != null)
        {
            mail.IsDeleted = true;
            mail.UpdatedAt = DateTime.Now;
            // await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Retrieves a Mail Template by its unique identifier.
    /// (Implementation details will depend on the chosen data storage mechanism)
    /// </summary>
    /// <param name="id">The unique identifier of the Mail Template to retrieve.</param>
    /// <returns>An  containing either the retrieved MailDTO object (on success) 
    /// or an error message (on failure).</returns>
    public async Task<MailDTO> GetMailTemplateByIDAsync(int id)
    {
        var mail = await _context.MailTemplates
        .Include(t=> t.Type)
        .Where(t=> t.Id == id)
        .FirstOrDefaultAsync();

        if(mail != null)
        {   
            return _mapper.Map<MailDTO>(mail);
        }

        return new MailDTO();
    }

    /// <summary>
    /// Retrieves all available Mail Templates.
    /// (Implementation details will depend on the chosen data storage mechanism)
    /// </summary>
    /// <returns>An  containing a List of MailDTO objects representing all Mail Templates
    /// or an error message (on failure).</returns>
    public async Task<List<MailDTO>> GetMailTemplatesAsync()
    {
        var mails = await _context.MailTemplates
        .Where(m=> !m.IsDeleted)
        .ProjectTo<MailDTO>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return mails;
    }

    /// <summary>
    /// Retrieves Mail Templates based on a specific Type ID.
    /// (Implementation details will depend on the chosen data storage mechanism)
    /// Consider adding comments explaining the "type" concept for Mail Templates if applicable.
    /// </summary>
    /// <returns>An  containing a List of MailDTO objects representing Mail Templates
    /// of the specified type or an error message (on failure).</returns>
    public async Task<List<MailDTO>> GetMailTemplatesByTypeIDAsync(int TemplateTypeID)
    {
        var mailsQuery = _context.MailTemplates.AsQueryable();
        if (TemplateTypeID > 0)
        {
            mailsQuery = mailsQuery.Where(m => m.Type.Id == TemplateTypeID);
        }
        var mails = await mailsQuery
            .Where(m => !m.IsDeleted)
            .OrderByDescending(m => m.Id)
            .ProjectTo<MailDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return mails;
    }

    /// <summary>
    /// Updates an existing Mail Template.
    /// (Implementation details will depend on the chosen data storage mechanism)
    /// </summary>
    /// <param name="mail">The MailDTO object containing the updated information for the Mail Template.</param>
    /// <returns>An  containing the updated MailDTO object (on success) 
    /// or an error message (on failure).</returns>
    public async Task<MailDTO> UpdateMailTemplateAsync(MailDTO mail)
    {
        var mailTemplate = await _context.MailTemplates.FindAsync(mail.TemplateID);
        _mapper.Map(mail, mailTemplate);
        mailTemplate.UpdatedAt = DateTime.Now;
        // mailTemplate.UpdatedBy = 
        _context.Entry(mail).State = EntityState.Modified;
        // await _context.SaveChangesAsync();
        return mail;
    }

    /// <summary>
    /// Create mail template type
    /// </summary>
    /// <param name="mail"></param>
    /// <returns>Created mail templated type</returns>
    public async Task<TemplateTypeDTO> CreateMailTemplateTypeAsync(TemplateTypeDTO mail)
    {
        if(mail == null) return new TemplateTypeDTO();

        var mailTemplateType = _mapper.Map<MailTemplateType>(mail);

        await _context.MailTemplateTypes.AddAsync(mailTemplateType);
        // await _context.SaveChangesAsync();  
        return await _context.MailTemplateTypes
        .Where(t=> t.Name == mail.Name)
        .ProjectTo<TemplateTypeDTO>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();
    }


    /// <summary>
    /// get all mail template type
    /// </summary>
    /// <returns>all mail templated type</returns>
    public async Task<List<TemplateTypeDTO>> GetAllMailTemplateTypeAsync()
    {
        return await _context.MailTemplateTypes
        .ProjectTo<TemplateTypeDTO>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }


}

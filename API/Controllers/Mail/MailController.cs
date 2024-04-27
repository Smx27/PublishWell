using JPS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishWell.API.Controllers.Mail.DTO;
using PublishWell.API.Extension;
using PublishWell.API.Interfaces;

namespace PublishWell.API.Controllers.Mail;
/// <summary>
/// Mail controller to handle mail related operations and templating.
/// </summary>
[Authorize]
public class MailController : BaseAPIController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MailController"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="emailService"></param>
    public MailController(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    /// <summary>
    /// Creates a new mail template in the database.
    /// </summary>
    /// <param name="mailDTO">The DTO containing information for the new mail template.</param>
    /// <returns>An `ActionResult` containing the created MailDTO on success, or BadRequest if model validation fails.</returns>
    [HttpPost]
    public async Task<ActionResult<MailDTO>> CreateMailTemplate(MailDTO mailDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        mailDTO.CreatedBy = User.getUserName();

        var mailTemplate = await _unitOfWork.mailRepository.CreateMailTemplateAsync(mailDTO);
        if(_unitOfWork.HasChanges()) await _unitOfWork.Complete();
        return Ok(mailTemplate);
    }

    /// <summary>
    /// Get mail template by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns> An `ActionResult` containing the mail template on success, or NotFound if the mail template is not found.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<MailDTO>> GetMailTemplateById(int id)
    {
        var mailTemplate = await _unitOfWork.mailRepository.GetMailTemplateByIDAsync(id);

        if (mailTemplate == null)
        {
            return NotFound();
        }

        return mailTemplate;
    }

    /// <summary>
    /// Get all mail templates.
    /// </summary>
    /// <returns> An `ActionResult` containing a list of mail templates on success.</returns>
    [HttpGet]
    public async Task<ActionResult<List<MailDTO>>> GetMailTemplates()
    {
        var mailTemplates = await _unitOfWork.mailRepository.GetMailTemplatesAsync();

        return mailTemplates;
    }

    /// <summary>
    /// Update mail template.
    /// </summary>
    /// <param name="mailDTO"></param>
    /// <returns> An `IActionResult` on success, or BadRequest if model validation fails.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateMailTemplate([FromBody]MailDTO mailDTO)
    {
        if (mailDTO.TemplateID > 0 || mailDTO == null)
        {
            return BadRequest("Failed To Fetch Template ID.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingMailTemplate = await _unitOfWork.mailRepository.GetMailTemplateByIDAsync(mailDTO.TemplateID);
        if (existingMailTemplate == null)
        {
            return NotFound("Template Not Found!");
        }
        mailDTO.CreatedBy = User.getUserName();
        await _unitOfWork.mailRepository.UpdateMailTemplateAsync(mailDTO);
        if(_unitOfWork.HasChanges()) await _unitOfWork.Complete();

        return Ok("Mail template updated successfully");
    }

    /// <summary>
    /// Delete mail template.
    /// </summary>
    /// <param name="id"></param>
    /// <returns> An `IActionResult` on success, or NotFound if the mail template is not found.</returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMailTemplate(int id)
    {
        var deleted = await _unitOfWork.mailRepository.DeleteMailTemplateAsync(id);

        if (!deleted)
        {
            return NotFound();
        }
        if(_unitOfWork.HasChanges()) await _unitOfWork.Complete();
        return Ok("Mail template deleted successfully");
    }

    /// <summary>
    /// Get list of templates within the specified type ID.
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns> List of templates within the specified type ID.</returns>
    [HttpGet("types/{typeId}")]
    public async Task<ActionResult<List<MailDTO>>> GetMailTemplatesByTypeId(int typeId)
    {
        var mailTemplates = await _unitOfWork.mailRepository.GetMailTemplatesByTypeIDAsync(typeId);

        return mailTemplates;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sendMailDTO"></param>
    /// <returns></returns>
    [HttpPost("test-mail")]
    public async Task<IActionResult> SendTestMail([FromBody] SendMailDTO<object> sendMailDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _emailService.TestMailAsync(sendMailDTO);
            return Ok("Test email sent successfully!");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Create a new mail template type.
    /// </summary>
    /// <param name="mail"></param>
    /// <returns> Created template type on success, or BadRequest if model validation fails.</returns>
    [HttpPost("createtype")]
    public async Task<IActionResult> CreateTemplateType(TemplateTypeDTO mail)
    {
        if(mail == null)
            return BadRequest("Invalid Request");
        // if(Utilities.IsNotNullOrEmpty(mail.Name))
        //     return BadRequest("Name is required");
            
        var type = await _unitOfWork.mailRepository.CreateMailTemplateTypeAsync(mail);
        if(_unitOfWork.HasChanges()) await _unitOfWork.Complete();
        return Ok(type);
    }
    /// <summary>
    /// get all a new mail template type.
    /// </summary>
    /// <returns> Created template type on success, or BadRequest if model validation fails.</returns>
    [HttpGet("getalltype")]
    public async Task<IActionResult> GetAllTypes()
    {   
        var types = await _unitOfWork.mailRepository.GetAllMailTemplateTypeAsync();
        return Ok(types);
    }
}
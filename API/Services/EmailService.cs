
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Fluid;
using Microsoft.Extensions.Options;
using PublishWell.API.Common.Helper;
using PublishWell.API.Interfaces;

/// <summary>
/// Email ser vice class which will be used to send and receive emails.
/// </summary>
public class EmailService : IEmailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly SmtpSetup _smtpSetup;

    /// <summary>
    /// Email service constructor.
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="smtpSetup"></param>
    public EmailService(IUnitOfWork unitOfWork, IOptions<SmtpSetup> smtpSetup)
    {
        _unitOfWork = unitOfWork;
        _smtpSetup = smtpSetup.Value;
    }

    /// <summary>
    /// Send mail method.
    /// </summary>
    /// <param name="sendMailDTO"></param>
    /// <returns>A task if the send complete or not</returns>
    public async Task SendMailAsync<T>(SendMailDTO<T> sendMailDTO)
    {
        var parser = new FluidParser();
        var template = await _unitOfWork.mailRepository.GetMailTemplateByIDAsync(sendMailDTO.TemplateID);

        if (parser.TryParse(template.MailTemplateBody, out var fluidTemplate, out var error))
        {
            var context = new TemplateContext(sendMailDTO.Model);
            var body = await fluidTemplate.RenderAsync(context);
            var email = await Email
                .From(sendMailDTO.Form)
                .To(sendMailDTO.To)
                .Subject(sendMailDTO.Subject)
                .UsingTemplate(body, sendMailDTO.Model)
                .SendAsync();
        }
        else
        {
            throw new Exception("Error parsing template: " + error);
        }
    }

    /// <summary>
    /// Test Mail send method to send test mail
    /// </summary>
    /// <param name="sendMailDTO"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task TestMailAsync<T>(SendMailDTO<T> sendMailDTO)
    {
        var parser = new FluidParser();
        var template = await _unitOfWork.mailRepository.GetMailTemplateByIDAsync(sendMailDTO.TemplateID);

        if (parser.TryParse(template.MailTemplateBody, out var fluidTemplate, out var error))
        {
            // var model = JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(sendMailDTO.Model));
            var model = new {
                CompanyName = "Publishwell",
                UserName = "Test",
                Role = "User",
                LoginLink ="https://PublishWell.API.com/login"                
            };
            var context = new TemplateContext(model);
            var body = await fluidTemplate.RenderAsync(context);

            var smtpClient = new SmtpClient(_smtpSetup.Host, _smtpSetup.Port);
            Email.DefaultSender = new SmtpSender(smtpClient);
                var email = Email
                .From(sendMailDTO.Form)
                .To(sendMailDTO.To)
                .Subject(sendMailDTO.Subject)
                .Body(body, true)
                .SendAsync();
        }
        else
        {
            throw new Exception("Error parsing template: " + error);
        }
    }
}

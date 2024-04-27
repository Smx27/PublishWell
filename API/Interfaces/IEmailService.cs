/// <summary>
/// Interface for mail service which will be used to send mail across the application.
/// This interface defines methods for sending and potentially testing emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email using the provided information.
    /// </summary>
    /// <typeparam name="T">The type of the model object included in the email content (if any).</typeparam>
    /// <param name="sendMailDTO">A SendMailDTO object containing details for sending the email, including the template, recipient, subject, sender, and optional model data.</param>
    Task SendMailAsync<T>(SendMailDTO<T> sendMailDTO);

    /// <summary>
    /// Sends a test email using the provided information. 
    /// (Implementation details may vary depending on the email service)
    /// </summary>
    /// <typeparam name="T">The type of the model object included in the email content (if any).</typeparam>
    /// <param name="sendMailDTO">A SendMailDTO object containing details for sending the test email.</param>
    Task TestMailAsync<T>(SendMailDTO<T> sendMailDTO);
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// Data Transfer Object (DTO) used for sending email information.
/// This class provides properties for specifying the mail template, recipient, 
/// subject, sender, and the model data to be included in the email content.
/// </summary>
/// <typeparam name="T">The type of the model object to be included in the email.</typeparam>
public class SendMailDTO<T>
{
    /// <summary>
    /// The unique identifier of the Mail Template to be used for sending the email.
    /// </summary>
    [DefaultValue(5)]
    public int TemplateID { get; set; }

    /// <summary>
    /// The email address of the recipient.
    /// </summary>
    [DefaultValue("test@example.com")]
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string To { get; set; }

    /// <summary>
    /// The subject line of the email.
    /// </summary>
    [DefaultValue("Test Email")]
    [Required(ErrorMessage = "Subject is required.")]
    [StringLength(50, ErrorMessage = "Subject cannot exceed 50 characters.")]
    public string Subject { get; set; }

    /// <summary>
    /// The email address of the sender.
    /// </summary>
    [DefaultValue("test@example.com")]
    [Required(ErrorMessage = "Sender email is required.")]
    [EmailAddress(ErrorMessage = "Invalid sender email address.")]
    public string Form { get; set; }

    /// <summary>
    /// The model object containing the data to be included in the email content.
    /// The type of the model can be any class.
    /// </summary>
    public T Model { get; set; }
}

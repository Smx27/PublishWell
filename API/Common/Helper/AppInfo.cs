namespace PublishWell.API.Common.Helper;

/// <summary>
/// Class representing application information.
/// </summary>
public class AppInfo
{
    /// <summary>
    /// The name of the application.
    /// </summary>
    public string AppName { get; set; }

    /// <summary>
    /// The version of the application.
    /// </summary>
    public string AppVersion { get; set; }

    /// <summary>
    /// The base URL of the application.
    /// </summary>
    public string ApplicationURL { get; set; }

    /// <summary>
    /// Indicates whether the application is using SSL (HTTPS).
    /// </summary>
    public bool UsingSSL { get; set; }
}

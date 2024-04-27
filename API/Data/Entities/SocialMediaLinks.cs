using JPS.Data.Entities;

namespace PublishWell.API.Data.Entities;

/// <summary>
/// Link table for social media accounts.
/// </summary>
public class SocialMediaLinks
{
    /// <summary>
    /// Id of social media link
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Facebook account link
    /// </summary>
    public string Facebook { get; set; }

    /// <summary>
    /// Twitter account link
    /// </summary>
    public string Twitter { get; set; }
    
    /// <summary>
    /// LinkedIn account link
    /// </summary>
    public string LinkedIn { get; set; }

    /// <summary>
    /// GitHub account link
    /// </summary>
    public string GitHub { get; set; }


    /// <summary>
    /// Instagram account link
    /// </summary>
    public string Instagram { get; set; }
    
    /// <summary>
    /// Pinterest account link
    /// </summary>
    public string Pinterest { get; set; }

    /// <summary>
    /// Id of appuser
    /// </summary>
    public int AppUserId { get; set; }
    /// <summary>
    /// Relationship with appuser
    /// </summary>
    /// <value></value>
    public AppUser User { get; set; }
}
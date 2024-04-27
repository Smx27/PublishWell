namespace PublishWell.API.Data.Enums;

/// <summary>
/// Enumeration representing options for filtering publications.
/// </summary>
public enum FilterByEnum
{
    /// <summary>
    /// Filter publications by newest publish date.
    /// </summary>
    Newest,

    /// <summary>
    /// Filter publications by oldest publish date.
    /// </summary>
    Oldest,

    /// <summary>
    /// Filter publications by most popular (e.g., most views, likes, etc.).
    /// </summary>
    MostPopular,

    /// <summary>
    /// Filter publications by least popular (e.g., fewest views, likes, etc.).
    /// </summary>
    LeastPopular,
}

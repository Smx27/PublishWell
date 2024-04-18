namespace PublishWell.Data.Enums
{
    /// <summary>
    /// This is a enum class to define the publication status.  
    /// </summary>
    public enum PublicationStatus
    {
        /// <summary>
        /// The publication is active and can be seen by the public.
        /// </summary>
        Active,
        /// <summary>
        /// The publication is inactive and cannot be seen by the public.
        /// </summary>
        InActive,
        /// <summary>
        /// The publication is draft and cannot be seen by the public but can be viewed by the author.
        /// </summary>
        Draft,
        /// <summary>
        /// The publication is deleted.
        /// </summary>
        Deleted
    }
}
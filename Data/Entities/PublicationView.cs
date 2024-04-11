using JPS.Data.Entities;

namespace PublishWell.Data.Entities
{
    /// <summary>
    /// Publication views table which will count the number of views for each publication have.
    /// </summary>
    public class PublicationView
    {
        /// <summary>
        /// Unique identifier for the publication view.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier for the publication.
        /// </summary>
        public int PublicationId { get; set; }

        /// <summary>
        /// Number of views for the publication.
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// Date and time of the last view.
        /// </summary>
        public DateTime LastViewDate { get; set; }

        /// <summary>
        /// Navigation property to the publication.
        /// </summary>
        public Publication Publication { get; set; }

        /// <summary>
        /// Navigation property to the users who have viewed the publication.
        /// </summary>
        public ICollection<AppUser> Viewers { get; set; }
    }
}
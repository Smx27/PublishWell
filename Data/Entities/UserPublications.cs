using JPS.Data.Entities;

namespace PublishWell.Data.Entities
{
    /// <summary>
    /// User Publications Relationship table
    /// </summary>
    public class UserPublications
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// App User
        /// </summary>
        public AppUser User { get; set; }

        /// <summary>
        /// Collection of publications which the user published
        /// </summary>
        /// <value></value>
        public ICollection<Publication> Publications { get; set; }
    
    }
}
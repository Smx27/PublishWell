using System.Security.Claims;

namespace PublishWell.Extension
{
    /// <summary>
    /// Claims principle extended class to add some more custom methods
    /// </summary>
    public static class ClaimsPrincipleExtension
    {
        /// <summary>
        /// Method to get User name from claims principle 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>String username if found else null</returns>
        public static string getUserName(this ClaimsPrincipal user){
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }


        /// <summary>
        /// Method to get UserID from claims principle 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>String userid if found else null</returns>
        public static int getID(this ClaimsPrincipal user){
            return int.Parse( user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }   
    }
}
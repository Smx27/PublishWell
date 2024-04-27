namespace JPS.Common
{
    /// <summary>
    /// This is a utility class to handle global common functions
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Extension method for string to check i the string not null
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Bool: if the string not null</returns>
        public static bool IsNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Extension method for IFormFile to check if the file type is allowed
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Bool: if the file type is allowed</returns>
        public static bool IsAllowedContentType(this IFormFile file)
        {
            var allowedExtensions = new List<string> { ".pdf", ".docx", ".xlsx", ".csv" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }
    }
}
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
    }
}
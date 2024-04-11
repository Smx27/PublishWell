namespace PublishWell.Data.Entities
{
    /// <summary>
    /// Exception log table wich will log exception into db 
    /// </summary>
    public class ExceptionLog
    {
        /// <summary>
        /// Exception id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Exception Message
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Exception stackTrace
        /// </summary>
        public  string StackTrace { get; set; }
        /// <summary>
        /// Exception source
        /// </summary>
        public  string Source { get; set; }
        /// <summary>
        /// Inner exception
        /// </summary>
        public string InnerException { get; set; }
        /// <summary>
        /// TargetSite
        /// </summary>
        public string TargetSite { get; set; }

        /// <summary>
        /// Exception date
        /// </summary>
        public DateTime TimeStamp { get; set; } = DateTime.Now;
       
    }
}
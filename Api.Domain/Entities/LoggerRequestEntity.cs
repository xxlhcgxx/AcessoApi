namespace Api.Domain.Entities
{
    public class LoggerRequestEntity
    {
        public string ApplicationName { get; set; }
        public string Message { get; set; }
        public string InnerMessage { get; set; }
        public string Stacktrace { get; set; }
    }
}

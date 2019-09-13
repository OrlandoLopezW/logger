namespace JobLogger.LoggerApi.Infrastructure.Utility.Constants
{

    public class NotificationMessageType
    {
        public const string FORMFIELDS = "1";
        public const string BUSINESSLOGIC = "2";
        public const string INTERNALERROR = "3";
    }

    public class JsonErrorResponse
    {
        public string[] Messages { get; set; }
        public string MessageType { get; set; }
        public object DeveloperMessage { get; set; }
    }

    public class TypeMessagesConstants
    {
        public const string MESSAGE = "MESSAGE";
        public const string WARNING = "WARNING";
        public const string ERROR = "ERROR";
    }

}

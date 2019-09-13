namespace JobLogger.LoggerApi.Infrastructure.Utility.Constants
{
    public class CreateLogValidationsMessages
    {
        public const string MESSAGE_EMPTY = "The Description can't be empty";
        public const string MESSAGE_MAX_LENGTH = "The Description can't have more than 200 characters";
        public const string INVALID_COMBINATION_TYPES = "One of Description Types have to be True";
        public const string INVALID_COMBINATION_TO_PERSIST = "One of Storage Types have to be True";
    }
}

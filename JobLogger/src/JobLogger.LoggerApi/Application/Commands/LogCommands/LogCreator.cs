using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Application.Commands.LogCommands
{
    public class LogCreatorStrategy
    {
        private readonly ILogCreator _logCreator;

        public LogCreatorStrategy(ILogCreator logCreator)
        {
            _logCreator = logCreator;
        }

        public async Task<bool> Execute(
            bool isMessageType, 
            bool isWarningType, 
            bool isErrorType, 
            string description)
        {
            await _logCreator.Execute(isMessageType, isWarningType, isErrorType, description);
            return true;
        }
    }
}

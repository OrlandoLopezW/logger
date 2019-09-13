using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Application.Commands.LogCommands
{
    public interface ILogCreator
    {
        Task<bool> Execute(bool isMessageType, bool isWarningType, bool isErrorType, string description);
    }
}

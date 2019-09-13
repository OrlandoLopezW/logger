using JobLogger.LoggerApi.Domain.Aggregates.LogAggregate;
using JobLogger.LoggerApi.Infrastructure.Utility.Constants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Application.Commands.LogCommands
{
    public class CreateLogToDatabaseCommandHandler : ILogCreator
    {
        private readonly ILogRepository _logRepository;

        public CreateLogToDatabaseCommandHandler(
            ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<bool> Execute(
            bool isMessageType,
            bool isWarningType,
            bool isErrorType,
            string description)
        {
            try
            {
                if (isMessageType) await _logRepository.CreateLog(TypeMessagesConstants.MESSAGE, description);
                if (isWarningType) await _logRepository.CreateLog(TypeMessagesConstants.WARNING, description);
                if (isErrorType) await _logRepository.CreateLog(TypeMessagesConstants.ERROR, description);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using JobLogger.LoggerApi.Infrastructure.Utility.Constants;
using MediatR;

namespace JobLogger.LoggerApi.ViewModels
{
    public class LogToCreateRequest
        : IRequest<bool>
    {
        public bool IsMessageType { get; set; }
        public bool IsWarningType { get; set; }
        public bool IsErrorType { get; set; }
                    
        public bool IsLogToDatabase { get; set; }
        public bool IsLogToFile { get; set; }
        public bool IsLogToConsole { get; set; }

        public string Description { get; set; }
    }

    public class LogToCreateRequestValidator
        : AbstractValidator<LogToCreateRequest>
    {
        public LogToCreateRequestValidator()
        {
            RuleFor(x => x.Description)
                .Must((a, b) => !string.IsNullOrWhiteSpace(a.Description))
                .WithMessage(CreateLogValidationsMessages.MESSAGE_EMPTY);

            RuleFor(x => x.Description)
                .Must((a, b) => !(a.Description.Length > 200))
                .WithMessage(CreateLogValidationsMessages.MESSAGE_MAX_LENGTH);

            RuleFor(x => x.IsMessageType && x.IsWarningType && x.IsErrorType)
                .Must((a, b) => IsValidCombinationType(a.IsMessageType, a.IsWarningType, a.IsErrorType))
                .WithMessage(CreateLogValidationsMessages.INVALID_COMBINATION_TYPES);

            RuleFor(x => x.IsLogToDatabase && x.IsLogToFile && x.IsLogToConsole)
                .Must((a, b) => IsValidCombinationToPersist(a.IsLogToDatabase, a.IsLogToFile, a.IsLogToConsole))
                .WithMessage(CreateLogValidationsMessages.INVALID_COMBINATION_TO_PERSIST);
        }

        #region Private Methods

        private bool IsValidCombinationType(
            bool IsMessageType,
            bool IsWarningType,
            bool IsErrorType)
        {
            bool isValidCombinationOfType = (IsMessageType || IsWarningType || IsErrorType);
            if (isValidCombinationOfType) return true;
            return false;
        }

        private bool IsValidCombinationToPersist(
           bool IsLogToDatabase,
           bool IsLogToFile,
           bool IsLogToConsole)
        {
            bool isValidCombinationToPersist = (IsLogToDatabase || IsLogToFile || IsLogToConsole);
            if (isValidCombinationToPersist) return true;
            return false;
        }

        #endregion
    }

    public class LogToCreateViewModelHandler
        : IRequestHandler<LogToCreateRequest, bool>
    {
        public async Task<bool> Handle(LogToCreateRequest request, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}

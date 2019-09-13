using System;
using System.Threading;
using System.Threading.Tasks;
using JobLogger.LoggerApi.Infrastructure.Utility.Constants;
using MediatR;

namespace JobLogger.LoggerApi.Application.Commands.LogCommands
{
    public class CreateLogToConsoleCommandHandler : ILogCreator
    {
        public async Task<bool> Execute(
            bool isMessageType, 
            bool isWarningType, 
            bool isErrorType,
            string description)
        {
            try
            {
                if (isMessageType)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string type = TypeMessagesConstants.MESSAGE;
                    await PrintDescription(type, description);
                }

                if (isWarningType)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string type = TypeMessagesConstants.WARNING;
                    await PrintDescription(type, description);
                }

                if (isErrorType)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    string type = TypeMessagesConstants.ERROR;
                    await PrintDescription(type, description);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }


        private async Task PrintDescription(string type, string description)
        {
            DateTime date = DateTime.Now;
            string descriptionToPrint = $"{date.ToString("dd/MM/yyyy HH:mm:ss")} - {type} - {description}";
            Console.WriteLine(descriptionToPrint);
            System.Diagnostics.Debug.WriteLine(descriptionToPrint);
        }
    }
}



using JobLogger.LoggerApi.Application.Commands.LogCommands;
using JobLogger.LoggerApi.Domain.Aggregates.LogAggregate;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Test
{
    public class CreateLogToConsoleCommandHandlerTest
    {
        private CreateLogToConsoleCommandHandler _createLogToConsoleCommandHandler;

        [SetUp]
        public void Setup()
        {
            _createLogToConsoleCommandHandler = new CreateLogToConsoleCommandHandler();
        }

        [Test]
        public async Task TestExecutePrintMessageTypeInConsole()
        {
            bool isMessageType = true;
            bool isWarningType = false;
            bool isErrorType = false;
            string description = "message";

            bool success = await _createLogToConsoleCommandHandler.Execute(isMessageType, isWarningType, isErrorType, description);

            Assert.IsTrue(success);
        }

        [Test]
        public async Task TestExecutePrintWarningTypeInConsole()
        {
            bool isMessageType = false;
            bool isWarningType = true;
            bool isErrorType = false;
            string description = "message";

            bool success = await _createLogToConsoleCommandHandler.Execute(isMessageType, isWarningType, isErrorType, description);

            Assert.IsTrue(success);
        }

        [Test]
        public async Task TestExecutePrintErrorTypeInConsole()
        {
            bool isMessageType = false;
            bool isWarningType = false;
            bool isErrorType = true;
            string description = "message";

            bool success = await _createLogToConsoleCommandHandler.Execute(isMessageType, isWarningType, isErrorType, description);

            Assert.IsTrue(success);
        }

        [Test]
        public async Task TestExecutePrintAllTypesInConsole()
        {
            bool isMessageType = true;
            bool isWarningType = true;
            bool isErrorType = true;
            string description = "message";

            bool success = await _createLogToConsoleCommandHandler.Execute(isMessageType, isWarningType, isErrorType, description);

            Assert.IsTrue(success);
        }

    }
}

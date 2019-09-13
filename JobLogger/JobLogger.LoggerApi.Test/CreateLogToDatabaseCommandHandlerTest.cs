using JobLogger.LoggerApi.Application.Commands.LogCommands;
using JobLogger.LoggerApi.Domain.Aggregates.LogAggregate;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Test
{
    public class CreateLogToDatabaseCommandHandlerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestExecuteRegisterMessageTypeInDatabase()
        {
            bool isMessageType = true;
            bool isWarningType = false;
            bool isErrorType = false;
            string description = "message";

            var logRepository = new Mock<ILogRepository>();
            logRepository.Setup(x => x.CreateLog(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var createLogToDatabaseCommandHandler = new CreateLogToDatabaseCommandHandler(logRepository.Object);
            bool success = await createLogToDatabaseCommandHandler.Execute(isMessageType, isWarningType, isErrorType, description);
            Assert.IsTrue(success);
        }

        [Test]
        public async Task TestExecuteRegisterWarningTypeInDatabase()
        {
            bool isMessageType = false;
            bool isWarningType = true;
            bool isErrorType = false;
            string description = "message";

            var logRepository = new Mock<ILogRepository>();
            logRepository.Setup(x => x.CreateLog(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var createLogToDatabaseCommandHandler = new CreateLogToDatabaseCommandHandler(logRepository.Object);
            bool success = await createLogToDatabaseCommandHandler.Execute(isMessageType, isWarningType, isErrorType, description);
            Assert.IsTrue(success);
        }

        [Test]
        public async Task TestExecuteRegisterErrorTypeInDatabase()
        {
            bool isMessageType = false;
            bool isWarningType = false;
            bool isErrorType = true;
            string description = "message";

            var logRepository = new Mock<ILogRepository>();
            logRepository.Setup(x => x.CreateLog(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var createLogToDatabaseCommandHandler = new CreateLogToDatabaseCommandHandler(logRepository.Object);
            bool success = await createLogToDatabaseCommandHandler.Execute(isMessageType, isWarningType, isErrorType, description);
            Assert.IsTrue(success);
        }

        [Test]
        public async Task TestExecuteRegisterAllTypeInDatabase()
        {
            bool isMessageType = true;
            bool isWarningType = true;
            bool isErrorType = true;
            string description = "message";

            var logRepository = new Mock<ILogRepository>();
            logRepository.Setup(x => x.CreateLog(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var createLogToDatabaseCommandHandler = new CreateLogToDatabaseCommandHandler(logRepository.Object);
            bool success = await createLogToDatabaseCommandHandler.Execute(isMessageType, isWarningType, isErrorType, description);
            Assert.IsTrue(success);
        }

    }
}

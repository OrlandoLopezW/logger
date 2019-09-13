using JobLogger.LoggerApi.Application.Commands.LogCommands;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Test
{
    public class CreateLogToFileCommandHandlerTest
    {
        private CreateLogToFileCommandHandler command;
        //private IHostingEnvironment _environment;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestExecuteCreateFileToRegisterLogs()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.Now.ToString("yyyy-MM-dd")}-Log.txt");
            command = new CreateLogToFileCommandHandler();
            Assert.IsTrue(File.Exists(path));
        }
    }
}

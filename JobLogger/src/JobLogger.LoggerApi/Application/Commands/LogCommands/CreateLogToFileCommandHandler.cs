using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JobLogger.LoggerApi.Infrastructure.Utility.Constants;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace JobLogger.LoggerApi.Application.Commands.LogCommands
{
    public class CreateLogToFileCommandHandler : ILogCreator
    {
        private readonly IHostingEnvironment _environment;
        public string path = "";


        public CreateLogToFileCommandHandler()
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.Now.ToString("yyyy-MM-dd")}-Log.txt");
            CreateFile(path);
        }

        public CreateLogToFileCommandHandler(
            IHostingEnvironment environment)
        {
            _environment = environment;
            path = Path.Combine(_environment.WebRootPath, $"{DateTime.Now.ToString("yyyy-MM-dd")}-Log.txt");
            CreateFile(path);
        }

        public async Task<bool> Execute(
            bool isMessageType,
            bool isWarningType,
            bool isErrorType,
            string description)
        {
            try
            {
                if (isMessageType) await RegisterLineInFile(path, TypeMessagesConstants.MESSAGE, description);
                if (isWarningType) await RegisterLineInFile(path, TypeMessagesConstants.WARNING, description);
                if (isErrorType) await RegisterLineInFile(path, TypeMessagesConstants.ERROR, description);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                }
            }
        }

        private async Task RegisterLineInFile(
            string path,
            string type,
            string description)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - {type} - {description}");
            }
        }
    }
}

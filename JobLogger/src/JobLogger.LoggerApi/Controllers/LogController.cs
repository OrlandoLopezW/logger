using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JobLogger.LoggerApi.Application.Commands.LogCommands;
using JobLogger.LoggerApi.Application.Queries;
using JobLogger.LoggerApi.Domain.Aggregates.LogAggregate;
using JobLogger.LoggerApi.Infrastructure.Utility.Helpers;
using JobLogger.LoggerApi.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace JobLogger.LoggerApi.Controllers
{
    [Route("api/v1/log")]
    public class LogController : Controller
    {
        #region Declarations
        private readonly IMediator _mediator;
        private readonly ILogRepository _logRepository;
        private readonly IHostingEnvironment _environment;
        private readonly ILogQueries _logQueries;
        #endregion


        #region Constructor
        public LogController(
            IMediator mediator,
            ILogRepository logRepository,
            IHostingEnvironment environment,
            ILogQueries logQueries)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
            _logRepository = logRepository ?? throw new System.ArgumentNullException(nameof(logRepository));
            _environment = environment ?? throw new System.ArgumentNullException(nameof(environment));
            _logQueries = logQueries ?? throw new System.ArgumentNullException(nameof(logQueries));
        }

        #endregion


        #region Methods
        [HttpPost]
        [Route("createlog")]
        public async Task<IActionResult> CreateLog(
            [FromBody] LogToCreateRequest model)
        {
            await _mediator.Send(model);

            if (model.IsLogToDatabase)
            {
                var createInDatabase = new LogCreatorStrategy(new CreateLogToDatabaseCommandHandler(_logRepository));
                await createInDatabase.Execute(model.IsMessageType, model.IsWarningType, model.IsErrorType, model.Description);
            }

            if (model.IsLogToFile)
            {
                var createInFile = new LogCreatorStrategy(new CreateLogToFileCommandHandler(_environment));
                await createInFile.Execute(model.IsMessageType, model.IsWarningType, model.IsErrorType, model.Description);
            }

            if (model.IsLogToConsole)
            {
                var createInConsole = new LogCreatorStrategy(new CreateLogToConsoleCommandHandler());
                await createInConsole.Execute(model.IsMessageType, model.IsWarningType, model.IsErrorType, model.Description);
            }

            return Ok(true);
        }


        [HttpGet]
        [Route("listlogsindatabase")]
        public async Task<IActionResult> ListLogsDatabase()
        {
            var logs = await _logQueries.ListLogsInDatabase();
            return Ok(logs);
        }


        [HttpGet]
        [Route("listlogsinfile")]
        public async Task<IActionResult> ListLogsInFile()
        {
            var logsInFile = await _logQueries.ListLogdInFile();
            return Ok(logsInFile);
        }

        #endregion


        


    }
}
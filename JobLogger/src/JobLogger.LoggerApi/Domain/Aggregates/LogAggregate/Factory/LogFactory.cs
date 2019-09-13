using System;
using System.Linq;
using System.Threading.Tasks;
using JobLogger.LoggerApi.Application.Commands.LogCommands;
using JobLogger.LoggerApi.Infrastructure.Utility.Constants;

namespace JobLogger.LoggerApi.Domain.Aggregates.LogAggregate.Factory
{
    public class LogFactory : ILogFactory
    {
        public async Task<Log> CreateLogToCreate(
            string type,
            string description)
        {
            Log log = new Log();
            log.Id = Guid.NewGuid().ToString();
            log.Type = type;
            log.Description = description;
            log.RegistrationDate = DateTime.UtcNow;
            return log;
        }
    }
}

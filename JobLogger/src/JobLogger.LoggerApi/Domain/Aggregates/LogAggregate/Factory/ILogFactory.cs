using JobLogger.LoggerApi.Application.Commands.LogCommands;
using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Domain.Aggregates.LogAggregate.Factory
{
    public interface ILogFactory
    {
        Task<Log> CreateLogToCreate(
            string type,
            string description);
    }
}

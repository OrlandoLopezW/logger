using JobLogger.LoggerApi.Application.Commands.LogCommands;
using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Domain.Aggregates.LogAggregate
{
    public interface ILogRepository
    {
        Task<bool> CreateLog(string type, string description);
    }
}

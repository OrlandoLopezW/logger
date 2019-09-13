using JobLogger.LoggerApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Application.Queries
{
    public interface ILogQueries
    {
        Task<List<LogsDatabaseResponse>> ListLogsInDatabase();
        Task<List<string>> ListLogdInFile();
    }
}

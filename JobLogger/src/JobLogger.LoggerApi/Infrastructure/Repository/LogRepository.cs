using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using JobLogger.LoggerApi.Application.Commands.LogCommands;
using JobLogger.LoggerApi.Domain.Aggregates.LogAggregate;
using JobLogger.LoggerApi.Domain.Aggregates.LogAggregate.Factory;

namespace JobLogger.LoggerApi.Infrastructure.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly DynamoDBContext _context;
        private readonly ILogFactory _logFactory;

        public LogRepository(
            IAmazonDynamoDB dynamoDbClient,
            ILogFactory logFactory)
        {
            _context = new DynamoDBContext(dynamoDbClient);
            _logFactory = logFactory;
        }

        public async Task<bool> CreateLog(string type, string description)
        {
            var logToCreate = await _logFactory.CreateLogToCreate(type, description);
            await _context.SaveAsync(logToCreate);
            return true;
        }
    }
}

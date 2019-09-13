using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using JobLogger.LoggerApi.Domain.Aggregates.LogAggregate;
using JobLogger.LoggerApi.Infrastructure.Utility.Helpers;
using JobLogger.LoggerApi.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JobLogger.LoggerApi.Application.Queries
{
    public class LogQueries : ILogQueries
    {
        private readonly DynamoDBContext _context;
        private readonly IHostingEnvironment _environment;

        public LogQueries(
            IAmazonDynamoDB dynamoDbClient,
            IHostingEnvironment environment)
        {
            _context = new DynamoDBContext(dynamoDbClient);
            _environment = environment;
        }

        public async Task<List<LogsDatabaseResponse>> ListLogsInDatabase()
        {
            List<LogsDatabaseResponse> logsToResponse = new List<LogsDatabaseResponse>();

            try
            {
                List<ScanCondition> conditions = new List<ScanCondition>();
                conditions.Add(new ScanCondition("Active", ScanOperator.Equal, true));

                var config = new DynamoDBOperationConfig
                {
                    IndexName = "DocumentType-index",
                    QueryFilter = conditions,
                    ConditionalOperator = ConditionalOperatorValues.And
                };

                var logsDB = await _context.ScanAsync<Log>(new List<ScanCondition>()).GetRemainingAsync();

                LogsDatabaseResponse logItemToResponse = null;
                for (int i = 0; i < logsDB.Count; i++)
                {
                    logItemToResponse = new LogsDatabaseResponse();
                    logItemToResponse.Type = logsDB[i].Type;
                    logItemToResponse.Description = logsDB[i].Description;
                    logItemToResponse.RegistrationDate = DateHelper.UtcToPeru(logsDB[i].RegistrationDate).ToString("dd/MM/yyyy HH:mm:ss");
                    logsToResponse.Add(logItemToResponse);
                }
            }
            catch (Exception ex)
            {
            }

            return logsToResponse;
        }

        public async Task<List<string>> ListLogdInFile()
        {
            List<string> logInFile = new List<string>();

            try
            {
                var datePeru = DateHelper.UtcToPeru(DateTime.UtcNow);
                string fileName = $"{datePeru.ToString("yyyy-MM-dd")}-Log.txt";
                string path = Path.Combine(_environment.WebRootPath, fileName);
                await ValidateExistLogFile(path);


                using (StreamReader sr = new StreamReader(path))
                {
                    string content = sr.ReadToEnd();
                    var lines = content.Split("\r\n");
                    foreach (var line in lines)
                    {
                        logInFile.Add(line.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return logInFile;
        }

        #region Private Methods

        private async Task ValidateExistLogFile(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                using (StreamWriter sw = System.IO.File.CreateText(path))
                {
                }
            }
        }

        #endregion
    }
}

using Amazon.DynamoDBv2.DataModel;
using System;

namespace JobLogger.LoggerApi.Domain.Aggregates.LogAggregate
{
    [DynamoDBTable("Logger")]
    public class Log
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}

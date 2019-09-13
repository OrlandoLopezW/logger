using System.Collections.Generic;

namespace JobLogger.LoggerApi.Infrastructure.Utility.Constants
{
    public class TypeLogsTemp
    {
        public static List<TypeLogModel> Values = new List<TypeLogModel>()
        {
            new TypeLogModel(){Id = "1", Description = "Message"},
            new TypeLogModel(){Id = "2", Description = "Warning"},
            new TypeLogModel(){Id = "3", Description = "Error"}
        };
    }

    #region models

    public class TypeLogModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }
    #endregion
}

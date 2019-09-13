using System;

namespace JobLogger.LoggerApi.Infrastructure.Utility.Helpers
{
    public class DateHelper
    {
        public static DateTime UtcToPeru(DateTime dateTime)
        {
            return dateTime.AddHours(-5);
        }

        public static DateTime PeruToUtc(DateTime dateTime)
        {
            return dateTime.AddHours(5);
        }
    }
}

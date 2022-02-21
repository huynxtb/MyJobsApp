using System;

namespace MyJobsApp.Helper
{
    public class StringHelper
    {
        public static string UniqueSqlBackupFileName()
        {
            var currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneHelper.TimeZoneGMT7());

            var randomStr = Guid.NewGuid().ToString().Split("-")[0];;
            
            var nameDate = currentTime.ToString()
                .Replace("/", "-")
                .Replace(":", ".");
            
            return $"{nameDate} Backup-{randomStr}.sql";
        }
    }
}
using System;

namespace MyJobsApp.Helper
{
    public class StringHelper
    {
        public static string UniqueSqlBackupFileName(string databaseName)
        {
            var currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneHelper.TimeZoneGMT7());

            var nameDate = currentTime.ToString()
                .Replace("/", "-")
                .Replace(":", ".");

            nameDate = nameDate.Replace(" ", " - ");
            
            return $"{databaseName} - {nameDate} - Backup.sql";
        }
    }
}
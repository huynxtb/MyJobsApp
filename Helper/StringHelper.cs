using System;
using MyJobsApp.Constants;
using MyJobsApp.Models;
using PasswordGenerator;

namespace MyJobsApp.Helper
{
    public class StringHelper
    {
        public static string UniqueSqlBackupFileName()
        {
            var currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneHelper.TimeZoneGMT7());
            
            var pwd = new Password()
                .IncludeLowercase()
                .IncludeNumeric()
                .LengthRequired(4);
            var randomStr = pwd.Next();
            
            var nameDate = currentTime.ToString()
                .Replace("/", "-")
                .Replace(" ", "_")
                .Replace(":", "-");
            
            return $"{nameDate}__{randomStr}__Backup.sql";
        }
    }
}
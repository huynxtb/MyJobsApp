using System;

namespace MyJobsApp.Helper
{
    public static class TimeZoneHelper
    {
        public static TimeZoneInfo TimeZoneGMT7()
        {
            var displayName = "(GMT+7) Asia/Viet Nam";
            var standardName = "VN"; 
            var offset = new TimeSpan(07, 00, 00);
            var vnTimeZone = TimeZoneInfo.CreateCustomTimeZone(standardName, offset, displayName, standardName);

            return vnTimeZone;
        }
    }
}
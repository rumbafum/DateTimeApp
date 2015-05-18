using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DateTimeOffsetApp
{
    public static class Client
    {
        public static int ClientOffset { get; set; }

        public static DateTimeOffset ClientDateTime { 
            get 
            {
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                return TimeZoneInfo.ConvertTime(DateTimeOffset.Now, tzi);
            } 
        }
    }
}
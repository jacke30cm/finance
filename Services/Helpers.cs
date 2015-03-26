using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class DateHelper
    {
     
        public static string DayMonthNameYear(DateTime time)
        {
            return time.ToString("dd MMMM, yyyy");
        }

        public static string TimeCounter(DateTime time)
        {

            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;

            var timeDiff = new TimeSpan(DateTime.Now.Ticks - time.Ticks);
            var delta = Math.Abs(timeDiff.TotalSeconds);

            if (delta < 0)
            {
                return "";
            }
            if (delta < 1 * minute)
            {
                return "alldeles nyss";
            }
            if (delta < 2 * minute)
            {
                return "en minut sedan";
            }
            if (delta < 45 * minute)
            {
                return timeDiff.Minutes + " minuter sedan";
            }
            if (delta < 90 * minute)
            {
                return "en timme sedan";
            }
            if (delta < 24 * hour)
            {
                return timeDiff.Hours + " timmar sedan";
            }
            if (delta < 48 * hour)
            {
                return "igår";
            }
            if (delta < 30 * day)
            {
                return timeDiff.Days + " dagar sedan";
            }
            if (delta < 12 * month)
            {
                var months = Convert.ToInt32(Math.Floor((double)timeDiff.Days / 30));
                return months <= 1 ? "en månad sedan" : months + " månader sedan";
            }
            else
            {
                var years = Convert.ToInt32(Math.Floor((double)timeDiff.Days / 365));
                return years <= 1 ? "ett år sedan" : years + " år sedan";
            }

        }

    }
}

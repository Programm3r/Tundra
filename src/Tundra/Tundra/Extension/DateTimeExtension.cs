using System;
using System.Globalization;

namespace Tundra.Extension
{
    /// <summary>
    /// Date Time Extension Class
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Determines whether the day of week is part of a weekend.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsWeekDay(this DateTime value)
        {
            return !(value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday);
        }

        /// <summary>
        /// Compares the year month day.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="compareTo">The compare to.</param>
        /// <returns></returns>
        public static bool CompareYearMonthDay(this DateTime d, DateTime compareTo)
        {
            return d.Day == compareTo.Day && d.Month == compareTo.Month && d.Year == compareTo.Year;
        }

        /// <summary>
        /// Unix's time stamp to .NET date time.
        /// </summary>
        /// <param name="unixTimeStamp">The Unix time stamp.</param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Java's time stamp to .NET date time.
        /// </summary>
        /// <param name="javaTimeStamp">The java time stamp.</param>
        /// <returns></returns>
        public static DateTime JavaTimeStampToDateTime(this double javaTimeStamp)
        {
            // Java timestamp is milliseconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(Math.Round(javaTimeStamp/1000)).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Dates the time to Unix timestamp.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static double DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            // DateTime to UNIX timestamp
            return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }

        /// <summary>
        /// Gets the week of month.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static int GetWeekOfMonth(this DateTime time)
        {
            var first = new DateTime(time.Year, time.Month, 1);
            return time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
        }

        /// <summary>
        /// Gets the week of year.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static int GetWeekOfYear(this DateTime time)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        /// <summary>
        /// Get the first day of the week based on the following standardISO8601.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="weekOfYear">The week of year.</param>
        /// <returns></returns>
        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            var firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            var firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        /// <summary>
        /// To the name of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        /// <summary>
        /// To the short name of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }

        /// <summary>
        /// Starts the of week.
        /// </summary>
        /// <param name="dt">The date.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <returns></returns>
        /// <example>
        /// DateTime date = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        /// </example>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
    }
}
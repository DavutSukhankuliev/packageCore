using System;

namespace SDTCore
{
    public static class TimeExtension
    {
        /// <summary>
        /// Checks if the <paramref name="currentDate"/> is in the range of [<paramref name="time1"/>,<paramref name="time2"/>).
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <param name="time1">The date which is greater (inclusive) to the <paramref name="currentDate"/>.</param>
        /// <param name="time2">The date which is less (exclusive) to the <paramref name="currentDate"/>.</param>
        /// <returns>Returns bool if the <paramref name="currentDate"/> is in the range of [<paramref name="time1"/>,<paramref name="time2"/>).</returns>
        public static bool IsBetween(this DateTime currentDate, DateTime time1, DateTime time2) 
            => currentDate >= time1 && currentDate < time2;

        /// <summary>
        /// Checks if the <paramref name="sourceTime"/> is less than <paramref name="targetTime"/>.
        /// </summary>
        /// <param name="sourceTime">The time which is going to be compared.</param>
        /// <param name="targetTime">The comparator (exclusive).</param>
        /// <returns>Returns bool if the <paramref name="sourceTime"/> is less than <paramref name="targetTime"/>.</returns>
        public static bool IsEarlierThan(this DateTime sourceTime, DateTime targetTime) 
            => sourceTime < targetTime;

        /// <summary>
        /// Checks if the exactly current time is less than <paramref name="timeUtc"/>.
        /// </summary>
        /// <param name="timeUtc">Time in UTC, comparator (exclusive).</param>
        /// <returns>Returns bool if the exactly current time is less than <paramref name="timeUtc"/>.</returns>
        public static bool IsEarlierThanNow(this DateTime timeUtc) 
            => DateTime.Now.ToUniversalTime() > timeUtc;

        /// <summary>
        /// Checks if the <paramref name="sourceTime"/> is greater than <paramref name="targetTime"/>.
        /// </summary>
        /// <param name="sourceTime">The time which is going to be compared.</param>
        /// <param name="targetTime">The comparator (inclusive).</param>
        /// <returns>Returns bool if the <paramref name="sourceTime"/> is greater than <paramref name="targetTime"/>.</returns>
        public static bool IsLaterThan(this DateTime sourceTime, DateTime targetTime) 
            => sourceTime >= targetTime;

        /// <summary>
        /// Returns a number of Ticks from <paramref name="timeUtc"/> in <see cref="T:System.TimeSpan" />.
        /// </summary>
        /// <param name="timeUtc">Time, whose number of ticks has to be returned.</param>
        /// <returns>A number of ticks from <paramref name="timeUtc"/> in <see cref="T:System.TimeSpan" />.</returns>
        public static TimeSpan GetTimeTillNow(this DateTime timeUtc) 
            => new(timeUtc.Ticks);

        /// <summary>
        /// Converts a <see cref="T:System.TimeSpan" /> to the required <see cref="T:Extensions.TimeExtension.TimeFormat" />.
        /// </summary>
        /// <param name="input">A <see cref="T:System.TimeSpan" /> to be converted.</param>
        /// <param name="timeFormat">Enum parameter which defines the time format.</param>
        /// <returns>A string of <paramref name="input"/> in <paramref name="timeFormat"/>.</returns>
        public static string ToString(this TimeSpan input, TimeFormat timeFormat)
        {
            var dateTime = new DateTime(input.Ticks);
            return timeFormat switch
            {
                TimeFormat.HourMinuteSecond 
                    => $"{dateTime.Hour} hours, {dateTime.Minute} minutes, {dateTime.Second} seconds",
                TimeFormat.DayHour 
                    => $"{dateTime.Day} days, {dateTime.Hour} hours",
                TimeFormat.MonthDayHour 
                    => $"{dateTime.Month} months, {dateTime.Day} days, {dateTime.Hour} hours",
                TimeFormat.Full 
                    => $"{dateTime.Year} years, {dateTime.Month} months, {dateTime.Day} days, {dateTime.Hour} hours, {dateTime.Minute} minutes, {dateTime.Second} seconds",
                _ => null
            };
        }

        /// <summary>
        /// Converts a <see cref="T:System.DateTime" /> to <see cref="T:System.String" />.
        /// </summary>
        /// <param name="input">The date which needs to be converted.</param>
        /// <param name="timeFormat">The time format of the conversion (always Full)</param>
        /// <returns>A <see cref="T:System.String" /> of full <see cref="T:System.DateTime" />.</returns>
        public static string ToString(this DateTime input, TimeFormat timeFormat = TimeFormat.Full) 
            => $"{input.ToLongDateString()} {input.ToLongTimeString()}";

        /// <summary>
        /// Converts a <see cref="T:System.TimeSpan" /> to short <see cref="T:System.DateTime" /> and returns <see cref="T:System.String" /> of it.
        /// </summary>
        /// <param name="time"><see cref="T:System.TimeSpan" /> to be converted.</param>
        /// <returns>A <see cref="T:System.String" /> of short <see cref="T:System.DateTime" /> which originally is <see cref="T:System.TimeSpan" />.</returns>
        public static string ToShortString(this TimeSpan time)
        {
            var dateTime = new DateTime(time.Ticks, DateTimeKind.Local);
            return $"{dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}";
        }
    }
}
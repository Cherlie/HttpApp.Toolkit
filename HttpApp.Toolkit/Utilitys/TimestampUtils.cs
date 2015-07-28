using System;

namespace HttpApp.Toolkit.Utilitys
{
    /// <summary>
    /// 时间戳和系统时间的转换
    /// </summary>
    public class TimestampUtils
    {
        public static readonly DateTime UnixTimestampLocalZero = System.TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
        public static readonly DateTime UnixTimestampUtcZero = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 系统时间转时间戳
        /// </summary>
        /// <param name="datetime">系统时间</param>
        /// <returns>时间戳</returns>
        public static long DateTimeToTimestamp(DateTime datetime)
        {
            return (long)(datetime - UnixTimestampLocalZero).TotalMilliseconds;
        }

        /// <summary>
        /// UTC时间转时间戳
        /// </summary>
        /// <param name="datetime">系统时间</param>
        /// <returns>时间戳</returns>
        public static long UtcToTimestamp(DateTime datetime)
        {
            return (long)(datetime - UnixTimestampUtcZero).TotalMilliseconds;
        }

        /// <summary>
        /// 时间戳转系统时间
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <returns>系统时间</returns>
        public static DateTime TimestampToDateTime(long timestamp)
        {
            return UnixTimestampLocalZero.AddMilliseconds(timestamp);
        }

        /// <summary>
        /// 时间戳转UTC时间
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <returns>UTC时间</returns>
        public static DateTime TimestampToUtc(long timestamp)
        {
            return UnixTimestampUtcZero.AddMilliseconds(timestamp);
        }
    }
}

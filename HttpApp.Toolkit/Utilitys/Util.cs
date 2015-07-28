using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HttpApp.Toolkit.Utilitys
{
    /// <summary>
    /// 一些常用的正则验证等
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 获取星期几的当前国家的时间格式
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public static string GetDayOfWeek(DateTime today)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(today.DayOfWeek);
        }

        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool VaildateUserName(string str)
        {
            return Regex.IsMatch(str, "^(?!_)(?!.*?_$)[a-zA-Z0-9_]+$");
        }

        /// <summary>
        /// 验证密码6-18位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool VaildatePwd(string str)
        {
            return Regex.IsMatch(str, "^\\S{6,18}$");
        }

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool VaildateEmail(string str)
        {
            return Regex.IsMatch(str, "^\\s*\\w+(?:\\.{0,1}[\\w-]+)*@[a-zA-Z0-9]+(?:[-.][a-zA-Z0-9]+)*\\.[a-zA-Z]+\\s*$");
        }

        /// <summary>
        /// 验证电话号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool VaildatePhone(String str)
        {
            return Regex.IsMatch(str, "^(1(([35][0-9])|(47)|[8][01236789]))\\d{8}$");
        }

    }
}

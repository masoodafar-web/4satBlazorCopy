using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace newFace.Shared
{
    public static class ConvertDate
    {
     
        public static Expression<Func<TSource, TResult>> CreateExpression<TSource, TResult>(
    Expression<Func<TSource, TResult>> expression)
        {
            return expression;
        }
        public static string BooleantoPesian(this bool boolValue,BoolStringType stringType)
        {
            switch (boolValue)
            {
                case true:
                    switch (stringType)
                    {
                        case BoolStringType.BoolDefult:
                            return "درست";
                            break;
                        case BoolStringType.HaveOrDontHave:
                            return "دارد";
                            break;
                        case BoolStringType.YesOrNo:
                            return "بله";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(stringType), stringType, null);
                    }
                    
                    break;
                default:
                    switch (stringType)
                    {
                        case BoolStringType.BoolDefult:
                            return "نادرست";
                            break;
                        case BoolStringType.HaveOrDontHave:
                            return "ندارد";
                            break;
                        case BoolStringType.YesOrNo:
                            return "خیر";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(stringType), stringType, null);
                    }
                    break;
            }
        }
        public static T GetObject<T>(this Dictionary<string, string> dict)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                type.GetProperty(kv.Key).SetValue(obj, kv.Value);
            }
            return (T)obj;
        }
        public static T CastModelTo<T>(this Object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

                propertyInfo.SetValue(x, value, null);
            }
            return (T)x;
        }
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
        public static string MiladiToJalali(this DateTime datetime)
        {
            PersianCalendar pc = new PersianCalendar();
            try
            {
                return string.Format("{0}/{1:00}/{2:00}", pc.GetYear(datetime), pc.GetMonth(datetime), pc.GetDayOfMonth(datetime));
            }
            catch (Exception)
            {

                return "";
            }

        }

        public static string MiladiToJalaliMonth(this DateTime datetime)
        {
            PersianCalendar pc = new PersianCalendar();
            try
            {
                return string.Format("{0:00}", pc.GetMonth(datetime));
            }
            catch (Exception)
            {

                return "";
            }

        }
        public static string MiladiToJalaliYear(this DateTime datetime)
        {
            PersianCalendar pc = new PersianCalendar();
            try
            {
                return string.Format("{0}", pc.GetYear(datetime));
            }
            catch (Exception)
            {

                return "";
            }

        }

        public static string MiladiToJalaliDay(this DateTime datetime)
        {
            PersianCalendar pc = new PersianCalendar();
            try
            {
                return string.Format("{0:00}", pc.GetDayOfMonth(datetime));
            }
            catch (Exception)
            {

                return "";
            }

        }
        public static int ParsianDayOfWeekInt(this System.DayOfWeek DateTimeDayOfWeek)
        {
            PersianCalendar pc = new PersianCalendar();
            int result = 8;
            switch (DateTimeDayOfWeek)
            {
                case DayOfWeek.Sunday:
                    result = 1;
                    break;
                case DayOfWeek.Monday:
                    result = 2;

                    break;
                case DayOfWeek.Tuesday:
                    result = 3;

                    break;
                case DayOfWeek.Wednesday:
                    result = 4;

                    break;
                case DayOfWeek.Thursday:
                    result = 5;

                    break;
                case DayOfWeek.Friday:
                    result = 6;

                    break;
                case DayOfWeek.Saturday:
                    result = 0;

                    break;
                default:
                    break;
            }
            return result;
        }

        public static string ParsianDayOfWeek(this System.DayOfWeek DateTimeDayOfWeek)
        {
            PersianCalendar pc = new PersianCalendar();
            string result = "";
            switch (DateTimeDayOfWeek)
            {
                case DayOfWeek.Sunday:
                    result = "یکشنبه";
                    break;
                case DayOfWeek.Monday:
                    result = "دوشنبه";

                    break;
                case DayOfWeek.Tuesday:
                    result = "سه شنبه";

                    break;
                case DayOfWeek.Wednesday:
                    result = "چهار شنبه";

                    break;
                case DayOfWeek.Thursday:
                    result = "پنج شنبه";

                    break;
                case DayOfWeek.Friday:
                    result = "جمعه";

                    break;
                case DayOfWeek.Saturday:
                    result = "شنبه";

                    break;
                default:
                    break;
            }
            return result;
        }
        public static string ParsianMonthName(this int persanmonthint)
        {
            string result = "";
            switch (persanmonthint)
            {
                case 1:
                    result = "فروردین";
                    break;
                case 2:
                    result = "اردیبهشت";

                    break;
                case 3:
                    result = "خرداد";

                    break;
                case 4:
                    result = "تیر";

                    break;
                case 5:
                    result = "مرداد";

                    break;
                case 6:
                    result = "شهریور";

                    break;
                case 7:
                    result = "مهر";

                    break;
                case 8:
                    result = "آبان";

                    break;
                case 9:
                    result = "آذر";

                    break;
                case 10:
                    result = "دی";

                    break;
                case 11:
                    result = "بهمن";

                    break;
                case 12:
                    result = "اسفند";

                    break;

                default:
                    break;
            }
            return result;
        }

        public static DateTime ToDateTime(this string objDate)
        {
            int year = 0;
            int month = 0;
            int day = 0;
            PersianCalendar persianCalendar = new PersianCalendar();

            string date = objDate as string;
            Match match;
            date = date.Trim();
            if (Regex.IsMatch(date, @"^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](13|14)?\d{2})|((13|14)\d{2}[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))$"))
            {
                match = Regex.Match(date, @"^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.]((13|14)?\d{2}))|(((13|14)\d{2})[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))$");

                if (match.Groups[1].Success)
                {
                    day = Convert.ToInt32(match.Groups[2].Value);
                    month = Convert.ToInt32(match.Groups[3].Value);

                    if (match.Groups[5].Success)
                    {
                        year = Convert.ToInt32(match.Groups[4].Value);
                    }
                    else
                    {
                        year = Convert.ToInt32(string.Format("{0:00}{1:00}", persianCalendar.GetYear(DateTime.Now) / 100, match.Groups[4].Value));
                    }
                }
                else
                {
                    day = Convert.ToInt32(match.Groups[10].Value);
                    month = Convert.ToInt32(match.Groups[9].Value);
                    year = Convert.ToInt32(match.Groups[7].Value);
                }
            }
            else
            {
                throw new Exception("Invalid Date Expression");
            }

            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
    }

   public enum BoolStringType
    {
        BoolDefult = 0,
        HaveOrDontHave = 1,
        YesOrNo = 2
    }
}
using System;
using System.Globalization;
using System.Linq;

namespace Shop.Application.Utilities
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                return pc.GetHour(value) + ":" + pc.GetMinute(value).ToString("00") + ":" +
                       pc.GetSecond(value).ToString("00") + " " + pc.GetYear(value) + "/" +
                       pc.GetMonth(value).ToString("00") + "/" +
                       pc.GetDayOfMonth(value).ToString("0");
            }
            catch
            {
                string persianDateString = value.ToString("yyyy/MM/dd", new CultureInfo("fa-IR"));
                return persianDateString;
            }

        }

        public static string ToShamsiLabel(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" +
                   pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("0") + " " +
                   pc.GetHour(value) + ":" + pc.GetMinute(value).ToString("00") + ":" +
                   pc.GetSecond(value).ToString("00");
        }

        public static string ToShamsiDate(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetYear(value) + "/" +
                   pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("0");
        }

        public static string ToShamsiTime(this DateTime value)
        {
            return value.Hour.ToString("00") + ":" +
                   value.Minute.ToString("00") + ":" +
                   value.Second.ToString("00");
        }

        public static DateTime ToDateTime(this string dateTime)
        {
            dateTime = dateTime.Fa2En().FixPersianChars();

            var p = new PersianCalendar();
            string[] parts = dateTime.Split(new[] { '/', '-' });
            var r = p.ToDateTime(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]), 0, 0,
                0, 0);
            return r;
        }

        public static string ToMiladi(this DateTime value)
        {
            GregorianCalendar pc = new GregorianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00") + " " +
                   pc.GetHour(value) + ":" + pc.GetMinute(value).ToString("00") + ":" +
                   pc.GetSecond(value).ToString("00");
        }
        
        public static string ToShamsi(this string dateTime)
        {

            var date = DateTime.Parse(dateTime, new CultureInfo("en-US", true));

            return ToShamsi(date);
        }

        public static bool IsMidnight(this DateTime dateTime)
        {
            return dateTime.TimeOfDay == TimeSpan.Zero;
        }
    
        public static DateTime? ToGregorian(this string dateTime)
        {
            try
            {
                dateTime = dateTime.Fa2En().FixPersianChars();
                if (string.IsNullOrEmpty(dateTime))
                    return null;

                var parts = (dateTime.Contains("-")) ? dateTime.Split('-') : dateTime.Split(' ');
                parts = parts.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

                string timePart, datePart;

                if (parts[0].Contains(":"))
                {
                    timePart = (parts.Length > 0) ? parts[0].Trim() : "";
                    datePart = (parts.Length > 1) ? parts[1].Trim() : "";

                }
                else
                {
                    datePart = (parts.Length > 0) ? parts[0].Trim() : "";
                    timePart = (parts.Length > 1) ? parts[1].Trim() : "";
                }

                // جدا کردن سال، ماه و روز
                string[] dateParts = (string.IsNullOrEmpty(datePart)) ? Array.Empty<string>() : datePart.Split('/');


                int year = (dateParts.Length > 0) ? int.Parse(dateParts[0]) : 0;
                int month = (dateParts.Length > 1) ? int.Parse(dateParts[1]) : 0;
                int day = (dateParts.Length > 2) ? int.Parse(dateParts[2]) : 0;


                // جدا کردن ساعت، دقیقه و ثانیه
                string[] timeParts = (string.IsNullOrEmpty(timePart)) ? Array.Empty<string>() : timePart.Split(':');
                int hour = (timeParts.Length > 0) ? int.Parse(timeParts[0]) : 0;
                int minute = (timeParts.Length > 1) ? int.Parse(timeParts[1]) : 0;
                int second = (timeParts.Length > 2) ? int.Parse(timeParts[2]) : 0;

                PersianCalendar persianCalendar = new PersianCalendar();

                DateTime gregorianDateTime = new DateTime(year, month, day, hour, minute, second, persianCalendar);

                return gregorianDateTime;
            }
            catch (Exception e)
            {
                bool isValid = DateTime.TryParseExact(
                    dateTime,
                    "MM/dd/yyyy HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out _
                );

                if (isValid)
                {
                    return DateTime.ParseExact(
                        dateTime,
                        "MM/dd/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture
                    );
                }
                
                return null;
            }
        }

        public static string GetDay(this DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    {
                        return "جمعه";
                    }
                case DayOfWeek.Monday:
                    {
                        return "دوشنبه";
                    }
                case DayOfWeek.Saturday:
                    {
                        return "شنبه";
                    }
                case DayOfWeek.Sunday:
                    {
                        return "یکشنبه";
                    }
                case DayOfWeek.Thursday:
                    {
                        return "پنچشنبه";
                    }
                case DayOfWeek.Wednesday:
                    {
                        return "چهارشنبه";
                    }
                case DayOfWeek.Tuesday:
                    {
                        return "سه شنبه";
                    }
                default:
                    {
                        return "هیچ";
                    }
            }

        }

        public static string GetMonth(this int month, int year)
        {
            return month switch
            {
                1 => "فروردین" + year,
                2 => "اردیبهشت" + year,
                3 => "خرداد" + year,
                4 => "تیر" + year,
                5 => "مرداد" + year,
                6 => "شهریور" + year,
                7 => "مهر" + year,
                8 => "آبان" + year,
                9 => "آذر" + year,
                10 => "دی" + year,
                11 => "بهمن" + year,
                12 => "اسفند" + year,
                _ => ""
            };
        }

        public static ValueTuple<DateTime, DateTime, string, string> GetRangeDate(this int month, int year)
        {

            switch (month)
            {
                case 1:
                    {
                        var startDate = year + "/01/01";
                        var endDate = year + "/02/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "فروردین" + year, "فروردین");
                    }
                case 2:
                    {
                        var startDate = year + "/02/01";
                        var endDate = year + "/03/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "اردیبهشت" + year, "اردیبهشت");
                    }
                case 3:
                    {
                        var startDate = year + "/03/01";
                        var endDate = year + "/04/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "خرداد" + year, "خرداد");
                    }
                case 4:
                    {
                        var startDate = year + "/04/01";
                        var endDate = year + "/05/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "تیر" + year, "تیر");
                    }
                case 5:
                    {
                        var startDate = year + "/05/01";
                        var endDate = year + "/06/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "مرداد" + year, "مرداد");
                    }
                case 6:
                    {
                        var startDate = year + "/05/01";
                        var endDate = year + "/06/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "شهریور" + year, "شهریور");
                    }
                case 7:
                    {
                        var startDate = year + "/06/01";
                        var endDate = year + "/07/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "مهر" + year, "مهر");
                    }
                case 8:
                    {
                        var startDate = year + "/08/01";
                        var endDate = year + "/09/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "آبان" + year, "آبان");
                    }
                case 9:
                    {
                        var startDate = year + "/09/01";
                        var endDate = year + "/10/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "آذر" + year, "آذر");
                    }
                case 10:
                    {
                        var startDate = year + "/10/01";
                        var endDate = year + "/11/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "دی" + year, "دی");
                    }
                case 11:
                    {
                        var startDate = year + "/11/01";
                        var endDate = year + "/12/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "بهمن" + year, "بهمن");
                    }
                case 12:
                    {
                        var startDate = year + "/12/01";
                        var endDate = year + 1 + "" + "/01/01";
                        return ValueTuple.Create(startDate.ToDateTime(), endDate.ToDateTime().AddDays(-1), "اسفند" + year, "اسفند");
                    }
                default:
                    return ValueTuple.Create(DateTime.Now, DateTime.Now, "", "");
            }
        }

        public static TimeSpan? GetTime(this string dateTime)
        {
            dateTime = dateTime.Fa2En().FixPersianChars();
            if (string.IsNullOrEmpty(dateTime))
                return null;

            string[] timeParts = dateTime.Split(' ')[0].Split(':');

            int hour = (timeParts.Length > 0 && int.TryParse(timeParts[0], out hour)) ? hour : 0;
            int minute = (timeParts.Length > 1 && int.TryParse(timeParts[1], out minute)) ? minute : 0;
            int second = (timeParts.Length > 2 && int.TryParse(timeParts[2], out second)) ? second : 0;

            return new TimeSpan(hour, minute, second);
        }
    }
}

using System;

namespace DecimalDateTimeApi.Utils
{
    public class DecimalDateTimeJsonResult
    {
        private const String DATE_FORMAT = "yyy-MM-ddThh:mm:ss";

        public String Timestamp { get; }
        public String GregorianTimestamp { get; }
        public int Year { get; }
        public int Month { get; }
        public String MonthName { get; }
        public int Day { get; }
        public String DayName { get; }
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }
        public int DayInYear { get; }

        public DecimalDateTimeJsonResult(Pallettaro.Revo.DateTime datetime)
        {
            Timestamp = Pallettaro.Revo.DateTimeFormat.Format(datetime, DATE_FORMAT);
            GregorianTimestamp = datetime.datetime.ToString(DATE_FORMAT);
            Year = datetime.RepublicanYear;
            Month = datetime.RepublicanMonth;
            MonthName = datetime.MonthName;
            Day = datetime.RepublicanDay;
            DayName = datetime.DayName;
            Hour = datetime.RepublicanHours;
            Minute = datetime.RepublicanMinutes;
            Second = datetime.RepublicanSeconds;
            DayInYear = datetime.RepublicanDayInYear;
        }
    }
}

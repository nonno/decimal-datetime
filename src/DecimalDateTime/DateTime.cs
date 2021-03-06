﻿using Pallettaro.Revo.i18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pallettaro.Revo
{
    public class DateTime
    {
        public const decimal SECONDS_RATIO = 0.864M;
        private const int REPUBLICAN_HOURS_IN_DAY = 10;
        private const int REPUBLICAN_MINUTES_IN_HOUR = 100;
        private const int REPUBLICAN_SECONDS_IN_MINUTE = 100;

        public static DateTime Now
        {
            get
            {
                return new DateTime(System.DateTime.Now);
            }
        }

        private System.DateTime FIRST_DATETIME = new System.DateTime(1980, 9, 22);
        private const int FIRST_YEAR = 189;
        private static List<Int32> bisestili = new List<int>();
        public System.DateTime datetime { get; private set; }
        private int totalRepublicanSecondsInDay = 0;
        private int totalRepublicanDays = 0;
        private int totalMilliSeconds = 0;
        private int DaysInYear(int year)
        {
            var result = 365;
            if (bisestili.Contains(year))
            {
                result = 366;
            }
            return result;
        }
        public String MonthName
        {
            get
            {
                return this.GetMonthName(this.RepublicanMonth);
            }
        }
        public String MonthDescription
        {
            get
            {
                int month = this.RepublicanMonth;
                if (month > 0 && month < 14)
                {
                    return MonthsDes.ResourceManager.GetString("m" + month.ToString("00"));
                }
                return string.Empty;
            }
        }
        public String DayName
        {
            get
            {
                return this.GetDayName(this.RepublicanDayInYear);
            }
        }
        private String GetMonthName(int month)
        {
            if(month > 0 && month < 14)
            {
                return Months.ResourceManager.GetString("m" + month.ToString("00"));
            }
            return string.Empty;
        }
        private String GetDayName(int day)
        {
            if (day > 0 && day < 366)
            {
                return Days.ResourceManager.GetString("d" + day.ToString("000"));
            }
            return string.Empty;
        }
        public DateTime(int year, int month, int day)
        {
            if(year < FIRST_YEAR || month < 1 || month > 13 || day < 1 || day > 30)
            {
                throw new ArgumentException("Parametri di inizializzazione data errati");
            }
            if (!bisestili.Any())
            {
                InitBisestili();
            }
            this.totalMilliSeconds = 0;
            var totalDays = 0;
            for(int i = FIRST_YEAR; i < year; i++)
            {
                if (bisestili.Contains(i))
                {
                    totalDays += 366;
                }
                else
                {
                    totalDays += 365;
                }
            }
            totalDays += ((month - 1) * 30);
            totalDays += day;
            this.totalRepublicanDays = totalDays;
            this.datetime = FIRST_DATETIME.AddDays(totalDays - 1);
            this.InitDate();
        }
        public DateTime(int year, int month, int day, int hour, int minute, int second) : this(year, month, day)
        {
            if(hour < 0 || hour > 9 || minute < 0 || minute > 99 || second < 0 || second > 99)
            {
                throw new ArgumentException("Parametri di inizializzazione data errati");
            }
            totalMilliSeconds = Decimal.ToInt32(second * 1000 * SECONDS_RATIO);
            totalMilliSeconds += Decimal.ToInt32(minute * REPUBLICAN_SECONDS_IN_MINUTE * 1000 * SECONDS_RATIO);
            totalMilliSeconds += Decimal.ToInt32(hour * REPUBLICAN_MINUTES_IN_HOUR * REPUBLICAN_SECONDS_IN_MINUTE * 1000 * SECONDS_RATIO);
            totalRepublicanSecondsInDay = Decimal.ToInt32((totalMilliSeconds / SECONDS_RATIO) / 1000);
            this.InitDate();
        }
        public DateTime(System.DateTime datetime)
        {
            if (!bisestili.Any())
            {
                InitBisestili();
            }
            this.datetime = datetime;
            totalMilliSeconds = (((((datetime.Hour * 60) + datetime.Minute) * 60) + datetime.Second) * 1000) + datetime.Millisecond;
            totalRepublicanSecondsInDay = Decimal.ToInt32((totalMilliSeconds / SECONDS_RATIO) / 1000);
            totalRepublicanDays = (int)Math.Ceiling(datetime.Subtract(FIRST_DATETIME).TotalDays);
            this.InitDate();
        }
        private void InitDate()
        {
            int totalDays = totalRepublicanDays;
            int year = FIRST_YEAR;
            while (totalDays > this.DaysInYear(year))
            {
                totalDays = totalDays - this.DaysInYear(year);
                year++;
            }
            this.RepublicanYear = year;
            if (totalDays > 360)
            {
                this.RepublicanMonth = 13;
                this.RepublicanDay = totalDays - 360;
            }
            else
            {
                this.RepublicanMonth = ((totalDays - 1) / 30) + 1;
                this.RepublicanDay = totalDays % 30;
                if (RepublicanDay == 0) RepublicanDay = 30;
            }
        }
        private void InitBisestili()
        {
			bisestili.Add(189);
			bisestili.Add(193);
			bisestili.Add(197);
			bisestili.Add(201);
			bisestili.Add(206);
			bisestili.Add(210);
			bisestili.Add(214);
			bisestili.Add(218);
			bisestili.Add(222);
            bisestili.Add(226);
            bisestili.Add(230);
        }
        public decimal Milliseconds
        {
            get
            {
                var totalRepMilli = this.totalMilliSeconds / SECONDS_RATIO;
                var subtract = (((((this.RepublicanHours * REPUBLICAN_MINUTES_IN_HOUR) + this.RepublicanMinutes) * REPUBLICAN_SECONDS_IN_MINUTE) + this.RepublicanSeconds) * 1000);
                var mill = totalRepMilli - subtract;
                return mill;
            }
        }
        public int RepublicanSeconds
        {
            get
            {
                return this.totalRepublicanSecondsInDay % (REPUBLICAN_SECONDS_IN_MINUTE);
            }
        }
        public int RepublicanMinutes
        {
            get
            {
                var totalRepMinutes = this.totalRepublicanSecondsInDay / REPUBLICAN_SECONDS_IN_MINUTE;
                return totalRepMinutes % REPUBLICAN_MINUTES_IN_HOUR;
            }
        }
        public int RepublicanHours
        {
            get
            {
                return this.totalRepublicanSecondsInDay / (REPUBLICAN_MINUTES_IN_HOUR * REPUBLICAN_SECONDS_IN_MINUTE);
            }
        }
        public int RepublicanDay { get; private set; }
        public int RepublicanDayInYear {
            get
            {
                int result = RepublicanDay;
                for(int i=2; i<= RepublicanMonth; i++)
                {
                    result += 30;
                }
                return result;
            }
        }
        public int RepublicanMonth { get; private set; }
        public int RepublicanYear { get; private set; }
        public override string ToString()
        {
            return DateTimeFormat.Format(this, "dd-MM-yyy");
        }
        public string ToString(string format)
        {
            if (String.IsNullOrEmpty(format))
            {
                return ToString();
            }
            return DateTimeFormat.Format(this, format); 
        }
    }
}

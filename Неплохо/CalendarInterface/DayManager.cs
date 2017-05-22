using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Rehlander
{
    public class DayManager
    {
        string localSavePath = @"dayEvents.xml";
        private List<CalendarDay> calendarDays = new List<CalendarDay>();

        public DayManager(string login, string password)
        {
            LoadLocal();
        }

        public void Save()
        {
            XmlDocument doc = EventSerializer.SerializeCalenderDayList(calendarDays);
            doc.Save(localSavePath);
        }

        public void UpdateDay(CalendarDay day)
        {
            // Query the calenderDays to see if this day is there
            var dayQ = from d in calendarDays
                       where d.EventDayNum == day.EventDayNum &&
                       d.EventMonth == day.EventMonth &&
                       d.EventYear == day.EventYear
                       select d;

            // If it is, update the data
            if (dayQ.Count() == 1)
                dayQ.First<CalendarDay>().Data = day.Data;
            else // Else add it to the collection
                calendarDays.Add(day);
        }

        public void LoadLocal()
        {
            if (!File.Exists(localSavePath))
                CreateEmptyDoc();

            using(StreamReader sr = new StreamReader(localSavePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sr.ReadToEnd());
                calendarDays = EventSerializer.GetCalendarEventsFromXml(doc);
            }
        }

        public List<CalendarDay> GetCalendarDays(int month, int year)
        {
            List<CalendarDay> daysInMonth = new List<CalendarDay>();
            var days = from d in calendarDays.AsEnumerable()
                       where d.EventYear == year && d.EventMonth == month
                       orderby d.EventDayNum
                       select d;

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                var thisDayQ = from tDay in days.AsEnumerable()
                               where tDay.EventDayNum == i
                               select tDay;

                if (thisDayQ.Count() > 0)
                {
                    daysInMonth.Add(thisDayQ.First<CalendarDay>());
                }
                else
                {
                    CalendarDay newDay = new CalendarDay(i, month, year);                    
                    daysInMonth.Add(newDay);
                }
            }

            return daysInMonth;
        }

        private void CreateEmptyDoc()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Calendar><CalendarEvents></CalendarEvents></Calendar>");
            doc.Save(localSavePath);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Rehlander
{
    public static class EventSerializer
    {
        public static List<CalendarDay> GetCalendarEventsFromXml(XmlDocument eventsDoc)
        {
            List<CalendarDay> eventList = new List<CalendarDay>();

            var events = from e in eventsDoc.SelectNodes("//Calendar/CalendarEvents/CalendarEvent").Cast<XmlNode>()
                         select e;

            foreach (var ev in events)
            {
                int day = int.Parse(ev.Attributes["day"].InnerText);
                int month = int.Parse(ev.Attributes["month"].InnerText);
                int year = int.Parse(ev.Attributes["year"].InnerText);

                CalendarDay cEvent = new CalendarDay(day, month, year);
                cEvent.Data = ev.InnerText;

                eventList.Add(cEvent);
            }

            return eventList;
        }

        public static XmlDocument SerializeCalenderDayList(List<CalendarDay> calenderDays)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Calendar");

            XmlElement eventList = doc.CreateElement("CalendarEvents");

            foreach (CalendarDay day in calenderDays)
            {
                XmlElement dayElement = doc.CreateElement("CalendarEvent");
                
                XmlAttribute dayAttr = doc.CreateAttribute("day");
                dayAttr.InnerText = day.EventDayNum.ToString();
                dayElement.Attributes.Append(dayAttr);

                XmlAttribute monthAttr = doc.CreateAttribute("month");
                monthAttr.InnerText = day.EventMonth.ToString();
                dayElement.Attributes.Append(monthAttr);

                XmlAttribute yearAttr = doc.CreateAttribute("year");
                yearAttr.InnerText = day.EventYear.ToString();
                dayElement.Attributes.Append(yearAttr);

                dayElement.InnerText = day.Data;
                eventList.AppendChild(dayElement);
            }
            root.AppendChild(eventList);

            doc.AppendChild(root);

            return doc;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Rehlander.CalendarBackend
{
    public static class DayDeserializer
    {
    	/*?>echo("<Calendar>
	 	<CalendarEvents>
			<CalendarEvent month=\"1\" day=\"2\" year=\"2009\">This is awesome!!</CalendarEvent>
			<CalendarEvent month=\"1\" day=\"22\" year=\"2009\">TLinqftw!!</CalendarEvent>
			<CalendarEvent month=\"1\" day=\"14\" year=\"2009\">Balls!!</CalendarEvent>
			<CalendarEvent month=\"3\" day=\"13\" year=\"2009\">Woot!!</CalendarEvent>
			<CalendarEvent month=\"12\" day=\"12\" year=\"2009\">Andrew</CalendarEvent>
			<CalendarEvent month=\"5\" day=\"31\" year=\"2009\">WORD</CalendarEvent>
		</CalendarEvents>
		</Calendar>");<?php */
        public static List<Day> Deserialize(XmlDocument doc)
        {
            var daysQ = from d in doc.SelectSingleNode("Calendar").SelectSingleNode("CalendarEvents").Cast<XmlNode>()
                    select d;
            if (daysQ.Count() == 0) return new List<Day>();

            List<Day> days = new List<Day>();
            foreach (XmlNode node in daysQ)
            {
                Day day = new Day();
                DateTime date = new DateTime(int.Parse(node.Attributes["year"].InnerText), int.Parse(node.Attributes["month"].InnerText), 
                    int.Parse(node.Attributes["day"].InnerText));
                day.CurrentDay = date;
                day.Data = node.InnerText;
                days.Add(day);
            }

            return days;
        }
    }
}

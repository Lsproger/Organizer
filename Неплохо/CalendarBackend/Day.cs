using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rehlander.CalendarBackend
{
    public class Day
    {
        DateTime currentDay;
        public DateTime CurrentDay { get { return currentDay; } set { currentDay = value; } }

        string data;
        public string Data { get { return data; } set { data = value; } }

        public Day()
        {
        }
   
    }
}

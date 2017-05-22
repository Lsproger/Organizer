using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Rehlander
{
    public class CalendarDay : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        #region Variables

        DateTime eventDay;
        DateTime EventDay { get { return eventDay; } }

        public int EventMonth { get { return eventDay.Month; } }
        public int EventDayNum { get { return eventDay.Day; } }
        public int EventYear { get { return eventDay.Year; } }

        string data = "";
        public string Data { get { return data; } 
            set 
            {
                if (data == value) return;

                data = value;
                NotifyPropertyChanged("Data");
            } 
        }

        #endregion


        public CalendarDay(int day, int month, int year)
        {
            eventDay = new DateTime(year, month, day);
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}

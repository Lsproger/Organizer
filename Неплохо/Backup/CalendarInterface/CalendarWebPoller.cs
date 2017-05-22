using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Rehlander
{
    public class CalendarWebPoller
    {

        public delegate void CalendarQueryCompleteHandler(List<CalendarDay> events);
        public event CalendarQueryCompleteHandler CalendarQueryComplete;


        Thread pollThread;

        // These should not change for the life of the syncer
        string login;
        string password;

        // This allows us to poll at set intervals
        DateTime dateChanged = DateTime.Now;

        // The polling time in seconds
        int POLL_TIME = 5;

        // This variable will tell the Polling thread that we need to poll the server for a new
        //  set of events immediately.  It is set from both public properties.
        bool refreshNow = false;

        int month;
        public int Month 
        { 
            get { return month; } 
            set 
            { 
                month = value;
                dateChanged = DateTime.Now;
                refreshNow = true;
            } 
        }

        int year;
        public int Year { 
            get { return year; } 
            set 
            { 
                year = value;
                dateChanged = DateTime.Now;
                refreshNow = true;
            }
        }


        public CalendarWebPoller(string userLogin, string userPassword)
        {
            // Set the initial variables
            login = userLogin;
            password = userPassword;
        }

        public void Start()
        {
            // Start the polling thread
            pollThread = new Thread(new ThreadStart(Syncer));
            pollThread.Start();
        }

        private void Syncer()
        {
            // Continually query the server via the calendarInterface
            WebCalendarInterface calendarInterface = new WebCalendarInterface();

            while (1 == 1)
            {
                if (!refreshNow && (DateTime.Now - dateChanged).TotalSeconds < POLL_TIME)
                {
                    System.Threading.Thread.Sleep(100);
                    continue;
                }

                dateChanged = DateTime.Now;
                refreshNow = false;

                List<CalendarDay> events = calendarInterface.GetEvents(login, password, month, year);
            }
        }
    }
}

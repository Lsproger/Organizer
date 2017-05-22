using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;


namespace Rehlander
{
    public class WebCalendarInterface
    {
        string server = @"http://www.rehlander.com/Calendar/calendarInterface.php";

        public List<CalendarDay> GetEvents(string login, string password, int month, int year)
        {
            string fullUri = server + CreateGetParams(login, password, month, year);
            string responseString = ExtractStringFromStream(GetRequestStream(fullUri));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseString);

            List<CalendarDay> calenderEvents = EventSerializer.GetCalendarEventsFromXml(doc);

            return calenderEvents;
        }

        private Stream GetRequestStream(string uri)
        {
            // prepare the web page we will be asking for
            HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create(uri);

            // execute the request
            HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();

            // we will read data via the response stream
            return response.GetResponseStream();
        }

        private string CreateGetParams(string login, string password, int month, int year)
        {
            string monthString = month.ToString();
            if (monthString.Length == 1)
                monthString = "0" + monthString;
            return string.Format("?login={0}&password={1}&month={2}&year={3}", login, password, monthString, year);
        }

        private string ExtractStringFromStream(Stream resStream)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            return sb.ToString();
        }

    }

}

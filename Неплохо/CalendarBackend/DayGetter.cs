using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.Web;
using System.IO;

namespace Rehlander.CalendarBackend
{
    public static class DayGetter
    {
        public static List<Day> GetDays(int year, int month)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(@"http://www.rehlander.com/Calendar/calendarInterface.php?login=scott&password=password&month=8&year=2009");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string str = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(str);
            // Clean up the streams and the response.
            reader.Close();
            response.Close();



            XmlDocument doc = new XmlDocument();
            doc.LoadXml(str);

            return DayDeserializer.Deserialize(doc);
        }
    }
}

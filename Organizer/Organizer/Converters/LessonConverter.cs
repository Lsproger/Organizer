using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Organizer
{
    [ValueConversion(typeof(int), typeof(string))]
    public class LessonConverter : IValueConverter
    {
        public string[] Lessons = { "8:00-9:35", "9:50-11:25", "11:40-13:15", "13:50-15:25" };
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return Lessons[((int)value) - 1];
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            for (int i = 0; i < Lessons.Count(); i++)
            {
                if ((string)value == Lessons[i])
                {
                    return i;
                }
            }
            return 0;
        }
    }
}

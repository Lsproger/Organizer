using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Organizer
{
    public enum Days
    {
        Понедельник = 1,
        Вторник,
        Среда,
        Четверг,
        Пятница,
        Суббота
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class DayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return ((Days)((int)value)).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Понедельник":
                    return 1;
                case "Вторник":
                    return 2;
                case "Среда":
                    return 3;
                case "Четверг":
                    return 4;
                case "Пятница":
                    return 5;
                case "Суббота":
                    return 6;
                default:
                    return 1;
            }
        }
    }
}

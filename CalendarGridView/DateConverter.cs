using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace CalendarGridView
{
    public class DateConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                var dateTime = ((ICalendarItem)value).Date;
                if (parameter != null)
                {
                    var strPar = (string)parameter;
                    return dateTime.ToString(strPar);
                }
                else
                {
                    return dateTime.ToString("ddd, MM/d");
                }
            }
            catch
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

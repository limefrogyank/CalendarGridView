﻿using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace McPhersonApps
{
    public class AbbreviatedMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((int)value != 0)
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName((int)value);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class FullMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((int)value != 0)
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((int)value);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    public class DateConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
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
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChatAppInCompany.Converters
{
    public class DateTimeToMessageTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currentTime = DateTime.Now;
            var dateTime = (DateTime)value;

            if (dateTime.Day == currentTime.Day)
            {
                return "Hôm nay";
            }

            return dateTime.Day == currentTime.AddDays(-1).Day ? "Hôm qua" : dateTime.ToString("dd MMMM, yyyy", CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

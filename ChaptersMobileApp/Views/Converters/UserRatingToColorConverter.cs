using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Views.Converters
{
    public class UserRatingToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rating = (int)value;
            if (rating < 0)
            {
                return Colors.Red;
            }
            else if (rating == 0)
            {
                return Colors.Grey;
            }
            return Colors.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rating = (Color)value;
            if (rating == Colors.Red)
            {
                return -1;
            }
            else if (rating == Colors.Grey)
            {
                return 0;
            }
            return 1;
        }
    }
}

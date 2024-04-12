using ChaptersMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Views.Converters
{
    public class BookStatusToImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            switch ((BookStatus)value)
            {
                case BookStatus.WillRead:
                    return "book_will.png";
                case BookStatus.Reading:
                    return "book_open.png";
                case BookStatus.StopReading:
                    return "book_cross.png";
                case BookStatus.Finished:
                    return "book_check";
                default:
                    return null;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

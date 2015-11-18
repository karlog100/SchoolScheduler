using System;
using System.Windows;
using System.Windows.Data;

namespace Views
{
    public class RadioButtonCheckedConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
            {
                return DependencyProperty.UnsetValue;
            }

            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                return DependencyProperty.UnsetValue;
            }

            object parameterValue = Enum.Parse(value.GetType(), parameterString);
            if (targetType.Name.Equals("Visibility"))
            {
                if (parameterValue.Equals(value))
                {
                    return Visibility.Visible;
                }
                else 
                { 
                    return Visibility.Collapsed;
                }
            }
            else
            {
                return parameterValue.Equals(value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }
        #endregion
    }
}

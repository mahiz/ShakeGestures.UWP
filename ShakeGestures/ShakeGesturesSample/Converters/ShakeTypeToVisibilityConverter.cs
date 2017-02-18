using ShakeGestures;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ShakeGesturesSample.Converters
{
    public class ShakeTypeToVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ShakeType shakeType = (ShakeType)value;
            string expectedShake = (string)parameter;

            return (shakeType.ToString() == expectedShake) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}

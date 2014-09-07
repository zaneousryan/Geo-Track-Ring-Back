using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace PuppyTrackerApp
{
   public class StopStatusConveter : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (value.ToString().ToLower())
            {
                case "1"://Green
                    return "#189900";
                case "2"://Yellow
                    return "#ff991d";
                case "3"://Red
                    return "#b11d01";
                default:
                    return "#094AB2";
            }

        }

       public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}

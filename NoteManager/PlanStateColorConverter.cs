using System;
using System.Globalization;
using System.Windows.Data;
using NoteManager.DBClasses;
namespace NoteManager
{
    [ValueConversion(typeof(Plan), typeof(string))]
    class PlanStateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Plan plan = value as Plan;
            string color = "Transparent";
            if (plan != null)
            {
                if (plan.IsCompleted == true)
                {
                    color = "Green";
                }
                else
                {
                    if (plan.DeadLineTime > DateTime.Now)
                    {
                        color = "Purple";
                    }
                    else
                    {
                        color = "Red";
                    }
                }
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
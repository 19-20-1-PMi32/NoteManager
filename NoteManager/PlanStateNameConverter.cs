using System;
using System.Windows.Data;
using System.Globalization;
using NoteManager.DBClasses;
namespace NoteManager
{
    [ValueConversion(typeof(Plan), typeof(string))]
    class PlanStateNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Plan plan = (Plan) value;
            string name = "Some state";
            if (plan != null)
            {
                if (plan.IsCompleted == true)
                {
                    name = "Completed";
                }
                else
                {
                    if (plan.DeadLineTime > DateTime.Now)
                    {
                        name = "In progres";
                    }
                    else
                    {
                        name = "Crashed";
                    }
                }
            }
            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}

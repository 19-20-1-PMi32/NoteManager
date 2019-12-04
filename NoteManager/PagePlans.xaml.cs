using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Diagnostics;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PagePlans.xaml
    /// </summary>
    public partial class PagePlans : Page
    {
        public PagePlans()
        {
            InitializeComponent();
            //Initialization only for tests for tests
            List<Plan> plans = GetTestPlans();
            ListPlans.ItemsSource = plans;

        }
        public List<Plan> GetTestPlans()
        {
            string longText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            int count = 10;
            List<Plan> result = new List<Plan>(count);
            for(int i =0; i < count; i++)
            {
                PlanState a;
                if (i % 3 == 0)
                {
                    a = PlanState.Complete;
                }
                else if(i % 2 == 0)
                {
                    a = PlanState.InProgres;
                }
                else
                {
                    a = PlanState.Crashed;
                }

                result.Add(new Plan()
                {
                    Text = $"План {i} і небагато тексту для тестування." + longText,
                    CreationTime = DateTime.Now.AddDays(i),
                    DeadLineTime = DateTime.Now.AddDays(10 + i),
                    State = a
                }); ;
            }
            return result;
        }

        private void ListPlans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Plan sel = ((sender as ListBox)?.SelectedItem as Plan);
            if (sel == null)
                return;
            LoadPlanView(sel);
        }
        private void LoadPlanView(Plan plan)
        {
            PlanEditor.Text = plan.Text;

        }
    }
    public enum PlanState
    {
        InProgres = 1,
        Complete = 2,
        Crashed = 3
    }

    [ValueConversion(typeof(PlanState), typeof(string))]
    public class StateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PlanState state = (PlanState) value;
            if (PlanState.Complete == state)
                return "#29a329";
            else if (PlanState.Crashed == state)
                return "#cc0000";
            else
                return "#9900cc";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = value as string;
            if (color == "Red")
                return PlanState.Crashed;
            else if (color == "Green")
                return PlanState.Complete;
            else
                return PlanState.InProgres;
            
        }
    }
    public class Plan
    {
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeadLineTime { get; set; }
        public PlanState State { get; set; }
    }
}

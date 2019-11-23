using System;
using System.Windows.Controls;
using System.Collections.Generic;

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
            List<Plan> plans = getTestPlans();
            ListPlans.ItemsSource = plans;

        }
        public List<Plan> getTestPlans()
        {
            string longText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            int count = 10;
            List<Plan> result = new List<Plan>(count);
            for(int i =0; i < count; i++)
            {
                result.Add(new Plan()
                {
                    Text = $"План {i} і небагато тексту для тестування." + longText,
                    CreationTime = DateTime.Now.AddDays(i),
                    DeadLineTime = DateTime.Now.AddDays(10 + i),
                    State = PlanState.InProgres
                }); ;
            }
            return result;
        }
    }
    public enum PlanState
    {
        InProgres,
        Complete
    }
    public class Plan
    {
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeadLineTime { get; set; }
        public PlanState State { get; set; }
        public string SmallText
        {
            get
            {
                int length = 20;
                if (Text.Length >= 20)
                    return Text.Substring(0, length);
                else if (Text.Length > 0)
                    return Text;
                else
                    return "Пусто";
            }
        }
    }

}

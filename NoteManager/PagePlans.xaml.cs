using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NoteManager.DBClasses;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PagePlans.xaml
    /// </summary>
    public partial class PagePlans : Page
    {
        List<Plan> plans;
        Plan currentEdit;
        bool isNew = true;
        public PagePlans()
        {
            InitializeComponent();
            //Initialization only for tests 
            plans = User.Plans;
            ListPlans.ItemsSource = plans;

            DataContext = currentEdit;
        }

        public bool IsValid(Plan plan)
        {
            if (plan.Text == String.Empty)
            {
                Notification.ShowMessage("Wrong content input");
                return false;
            }
            if (plan.Name == String.Empty)
            {
                return false;
            }
            if(plan.DeadLineTime < plan.CreationTime)
            {
                Notification.ShowMessage("Deadline is earlier than creation time");
                return false;
            }
            return true;
        }
        private void ListPlans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Plan sel = ((sender as ListBox)?.SelectedItem as Plan);
            if (sel != null)
            {
                ChangeCurrent(sel);
                isNew = false;
            }
            else
                isNew = true;
        }
        private void ChangeCurrent(Plan plan)
        {
            BeginInit();
            currentEdit = plan;
            DataContext = currentEdit;
            EndInit();
        }
        private IEnumerable<Plan> SearchPlan(string text)
        {
            return plans.Where(x => x.Name.Contains(text) || x.Text.Contains(text));
        }
        private void UpdateList(string query)
        {
            ListPlans.BeginInit();
            ListPlans.ItemsSource = SearchPlan(query).ToList();
            ListPlans.EndInit();
        }
        private void UpdateList()
        {
            ListPlans.BeginInit();
            ListPlans.ItemsSource = plans;
            ListPlans.EndInit();
        }
        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchBox.Text != String.Empty)
            {
                UpdateList(SearchBox.Text);
            }
            else
            {
                UpdateList();
            }
        }
        private Plan SelectedItem()
        {
            return ListPlans.SelectedItem as Plan;
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var selected = SelectedItem();
            if(selected != null)
            {
                plans.Remove(selected);
                UpdateList();
                ChangeCurrent(null);
                DeletePlan(selected);
            }
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            var selected = SelectedItem();
            //Here we must also check is correct new plan or changed(is name not empty and something else)
            if (isNew == true)
            {
                if (!IsValid(currentEdit))
                {
                    return;
                }
                AddToList(currentEdit);
                isNew = false;
                SavePlan(currentEdit);
                UpdateList();
                ListPlans.SelectedItem = currentEdit;
            }
            if (selected != null)
            {
                if (!IsValid(selected))
                {
                    return;
                }
                UpdatePlan(selected);
            }

            UpdateList();
        }
        private void AddToList(Plan plan)
        {
            BeginInit();
            plans.Add(plan);
            EndInit();
        }
        private void New(object sender, RoutedEventArgs e)
        {
            isNew = true;
            currentEdit = new Plan() {
                Text="", 
                Name="", 
                CreationTime=DateTime.Now, 
                DeadLineTime=DateTime.Now.AddDays(1)
            };
            ChangeCurrent(currentEdit);
            ListPlans.SelectedItem = null;
        }
        private void DeletePlan(Plan plan)
        {
            // here must be logic for delete plan from database
        }
        private void SavePlan(Plan plan)
        {
            // here must be logic for save plan to database
        }
        private void UpdatePlan(Plan plan)
        {
            // here must be logic for update plan to database
        }
    }
}

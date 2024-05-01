using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public TaskWindow()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<Task> members = new ObservableCollection<Task>();

            members.Add(new Task{ID=1, ProjectCode="PJ1", TaskCode="TK1", EmployeeCode="EP1", TaskName="Farming", DateStart="21/12/2112", DateEnd="22/12/2112", TechniqueCode="SE", Money=3000, MoneyUsed=1500, Progress="Ongoing", Note="none" });
           

            membersDataGrid.ItemsSource = members;
        }
        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.IsEnabled = true;
            ButtonOpenMenu.IsEnabled = false;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.IsEnabled = false;
            ButtonOpenMenu.IsEnabled = true;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (ButtonCloseMenu.IsEnabled == true && ButtonOpenMenu.IsEnabled == false)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_employee.Visibility = Visibility.Collapsed;
                tt_project.Visibility = Visibility.Collapsed;
                tt_task.Visibility = Visibility.Collapsed;
            }
            else if (ButtonCloseMenu.IsEnabled == false && ButtonOpenMenu.IsEnabled == true)
            {
                tt_home.Visibility = Visibility.Visible;
                tt_employee.Visibility = Visibility.Visible;
                tt_project.Visibility = Visibility.Visible;
                tt_task.Visibility = Visibility.Visible;
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
    public class Task
    {
        public int ID { get; set; }
        public string ProjectCode { get; set; }
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public string EmployeeCode { get; set; }
        public string TechniqueCode { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public long MoneyUsed { get; set; }
        public long Money { get; set; }
        public string Progress { get; set; }
        public string Note { get; set; }
    }
}

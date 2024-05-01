using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace GUI
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class Test : Window
    {
        public Test()
        {
            InitializeComponent();
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
    }
}
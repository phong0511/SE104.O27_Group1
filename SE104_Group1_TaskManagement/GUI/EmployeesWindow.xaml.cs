using BUS;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    public partial class EmployeesWindow : Window
    {
        public static DTO_NhanVien crnNV = new DTO_NhanVien();
        BUS_NhanVien nvManager = new BUS_NhanVien();
        BUS_TaiKhoan tkManager = new BUS_TaiKhoan();
        BindingList<DTO_NhanVien> members = new BindingList<DTO_NhanVien>();
        public EmployeesWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            crnNV = nvManager.GetByID(LoginWindow.crnUser.MANV);
            if (crnNV.MANV != "") username.Text = crnNV.TENNV;
            var converter = new BrushConverter();


            //  members.Add(new Employee { ID=1, EmployeeCode = "EP1", EmployeeName = "Nguyen Lam Thanh Triet", Gender="Nam", Technique="Software Engineer", Level="1", Email = "trietn61@gmail.com", Phone = "0985-825-804", Note="None" });

            members = nvManager.GetAllData();
            membersDataGrid.ItemsSource = members;
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddAndUpdateEmployee addDialog = new AddAndUpdateEmployee();
            bool? res = addDialog.ShowDialog();
            if (res != null && res == true)
            {
                members = nvManager.GetAllData();
                membersDataGrid.ItemsSource = members;
            }
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                // Find the DataGridRow that contains the clicked button
                DataGridRow row = FindVisualParent<DataGridRow>(button);
                if (row != null)
                {
                    // Access the data item behind the row
                    DTO_NhanVien? item = row.Item as DTO_NhanVien;
                    if (item != null)
                    {
                        AddAndUpdateEmployee updateDialog = new AddAndUpdateEmployee(item);
                        bool? res = updateDialog.ShowDialog();
                        if (res != null && res == true)
                        {
                            members = nvManager.GetAllData();
                            membersDataGrid.ItemsSource = members;
                        }
                    }

                    // Do something with the item...
                }
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Find the DataGridRow that contains the clicked button
                DataGridRow row = FindVisualParent<DataGridRow>(button);
                if (row != null)
                {
                    // Access the data item behind the row
                    DTO_NhanVien? item = row.Item as DTO_NhanVien;
                    if (item != null)
                    {
                        (bool, string) res = nvManager.DeleteByID(item);
                        if (res.Item1 == true)
                        {
                            MessageBox.Show(res.Item2);
                            members = nvManager.GetAllData();
                            membersDataGrid.ItemsSource = members;
                        }
                        else
                        {
                            MessageBox.Show(res.Item2);
                        }

                    }
                }

                // Do something with the item...
            }
        }

        private T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;
            else
                return FindVisualParent<T>(parentObject);
        }

        
    }
}

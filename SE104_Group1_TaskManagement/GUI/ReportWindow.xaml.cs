using BUS;
using DAL;
using DTO;
using MailKit.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    /// Interaction logic for ReportWindow.xaml
    /// </summary>


    public partial class ReportWindow : Window
    {
        public static DTO_NhanVien crnUser = new DTO_NhanVien();
        BUS_NhanVien nvManager = new BUS_NhanVien();
        BUS_TaiKhoan tkManager = new BUS_TaiKhoan();
        BUS_DuAn daManager = new BUS_DuAn();
        BindingList<DTO_DuAn> projects = new BindingList<DTO_DuAn>();
        Dictionary<string, DTO_ChuyenMon> cm = BUS_StaticTables.Instance.GetAllDataCM();
        public ReportWindow()
        {
            InitializeComponent();
            projectsDataGrid.LoadingRow += ProjectsDataGrid_LoadingRow;
            this.WindowState = WindowState.Maximized;

            setUser();
            projects = daManager.GetAllData();
            showProjects();
        }

        void setUser()
        {
            crnUser = nvManager.GetByID(LoginWindow.crnUser.MANV);
            if (crnUser.MANV != "") username.Text = crnUser.TENNV;
        }

        private void ProjectsDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var firstCol = projectsDataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "C");

            e.Row.Loaded += (s, args) =>
            {
                var row = (DataGridRow)s;
                var item = row.Item;

                DTO_DuAn? da = item as DTO_DuAn;
                if (da != null && da.MADA == LoginWindow.crnUser.MANV)
                {
                    if (firstCol != null)
                    {
                        var chBx = firstCol.GetCellContent(row) as CheckBox;
                        if (chBx != null)
                        {
                            chBx.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            };
        }
        void showProjects()
        {
            projectsDataGrid.ItemsSource = projects;
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddAndUpdateProject addDialog = new AddAndUpdateProject();
            bool? res = addDialog.ShowDialog();
            if (res != null && res == true)
            {
                projects = daManager.GetAllData();
                projectsDataGrid.ItemsSource = projects;
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

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility
            if (ButtonCloseMenu.IsEnabled == true && ButtonOpenMenu.IsEnabled == false)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_project.Visibility = Visibility.Collapsed;
                tt_task.Visibility = Visibility.Collapsed;
            }
            else if (ButtonCloseMenu.IsEnabled == false && ButtonOpenMenu.IsEnabled == true)
            {
                tt_home.Visibility = Visibility.Visible;
                tt_project.Visibility = Visibility.Visible;
                tt_task.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
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

        private void Sel_CheckBox_DataContextChanged(object sender, RoutedEventArgs e)
        {
            var chkSelectAll = sender as CheckBox;
            var firstCol = projectsDataGrid.Columns.OfType<DataGridCheckBoxColumn>().FirstOrDefault(c => c.DisplayIndex == 0);
            if (chkSelectAll == null || firstCol == null || projectsDataGrid?.Items == null)
            {
                return;
            }
            foreach (var item in projectsDataGrid.Items)
            {
                var chBx = firstCol.GetCellContent(item) as CheckBox;
                if (chBx == null || chBx.Visibility != Visibility.Visible)
                {
                    continue;
                }
                chBx.IsChecked = chkSelectAll.IsChecked;
            }
        }

        private void projectsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // Implement logic if needed
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void projectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

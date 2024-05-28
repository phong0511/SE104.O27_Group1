using DTO;
using BUS;
using DAL;
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
using System.ComponentModel;
using MailKit.Search;
using System.Xml;
using System.Windows.Controls.Primitives;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : UserControl
    {
        public static DTO_DuAn project = new DTO_DuAn();
        BUS_DuAn projectManager = new BUS_DuAn();
        BindingList<DTO_DuAn> members = new BindingList<DTO_DuAn>();

        //Dictionary<string, DTO_DuAn> stat = BUS_DuAn.Instance.GetAllDataStat();
        
        public ProjectWindow()
        {
            InitializeComponent();
            membersDataGrid.LoadingRow += MembersDataGrid_LoadingRow;
            statText.SelectedValuePath = "Value.STAT";
            var converter = new BrushConverter();
            members = projectManager.GetAllData();
            showMember();
        }
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            DTO_DuAn filter = new DTO_DuAn();
            filter.MADA = searchText.Text != null ? searchText.Text.ToString() : "";
            filter.TENDA = searchText.Text != null ? searchText.Text.ToString() : "";
            filter.MALSK = searchText.Text != null ? searchText.Text.ToString() : "";
            filter.MAOWNER = searchText.Text != null ? searchText.Text.ToString() : "";
            long ngsl = -1;
            long ngsh = -1;
            if (statCheck.IsChecked == true)
            {
                filter.STAT = statText.SelectedValue != null ? statText.SelectedValue.ToString() : "";
            }
            if (ngansachCheck.IsChecked == true)
            {
                ngsl = long.TryParse(NganSachLTextBox.Text, out long tempLResult) ? tempLResult : -1L;
                ngsh = long.TryParse(NganSachHTextBox.Text, out long tempHResult) ? tempHResult : -1L;
            }
            if (TStartCheck.IsChecked == true)
            {
                filter.TSTART = TStartText.Text != null ? TStartText.Text.ToString() : "";
            }
            if (TEndCheck.IsChecked == true)
            {
                filter.TEND = TEndText.Text != null ? TEndText.Text.ToString() : "";
            }
            members = projectManager.FindDA(filter, ngsl, ngsh);
            showMember();
        }

        private void MembersDataGrid_LoadingRow(object? sender, DataGridRowEventArgs e)
        {
            var firstCol = membersDataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "C");
            var statCol = membersDataGrid.Columns.First(c => c.Header.ToString() == "Tình trạng");
            e.Row.Loaded += (s, args) =>
            {
                var row = (DataGridRow)s;
                var item = row.Item;

                DTO_DuAn? da = item as DTO_DuAn;
                if (da != null)
                {
                    if (firstCol != null)
                    {
                        var chBx = firstCol.GetCellContent(row) as CheckBox;
                        if (chBx != null)
                        {
                            chBx.Visibility = Visibility.Visible;
                        }
                    }
                }

                if (statCol != null)
                {
                    var chBx = statCol.GetCellContent(row) as TextBlock;
                }
                    //    DTO_TinhTrang temp = new DTO_TinhTrang();
                    //    stat.TryGetValue(da.STAT, out temp);
                    //    chBx.Text = temp.STATNAME;

                    //}
                };
        }


        void showMember()
        {
            membersDataGrid.ItemsSource = members;
        }

        private void membersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var firstCol = membersDataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "C");
            var operationCol = membersDataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "Operations");
            foreach (var item in membersDataGrid.Items)
            {
                DTO_DuAn? da = item as DTO_DuAn;
                if (da != null)
                {
                    var chBx = firstCol.GetCellContent(item) as CheckBox;
                    if (chBx == null)
                    {
                        continue;
                    }
                    else
                    {
                        chBx.IsEnabled = false;
                    }
                }
            }
        }

        private void Button_Delete_Loaded(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Find the DataGridRow that contains the clicked button
                DataGridRow row = FindVisualParent<DataGridRow>(button);
                if (row != null)
                {
                    // Access the data item behind the row
                    DTO_DuAn? item = row.Item as DTO_DuAn;
                    if (item != null)
                    {
                        button.Visibility = Visibility.Collapsed;
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

        private void Sel_CheckBox_DataContextChanged(object sender, RoutedEventArgs e)
        {
            var chkSelectAll = sender as CheckBox;
            var firstCol = membersDataGrid.Columns.OfType<DataGridCheckBoxColumn>().FirstOrDefault(c => c.DisplayIndex == 0);
            if (chkSelectAll == null || firstCol == null || membersDataGrid?.Items == null)
            {
                return;
            }
            foreach (var item in membersDataGrid.Items)
            {
                var chBx = firstCol.GetCellContent(item) as CheckBox;
                if (chBx == null || chBx.Visibility != Visibility.Visible)
                {
                    continue;
                }
                chBx.IsChecked = chkSelectAll.IsChecked;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBtn_Click(Object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAndUpdateProject addDialog = new AddAndUpdateProject();
            bool? res = addDialog.ShowDialog();
            if (res != null && res == true)
            {
                members = projectManager.GetAllData();
                membersDataGrid.ItemsSource = members;
            }
        }

        private void ButtonView_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

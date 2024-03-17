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
using DTO;
using DAL;
using System.Diagnostics.Metrics;
using System.Data;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DAL_NhanVien nvManager = new DAL_NhanVien();
        DAL_ChuyenMon cmManager = new DAL_ChuyenMon();
        public MainWindow()
        {
            InitializeComponent();
            datagrid.ItemsSource = nvManager.GetAllData();
            foreach (DataRow row in cmManager.GetAllData().Rows)
            {
                CM.Items.Add(row[1].ToString()) ;
            }    
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int level;
            level = int.TryParse(LVL.Text, out level) ? level : -1;
            DTO_NhanVien nhanvien = new DTO_NhanVien("", TENNV.Text, EMAIL.Text, PHONE.Text, DOB.Text, level, cmManager.ConvertNametoID(CM.Text), NOTE.Text);
            nvManager.AddData(nhanvien);
            this.datagrid.ItemsSource = nvManager.GetAllData();
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid.SelectedItems.Count > 0)
            {
                DTO_NhanVien member = new DTO_NhanVien();
                foreach (var obj in datagrid.SelectedItems)
                {
                    member = obj as DTO_NhanVien;
                    MANV.Text = member.MANV;
                    TENNV.Text = member.TENNV;
                    PHONE.Text = member.PHONE;
                    EMAIL.Text = member.EMAIL;
                    CM.Text = cmManager.ConvertIDtoName(member.MACM);
                    NOTE.Text = member.GHICHU;
                    LVL.Text = member.LEVEL.ToString();
                    DOB.Text = member.NGAYSINH;
                }
            }
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DAL_NhanVien dal = new DAL_NhanVien();
            (bool, string) res = dal.DeleteByID(MANV.Text);
            if (res.Item1)
                Console.Write(res.Item2);
            else Console.Write(res.Item2);

            this.datagrid.ItemsSource = nvManager.GetAllData();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DTO_NhanVien nhanvien = new DTO_NhanVien(MANV.Text, TENNV.Text, EMAIL.Text, PHONE.Text, DOB.Text, int.Parse(LVL.Text), cmManager.ConvertNametoID(CM.Text), NOTE.Text);
            nvManager.SetData(nhanvien);
            this.datagrid.ItemsSource = nvManager.GetAllData();
        }
    }
}

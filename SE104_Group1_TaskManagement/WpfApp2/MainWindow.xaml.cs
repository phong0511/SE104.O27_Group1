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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void button_click()
        {
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DTO_NhanVien nhanvien = new DTO_NhanVien();
            DAL_NhanVien add = new DAL_NhanVien();
            add.Add(nhanvien);
        }
    }
}

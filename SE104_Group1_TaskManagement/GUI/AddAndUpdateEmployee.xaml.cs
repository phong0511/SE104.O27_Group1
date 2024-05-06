using BUS;
using DTO;
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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for AddAndUpdateEmployee.xaml
    /// </summary>
    public partial class AddAndUpdateEmployee : Window
    {
        BUS_NhanVien nvManager = new BUS_NhanVien();
        BUS_TaiKhoan tkManager = new BUS_TaiKhoan();
        public AddAndUpdateEmployee(DTO_NhanVien initializeNV = null)
        {
            
            InitializeComponent();
            qhText.Items.Add(1);
            qhText.Items.Add((2));
            qhText.Items.Add((3));
            qhText.Items.Add((4));
            if (initializeNV != null)
            {
                wTitle.Text = "SỬA NHÂN VIÊN";
                ButtonAddNew.Visibility = Visibility.Hidden;
                ButtonUpdate.Visibility = Visibility.Visible;
                manvText.Text = initializeNV.MANV;
                tennvText.Text = initializeNV.TENNV;
                cmText.Text = initializeNV.MACM;
                levelText.Text = initializeNV.LEVEL.ToString();
                dobText.Text = initializeNV.NGAYSINH;
                emailText.Text = initializeNV.EMAIL;
                phoneText.Text = initializeNV.PHONE;
                noteText.Text = initializeNV.GHICHU;
                
            }    
        }

        private void ButtonAddNew_Click(object sender, RoutedEventArgs e)
        {
            DTO_NhanVien newNV = new DTO_NhanVien();
            newNV.TENNV = tennvText.Text;
            newNV.MACM = cmText.Text;
            int level = -1;
            int.TryParse(levelText.Text, out level);
            newNV.LEVEL=level;
            newNV.NGAYSINH = dobText.Text;
            newNV.EMAIL = emailText.Text;
            newNV.PHONE = phoneText.Text;
            newNV.GHICHU = noteText.Text;
            (bool, string) res = nvManager.AddData(newNV);
            
            if (res.Item1 == true)
            {
                DTO_TaiKhoan tk = new DTO_TaiKhoan(qhText.Text, newNV.EMAIL, newNV.PHONE, res.Item2);
                tkManager.Register(tk);
                MessageBox.Show("Thêm nhân viên thành công!");
                this.DialogResult = true;
                this.Close();
            }    
            else
            {
                MessageBox.Show(res.Item2);
            }    
            
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            DTO_NhanVien nv = new DTO_NhanVien();
            nv.MANV = manvText.Text;
            nv.TENNV = tennvText.Text;
            nv.MACM = cmText.Text;
            int level = -1;
            int.TryParse(levelText.Text, out level);
            nv.LEVEL = level;
            nv.NGAYSINH = dobText.Text;
            nv.EMAIL = emailText.Text;
            nv.PHONE = phoneText.Text;
            nv.GHICHU = noteText.Text;
            (bool, string) res = nvManager.SuaNhanVien(nv);

            if (res.Item1 == true)
            {
                MessageBox.Show("Sửa nhân viên thành công!");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(res.Item2);
            }
        }
    }
}

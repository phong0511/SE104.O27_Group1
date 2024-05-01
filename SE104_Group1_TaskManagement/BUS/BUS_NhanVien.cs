using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BUS
{
    public class BUS_NhanVien
    {
        public void ThemNhanVien(DTO_NhanVien nhanVienMoi)
        {
            if(DAL_NhanVien.ThemNhanVien(nhanVienMoi) == 0)
            {
                MessageBox.Show("Them thanh cong");
            }
            else
            {

            }

        }
    }
}

using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BUS
{
    public class BUS_NhanVien
    {
        public static BindingList<DTO_NhanVien> TatCaNhanVien()
        {
            BindingList<DTO_NhanVien> result = new BindingList<DTO_NhanVien>();
            DataTable dsNhanVien = DAL_NhanVien.DanhSachNhanVien();

            for (int i = 0; i < dsNhanVien.Rows.Count; i++)
            {
                //DTO_NhanVien temp = new DTO_NhanVien();
                //temp.MANV = dsNhanVien.Rows[i]["MANV"].ToString();
                //temp.EMAIL = dsNhanVien.Rows[i]["EMAIL"].ToString();
                //temp.PHONE = dsNhanVien.Rows[i]["PHONE"].ToString();
                //temp.LEVEL = int.Parse(dsNhanVien.Rows[i]["LEVEL"].ToString());
                //temp.NGAYSINH = dsNhanVien.Rows[i]["NGAYSINH"].ToString();
                //temp.MACM = dsNhanVien.Rows[i]["MACM"].ToString();
                //temp.GHICHU = dsNhanVien.Rows[i]["GHICHU"].ToString();
                //result.Add(temp);
            }
            return result;
        }

        //Tra ve 0 neu them, xoa, sua nhan vien = 0, tra ve -1 neu them, xoa, sua nhan vien that bai
        public static void ThemNhanVien(DTO_NhanVien nhanVienMoi)
        {
            if (DAL_NhanVien.ThemNhanVien(nhanVienMoi) == 0)
            {
                MessageBox.Show("Them thanh cong");
            }
            else
            {
                MessageBox.Show("Them that bai");
            }

        }

        public static void XoaNhanVien(DTO_NhanVien nhanVienCanXoa)
        {
            if (DAL_NhanVien.ThemNhanVien(nhanVienCanXoa) == 0)
            {
                MessageBox.Show("Xoa thanh cong");
            }
            else
            {
                MessageBox.Show("Xoa that bai");
            }
        }

        public static void SuaNhanVien(DTO_NhanVien nhanVienCanSua)
        {
            if (DAL_NhanVien.ThemNhanVien(nhanVienCanSua) == 0)
            {
                MessageBox.Show("Them thanh cong");
            }
            else
            {
                MessageBox.Show("Them that bai");
            }

        }
    }
}

using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static (bool, string) ThemNhanVien(DTO_NhanVien nhanVienMoi)
        {
            (bool result, string message) = IsValidStaffInfo(nhanVienMoi);
            if(result == true)
            {
                return IsValidStaffInfo(nhanVienMoi);       
            }
            else
            {
                if (DAL_NhanVien.ThemNhanVien(nhanVienMoi) == 0)
                {
                    return (true, "Them thanh cong");
                }
                else
                {
                    return (false, "Them that bai") ;
                }
            }
        }

        public static (bool, string) XoaNhanVien(DTO_NhanVien nhanVienCanXoa)
        {
            (bool result, string message) = IsValidStaffInfo(nhanVienCanXoa);
            if (result == true)
            {
                return IsValidStaffInfo(nhanVienCanXoa);
            }
            else
            {
                if (DAL_NhanVien.XoaNhanVien(nhanVienCanXoa) == 0)
                {
                    return (true, "Them thanh cong");
                }
                else
                {
                    return (false, "Them that bai");
                }
            }
        }

        public static (bool, string) SuaNhanVien(DTO_NhanVien nhanVienCanSua)
        {
            (bool result, string message) = IsValidStaffInfo(nhanVienCanSua);
            if (result == true)
            {
                return IsValidStaffInfo(nhanVienCanSua);
            }
            else
            {
                if (DAL_NhanVien.SuaNhanVien(nhanVienCanSua) == 0)
                {
                    return (true, "Them thanh cong");
                }
                else
                {
                    return (false, "Them that bai");
                }
            }
        }

        //Check staff info 
        public static (bool, string) IsValidStaffInfo(DTO_NhanVien nv)
        {
            if (nv == null)
                return (false, "Thong tin khong hop le");
            if (!IsValidName(nv.TENNV))
                return (false, "Ten khong hop le!");
            if (!IsValidEmail(nv.EMAIL))
                return (false, "Email khong hop le!");
            if (!IsValidPhoneNumber(nv.PHONE))
                return (false, "So dien thoai khong hop le!");
            if (!IsValidBirthDay(DateTime.Now))
                return (false, "Ngay sinh khong hop le");
            return (true, "Thong tin hop le");

        }

        //Check Name
        private static bool IsValidName(string name)
        {
            if(name == null)
                return false;
            else
            {
                foreach (char c in name)
                {
                    if (!char.IsLetter(c))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        

        //Check Email
        public static bool IsValidEmail(string email)
        {
            if(email == null)
                return false;
            else
            {
                // Mẫu regex để kiểm tra định dạng email
                string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

                // Kiểm tra xem chuỗi khớp với mẫu regex hay không
                bool isMatch = Regex.IsMatch(email, pattern);

                return isMatch;
            }
        }

        //Check phone
        public static bool IsValidPhoneNumber(string soDienThoai)
        {
            if(soDienThoai == null)
                return false;
            else
            {
                // Mẫu regex để kiểm tra định dạng số điện thoại
                string pattern = @"^0\d{9}$";

                // Kiểm tra xem chuỗi khớp với mẫu regex hay không
                bool isMatch = Regex.IsMatch(soDienThoai, pattern);

                return isMatch;
            }
        }
        //Check birthday
        public static bool IsValidBirthDay(DateTime ngaySinh)
        {
            if(ngaySinh == null || ngaySinh >= DateTime.Now)
                return false;
            else
                return true;
        }
    }
}

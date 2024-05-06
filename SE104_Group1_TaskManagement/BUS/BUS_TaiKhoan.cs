using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BUS
{
    public class BUS_TaiKhoan
    {
        //--Duoc goi khi event handling cua button dang nhap duoc goi
        //--Kiem tra tai khoan co ton tai hay khong, neu ton tai thi tra ve mot doi tuong cua lop DTO_NhanVien
        //
        DAL_TaiKhoan dalTK = new DAL_TaiKhoan();
        public DTO_TaiKhoan Login(DTO_TaiKhoan tk)
        {
            return dalTK.CheckLogicDTO(tk);
            //Tra ve mot doi tuong cua lop DTO_NhanVien neu tk co ton tai
            //Tra ve mot doi tuong thuoc lop DTO_NhanVien co ma nhan vien = -1 neu tai khoan khong ton tai
        }

        public string Register(DTO_TaiKhoan tk)
        {
            return dalTK.TaoMoiTaiKhoan(tk); //Chuyen ham tao moi tai khoan thanh static 
            //Tra ve mot instance cua DTO_NhanVien voi cac gia tri da duoc khoi tao
        }

        public (string, DTO_TaiKhoan) ChangeInfo(string email, string oldPass, string newPass) 
        {
            return (dalTK.ChangePassword(email, oldPass, newPass));
            //Tra ve instance moi cua DTO_NhanVien
        }

        //Cac ham  kiem tra
        //Kiem tra ten dang nhap
        public bool IsValidName (string name)
        {
            if(name == null)
            {
                return false;
            }
            else
            {
                foreach(char c in name)
                {
                    if(!char.IsLetter(c) || char.IsWhiteSpace(c))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        //Kiem tra mat khau
        public bool IsValidPassword (string password) 
        { 
            if(password == null)
            {
                return false;   
            }
            else
            {
                string pattern = @"^[A-Za-z0-9]{6,}$";
                return Regex.IsMatch(password, pattern);
            }
        }




    }
}

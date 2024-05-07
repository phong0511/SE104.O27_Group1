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
        public (DTO_TaiKhoan? , string) Login(DTO_TaiKhoan tk)
        {
            if (!IsValidEmail(tk.EMAIL))
                return (null, "Invalid_email");
            else if (!IsValidPassword(tk.PASS))
                return (null, "invalid_password");
            else
            {
                return (dalTK.CheckLogicDTO(tk), "valid_info");
            }
        }

        public (string ,DTO_TaiKhoan?) ChangeInfo(string email, string oldPass, string newPass) 
        {
            if(IsValidEmail(email))
            {
                return ("Invalid_email", null);
            }
            else if(IsValidPassword(oldPass) || IsValidPassword(newPass)) 
            {
                return ("Invalid_password", null);
            }
            return dalTK.ChangePassword(email, oldPass, newPass);
            //Tra ve instance moi cua DTO_NhanVien
        }

        //Cac ham  kiem tra
        //Kiem tra ten dang nhap
        public bool IsValidEmail(string email)
        {
            if (email == null)
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

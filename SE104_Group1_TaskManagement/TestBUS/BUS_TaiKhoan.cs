using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace TestBUS
{
    public class BUS_TaiKhoan
    {
        TaiKhoanAcess tkAccess = new TaiKhoanAcess();
        public string CheckLogic(DTO_TaiKhoan taikhoan)
        {
            // Kiểm tra nghiệp vụ
            if (taikhoan.EMAIL == "")
            {
                return "requeid_taikhoan";
            }

            if (taikhoan.PASS == "")
            {
                return "requeid_password";
            }

            string info = tkAccess.CheckLogic(taikhoan);
            return info;
        }
    }
}

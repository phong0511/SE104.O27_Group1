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
        public string TaoTaiKhoan(DTO_TaiKhoan taiKhoan)
        {
            // Kiểm tra tính hợp lệ của thông tin tài khoản nếu cần
            if (!IsValidAccount(taiKhoan))
            {
                return "Thông tin tài khoản không hợp lệ!";
            }

            // Tạo một instance của lớp DAL_TaiKhoan
            TaoTaiKhoan dalTaiKhoan = new TaoTaiKhoan();
            
            // Gọi phương thức tạo tài khoản trong lớp DAL_TaiKhoan
            string result = dalTaiKhoan.TaoMoiTaiKhoan(taiKhoan);

            // Trả về kết quả từ phương thức trong lớp DAL_TaiKhoan
            return result;
        }

        private bool IsValidAccount(DTO_TaiKhoan taiKhoan)
        {
            // Thực hiện các kiểm tra tính hợp lệ của thông tin tài khoản
            // Ví dụ: kiểm tra độ dài của mật khẩu, định dạng email, v.v.
            // Trả về true nếu thông tin hợp lệ, ngược lại trả về false
            // Bạn có thể cải tiến hàm này tùy theo yêu cầu của ứng dụng của bạn
            return true;
        }
    }
}

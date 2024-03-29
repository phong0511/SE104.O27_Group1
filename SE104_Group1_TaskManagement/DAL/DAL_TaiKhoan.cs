using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class DatabaseAccess : BaseClass
    {

        public string CheckLogicDTO(DTO_TaiKhoan taikhoan)
        {
            string user = null;
            // Hàm connect tới CSDL 
            //SqlConnection conn = BaseClass;
            conn.Open();
            SqlCommand command = new SqlCommand("proc_logic", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@user", taikhoan.EMAIL);
            command.Parameters.AddWithValue("@pass", taikhoan.PASS);
            // Kiểm tra quyền các bạn thêm 1 cái parameter...
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user = reader.GetString(0);
                }
                reader.Close();
                conn.Close();
            }
            else
            {
                reader.Close();
                conn.Close();
                return "Tài khoản hoặc mật khẩu không chính xác!";
            }

            return user;
        }
    }
    public class TaiKhoanAcess: DatabaseAccess
    {
        public string CheckLogic(DTO_TaiKhoan taikhoan)
        {
            string info = CheckLogicDTO(taikhoan);
            return info;
        }
    }
    public class TaoTaiKhoan : BaseClass
    {
        public string TaoMoiTaiKhoan(DTO_TaiKhoan taiKhoan)
        {
            try
            {
                conn.Open();


                // Truy vấn để lấy mã nhân viên mới
                string query = "SELECT MAX(CAST(SUBSTRING(MANV, 3, LEN(MANV) - 2) AS INT)) FROM NHANVIEN";
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();
                int newManv = 1;

                if (result != DBNull.Value)
                {
                    newManv = Convert.ToInt32(result) + 1;
                }

                // Tạo mã nhân viên mới
                string newManvString = "NV" + newManv.ToString("0000");

                // Thêm mã nhân viên vào đối tượng tài khoản
                taiKhoan.MANV = newManvString;
           
                // Tạo tài khoản

                SqlCommand command = new SqlCommand("proc_tao_tai_khoan", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@maqh", taiKhoan.MAQH);
                command.Parameters.AddWithValue("@email", taiKhoan.EMAIL);
                command.Parameters.AddWithValue("@pass", taiKhoan.PASS);
                command.Parameters.AddWithValue("@manv", taiKhoan.MANV);
             

                command.ExecuteNonQuery();

                return "Tạo tài khoản thành công!";
            }
            catch (SqlException ex)
            {
                return "Lỗi khi tạo tài khoản: " + ex.Message;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
    public class DoiMatKhau : BaseClass
    {
        public string ChangePassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("proc_change_password", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@OldPassword", oldPassword);
                command.Parameters.AddWithValue("@NewPassword", newPassword);
                command.ExecuteNonQuery();
                return "Đổi mật khẩu thành công!";
            }
            catch (SqlException ex)
            {
                return "Lỗi khi đổi mật khẩu: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

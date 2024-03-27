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

}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class DAL_NhanVien:BaseClass
    {
        public bool Add(DTO_NhanVien nhanVien)
        {
            try
            {
                conn.Open();
                // id declaration
                string idString = "SELECT TOP 1 MANV FROM NHANVIEN ORDER BY MANV DESC";
                var command = new SqlCommand(idString, conn);
                string id = (string)command.ExecuteScalar();
                int number = int.Parse(id.Substring(id.Length - 4)) + 1;
                string manv = "NV" + number.ToString("0000");


                string queryString = "INSERT INTO NHANVIEN VALUES (@manv, @hoten, @email, @sdt, @ngaysinh, @lvl, @macm, @ghichu)";

                command = new SqlCommand(
                    queryString,
                    conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@manv", manv);
                command.Parameters.AddWithValue("@hoten", nhanVien.TENNV);
                command.Parameters.AddWithValue("@email", nhanVien.EMAIL);
                command.Parameters.AddWithValue("@sdt", nhanVien.PHONE);
                command.Parameters.AddWithValue("@ngaysinh", nhanVien.NGAYSINH);
                command.Parameters.AddWithValue("@lvl", nhanVien.LEVEL);
                command.Parameters.AddWithValue("@macm", nhanVien.MACM);
                command.Parameters.AddWithValue("@ghichu", nhanVien.GHICHU);

                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return true;
                }

                conn.Close();
                return false;
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
                conn.Close();
                return false;
            }
        }
    }
}

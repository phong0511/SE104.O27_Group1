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
    public class DAL_QuyenHan : BaseClass
    {
        public (bool, string) ThemQuyenHan(DTO_QuyenHan quyenHan)
        {
            try
            {
                conn.Open();
                string queryString = "INSERT INTO QUYENHAN (MAQH, TENQH) VALUES (@maqh, @tenqh)";
                var command = new SqlCommand(queryString, conn);

                command.Parameters.AddWithValue("@maqh", quyenHan.MAQH);
                command.Parameters.AddWithValue("@tenqh", quyenHan.TENQH);

                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Thêm quyền hạn thành công!");
                }

                conn.Close();
                return (false, "Thêm quyền hạn không thành công!");
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
                conn.Close();
                return (false, e.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return (false, ex.Message);
            }
        }

        public (bool, string) SuaQuyenHan(DTO_QuyenHan quyenHan)
        {
            try
            {
                conn.Open();
                string queryString = "UPDATE QUYENHAN SET TENQH=@tenqh WHERE MAQH=@maqh";
                var command = new SqlCommand(queryString, conn);

                command.Parameters.AddWithValue("@maqh", quyenHan.MAQH);
                command.Parameters.AddWithValue("@tenqh", quyenHan.TENQH);

                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Sửa quyền hạn thành công.");
                }

                conn.Close();
                return (false, "Sửa quyền hạn không thành công.");
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
                conn.Close();
                return (false, e.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return (false, ex.Message);
            }
        }

        public (bool, string) XoaQuyenHan(string maqh)
        {
            try
            {
                conn.Open();
                string queryString = "DELETE FROM QUYENHAN WHERE MAQH=@maqh";
                var command = new SqlCommand(queryString, conn);

                command.Parameters.AddWithValue("@maqh", maqh);

                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Xóa quyền hạn thành công.");
                }

                conn.Close();
                return (false, "Xóa quyền hạn không thành công.");
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
                conn.Close();
                return (false, e.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return (false, ex.Message);
            }
        }

        public DTO_QuyenHan LayQuyenHan(string maqh)
        {
            try
            {
                conn.Open();
                string queryString = "SELECT MAQH, TENQH FROM QUYENHAN WHERE MAQH=@maqh";
                var command = new SqlCommand(queryString, conn);

                command.Parameters.AddWithValue("@maqh", maqh);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DTO_QuyenHan quyenHan = new DTO_QuyenHan();
                    quyenHan.MAQH = reader.GetString(0);
                    quyenHan.TENQH = reader.GetString(1);

                    reader.Close();
                    conn.Close();
                    return quyenHan;
                }

                reader.Close();
                conn.Close();
                return null;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return null;
            }
        }

        public List<DTO_QuyenHan> LayTatCaQuyenHan()
        {
            List<DTO_QuyenHan> danhSachQuyenHan = new List<DTO_QuyenHan>();
            try
            {
                conn.Open();
                string queryString = "SELECT MAQH, TENQH FROM QUYENHAN";
                var command = new SqlCommand(queryString, conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DTO_QuyenHan quyenHan = new DTO_QuyenHan();
                    quyenHan.MAQH = reader.GetString(0);
                    quyenHan.TENQH = reader.GetString(1);

                    danhSachQuyenHan.Add(quyenHan);
                }

                reader.Close();
                conn.Close();
                return danhSachQuyenHan;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
                conn.Close();
                return danhSachQuyenHan;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return danhSachQuyenHan;
            }
        }
    }
}
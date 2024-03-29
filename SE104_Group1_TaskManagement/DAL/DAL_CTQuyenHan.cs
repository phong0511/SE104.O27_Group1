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
    public class DAL_CTQuyenHan : BaseClass
    {
        public (bool, string) ThemCTQuyenHan(DTO_CTQuyenHan ctQuyenHan)
        {
            try
            {
                conn.Open();
                string queryString = "INSERT INTO CTQUYENHAN (MACT, MAQH, ACTION) VALUES (@mact, @maqh, @action)";
                var command = new SqlCommand(queryString, conn);

                command.Parameters.AddWithValue("@mact", ctQuyenHan.MACT);
                command.Parameters.AddWithValue("@maqh", ctQuyenHan.MAQH);
                command.Parameters.AddWithValue("@action", ctQuyenHan.ACTION);

                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Thêm chi tiết quyền hạn thành công!");
                }

                conn.Close();
                return (false, "Thêm chi tiết quyền hạn không thành công!");
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

        public (bool, string) SuaCTQuyenHan(DTO_CTQuyenHan ctQuyenHan)
        {
            try
            {
                conn.Open();
                string queryString = "UPDATE CTQUYENHAN SET MAQH=@maqh, ACTION=@action WHERE MACT=@mact";
                var command = new SqlCommand(queryString, conn);

                command.Parameters.AddWithValue("@mact", ctQuyenHan.MACT);
                command.Parameters.AddWithValue("@maqh", ctQuyenHan.MAQH);
                command.Parameters.AddWithValue("@action", ctQuyenHan.ACTION);

                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Sửa chi tiết quyền hạn thành công.");
                }

                conn.Close();
                return (false, "Sửa chi tiết quyền hạn không thành công.");
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

        public (bool, string) XoaCTQuyenHan(string mact)
        {
            try
            {
                conn.Open();
                string queryString = "DELETE FROM CTQUYENHAN WHERE MACT=@mact";
                var command = new SqlCommand(queryString, conn);

                command.Parameters.AddWithValue("@mact", mact);

                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Xóa chi tiết quyền hạn thành công.");
                }

                conn.Close();
                return (false, "Xóa chi tiết quyền hạn không thành công.");
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

        public List<DTO_CTQuyenHan> LayCTQuyenHanTheoMaQH(string maqh)
        {
            List<DTO_CTQuyenHan> danhSachCTQuyenHan = new List<DTO_CTQuyenHan>();
            try
            {
                conn.Open();
                string queryString = "SELECT MACT, MAQH, ACTION FROM CTQUYENHAN WHERE MAQH=@maqh";
                var command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@maqh", maqh);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DTO_CTQuyenHan ctQuyenHan = new DTO_CTQuyenHan();
                    ctQuyenHan.MACT = reader.GetString(0);
                    ctQuyenHan.MAQH = reader.GetString(1);
                    ctQuyenHan.ACTION = reader.GetString(2);

                    danhSachCTQuyenHan.Add(ctQuyenHan);
                }

                reader.Close();
                conn.Close();
                return danhSachCTQuyenHan;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
                conn.Close();
                return danhSachCTQuyenHan;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return danhSachCTQuyenHan;
            }
        }
    }
}

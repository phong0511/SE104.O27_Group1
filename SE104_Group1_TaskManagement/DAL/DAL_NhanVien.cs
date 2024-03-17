using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public (bool, string) SetData(DTO_NhanVien nv_new)
        {
            try
            {
                conn.Open();
                string queryString = "UPDATE NHANVIEN SET HOTEN=@hoten, EMAIL=@email, SODT=@sdt, NGSINH=@ngaysinh, LVL=@lvl, MACM=@macm, GHICHU=@ghichu WHERE MANV=@manv";
                var command = new SqlCommand(
                    queryString,
                    conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@manv", nv_new.MANV);
                command.Parameters.AddWithValue("@hoten", nv_new.TENNV);
                command.Parameters.AddWithValue("@email", nv_new.EMAIL);
                command.Parameters.AddWithValue("@sdt", nv_new.PHONE);
                command.Parameters.AddWithValue("@ngaysinh", nv_new.NGAYSINH);
                command.Parameters.AddWithValue("@lvl", nv_new.LEVEL);
                command.Parameters.AddWithValue("@macm", nv_new.MACM);
                command.Parameters.AddWithValue("@ghichu", nv_new.GHICHU);
                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Sửa thành công.");
                }

                conn.Close();
                return (false, "Sửa không thành công.");
            }
            catch (SqlException e)
            {
                Console.Write(e.Message);
                conn.Close();
                return (false, e.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                conn.Close();
                return (false, ex.Message);
            }
        }
        public (bool, string) DeleteByID (string MANV)
        {
            try
            {
                conn.Open();
                string queryString = "DELETE FROM NHANVIEN WHERE MANV='" + MANV + "'";


                var command = new SqlCommand(
                    queryString,
                    conn);
                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Xóa nhân viên thành công.");
                }
                conn.Close();
                return (false, "Xóa nhân viên không thành công.");
            }
            catch (SqlException e)
            {
                Console.Write(e.Message);
                conn.Close();
                return (false, e.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                conn.Close();
                return (false, ex.Message);
            }
        }
        public ObservableCollection<DTO_NhanVien> GetAllData()
        {
            ObservableCollection<DTO_NhanVien> collection = new ObservableCollection<DTO_NhanVien> ();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string queryString = "SELECT MANV as 'Mã số', HOTEN as 'Họ tên', EMAIL as 'Email', SODT as 'Số điện thoại',  " +
                    "CONVERT(VARCHAR(10), NGSINH, 104) as 'Ngày sinh', " +
                    "LVL as 'Level', TENCM as 'Chuyên môn', GHICHU as 'Ghi chú' " +
                    "FROM NHANVIEN LEFT JOIN CHUYENMON ON NHANVIEN.MACM = CHUYENMON.MACM";

                var command = new SqlCommand(
                    queryString,
                    conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                foreach (DataRow row in dt.Rows)
                {
                    DTO_NhanVien nhanVien = new DTO_NhanVien();
                    nhanVien.MANV = row[dt.Columns[0]].ToString();
                    nhanVien.TENNV = row[dt.Columns[1]].ToString();
                    nhanVien.EMAIL = row[dt.Columns[2]].ToString();
                    nhanVien.PHONE = row[dt.Columns[3]].ToString();
                    nhanVien.NGAYSINH = row[dt.Columns[4]].ToString();
                    int level = -1;
                    int.TryParse(row[dt.Columns[5]].ToString(), out level);
                    nhanVien.LEVEL = level;
                    nhanVien.MACM = row[dt.Columns[6]].ToString();
                    nhanVien.GHICHU = row[dt.Columns[7]].ToString();

                    collection.Add(nhanVien);
                }
                return collection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conn.Close();
                return collection;
            }
            
        }
        public (bool, string) AddData (DTO_NhanVien nhanVien)
        {
            try
            {
                string manv = getCrnID();

                conn.Open();
                string queryString = "INSERT INTO NHANVIEN VALUES (@manv, @hoten, @email, @sdt, CONVERT(smalldatetime,@ngaysinh, 104), @lvl, @macm, @ghichu)";
                var command = new SqlCommand(
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
                    return (true, "Thêm nhân viên thành công!");
                }

                conn.Close();
                return (false, "Thêm không thành công!");
            }
            catch (SqlException e)
            {
                Console.Write(e.Message);
                conn.Close();
                return (false, e.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                conn.Close();
                return (false, ex.Message);
            }
        }
        string getCrnID()
        {
            try
            {
                conn.Open();
                string idString = "SELECT TOP 1 MANV FROM NHANVIEN ORDER BY MANV DESC";
                var command = new SqlCommand(idString, conn);
                string id = (string)command.ExecuteScalar();
                int number = 0;
                if (id != null)
                {
                    number = int.Parse(id.Substring(id.Length - 4)) + 1;
                }

                conn.Close();
                return "NV" + number.ToString("0000");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conn.Close();
                return "";
            }
        }
    }
}

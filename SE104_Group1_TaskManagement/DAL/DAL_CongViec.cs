﻿using DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_CongViec:BaseClass
    {
        public (bool, string) DeleteByID(string MACV)
        {
            try
            {
                conn.Open();
                string queryString = "UPDATE CONGVIEC SET IsDeleted = 1 WHERE MACV='" + MACV + "'";


                var command = new SqlCommand(
                    queryString,
                    conn);
                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Xóa thành công.");
                }
                conn.Close();
                return (false, "Xóa không thành công.");
            }
            catch (SqlException e)
            {
                Debug.Write(e.ToString());
                conn.Close();
                return (false, e.Message);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                conn.Close();
                return (false, ex.Message);
            }
        }
        public (bool, string) AddData(DTO_CongViec congViec)
        {
            try
            {
                string macv = getCrnID();
                if (macv == "") return (false, "Thêm không thành công!");
                macv = congViec.MADA.Substring(congViec.MADA.Length - 2) + macv;
                conn.Open();
                string queryString = "INSERT INTO CONGVIEC VALUES (@macv, @mada, @macm, @tencv, CONVERT(smalldatetime,@tstart, 104),  CONVERT(smalldatetime,@tend, 104), @ngansach, @dadung, @tiendo, @ycdk, @dk, @isdel)";
                var command = new SqlCommand(
                    queryString,
                    conn);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@macv", macv);
                command.Parameters.AddWithValue("@mada", congViec.MADA);
                command.Parameters.AddWithValue("@macm", congViec.MACM);
                command.Parameters.AddWithValue("@tencv", congViec.TENCV);
                command.Parameters.AddWithValue("@tstart", congViec.TSTART);
                command.Parameters.AddWithValue("@tend", congViec.TEND);
                command.Parameters.AddWithValue("@ngansach", congViec.NGANSACH);
                command.Parameters.AddWithValue("@dadung", congViec.DADUNG);
                command.Parameters.AddWithValue("@tiendo", 0);
                command.Parameters.AddWithValue("@ycdk", congViec.YCDK);
                command.Parameters.AddWithValue("@dk", congViec.TEPDK);
                command.Parameters.AddWithValue("@isdel", 0);
                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return (true, "Thêm thành công!");
                }

                conn.Close();
                return (false, "Thêm không thành công!");
            }
            catch (SqlException e)
            {
                Debug.Write(e.ToString());
                conn.Close();
                return (false, e.Message);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                conn.Close();
                return (false, ex.Message);
            }
        }
        public (bool, string) SetData(DTO_CongViec cv_new)
        {
            try
            {
                conn.Open();
                string queryString = "UPDATE CONGVIEC SET MADA=@mada, MACM@macm, TENCV=@tencv, TSTART=CONVERT(smalldatetime,@tstart, 104),  TEND=CONVERT(smalldatetime,@tend, 104), NGANSACH=@ngansach, DADUNG=@dadung, TIENDO=@tiendo, YCDINHKEM=@ycdk, TEPDINHKEM=@dk WHERE MACV=@macv";
                var command = new SqlCommand(
                    queryString,
                    conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@macv", cv_new.MACV);
                command.Parameters.AddWithValue("@mada", cv_new.MADA);
                command.Parameters.AddWithValue("@macm", cv_new.MACM);
                command.Parameters.AddWithValue("@tencv", cv_new.TENCV);
                command.Parameters.AddWithValue("@tstart", cv_new.TSTART);
                command.Parameters.AddWithValue("@tend", cv_new.TEND);
                command.Parameters.AddWithValue("@ngansach", cv_new.NGANSACH);
                command.Parameters.AddWithValue("@dadung", cv_new.DADUNG);
                command.Parameters.AddWithValue("@tiendo", cv_new.TIENDO);
                command.Parameters.AddWithValue("@ycdk", cv_new.YCDK);
                command.Parameters.AddWithValue("@dk", cv_new.TEPDK);
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
                Debug.Write(e.ToString());
                conn.Close();
                return (false, e.Message);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                conn.Close();
                return (false, ex.Message);
            }
        }
        public DTO_CongViec GetByID(string MACV)
        {

            try
            {
                DTO_CongViec res = new DTO_CongViec();
                conn.Open();
                string queryString = "SELECT MACV, MADA, MACM, TENCV, CONVERT(smalldatetime,TSTART, 104),  CONVERT(smalldatetime,TEND, 104), NGANSACH, DADUNG, TIENDO, YCDINHKEM, TEPDINHKEM FROM CONGVIEC" +
                    " WHERE MACV=@macv AND ISDELETED <>1";

                var command = new SqlCommand(
                    queryString,
                    conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@macv", MACV);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                res.MACV = reader.GetString(0);
                res.MADA = reader.GetString(1);
                res.MACM = reader.GetString(2);
                res.TENCV = reader.GetString(3);
                res.TSTART = reader.GetString(4);
                res.TEND = reader.GetString(5);
                res.NGANSACH = reader.GetInt32(6);
                res.DADUNG = reader.GetInt32(7);
                res.TIENDO = reader.GetInt16(8);
                res.YCDK = reader.GetString(9);
                res.TEPDK = reader.GetString(10);
                reader.Close();
                conn.Close();
                return res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return null;
            }
        }

        public DataTable GetByName(string TENCV)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string queryString = "SELECT MACV, MADA, MACM, TENCV, CONVERT(smalldatetime,TSTART, 104),  CONVERT(smalldatetime,TEND, 104), NGANSACH, DADUNG, TIENDO, YCDINHKEM, TEPDINHKEM FROM CONGVIEC" +
                    " WHERE TENCV LIKE @tencv AND ISDELETED <>1";

                var command = new SqlCommand(
                    queryString,
                    conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@tencv", TENCV);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return dt;
            }
        }

        public DataTable GetByTienDo(int progress)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string queryString = "SELECT MACV, MADA, MACM, TENCV, CONVERT(smalldatetime,TSTART, 104),  CONVERT(smalldatetime,TEND, 104), NGANSACH, DADUNG, TIENDO, YCDINHKEM, TEPDINHKEM FROM CONGVIEC" +
                    " WHERE TIENDO >= @tiendo AND ISDELETED <>1";

                var command = new SqlCommand(
                    queryString,
                    conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@tiendo", progress);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return dt;
            }
        }
        public DataTable GetByMaDA(string MADA)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string queryString = "SELECT MACV, MADA, MACM, TENCV, CONVERT(smalldatetime,TSTART, 104),  CONVERT(smalldatetime,TEND, 104), NGANSACH, DADUNG, TIENDO, YCDINHKEM, TEPDINHKEM FROM CONGVIEC" +
                    " WHERE MADA=@mada AND ISDELETED <>1";

                var command = new SqlCommand(
                    queryString,
                    conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@mada", MADA);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return dt;
            }
        }

        public DataTable GetByTStartLimit(string TStartLimit) //lấy dự án bắt đầu sau mốc thời gian TStart
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string queryString = "SELECT MACV, MADA, MACM, TENCV, CONVERT(smalldatetime,TSTART, 104),  CONVERT(smalldatetime,TEND, 104), NGANSACH, DADUNG, TIENDO, YCDINHKEM, TEPDINHKEM FROM CONGVIEC" +
                    " WHERE TSTART <= CONVERT(smalldatetime,@tstart, 104) AND ISDELETED <>1";

                var command = new SqlCommand(
                    queryString,
                    conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@tstart", TStartLimit);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return dt;
            }
        }

        public DataTable GetByTEndLimit(string TEndLimit) //lấy dự án kết thúc trước mốc thời gian TEnd
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string queryString = "SELECT MACV, MADA, MACM, TENCV, CONVERT(smalldatetime,TSTART, 104),  CONVERT(smalldatetime,TEND, 104), NGANSACH, DADUNG, TIENDO, YCDINHKEM, TEPDINHKEM FROM CONGVIEC" +
                    " WHERE TEND >= CONVERT(smalldatetime,@tend, 104) AND ISDELETED <>1";

                var command = new SqlCommand(
                    queryString,
                    conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@tend", TEndLimit);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return dt;
            }
        }
        public DataTable GetAllData()
        {
            DataTable dt = new DataTable();
            try
            {

                conn.Open();
                string queryString = "SELECT MACV, MADA, MACM, TENCV, CONVERT(smalldatetime,TSTART, 104),  CONVERT(smalldatetime,TEND, 104), NGANSACH, DADUNG, TIENDO, YCDINHKEM, TEPDINHKEM FROM CONGVIEC WHERE ISDELETED <>1";

                var command = new SqlCommand(
                    queryString,
                    conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return dt;
            }

        }

        string getCrnID()
        {
            try
            {
                conn.Open();
                string idString = "SELECT TOP 1 MACV FROM CONGVIEC ORDER BY MACV DESC";
                var command = new SqlCommand(idString, conn);
                string id = (string)command.ExecuteScalar();
                int number = 0;
                if (id != null)
                {
                    number = int.Parse(id.Substring(id.Length - 2)) + 1;
                }

                conn.Close();
                return number.ToString("00");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                conn.Close();
                return "";
            }
        }
    }
}

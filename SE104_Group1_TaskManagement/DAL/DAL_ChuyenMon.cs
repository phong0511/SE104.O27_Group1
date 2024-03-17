using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_ChuyenMon:BaseClass
    {
        public string ConvertNametoID(string TenCM)
        {
            string macm;
            try
            {
                conn.Open();
                string cmString = "SELECT MACM FROM CHUYENMON WHERE TENCM='" + TenCM + "'";
                var command = new SqlCommand(cmString, conn);
                macm = (string)command.ExecuteScalar();
                if (macm == null)
                { macm = ""; }
                conn.Close();
                return macm;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                macm = "";
                conn.Close();
                return macm;
            }
        }

        public DataTable LoadTenCM()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string queryString = "SELECT TENCM FROM CHUYENMON";
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
                Console.WriteLine(ex.Message);
                conn.Close();
                return dt;
            }
        }
    }
}

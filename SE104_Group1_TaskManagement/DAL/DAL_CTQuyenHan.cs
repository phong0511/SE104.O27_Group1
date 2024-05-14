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
        public bool CheckPermission(string maQH, string action)
        {
            try
            {
                conn.Open();
                string queryString = @"SELECT COUNT(*) 
                                       FROM CT_QUYENHAN 
                                       WHERE MaQH = @maQH AND Action = @action";
                var command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@maQH", maQH);
                command.Parameters.AddWithValue("@action", action);
                int count = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                conn.Close();
                return false;
            }
        }


    }
}

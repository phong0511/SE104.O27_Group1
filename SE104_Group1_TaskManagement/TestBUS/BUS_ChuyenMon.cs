using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace TestBUS
{
    public class BUS_ChuyenMon
    {
        DAL_ChuyenMon dalCM = new DAL_ChuyenMon();
         public DataTable GetAllData()
        {
            return dalCM.GetAllData();
        }
    }
}

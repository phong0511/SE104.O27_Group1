using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBUS
{
    public class BUS_LoaiSK
    {
        DAL_LoaiSK x = new DAL_LoaiSK();
        public (long, long) GetMinMaxByID(string MALSK)
        {
            return x.GetMinMaxByID( MALSK);
        }
    }
}

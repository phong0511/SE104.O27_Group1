using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ChuyenMon
    {
        string _macm;
        string _cm;
        public DTO_ChuyenMon(string macm="", string cm = "")
        {
            MACM = macm;
            TENCM = cm;
        }    
        public string MACM
        { get { return _macm; } set { _macm = value; } }
        public string TENCM
        { get { return _cm; } set { _cm = value; } }
    }
}

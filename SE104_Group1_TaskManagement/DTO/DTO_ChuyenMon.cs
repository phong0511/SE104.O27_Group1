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
        string _inshort;
        public DTO_ChuyenMon(string macm="", string cm = "", string inshort = "")
        {
            MACM = macm;
            TENCM = cm;
            _inshort = inshort;
        }    
        public string MACM
        { get { return _macm; } set { _macm = value; } }
        public string TENCM
        { get { return _cm; } set { _cm = value; } }
        public string INSHORT
        { get { return _inshort; } set { _inshort = value; } }
    }
}

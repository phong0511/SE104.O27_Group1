using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_PhanCong
    {
        string _manv;
        string _macv;

        public DTO_PhanCong(string manv = "", string macv = "")
        {
            _macv = macv;
            _manv = manv;
        }

        public string MANV { get { return _manv; } set { _manv = value; } }
        public string MACV { get { return _macv; } set { _macv = value; } }
    }
}

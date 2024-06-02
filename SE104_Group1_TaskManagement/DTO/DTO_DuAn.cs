using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_DuAn
    {
        string _mada;
        string _malsk;
        string _maowner;
        string _tenda;
        long _ngansach;
        string _tstart;
        string _tend;
        string _stat;
        long _dadung;

        public DTO_DuAn(string mada = "", string malsk = "", string maowner = "", string tenda = "", long ngansach = -1, string tstart = "", string tend = "", string stat = "", long dadung = 0)
        {
            _mada = mada;
            _malsk = malsk;
            _maowner = maowner;
            _tenda = tenda;
            _ngansach = ngansach;
            _tstart = tstart;
            _tend = tend;
            _stat = stat;
            _dadung = dadung;
        }

        public string MADA
        { get { return _mada; } set { _mada = value; } }
        public string MALSK
        {
            get { return _malsk; }
            set { _malsk = value; }
        }
        public string MAOWNER
        { get { return _maowner; } set { _maowner = value; } }
        public string TENDA
        { get { return _tenda; } set { _tenda = value; } }

        public long NGANSACH
        { get { return _ngansach; } set { _ngansach = value; } }

        public string TSTART
        {
            get { return _tstart; }
            set { _tstart = value; }
        }
        public string TEND
        { get { return _tend; } set { _tend = value; } }

        public string STAT
        { get { return _stat; } set { _stat = value; } }

        public long DADUNG
        { get { return _dadung; } set { _dadung = value; } }
    }
}

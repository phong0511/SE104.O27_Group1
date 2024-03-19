﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_LoaiSK
    {
        string _malsk;
        string _tenlsk;
        long _min;
        long _max;

        public DTO_LoaiSK(string malsk="", string tenlsk="", long min=-1, long max=-1)
        {
            _malsk = malsk;
            _tenlsk = tenlsk;
            _min = min;
            _max = max;
        }

        public string MALSK
        { get { return _malsk; } set { _malsk = value; } }
        public string TENLSK
        { get { return _tenlsk; } set { _tenlsk = value; } }
        public long MIN
        { get { return _min; } set { _min = value; } }
        public long MAX
        { get { return _max; } set { _max = value; } }
    }
}
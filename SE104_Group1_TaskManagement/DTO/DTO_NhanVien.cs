using System;

namespace DTO
{
    public class DTO_NhanVien
    {
        private string _manv;
        private string _tennv;
        private string _email;
        private string _phone;
        private string _ngaysinh;
        private int _level;
        private string _cm;
        private string _ghichu;

        public DTO_NhanVien (string manv = "", string tennv = "", string email = "", string phone = "", string ngaysinh = "", int level = -1, string cm = "", string ghichu = "")
        {
            _manv = manv;
            _tennv = tennv;
            _email = email;
            _phone = phone;
            _ngaysinh = ngaysinh;
            _level = level;
            _cm = cm;
            _ghichu = ghichu;
        }

        public string MANV { get { return _manv;} set { _manv = value; } }
        public string TENNV { get { return _tennv;} set {  _tennv = value; } }
        public string EMAIL { get { return _email; } set { _email = value; } }
        public string PHONE { get { return _phone; } set { _phone = value; } }
        public int LEVEL { get { return _level; } set { _level = value; } }
        public string NGAYSINH { get { return _ngaysinh; } set { _ngaysinh = value; } }
        public string CM { get { return _cm; } set { _cm = value; } }
        public string GHICHU { get { return _ghichu; } set { _ghichu = value; } }

    }
}

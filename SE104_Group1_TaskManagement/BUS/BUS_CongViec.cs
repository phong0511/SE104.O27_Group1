using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.AxHost;

namespace BUS
{
    public class BUS_CongViec
    {
        DAL_CongViec dalCV = new DAL_CongViec();
        public BindingList<DTO_CongViec> GetAllData()
        {
            BindingList<DTO_CongViec> result = new BindingList<DTO_CongViec>();
            DataTable dsCongViec = dalCV.GetAllData();

            for (int i = 0; i < dsCongViec.Rows.Count; i++)
            {
                //string _macv;
                DTO_CongViec temp = new DTO_CongViec();
                temp.MADA = dsCongViec.Rows[i]["MADA"].ToString();
                temp.MACM = dsCongViec.Rows[i]["MACM"].ToString();
                temp.TENCV = dsCongViec.Rows[i]["TENCV"].ToString();
                temp.TSTART = dsCongViec.Rows[i]["TSTART"].ToString();
                temp.TEND = dsCongViec.Rows[i]["TEND"].ToString();
                temp.NGANSACH = long.Parse(dsCongViec.Rows[i]["NGANSACH"].ToString());
                temp.DADUNG = long.Parse(dsCongViec.Rows[i]["DADUNg"].ToString());
                temp.TIENDO = int.Parse(dsCongViec.Rows[i]["TIENDO"].ToString());
                temp.YCDK = dsCongViec.Rows[i]["YCDINHKEM"].ToString();
                temp.TEPDK = dsCongViec.Rows[i]["TEPDINHKEM"].ToString();
                result.Add(temp);
            }
            return result;
        }

        //ADD
        public (bool, string) AddData(DTO_CongViec CongViecMoi)
        {
            (bool result, string message) = IsValidProjectInfo(CongViecMoi);
            if (result == false)
            {
                return IsValidProjectInfo(CongViecMoi);
            }
            else
            {
                return (dalCV.AddData(CongViecMoi));
            }
        }

        //EDIT
        public (bool, string) EditProject(DTO_CongViec CongViecCanSua)
        {
            (bool result, string message) = IsValidProjectInfo(CongViecCanSua);
            if (result == false)
            {
                return IsValidProjectInfo(CongViecCanSua);
            }
            else
            {
                return (dalCV.SetData(CongViecCanSua));
            }
        }

        //SET STATUS
        


        //GETBy
        public DTO_CongViec GetByID(string MACV)
        {
            return dalCV.GetByID(MACV);
        }
        public (string, DataTable) GetByName(string name)
        {
            bool result = (IsValidNameProject(name));
            if (result == false)
            {
                return ("Ten du an khong hop le", null);
            }
            else
                return (null, dalCV.GetByName(name));
        }
        public (string, DataTable) GetByTStartLimit(DateTime TStartLimit)
        {
            bool result = (IsValidTSTART(TStartLimit));
            if (result == false)
            {
                return ("Ten du an khong hop le", null);
            }
            else
                return (null, dalDA.GetByTStartLimit(TStartLimit));
        }
        public (string, DataTable) GetByTENDLimit(DateTime TEndLimit)
        {
            bool result = (IsValidTEND(TEndLimit));
            if (result == false)
            {
                return ("Ten du an khong hop le", null);
            }
            else
                return (null, dalDA.GetByTEndLimit(TEndLimit));
        }

        public DataTable FindDA(DTO_CongViec filter)
        {
            return dalDA.GetDataByFilter(filter);
        }

        public DataTable GetByNganSachMoreLess(long NganSachH, long NganSachL)
        {
            return dalDA.GetByNganSachMoreLess(NganSachH, NganSachL);
        }
        public DataTable GetByLoaiSK(string MALSK)
        {
            return dalDA.GetByLoaiSK(MALSK);
        }
        public DataTable GetByOwner(string MAOWNER)
        {
            return dalDA.GetByOwner(MAOWNER);
        }
        public DataTable GetByStat(string STAT)
        {
            return dalDA.GetByStat(STAT);
        }

        //check staff info 
        public static (bool, string) IsValidProjectInfo(DTO_CongViec DA)
        {
            if (DA == null)
                return (false, "Du an khong ton tai");
            if (!IsValidNameProject(DA.TENDA))
                return (false, "Ten du an khong hop le");
            if (!IsValidTSTART(DateTime.Now))
                return (false, "Ngay bat dau khong hop le");
            if (!IsValidTEND(DateTime.Now))
                return (false, "Ngay ket thuc khong hop le");
            return (true, "Thong tin hop le");
        }

        //check Project's name
        private static bool IsValidNameProject(string name)
        {
            if (name == null)
                return false;
            else
            {
                foreach (char c in name)
                {
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        //check start date
        public static bool IsValidTSTART(DateTime NgayBatDau)
        {
            if (NgayBatDau == null || NgayBatDau >= DateTime.Now)
                return false;
            else
                return true;
        }

        //check end date 
        public static bool IsValidTEND(DateTime NgayKetThuc)
        {
            if (NgayKetThuc == null || NgayKetThuc >= DateTime.Now)
                return false;
            else
                return true;
        }
    }
}

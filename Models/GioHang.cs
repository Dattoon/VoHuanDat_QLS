using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;

namespace VoHuanDat_QLS.Models
{
    public class GioHang
    {
        DataClasses1DataContext data = new DataClasses1DataContext("DATTOON\\DATER");
        


        public int iMasach {  get; set; }
        public string sTensach {  get; set; }
        public string sAnhbia { get; set; }

        public Double dDongia { get; set; }

        public int iSoluong {  get; set; }


        public Double dThanhTien {
            get {  return iSoluong * dDongia; }
        }
        public GioHang(int Masach)
        {

            iMasach = Masach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Anhbia;
            dDongia = double.Parse(sach.Giaban.ToString());
            iSoluong = 1;
        }

    }
}
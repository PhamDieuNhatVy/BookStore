using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPTTKHDT.Models
{
    public class GioHang
    {
        WebsiteBanSachEntities data = new WebsiteBanSachEntities();

        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get
            {
                return iSoLuong * dDonGia;
            }
        }

        public GioHang(int ms)
        {
            iMaSach = ms;
            SACH s = data.SACH.Single(n => n.MaSach == iMaSach);
            sTenSach = s.TenSach;
            sAnhBia = s.HinhMinhHoa;
            dDonGia = double.Parse(s.DonGia.ToString());
            iSoLuong = 1;
        }

    }
}
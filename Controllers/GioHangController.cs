using DoAnPTTKHDT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnPTTKHDT.Controllers
{
    public class GioHangController : Controller
    {
        WebsiteBanSachEntities data = new WebsiteBanSachEntities();
        // GET: GioHang

        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                //Khởi tạo giỏ hàng
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }

        public ActionResult ThemGioHang(int ms, string url)
        {
            List<GioHang> listGioHang = LayGioHang();
            //Nếu giỏ hàng đã có sp thì tăng số lượng
            GioHang sp = listGioHang.Find(n => n.iMaSach == ms);
            if (sp == null)
            {
                sp = new GioHang(ms);
                listGioHang.Add(sp);

            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }
        int iTongSoLuong = 0;
        public int getTongSoLuong()
        {
            return iTongSoLuong;
        }
        private int TongSoLuong()
        {

            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.iSoLuong);

            }
            return iTongSoLuong;

        }


        static private double dTongTien = 0;
        public double getTongTien()
        {
            return dTongTien;
        }

        private double TongTien()
        {

            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;

            if (listGioHang != null)
            {
                dTongTien = listGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }

        public ActionResult GioHang()
        {
            List<GioHang> listGioHang = LayGioHang();
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult XoaSPKhoiGiohang(int iMaSach)
        {
            List<GioHang> listGioHang = LayGioHang();
            //Kiem tra sach da co trong Session["GioHang"]
            GioHang sp = listGioHang.SingleOrDefault(n => n.iMaSach == iMaSach);
            if (sp != null)
            {
                listGioHang.RemoveAll(n => n.iMaSach == iMaSach);
                if (listGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("GioHang");

        }

        public ActionResult CapNhatGioHang(int iMaSach, FormCollection f)
        {
            List<GioHang> listGioHang = LayGioHang();
            GioHang sp = listGioHang.SingleOrDefault(n => n.iMaSach == iMaSach);

            //Nếu tồn tại số lượng thì cho sửa số lượng
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGiohang()
        {
            List<GioHang> listGioHang = LayGioHang();
            listGioHang.Clear();
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap?id=2");
            }
            //Kiem tra có hàng trong gio chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "SachOnline");
            }
            //Lấy giỏ hàng từ session
            List<GioHang> listGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);

        }

        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {

            //Thêm đơn hàng
            DONDATHANG ddh = new DONDATHANG();
           
           NGUOIDUNG kh = (NGUOIDUNG)Session["TaiKhoan"];
            List<GioHang> listGioHang = LayGioHang();
            ViewBag.TongTien = TongTien();
            // ddh.MaKH = kh.MaKH;
            ddh.MaKH = kh.MaKH;
            ddh.NgayDH = DateTime.Now;
            ddh.TriGia = Convert.ToDecimal(ViewBag.TongTien);
            var NgayGiao = String.Format("{0:MM/mm/yyyy}", f["NgayGiao"]);
            ddh.NgayGiaoHang = DateTime.Parse(NgayGiao);
            ddh.TenNguoiNhan = kh.HoTenKH;
            ddh.DiaChiNhan = kh.DiaChiKH;
            ddh.DienThoaiNhan = kh.DienThoaiKH;
           
            data.DONDATHANG.Add(ddh);
            data.SaveChanges();

            //Thêm chi tiết đơn hàng
            foreach (var item in listGioHang)
            {
                CTDATHANG ctdh = new CTDATHANG();
                ctdh.SoDH = ddh.SoDH;
                ctdh.MaSach = item.iMaSach;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDonGia;
                data.CTDATHANG.Add(ctdh);


            }
            data.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Payment", "Home");



        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}
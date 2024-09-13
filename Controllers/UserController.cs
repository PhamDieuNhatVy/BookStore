using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnPTTKHDT.Models;

namespace DoAnPTTKHDT.Controllers
{
    public class UserController : Controller
    {
        WebsiteBanSachEntities data = new WebsiteBanSachEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            int state = Convert.ToInt32(Request.QueryString["id"]);
            var sTenDN = collection["TenDN"];
            var sMatkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập đúng tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatkhau))
            {
                ViewData["Err3"] = "Bạn phải nhập  mật khẩu";
            }
            else
            {
                NGUOIDUNG kh = data.NGUOIDUNG.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatkhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    if (state == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("DangNhap", "User");
                    }
                    ViewBag.ThongBao = "Bạn đã đăng nhập thành công";
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, NGUOIDUNG kh)
        {
            // Gan cac gia tri nguoi dung nhap du lieu cho cac bien
            var sHoTen = collection["HoTen"];
            var sTenDN = collection["TenDN"];
            var sMatkhau = collection["MatKhau"];
            var sMatkhauNhapLai = collection["MatkhauNL"];
            var sDiaChi = collection["DiaChiKH"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var dNgaySinh = String.Format("{0:y yy yyy yyyy}", collection["NgaySinh"]);
            var dGioiTinh = collection["GT"];
            //var aGioiTinh = collection["Nữ"];

            if (String.IsNullOrEmpty(sHoTen))
            {
                ViewData["err1"] = " Họ tên không được rỗng ";
            }
            else if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["err2"] = " Tên đăng nhập không được rỗng ";
            }
            else if (String.IsNullOrEmpty(sMatkhau))
            {
                ViewData[" err3 "] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(sMatkhauNhapLai))
            {
                ViewData[" err4 "] = " Phải nhập lại mật khẩu ";
            }
            else if (sMatkhau != sMatkhauNhapLai)
            {
                ViewData[" err4 "] = " Mật khẩu nhập lại không khớp ";
            }
            else if (String.IsNullOrEmpty(sEmail))
            {
                ViewData[" err5 "] = " Email không được rỗng ";
            }
            else if (String.IsNullOrEmpty(sDienThoai))
            {
                ViewData[" err6 "] = " Số điện thoại không được rỗng ";
            }
            else if (data.NGUOIDUNG.SingleOrDefault(n => n.TenDN == sTenDN) != null)
            {
                ViewBag.ThongBao = " Tên đăng nhập đã tồn tại ";
            }
            else if (data.NGUOIDUNG.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = " Email đã được sử dụng ";
            }
            else
            {
                // Gán giá trị cho đối tượng được tạo mới ( kh )
                kh.HoTenKH = sHoTen;
                kh.TenDN = sTenDN;
                kh.MatKhau = sMatkhau;
                kh.Email = sEmail;
                kh.DiaChiKH = sDiaChi;
                kh.DienThoaiKH = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                if (dGioiTinh == "Nam")
                {

                kh.GioiTinh = dGioiTinh;
                }else if(dGioiTinh == "Nữ")
                {
                    kh.GioiTinh = dGioiTinh;
                }
               
                data.NGUOIDUNG.Add(kh);
                data.SaveChanges();
                return Redirect("~/User/DangNhap?id=1");
            }
            return this.DangKy();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
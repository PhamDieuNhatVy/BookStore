using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DoAnPTTKHDT.Models;
using PagedList.Mvc;
using PagedList;

namespace DoAnPTTKHDT.Areas.Admin.Controllers
{
    public class QuanlysachController : Controller
    {
        private WebsiteBanSachEntities db = new WebsiteBanSachEntities();

        // GET: Admin/Quanlysach
        public ActionResult Index(int? page)
        {
            int iSize = 4;
            int iPageNum = (page ?? 1);
            var sach = from s in db.SACH select s;
            return View(sach.OrderBy(s => s.MaSach).AsEnumerable().ToPagedList(iPageNum, iSize));
        }

        // GET: Admin/Quanlysach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACH.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        // GET: Admin/Quanlysach/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(db.CHUDE.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SACH sach, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            // Đưa dữ liệu vào DropDown
            ViewBag.MaCD = new SelectList(db.CHUDE.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            if (fFileUpload == null)
            {
                // Nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = " Hãy chọn ảnh bìa . ";
                // Lưu thông tin để khi load lại trang do yêu cầu chọn ảnh bìa sẽ hiển thị               các thông tin này lên trang
                ViewBag.TenSach = f["sTenSach"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaCD = new SelectList(db.CHUDE.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", int.Parse(f["MaCD"]));
                ViewBag.MaNXB = new SelectList(db.NHAXUATBAN.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", int.Parse(f["MaNXB"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // Lấy tên file ( Khai báo thư viện : System.IO )
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    // Lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    // Lưu Sạch vào CSDL
                    sach.TenSach = f["sTenSach"];
                    sach.MoTa = f["sMoTa"].Replace("<p>", " ").Replace("</p> ", "\n");
                    sach.HinhMinhHoa = sFileName;
                    sach.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sach.SoLuongBan = int.Parse(f["iSoLuong"]);
                    sach.DonGia = decimal.Parse(f["mGiaBan"]);
                    sach.MaCD = int.Parse(f["MaCD"]);
                    sach.MaNXB = int.Parse(f["MaNXB"]);
                    db.SACH.Add(sach);
                    db.SaveChanges();
                    // Về lại trang Quản lý sách
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        // GET: Admin/Quanlysach/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var sach = db.SACH.SingleOrDefault(n => n.MaSach == id);
            if (sach == null)

            {
                Response.StatusCode = 404;
                return null;
            }
            // Hiển thị danh sách chủ đề và nhà xuất bản đồng thời chọn chủ đề và nhà xuất bản của cuốn hiện tại
            ViewBag.MaCD = new SelectList(db.CHUDE.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sach.MaCD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }


        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {

            var sach = db.SACH.AsEnumerable().SingleOrDefault(n => n.MaSach == int.Parse(f["iMaSach"]));
            ViewBag.MaCD = new SelectList(db.CHUDE.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sach.MaCD);

            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);

            if (ModelState.IsValid)

            {
                if (fFileUpload != null)
                {

                    //Lay tén file (Khai bao thu vién: System.10)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), sFileName);
                    //Kiém tra file da ton tai chua
                    if (!System.IO.File.Exists(path))
                    {

                        fFileUpload.SaveAs(path);
                    }
                    sach.HinhMinhHoa = sFileName;
                }
                //Lutu Sach vao CSDL
                sach.TenSach = f["sTenSach"];
                sach.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                sach.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                sach.SoLuongBan = int.Parse(f["isoLuong"]);
                sach.DonGia = decimal.Parse(f["mGiaBan"]);
                sach.MaCD = int.Parse(f["MaCD"]);
                sach.MaNXB = int.Parse(f["MaNxB"]);
                db.SaveChanges();
                //N@ igi trang Quan ly s
                return RedirectToAction("Index");
            }
            return View(sach);
        }


        // GET: Admin/Quanlysach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACH.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        // POST: Admin/Quanlysach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SACH sACH = db.SACH.Find(id);
            db.SACH.Remove(sACH);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

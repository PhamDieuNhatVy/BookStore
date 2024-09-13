using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnPTTKHDT.Models;

namespace DoAnPTTKHDT.Areas.Admin.Controllers
{
    public class HomeADController : Controller
    {
        private WebsiteBanSachEntities db = new WebsiteBanSachEntities();

        // GET: Admin/HomeAD
        public ActionResult Index()
        {
            var sACH = db.SACH.Include(s => s.CHUDE).Include(s => s.NHAXUATBAN);
            return View(sACH.ToList());
        }

        // GET: Admin/HomeAD/Details/5
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

        // GET: Admin/HomeAD/Create
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(db.CHUDE, "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN, "MaNXB", "TenNXB");
            return View();
        }

        // POST: Admin/HomeAD/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,DonViTinh,DonGia,MoTa,HinhMinhHoa,MaCD,MaNXB,NgayCapNhat,SoLuongBan,SoLanXem")] SACH sACH)
        {
            if (ModelState.IsValid)
            {
                db.SACH.Add(sACH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCD = new SelectList(db.CHUDE, "MaCD", "TenChuDe", sACH.MaCD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN, "MaNXB", "TenNXB", sACH.MaNXB);
            return View(sACH);
        }

        // GET: Admin/HomeAD/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.MaCD = new SelectList(db.CHUDE, "MaCD", "TenChuDe", sACH.MaCD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN, "MaNXB", "TenNXB", sACH.MaNXB);
            return View(sACH);
        }

        // POST: Admin/HomeAD/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,DonViTinh,DonGia,MoTa,HinhMinhHoa,MaCD,MaNXB,NgayCapNhat,SoLuongBan,SoLanXem")] SACH sACH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sACH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCD = new SelectList(db.CHUDE, "MaCD", "TenChuDe", sACH.MaCD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN, "MaNXB", "TenNXB", sACH.MaNXB);
            return View(sACH);
        }

        // GET: Admin/HomeAD/Delete/5
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

        // POST: Admin/HomeAD/Delete/5
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatkhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                ADMIN ad = db.ADMIN.SingleOrDefault(n => n.TenDNAdmin == sTenDN && n.MatKhauAdmin == sMatkhau);
                if (ad != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["Admin"] = ad;
                    return RedirectToAction("Index", "HomeAD");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }


            }
            return View();
        }

        public ActionResult LoginLogoutPartial_AD()
        {
            return PartialView();

        }

        public ActionResult DangXuat()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login", "HomeAD");
        }
    }
}

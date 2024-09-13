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
    public class QuanlydonhangController : Controller
    {
        private WebsiteBanSachEntities db = new WebsiteBanSachEntities();

        // GET: Admin/Quanlydonhang
        public ActionResult Index()
        {
            var dONDATHANG = db.DONDATHANG.Include(d => d.NGUOIDUNG);
            return View(dONDATHANG.ToList());
        }

        // GET: Admin/Quanlydonhang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = db.DONDATHANG.Find(id);
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONDATHANG);
        }

        // GET: Admin/Quanlydonhang/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.NGUOIDUNG, "MaKH", "HoTenKH");
            return View();
        }

        // POST: Admin/Quanlydonhang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoDH,MaKH,NgayDH,TriGia,NgayGiaoHang,TenNguoiNhan,DiaChiNhan,DienThoaiNhan,daduyet,MaShipper")] DONDATHANG dONDATHANG)
        {
            if (ModelState.IsValid)
            {
                db.DONDATHANG.Add(dONDATHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.NGUOIDUNG, "MaKH", "HoTenKH", dONDATHANG.MaKH);
            return View(dONDATHANG);
        }

        // GET: Admin/Quanlydonhang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = db.DONDATHANG.Find(id);
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.NGUOIDUNG, "MaKH", "HoTenKH", dONDATHANG.MaKH);
            return View(dONDATHANG);
        }

        // POST: Admin/Quanlydonhang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoDH,MaKH,NgayDH,TriGia,NgayGiaoHang,TenNguoiNhan,DiaChiNhan,DienThoaiNhan,daduyet,MaShipper")] DONDATHANG dONDATHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONDATHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.NGUOIDUNG, "MaKH", "HoTenKH", dONDATHANG.MaKH);
            return View(dONDATHANG);
        }

        // GET: Admin/Quanlydonhang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = db.DONDATHANG.Find(id);
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONDATHANG);
        }

        // POST: Admin/Quanlydonhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DONDATHANG dONDATHANG = db.DONDATHANG.Find(id);
            db.DONDATHANG.Remove(dONDATHANG);
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

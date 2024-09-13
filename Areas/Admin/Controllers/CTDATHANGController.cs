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
    public class CTDATHANGController : Controller
    {
        private WebsiteBanSachEntities db = new WebsiteBanSachEntities();

        // GET: Admin/CTDATHANG
        public ActionResult Index(int? id)
        {
            var cTDATHANGs = db.CTDATHANG.Include(c => c.DONDATHANG).Include(c => c.SACH);
            return View(cTDATHANGs.ToList());
        }

        // GET: Admin/CTDATHANG/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDATHANG cTDATHANG = db.CTDATHANG.Find(id);
            if (cTDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(cTDATHANG);
        }

        // GET: Admin/CTDATHANG/Create
        public ActionResult Create()
        {
            ViewBag.SoDH = new SelectList(db.DONDATHANG, "SoDH", "TenNguoiNhan");
            ViewBag.MaSach = new SelectList(db.SACH, "MaSach", "TenSach");
            return View();
        }

        // POST: Admin/CTDATHANG/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoDH,MaSach,SoLuong,DonGia,ThanhTien")] CTDATHANG cTDATHANG)
        {
            if (ModelState.IsValid)
            {
                db.CTDATHANG.Add(cTDATHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SoDH = new SelectList(db.DONDATHANG, "SoDH", "TenNguoiNhan", cTDATHANG.SoDH);
            ViewBag.MaSach = new SelectList(db.SACH, "MaSach", "TenSach", cTDATHANG.MaSach);
            return View(cTDATHANG);
        }

        // GET: Admin/CTDATHANG/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDATHANG cTDATHANG = db.CTDATHANG.Find(id);
            if (cTDATHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.SoDH = new SelectList(db.DONDATHANG, "SoDH", "TenNguoiNhan", cTDATHANG.SoDH);
            ViewBag.MaSach = new SelectList(db.SACH, "MaSach", "TenSach", cTDATHANG.MaSach);
            return View(cTDATHANG);
        }

        // POST: Admin/CTDATHANG/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoDH,MaSach,SoLuong,DonGia,ThanhTien")] CTDATHANG cTDATHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTDATHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SoDH = new SelectList(db.DONDATHANG, "SoDH", "TenNguoiNhan", cTDATHANG.SoDH);
            ViewBag.MaSach = new SelectList(db.SACH, "MaSach", "TenSach", cTDATHANG.MaSach);
            return View(cTDATHANG);
        }

        // GET: Admin/CTDATHANG/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDATHANG cTDATHANG = db.CTDATHANG.Find(id);
            if (cTDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(cTDATHANG);
        }

        // POST: Admin/CTDATHANG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTDATHANG cTDATHANG = db.CTDATHANG.Find(id);
            db.CTDATHANG.Remove(cTDATHANG);
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

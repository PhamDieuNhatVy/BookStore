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
    public class QuanlychudeController : Controller
    {
        private WebsiteBanSachEntities db = new WebsiteBanSachEntities();

        // GET: Admin/Quanlychude
        public ActionResult Index()
        {
            return View(db.CHUDE.ToList());
        }

        // GET: Admin/Quanlychude/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUDE cHUDE = db.CHUDE.Find(id);
            if (cHUDE == null)
            {
                return HttpNotFound();
            }
            return View(cHUDE);
        }

        // GET: Admin/Quanlychude/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Quanlychude/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCD,TenChuDe")] CHUDE cHUDE)
        {
            if (ModelState.IsValid)
            {
                db.CHUDE.Add(cHUDE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cHUDE);
        }

        // GET: Admin/Quanlychude/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUDE cHUDE = db.CHUDE.Find(id);
            if (cHUDE == null)
            {
                return HttpNotFound();
            }
            return View(cHUDE);
        }

        // POST: Admin/Quanlychude/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCD,TenChuDe")] CHUDE cHUDE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHUDE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cHUDE);
        }

        // GET: Admin/Quanlychude/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUDE cHUDE = db.CHUDE.Find(id);
            if (cHUDE == null)
            {
                return HttpNotFound();
            }
            return View(cHUDE);
        }

        // POST: Admin/Quanlychude/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHUDE cHUDE = db.CHUDE.Find(id);
            db.CHUDE.Remove(cHUDE);
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

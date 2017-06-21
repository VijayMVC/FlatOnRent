using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentFlat.Model;
using Microsoft.AspNet.Identity;

namespace RentFlat.Web.Controllers
{
    [Authorize]
    public class FacilityInFlatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FacilityInFlats
        public async Task<ActionResult> Index()
        {
            string userId = User.Identity.GetUserId();

            var facilityInFlats = db.FacilityInFlats
                .Include(f => f.Facility)
                .Include(f => f.Flat)
                .Where(id => id.Flat.OwnerId == userId);
            return View(await facilityInFlats.ToListAsync());
        }

        // GET: FacilityInFlats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            string userId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityInFlat facilityInFlat = await db.FacilityInFlats.FindAsync(id);
            if (facilityInFlat == null)
            {
                return HttpNotFound();
            }
            return View(facilityInFlat);
        }

        // GET: FacilityInFlats/Create
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.FacilityId = new SelectList(db.Facilities, "ID", "Type");
            var flats = db.Flats.Where(id => id.OwnerId == userId).ToList();
            ViewBag.FlatId = new SelectList(flats, "ID", "FlatName");
            return View();
        }

        // POST: FacilityInFlats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FlatId,FacilityId")] FacilityInFlat facilityInFlat)
        {
            string userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.FacilityInFlats.Add(facilityInFlat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FacilityId = new SelectList(db.Facilities, "ID", "Type", facilityInFlat.FacilityId);

            var flats = db.Flats.Where(id => id.OwnerId == userId).ToList();
            ViewBag.FlatId = new SelectList(flats, "ID", "FlatName", facilityInFlat.FlatId);
            return View(facilityInFlat);
        }

        // GET: FacilityInFlats/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityInFlat facilityInFlat = await db.FacilityInFlats.FindAsync(id);
            if (facilityInFlat == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacilityId = new SelectList(db.Facilities, "ID", "Type", facilityInFlat.FacilityId);
            ViewBag.FlatId = new SelectList(db.Flats, "ID", "Country", facilityInFlat.FlatId);
            return View(facilityInFlat);
        }

        // POST: FacilityInFlats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FlatId,FacilityId")] FacilityInFlat facilityInFlat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilityInFlat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FacilityId = new SelectList(db.Facilities, "ID", "Type", facilityInFlat.FacilityId);
            ViewBag.FlatId = new SelectList(db.Flats, "ID", "Country", facilityInFlat.FlatId);
            return View(facilityInFlat);
        }

        // GET: FacilityInFlats/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityInFlat facilityInFlat = await db.FacilityInFlats.FindAsync(id);
            if (facilityInFlat == null)
            {
                return HttpNotFound();
            }
            return View(facilityInFlat);
        }

        // POST: FacilityInFlats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FacilityInFlat facilityInFlat = await db.FacilityInFlats.FindAsync(id);
            db.FacilityInFlats.Remove(facilityInFlat);
            await db.SaveChangesAsync();
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

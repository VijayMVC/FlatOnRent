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
using Microsoft.VisualBasic;

namespace RentFlat.Web.Controllers
{
    [Authorize]
    public class RentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rents
        public async Task<ActionResult> Index()
        {
            string userId = User.Identity.GetUserId();
            var rents = db.Rents.Include(r => r.Flat).Where( id => id.Flat.OwnerId == userId);
            return View(await rents.ToListAsync());
        }

        // GET: Rents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = await db.Rents.FindAsync(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // GET: Rents/Create
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            var flats = db.Flats.Where(id => id.OwnerId == userId).ToList();
            ViewBag.FlatId = new SelectList(flats, "ID", "FlatName");
            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Rent rent)
        {
            string userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Rents.Add(rent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var flats = db.Flats.Where(id => id.OwnerId == userId).ToList();
            ViewBag.FlatId = new SelectList(db.Flats, "ID", "FlatName", rent.FlatId);
            return View(rent);
        }

        // GET: Rents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = await db.Rents.FindAsync(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlatId = new SelectList(db.Flats, "ID", "FlatName", rent.FlatId);
            return View(rent);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,RentingUserId,FlatId,StartOfRent,EndOfRent")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FlatId = new SelectList(db.Flats, "ID", "FlatName", rent.FlatId);
            return View(rent);
        }

        // GET: Rents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = await db.Rents.FindAsync(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rent rent = await db.Rents.FindAsync(id);
            db.Rents.Remove(rent);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async System.Threading.Tasks.Task<ActionResult> RentAFlat(int? id)
        {
            if (HttpContext.Request.Cookies["userEmail"] == null)
            {
                return View("NoSuchRules");
            }
            IEnumerable<Rent> rents = await db.Rents.ToListAsync();
            var currentRent = rents.Where(i => i.FlatId == id && i.StartOfRent < DateTime.Now && i.EndOfRent > DateTime.Now)
                .FirstOrDefault();
            if (currentRent != null)
            {
                return View("CantRent");
            }
            Flat flat = await db.Flats.FindAsync(id);
            return View(new Rent() { FlatId = flat.ID });
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> RentAFlat(Rent rent)
        {
            Flat flat = await db.Flats.FindAsync(rent.FlatId);
            DateInterval interval = DateInterval.Day;
            long difference = DateAndTime.DateDiff(interval, rent.StartOfRent.Value, rent.EndOfRent.Value, FirstDayOfWeek.Monday);
            decimal price = difference > 30 == true ? flat.PriceForMonth / 30 * difference : flat.PriceForDay * difference;
            ViewBag.Price = price;
            return View("Confirmation", rent);
        }

        [NonAction]
        private bool RentExists(int id)
        {
            return db.Rents.Count(e => e.ID == id) > 0;
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

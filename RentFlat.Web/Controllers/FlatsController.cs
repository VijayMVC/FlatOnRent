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
using RentFlat.Web.Models.ViewModels;

namespace RentFlat.Web.Controllers
{
    [Authorize]
    public class FlatsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Flats
        public async Task<ActionResult> Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var flats = db.Flats.Where(id => id.OwnerId == currentUserId );
            return View(await flats.ToListAsync());
        }

        // GET: Flats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = await db.Flats.FindAsync(id);
            if (flat == null)
            {
                return HttpNotFound();
            }

            FlatDetailViewModel model = new FlatDetailViewModel();
            model.Facilities = await db.FacilityInFlats.Where(f => f.FlatId == flat.ID).Select(i => i.Facility).ToListAsync();
            model.Flat = flat;

            return View(model);
        }

        // GET: Flats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Flat flat)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                flat.OwnerId = currentUserId;
                flat.ApplicationUser = currentUser;
                db.Flats.Add(flat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(flat);
        }

        // GET: Flats/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = await db.Flats.FindAsync(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            return View(flat);
        }

        // POST: Flats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Flat flat)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                flat.OwnerId = currentUserId;
                flat.ApplicationUser = currentUser;
                db.Entry(flat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(flat);
        }

        // GET: Flats/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = await db.Flats.FindAsync(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            return View(flat);
        }

        // POST: Flats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Flat flat = await db.Flats.FindAsync(id);
            db.Flats.Remove(flat);
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

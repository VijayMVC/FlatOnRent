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
using RentFlat.Web.Infrastructure.FileHelpers;

namespace RentFlat.Web.Controllers
{
    [Authorize]
    public class PicturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pictures
        public async Task<ActionResult> Index()
        {
            string userId = User.Identity.GetUserId();

            var pictures = db.Pictures.Include(p => p.Flat).Where( id => id.Flat.OwnerId == userId);
            return View(await pictures.ToListAsync());
        }

        // GET: Pictures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = await db.Pictures.FindAsync(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // GET: Pictures/Create
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            var flats = db.Flats.Where(id => id.OwnerId == userId).ToList();
            ViewBag.FlatId = new SelectList(flats, "ID", "FlatName");
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Picture picture, HttpPostedFileBase upload)
        {
            string userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                if (upload.ContentLength > 0)
                {
                    string fileName = FileUtils.UploadFile(upload, User.Identity.GetUserName(), picture.FlatId.ToString());

                    if(fileName != null && fileName.Length > 0)
                    {
                        picture.LinkToImage = fileName;
                        db.Pictures.Add(picture);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                   
                }
                
            }

            var flats = db.Flats.Where(id => id.OwnerId == userId).ToList();
            ViewBag.FlatId = new SelectList(flats, "ID", "FlatName", picture.FlatId);
            ViewBag.UpLoadMessage = "Unable to Upload Picture";
            return View(picture);
        }

        // GET: Pictures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = await db.Pictures.FindAsync(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlatId = new SelectList(db.Flats, "ID", "FlatName", picture.FlatId);
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,LinkToImage,FlatId")] Picture picture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(picture).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FlatId = new SelectList(db.Flats, "ID", "FlatName", picture.FlatId);
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = await db.Pictures.FindAsync(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Picture picture = await db.Pictures.FindAsync(id);
            db.Pictures.Remove(picture);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PictureExists(int id)
        {
            return db.Pictures.Count(e => e.ID == id) > 0;
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

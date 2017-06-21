using RentFlat.Model;
using RentFlat.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RentFlat.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string keywords, string city)
        {
            ViewBag.keywords = keywords;
            ViewBag.city = city;
            
            if( string.IsNullOrEmpty(keywords) && string.IsNullOrEmpty(city))
            {
                return View();
            }

            var rents = this.db.Rents.Where( s => s.isRentAvaiable == true && s.Flat.Description.Contains(keywords) | keywords == null && s.Flat.City.ToUpper() == city.ToUpper() | city == null )
              .OrderBy(d => d.Flat.City)
              .ToList();

            return View(rents);
        }

        public async Task<ActionResult> Rents(int? id)
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

            RentViewModel model = new RentViewModel();
            model.Address = rent.Flat.Address;
            model.City = rent.Flat.City;
            model.Country = rent.Flat.Country;
            model.Description = rent.Flat.Country;
            model.District = rent.Flat.District;
            model.FlatName = rent.Flat.FlatName;
            model.FlatNumber = rent.Flat.FlatNumber;
            model.RoomNumber = rent.Flat.RoomNumber;
            model.Latitude = rent.Flat.Latitude;
            model.Longitude = rent.Flat.Longitude;
            model.PriceForMonth = rent.Flat.PriceForMonth;
            model.PriceForDay = rent.Flat.PriceForDay;
            model.ZipCode = rent.Flat.ZipCode;

            model.Facilities = rent.Flat.Facilities.Select(i => i.Facility);
            model.Pictures = rent.Flat.Pictures;

            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using Microsoft.AspNet.Identity;
using RentFlat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RentFlat.Web.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Get User ID
        [NonAction]
        public string GetUserId()
        {
            return User.Identity.GetUserId();
        }

        [NonAction]
        public ApplicationUser GetUser()
        {
            return db.Users.Where(id => id.Id == GetUserId()).FirstOrDefault();
        }
    }
}
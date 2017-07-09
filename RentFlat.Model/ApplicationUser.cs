using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentFlat.Model
{
    public enum UserRoles
    {
        Administrator = 1,
        Manager = 2,
        Operator = 4,
        Driver = 8,
    }
    public class ApplicationUser : IdentityUser
    {
        [Display(Name="First Name")]
        public string FirstName { set; get; }

        [Display(Name = "Last Name")]
        public string LastName { set; get; }

        [StringLength(100)]
        public string DefaultAddress { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [StringLength(10)]
        public string MobileNumber { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }

        public virtual IEnumerable<Flat> Flats { set; get; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<RentFlat.Model.Facility> Facilities { get; set; }

        public System.Data.Entity.DbSet<RentFlat.Model.FacilityInFlat> FacilityInFlats { get; set; }

        public System.Data.Entity.DbSet<RentFlat.Model.Flat> Flats { get; set; }

        public System.Data.Entity.DbSet<RentFlat.Model.Picture> Pictures { get; set; }

        public System.Data.Entity.DbSet<RentFlat.Model.Rent> Rents { get; set; }
        public System.Data.Entity.DbSet<RentFlat.Model.Country> Countries { get; set; }
        public System.Data.Entity.DbSet<RentFlat.Model.City> Cities { get; set; }
    }
}

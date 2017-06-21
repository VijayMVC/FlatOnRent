using RentFlat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentFlat.Web.Models.ViewModels
{
    public class RentViewModel
    {
        [Display(Name = "Flat Name")]
        public string FlatName { get; set; }
        [Display(Name = "Flat Number")]
        public string FlatNumber { get; set; }
        [Display(Name = "Number of rooms")]
        public int RoomNumber { set; get; }
        public string Country { set; get; }
        public string City { get; set; }
        public string Address { set; get; }
        public string District { set; get; }
        public string Description { set; get; }
        [Display(Name = "Price for day")]
        public decimal PriceForDay { set; get; }
        [Display(Name = "Price for month")]
        public decimal PriceForMonth { set; get; }
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        public int ZipCode { set; get; }

        public IEnumerable<Facility> Facilities { set; get; }
        public IEnumerable<Picture> Pictures { set; get; }
    }
}
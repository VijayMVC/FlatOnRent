    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentFlat.Model
{
    public class Flat : Entity
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
        public string OwnerId { set; get; }

        public virtual List<Picture> Pictures { set; get; }
        public virtual List<FacilityInFlat> Facilities { set; get; }
        public virtual ApplicationUser ApplicationUser { set; get; }
    }
}

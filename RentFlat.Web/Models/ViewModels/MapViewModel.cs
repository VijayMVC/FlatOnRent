using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentFlat.Web.Models.ViewModels
{
    public class MapViewModel
    {
        public string Latitude { set; get; }
        public string Longitude { set; get; }
        public string Address { set; get; }
        public decimal PriceForDay { set; get; }
        public decimal PriceForMonth { set; get; }
    }
}
using RentFlat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentFlat.Web.Models.ViewModels
{
    public class FlatDetailViewModel
    {
        public Flat Flat { set; get; }
        public IEnumerable<Facility> Facilities { set; get; }
    }
}
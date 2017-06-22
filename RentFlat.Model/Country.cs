using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentFlat.Model
{
    public class Country : Entity
    {
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace RentFlat.Model
{
    public class City : Entity
    {
        [Display(Name="City Name")]
        public string CityName { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}

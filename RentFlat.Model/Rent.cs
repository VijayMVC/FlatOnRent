using System;
using System.ComponentModel.DataAnnotations;

namespace RentFlat.Model
{
    public class Rent : Entity
    {
        [Display(Name = "Flat Name")]
        public int FlatId { set; get; }

        [DataType(DataType.Date)]
        [Display(Name = "Start of Rent")]
        public DateTime? StartOfRent { set; get; }

        [DataType(DataType.Date)]
        [Display(Name = "End of Rent")]
        public DateTime? EndOfRent { set; get; }

        [Display(Name = "Rent Avaiable")]
        public Boolean isRentAvaiable { get; set; }
        public virtual Flat Flat { set; get; }
    }
}

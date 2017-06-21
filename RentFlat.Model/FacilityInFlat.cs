using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentFlat.Model
{
    public class FacilityInFlat : Entity
    {
        public int FlatId { set; get; }
        public int FacilityId { set; get; }

        public virtual Flat Flat { set; get; }
        public virtual Facility Facility { set; get; }

    }
}

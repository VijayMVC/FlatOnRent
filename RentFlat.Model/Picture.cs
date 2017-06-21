using System;

namespace RentFlat.Model
{
    public class Picture : Entity
    {
        public string Name { get; set; }
        public string LinkToImage { get; set; }

        public int FlatId { set; get; }
        public virtual Flat Flat { set; get; }
    }
}

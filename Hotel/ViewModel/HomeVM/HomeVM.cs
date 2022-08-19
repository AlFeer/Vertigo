using DAL.Models;
using System.Collections.Generic;

namespace Hotel.ViewModel
{
    public class HomeVM
    {
        public List<SliderHome> SlidersHome { get; set; }
        public List<Promotion> Promotions { get; set; }
        public List<SomeBlog> SomeBlogs { get; set; }
        public List<SomeHotel> SomeHotels { get; set; }
    }
}

using DAL.Models;
using System.Collections.Generic;
using X.PagedList;

namespace Hotel.ViewModel
{
    public class HotelsVM
    {
        public IPagedList<Hotels> Hotels { get; set; }
    }
}

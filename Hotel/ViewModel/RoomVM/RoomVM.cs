using DAL.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Hotel.ViewModel
{
    public class RoomVM
    {
        public List<IFormFile> ImageFile { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}

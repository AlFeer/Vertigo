using DAL.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class SomeHotel : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public string Stars { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}

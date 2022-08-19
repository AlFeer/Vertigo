﻿using DAL.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Room : BaseEntity, IEntity
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}

using DAL.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class RoomImages : BaseEntity, IEntity
    {
        public string Images { get; set; }
        public bool IsMain { get; set; }
    }
}

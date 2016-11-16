using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Role
    {
        public int R_ID { get; set; }
        public string R_Description { get; set; }
        public int ID_Seller { get; set; }
    }
}
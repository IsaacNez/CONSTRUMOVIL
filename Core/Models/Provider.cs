using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Provider
    {
        public int P_ID { get; set; }
        public string P_Name { get; set; }
        public string P_LName { get; set; }
        public string P_Address { get; set; }
        public DateTime P_Date { get; set; }
        public int ID_Seller { get; set; }
        public string P_Status { get; set; }

    }
}

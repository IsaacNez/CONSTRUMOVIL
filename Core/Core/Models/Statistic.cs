using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Statistic
    {
        public int S_ID { set; get; }
        public string PRName { set; get; }
        public int PRAmount { set; get; }

        public int SAmount { set; get; }
        public int ID_Seller { get; set; }

    }
}
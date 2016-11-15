﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Order
    {
        public string Products { get; set; }
        public string Amount { get; set; }
        public int O_ID { get; set; }
        public int O_Priority { get; set; }
        public string O_Status { get; set; }
        public DateTime O_Date { get; set; }
        public int O_PPhone{get;set;}
        public int W_ID { get; set; }
        public int S_ID { get; set; }
        public int C_ID { get; set; }
        public int ID_Seller { get; set; }

    }
}

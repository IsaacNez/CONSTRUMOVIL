﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Category
    {
        public int CXP_ID { get; set; }
        public string CXP_Status{ get; set; }
        public int PR_ID { get; set; }
        public string CA_ID { get; set; }
        public string CA_Description { get; set; }
        public string CA_Status { get; set; }
        public int ID_Seller { get; set; }
    }
}

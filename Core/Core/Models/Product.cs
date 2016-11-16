using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Product
    {
        public int PR_ID { get; set; }
        public int PR_Price { get; set; }
        public int PR_Exempt { get; set; }
        public string PR_Description { get; set; }
        public int PR_Quantity { get; set; }
        public string PR_Name { get; set; }
        public string CA_ID { get; set; }
        public int P_ID { get; set; }
        public int S_ID { get; set; }
        public int ID_Seller { get; set; }
        public string PR_Status { get; set; }

    }
}

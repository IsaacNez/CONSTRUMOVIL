using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication1.Models
{
    public class Orderxproduct
    {
        public int OXP_ID { get; set; }
        public int O_ID { get; set; }
        public string PR_Name { get; set; }
        public int PR_Price { get; set; }
    }
}

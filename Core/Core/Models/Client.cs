using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Client
    {
        public int C_ID { get; set; }
        public string C_Name { get; set; }
        public string C_LName { get; set; }
        public string C_Address { get; set; }
        public int C_Phone { get; set; }
        public DateTime C_Date { get; set; }
        public int C_Penalization { get; set; }
        public int ID_Seller { get; set; }
        public string C_Status { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace WebApplication1.Models
{
    public class Sync
    {
        public string action { get; set; }
        public string table { get; set; }
        public string model { get; set; }
        public List<int> seller = new List<int>();
    }
}
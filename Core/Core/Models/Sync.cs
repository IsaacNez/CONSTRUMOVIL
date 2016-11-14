using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Sync
    {
        public string action { get; set; }
        public string table { get; set; }
        public EnvironmentVariableTarget model { get; set; }
        public List<int> selles = new List<int>();
    }
}
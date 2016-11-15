using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Core.Models
{
    public static class Tasks
    {
        public static List<Sync>tasks
        {
            get
            {
                return HttpContext.Current.Application["tasks"] as List<Sync>;
            }
            set
            {
                HttpContext.Current.Application["tasks"] = value;
            }
        }
    }
}

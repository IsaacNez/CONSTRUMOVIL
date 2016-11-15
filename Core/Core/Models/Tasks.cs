using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public static class Tasks
    {
        public static List<Sync>tasks
        {

            get
            {
                if (HttpContext.Current.Application["tasks"] == null)
                {
                    HttpContext.Current.Application["tasks"] = new List<Sync>();
                    return HttpContext.Current.Application["tasks"] as List<Sync>;
                }
                else
                {
                    return HttpContext.Current.Application["tasks"] as List<Sync>;
                }
            }
            set
            {
                HttpContext.Current.Application["tasks"] = value;
            }
        }
    }
}

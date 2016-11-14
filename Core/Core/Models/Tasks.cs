using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Models
{
    public class Tasks
    {
            public static List<int> tasks
            {
                get
                {
                    return HttpContext.Current.Application["Bar"] as List<int>;
                }
                set
                {
                    HttpContext.Current.Application["Bar"] = value;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Core
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            Application["tasks"] = new List<int>();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

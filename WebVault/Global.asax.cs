using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebVault
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["UsersOnline"] = 0;
            Application["wc"] = new System.Net.WebClient();

        }
        public void Session_OnStart()
        {
            Application.Lock();
            Application["UsersOnline"] = (int)Application["UsersOnline"] + 1;
            Application.UnLock();
            Session["salty"] = ECDHAES256s.AES.GetSalt();
        }

        public void Session_OnEnd()
        {
            Application.Lock();
            Application["UsersOnline"] = (int)Application["UsersOnline"] - 1;
            Application.UnLock();
            var list =  Context.Session["temp"] as List<string>;
            foreach(var file in list)
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch
                {

                }
            }
        }

    }
}
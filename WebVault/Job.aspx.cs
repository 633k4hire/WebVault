using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebVault
{
    public partial class Job : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Net.WebClient wc;
            if (Application["wc"] != null)
            {
                wc = Application["wc"] as System.Net.WebClient;

            }
            else
            {
                wc = new WebClient();
                wc.UseDefaultCredentials = true;
                wc.Credentials = new NetworkCredential("admin", "l0veisk1ng");
            }
            System.Threading.Thread.Sleep(1000);
            var bytes = wc.DownloadData("http://localhost:1880/Job");
        }
    }
}
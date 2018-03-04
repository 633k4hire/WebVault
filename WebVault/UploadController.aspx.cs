using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebVault
{
    public partial class UploadController : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static bool UploadData(object data)
        {
            try
            {
                
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
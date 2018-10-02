using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebVault
{
    public partial class L0XX0R : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userid = ECDHAES256s.AES.Sha256(User.Identity.Name);
            if (Application[userid + "pass"] != null)
            {
                
                var script = @"$(document).ready(function (){ HideDiv('SignInModal');});";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "testScript", script, true);

            }
            else
            {
                if (!IsPostBack)
                {
                    var script = @"$(document).ready(function (){ ShowDiv('SignInModal');});";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "testScript", script, true);
                }

               // var salty = ECDHAES256s.AES.GetSalt();
               
            }           
        }

        protected void SubmitPasswordBtn_Click(object sender, EventArgs e)
        {
           
        }

    }
}
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
            if (Application[userid + "key"] != null)
            {
                KeyField.Text = "********";

            }
            else
            {
                var salty = ECDHAES256s.AES.GetSalt();
                var tup = ECDHAES256s.AES.PassToKeyIVb(Convert.ToBase64String(salty));            
                Application[userid+"key"] = tup.Item1;
                Application[userid+"iv"] = tup.Item2;
                KeyField.Text = Convert.ToBase64String(salty);
            }           
        }

        protected void SubmitPasswordBtn_Click(object sender, EventArgs e)
        {
            var tup = ECDHAES256s.AES.PassToKeyIVb(KeyField.Text);
            var userid = ECDHAES256s.AES.Sha256(User.Identity.Name);
            Application[userid + "key"] = tup.Item1;
            Application[userid + "iv"] = tup.Item2;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebVault
{
    public partial class OTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            var userid = ECDHAES256s.AES.Sha256(User.Identity.Name);
            if (Application[userid + "pass"] != null)
            {
                KeyField.Text = "********";
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
           
            if ((Application[Session.SessionID + "keyupdate"] as string) == "true")
            {
                KeyField.Text = Application[Session.SessionID + "pass"] as string;
                Application[Session.SessionID + "keyupdate"] = "false";
            }
            else
            {

            } */
            if (!IsPostBack)
            {
                KeyField.Text = Convert.ToBase64String( ECDHAES256s.AES.GetSalt());
            Application[Session.SessionID+"pass"] = KeyField.Text;
                Application[Session.SessionID + "keyupdate"] = "false";
            }
            
        }
        public void Downloadfile(string sFilePath)
        {
            try
            {
                var file = new System.IO.FileInfo(sFilePath.Map());

                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString(CultureInfo.InvariantCulture));
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            catch
            {
                Page.SiteMaster().ShowError("Problem Downloading File:"+sFilePath);
            }
        }
        protected void PushArchiveToClient(string file, bool deleteAfter = false)
        {
            try
            {
                HttpContext.Current.Response.ClearContent();

                HttpContext.Current.Response.ClearHeaders();

                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + Path.GetFileName(file));

                HttpContext.Current.Response.ContentType = "application/zip";

                HttpContext.Current.Response.WriteFile(file);
                if (deleteAfter)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {

                    }
                }
                HttpContext.Current.Response.End();
            }
            catch { }
        }

        protected void SubmitPasswordBtn_Click(object sender, EventArgs e)
        {
            Application[Session.SessionID + "pass"] = KeyField.Text;
        }

        protected void NavBack_Click(object sender, EventArgs e)
        {

        }

        protected void NavHome_Click(object sender, EventArgs e)
        {

        }

        protected void SuperButton_Click(object sender, EventArgs e)
        {
         if (SuperButtonArg.Text== "updatekey")
            {
                KeyField.Text = Application[Session.SessionID + "pass"] as string;
               // KeyField.Focus();
                Application[Session.SessionID + "keyupdate"] = "false";
                CogUpdatePanel.Update();
            }
        }

        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            var arg = SuperButtonArg.Text;
            if (arg.StartsWith("download,"))
            {
                var file = arg.Split(',')[1];
                if (file == "nofile")
                {
                    Page.SiteMaster().ShowError("Incorrect Key");
                }                  
                else
                {
                    var rel = file.Replace("\\", "/").Split(new string[] { "/Upload/Files" }, StringSplitOptions.None).Last();
                    rel = "~/Upload/Files" + rel;
                    Downloadfile(file);

                }
                    
              
               
            }
            //OTP_UpdatePanel.Update();
           
        }

        protected void EncryptModeBtn_Click(object sender, EventArgs e)
        {
           
        }

        protected void DecryptModeBtn_Click(object sender, EventArgs e)
        {
        }

        protected void DownloadKeyFileBtn_Click(object sender, EventArgs e)
        {
            var filename = "KeyFile.kl0x";
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(KeyField.Text);          

                Response.AddHeader("Content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytes);
               
            }
            Response.End();

        }
    }
}
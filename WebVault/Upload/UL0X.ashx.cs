using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebVault.Upload
{
    /// <summary>
    /// Summary description for UL0X
    /// </summary>
    public class UL0X : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //authenitcate session user
                if (!context.User.Identity.IsAuthenticated)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("error");
                    return;
                }
            
                //decryption username and pass
                var userid = context.Request.Path.TrimStart('/').Split(new string[] { ".ul0x" }, StringSplitOptions.None)[0];
                var pass = context.Application[userid + "pass"] as string;

                if (!Directory.Exists(context.Server.MapPath("/Upload/Files/" + userid)))
                {
                    Directory.CreateDirectory(context.Server.MapPath("/Upload/Files/" + userid));
                }

                HttpFileCollection files = context.Request.Files;
                string fileName = "";
                foreach (string key in files)
                {
                    HttpPostedFile file = files[key];
                    fileName = file.FileName;
                    fileName = "/Upload/Files/" + userid + "/" + Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName);
                    if (fileName.Length > 260)
                    {
                        //file name too long truncate
                        fileName = fileName.Substring(0, 250) + Path.GetExtension(fileName);
                    }
                    var dest = context.Server.MapPath(fileName);
                    using (FileStream writer = new FileStream(dest, FileMode.Create))
                    {
                        var os = ECDHAES256s.AES.Encrypt(pass, file.InputStream);
                        os.Position = 0;
                        os.CopyTo(writer);
                    }
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write("ok");
            }
            catch {
                context.Response.ContentType = "text/plain";
                context.Response.Write("error");}
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
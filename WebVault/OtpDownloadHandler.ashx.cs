using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace WebVault
{
    /// <summary>
    /// Summary description for OtpDownloadHandler
    /// </summary>
    public class OtpDownloadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {


                    byte[] iv;

                    if (context.Application == null)
                    {
                        var tt = "error no application data";
                        var jr = new JavaScriptSerializer().Serialize(tt);
                        context.Response.ContentType = "text/json";
                        context.Response.Write(jr);
                        return;
                    }
                    var userid = ECDHAES256s.AES.Sha256(context.User.Identity.Name);

                    var pass = context.Application[context.Session.SessionID + "pass"] as string;

                    if (!Directory.Exists(context.Server.MapPath("/Upload/Files/OTP/" + context.Session.SessionID)))
                    {
                        Directory.CreateDirectory(context.Server.MapPath("/Upload/Files/OTP/" + context.Session.SessionID));
                    }

                    HttpFileCollection files = context.Request.Files;
                    string fileName = "";
                    List<string> FILES = new List<string>();
                    foreach (string key in files)
                    {
                        HttpPostedFile file = files[key];
                        fileName = file.FileName;
                        fileName = "/Upload/Files/OTP/" + context.Session.SessionID + "/" + Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName) + ".otp";
                        if (fileName.Length > 260)
                        {
                            //file name too long truncate
                            fileName = fileName.Substring(0, 250) + Path.GetExtension(fileName);
                        }
                        FILES.Add(context.Server.MapPath(fileName));                      

                        using (FileStream writer = new FileStream(context.Server.MapPath(fileName), FileMode.Create))
                        {
                           
                            file.InputStream.CopyTo(writer);
                        }
                    }

                    var ret = context.Server.MapPath(fileName);
                    var jsonizer = new JavaScriptSerializer().Serialize(ret);
                    context.Response.ContentType = "text/json";
                    context.Response.Write(jsonizer);
                }
                else
                {
                    var ret = "nofile";
                    var jsonizer = new JavaScriptSerializer().Serialize(ret);
                    context.Response.ContentType = "text/json";
                    context.Response.Write(jsonizer);
                }

            }
            catch (Exception ex)
            {
                var ret = ex.ToString();
                var jsonizer = new JavaScriptSerializer().Serialize(ret);
                context.Response.ContentType = "text/json";
                context.Response.Write(jsonizer);
            }
        }
        public void DownloadFile(HttpContext context)
        {     
            
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






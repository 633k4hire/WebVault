using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ECDHAES256s;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.SessionState;
namespace WebVault.Upload
{
    /// <summary>
    /// Encrypted File Uploader
    /// </summary>
    public class FileHandler : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    
                   
                    byte[] iv;
                    
                    if (context.Application==null)
                    {
                        var tt = "error no application data";
                        var jr = new JavaScriptSerializer().Serialize(tt);
                        context.Response.ContentType = "text/json";
                        context.Response.Write(jr);
                        return;
                    }
                    var userid = ECDHAES256s.AES.Sha256(context.User.Identity.Name);
                    var pass = context.Application[userid + "pass"] as string;
                   
                    if (! Directory.Exists(context.Server.MapPath("/Upload/Files/" +userid)))
                    {
                        Directory.CreateDirectory(context.Server.MapPath("/Upload/Files/" + userid));
                    }

                    HttpFileCollection files = context.Request.Files;
                    string fileName = "";
                    foreach (string key in files)
                    {                        
                        HttpPostedFile file = files[key];                      
                        fileName = file.FileName;
                        fileName = "/Upload/Files/"+userid+"/" +Path.GetFileNameWithoutExtension(fileName)+ Path.GetExtension(fileName);
                        if (fileName.Length>260)
                        {
                            //file name too long truncate
                            fileName = fileName.Substring(0,250)+ Path.GetExtension(fileName);
                        }
                     

                        
                        using (FileStream writer = new FileStream(context.Server.MapPath(fileName), FileMode.Create))
                        {
                            var os = ECDHAES256s.AES.Encrypt(pass, file.InputStream);
                            os.Position = 0;
                            os.CopyTo(writer);
                        }
                    }
                    var ret = "ok";
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
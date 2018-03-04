using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ECDHAES256s;
using System.Text;
using System.Web.Script.Serialization;

namespace WebVault.Upload
{
    /// <summary>
    /// Summary description for FileHandler
    /// </summary>
    public class FileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    AES aes = new AES();
                    var k = aes.Key;
                    byte[] iv;
                    
                    if (context.Application==null)
                    {
                        KeyValuePair<string, string>[] tt = new KeyValuePair<string, string>[2] { new KeyValuePair<string, string>("status", "error"), new KeyValuePair<string, string>("message", "no session") };
                        var jr = new JavaScriptSerializer().Serialize(tt);
                        context.Response.ContentType = "text/json";
                        context.Response.Write(jr);
                        return;
                    }
                    var userid = ECDHAES256s.AES.Sha256(context.User.Identity.Name);
                    k =context.Application[userid + "key"] as byte[];
                    iv = context.Application[userid + "iv"] as byte[];

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
                        fileName = "/Upload/Files/"+userid+"/" +Guid.NewGuid()+"_-_"+ Path.GetFileNameWithoutExtension(fileName)+ Path.GetExtension(fileName);
                        if (fileName.Length>260)
                        {
                            //file name too long truncate
                            fileName = fileName.Substring(0,255)+ Path.GetExtension(fileName);
                        }
                        using (MemoryStream outstream = aes.Encrypt(k, file.InputStream, iv))
                        using (FileStream fs = new FileStream(context.Server.MapPath(fileName), FileMode.Create))
                        {
                            outstream.Position = 0;
                            outstream.CopyTo(fs);
                        }   
                    }
                    KeyValuePair<string, string>[] ret = new KeyValuePair<string, string>[2] { new KeyValuePair<string, string>("status", "ok"), new KeyValuePair<string, string>("path", fileName) };
                    var jsonizer = new JavaScriptSerializer().Serialize(ret);
                    context.Response.ContentType = "text/json";
                    context.Response.Write(jsonizer);
                }
                else
                {
                    KeyValuePair<string, string>[] ret = new KeyValuePair<string, string>[2] { new KeyValuePair<string, string>("status", "error"), new KeyValuePair<string, string>("message", "no file") };
                    var jsonizer = new JavaScriptSerializer().Serialize(ret);
                    context.Response.ContentType = "text/json";
                    context.Response.Write(jsonizer);
                }
                
            }
            catch (Exception ex)
            {
                KeyValuePair<string, string>[] ret = new KeyValuePair<string, string>[2] { new KeyValuePair<string, string>("status", "error"), new KeyValuePair<string, string>("message", ex.Message) };
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
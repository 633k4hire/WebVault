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
    /// Summary description for OTPHandler
    /// </summary>
    public class OTPHandler : IHttpHandler, IRequiresSessionState
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
                    
                    var pass = context.Application[  context.Session.SessionID+ "pass"] as string;

                    if (!Directory.Exists(context.Server.MapPath("/Upload/Files/OTP/"+ context.Session.SessionID)))
                    {
                        Directory.CreateDirectory(context.Server.MapPath("/Upload/Files/OTP/"+ context.Session.SessionID));
                    }

                    HttpFileCollection files = context.Request.Files;
                    string fileName = "";
                    List<string> FILES = new List<string>();
                    foreach (string key in files)
                    {
                        HttpPostedFile file = files[key];
                        fileName = file.FileName;
                        if (Path.GetExtension(fileName) == ".kl0x")
                        {
                            //updatekeyfile
                            byte[] keyBytes = new byte[] { };
                            using (MemoryStream ms = new MemoryStream())
                            {
                                file.InputStream.CopyTo(ms);
                                ms.Position = 0;
                                keyBytes = ms.ToArray();
                            }
                            var newKey = Encoding.UTF8.GetString(keyBytes);
                            context.Application[context.Session.SessionID + "pass"] = newKey;
                            context.Application[context.Session.SessionID + "keyupdate"] = "true";
                            var rr = "keyfile";
                            var jj = new JavaScriptSerializer().Serialize(rr);
                            context.Response.ContentType = "text/json";
                            context.Response.Write(jj);
                            return;
                        }

                        if (Path.GetExtension(fileName)==".otp")
                        {
                            //decrypt
                            //get file
                            //its just the file name here becaue its the file uplaod
                            fileName = "/Upload/Files/OTP/" + context.Session.SessionID + "/" + Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName);

                            var fileInfo = new FileInfo(fileName.Map());
                            var dest = ("/Upload/Files/OTP/" + context.Session.SessionID+"/"+fileInfo.Name).TrimEnd(new char[] {'.','o','t','p' });
                            using (FileStream reader = new FileStream(fileName.Map(), FileMode.Open))
                            {
                                try
                                {

                                    using (FileStream writer = new FileStream(dest.Map(), FileMode.Create))
                                    {
                                        var os = ECDHAES256s.AES.Decrypt(reader, pass);
                                        if ( os.Length==0)
                                        {              
                                            context.Response.ContentType = "text/json";
                                            context.Response.Write(new JavaScriptSerializer().Serialize("nofile"));
                                            return;
                                        }
                                        os.Position = 0;
                                        os.CopyTo(writer);
                                    }


                                }
                                catch (Exception ex)
                                {                              
                                    context.Response.ContentType = "text/json";
                                    context.Response.Write(new JavaScriptSerializer().Serialize(ex.ToString()));
                                    return;
                                }
                            }
                            var aa = ("/Upload/Files/OTP/" + context.Session.SessionID + "/" + fileInfo.Name).TrimEnd(new char[] {'.','o','t','p' });
                            var jj = new JavaScriptSerializer().Serialize(aa);
                            context.Response.ContentType = "text/json";
                            context.Response.Write(jj);
                            return;

                        }
                        fileName = "/Upload/Files/OTP/"+ context.Session.SessionID+"/" + Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName)+".otp";
                        if (fileName.Length > 260)
                        {
                            //file name too long truncate
                            fileName = fileName.Substring(0, 250) + Path.GetExtension(fileName);
                        }
                        FILES.Add(context.Server.MapPath(fileName));


                        using (FileStream writer = new FileStream(context.Server.MapPath(fileName), FileMode.Create))
                        {
                            var os = ECDHAES256s.AES.Encrypt(pass, file.InputStream);
                            os.Position = 0;
                            os.CopyTo(writer);
                        }
                    }

                    var ret = fileName;
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
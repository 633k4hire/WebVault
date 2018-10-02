using ECDHAES256s;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace WebVault
{
    /// <summary>
    /// Decryptor File Downloader
    /// </summary>
    public class DownloadHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //get keys
            var userid = ECDHAES256s.AES.Sha256(context.User.Identity.Name);
            var pass = context.Application[userid + "pass"] as string;
           
            //get file
            var url = context.Request.Url;
            var path = context.Request.QueryString["f"];
            if (path == null) return;
            path = Encoding.UTF8.GetString(Convert.FromBase64String(path));
            var fileInfo = new FileInfo(path);
            using (FileStream reader = new FileStream(path, FileMode.Open))
            {       
               try
                {

                    using (MemoryStream writer = new MemoryStream())
                    {
                            var os = ECDHAES256s.AES.Decrypt(reader, pass);
                            os.Position = 0;
                            os.CopyTo(writer);
                        
                        context.Response.Clear();
                        //change name?
                        context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name + "");
                        context.Response.AddHeader("Content-Length", writer.Length.ToString());
                        context.Response.ContentType = "application/octet-stream";
                        context.Response.BinaryWrite(writer.ToArray());
                        context.Response.Flush();


                        context.Response.End();
                    }
                    
                       
                }
                catch {
                    
                }
            }
            

        }

        #endregion
    }
}

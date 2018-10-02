using ECDHAES256s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVault
{
    /// <summary>
    /// Summary description for key
    /// </summary>
    public class Key : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var salty = ECDHAES256s.AES.GetSalt();
                var id = context.Request.Path.TrimStart('/').Split(new string[] { ".key" }, StringSplitOptions.None)[0];
                              
                var pk = context.Request.QueryString["pk"];
                var cng = new CNG(HttpServerUtility.UrlTokenDecode(pk));
                cng = DH.B(cng); //MAKE KEY     
                
                context.Application[id + "pass"] = Convert.ToBase64String(cng.Key);
                context.Response.ContentType = "text/plain";
                context.Response.Write(HttpServerUtility.UrlTokenEncode(cng.PublicKey));
            }
            catch
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("error");

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
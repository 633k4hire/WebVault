using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace WebVault.Upload
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class Login : IHttpHandler, IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var id = context.Request.Path.TrimStart('/').Split(new string[] { ".login" }, StringSplitOptions.None)[0];

                var pass = context.Application[id + "pass"] as string;
                if (pass == null)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("error");
                }
                var q = context.Request.QueryString["q"];
                var decoded = Encoding.UTF8.GetString(ECDHAES256s.AES.Decrypt(HttpServerUtility.UrlTokenDecode(q), pass));
                var split = decoded.Split(',');
                var username = split[0];
                var password = split[1];



                // Validate the user password
                var manager = context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(username, password, false, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        var userid = ECDHAES256s.AES.Sha256(username);
                        context.Application[userid + "pass"] = password;
                        //respond
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("ok");
                        break;
                    case SignInStatus.LockedOut:
                        context.Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.Failure:
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("failure");
                        break;
                    default:
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("error");
                        break;
                }

            }
            catch {
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
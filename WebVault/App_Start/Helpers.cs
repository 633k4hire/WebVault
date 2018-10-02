using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVault
{
    public static class Helpers
    {
        public static string Terminate(this string str, string end = "/")
        {
            if (!str.EndsWith(end)) str += "/";
            return str;
        }
        public static string Map(this string input)
        {
            return HttpContext.Current.Server.MapPath(input);
        }
    }
}
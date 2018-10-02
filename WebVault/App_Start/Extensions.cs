using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WebVault
{
    public static class Extensions
    {
        public static SiteMaster SiteMaster(this Page page)
        {
            return page.Master as SiteMaster;
        }
    }
}
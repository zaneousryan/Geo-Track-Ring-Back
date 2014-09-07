using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon2.Web
{
    public class PageExpirationActionFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetNoServerCaching();
            filterContext.HttpContext.Response.Cache.SetAllowResponseInBrowserHistory(false);
            filterContext.HttpContext.Response.CacheControl = "no-cache";
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnActionExecuted(filterContext);
        }
    } 
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Team11_CA.Shop.Core.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionId = filterContext.HttpContext.Session["SessionID"];

            if(sessionId == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller","Account" },
                    {"action","Login" }
                });
            }
        }
    }
}
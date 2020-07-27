using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Web.Security
{
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        public static string BuyerRole => ConfigurationManager.AppSettings["BuyerRole"] ?? string.Empty;

        public CustomAuthorizeAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var redirectControllerName = ConfigurationManager.AppSettings["AccountController"] ?? string.Empty;
                var redirectActionName = ConfigurationManager.AppSettings["LoginAction"] ?? string.Empty;
                if (!filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    //var routeUrl = string.Format("~/{0}/{1}?returnUrl={2}", redirectControllerName, redirectActionName,
                    //    HttpContext.Current.Request.RawUrl);
                    filterContext.Result = new RedirectResult("~/Account/Login");
                    return;
                }
                var urlHelper=new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new JavaScriptResult
                {
                    Script = string.Format("window.location='/Account/Login';")
                };
                return;

            }

            if (filterContext.HttpContext.User.IsInRole(BuyerRole))
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            else
            {
                if (!string.IsNullOrEmpty(Roles))
                {
                    var roles = Roles.Split(',');
                    if (!roles.Any(x => filterContext.HttpContext.User.IsInRole(x)))
                    {
                        filterContext.Result = new ViewResult{ViewName = "~/Views/Shared/Error.cshtml"
                    }
                    ;
                    }
                }
            }

        }
    }
}
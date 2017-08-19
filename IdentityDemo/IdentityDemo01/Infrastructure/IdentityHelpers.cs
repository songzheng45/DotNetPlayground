using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace Users.Infrastructure
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper htmlHelper, string id)
        {
            AppUserManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }

        public static MvcHtmlString ClaimType(this HtmlHelper html, string claimType)
        {
            FieldInfo[] fields = typeof(ClaimTypes).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.GetValue(null) == claimType)
                {
                    return new MvcHtmlString(claimType);
                }
            }

            return new MvcHtmlString(claimType.Split('.', '/').Last());
        }
    }
}
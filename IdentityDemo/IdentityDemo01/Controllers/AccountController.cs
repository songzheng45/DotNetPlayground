using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new[] { "Access Denied" });
            }

            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(details.Name, details.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "用户名或密码错误");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                    // 模拟:将该登录用户认定来自从第三方系统的登录
                    ident.AddClaims(LocationClaimsProvider.GetClaims(ident));

                    // 添加地址在北京的员工权限
                    ident.AddClaims(ClaimsRoles.CreateRolesFromClaims(ident));

                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, ident);
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(details);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}
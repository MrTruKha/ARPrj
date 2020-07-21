using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ARPrj.Service;
using ARPrj.WebManagement.Models;
using Microsoft.AspNet.Identity.Owin;
using ARPrj.WebManagement.App_Start;
using System.Web.WebPages;
using ARPrj.DataAccess.Model;
using Microsoft.AspNet.Identity;

namespace PMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        //public AccountController(IUserService userService, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    _userService = userService;
        //    _signInManager = signInManager;
        //    _userManager = userManager;

        //}
        public AccountController(IUserService userService)
        {
            _userService = userService;

        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountView model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_userService.GetUserByName(model.UserName)!=null)
            {
                ModelState.AddModelError("Error", "UserName is already in use");
                return View(model);
            }
            if (model.Email.IsEmpty())
            {
                ModelState.AddModelError("Error", "Email cannot be empty ");
                return View(model);
            }
            if (_userService.CheckEmail(model.Email,""))
            {
                ModelState.AddModelError("Error", "Email is already in use");
                return View(model);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = new UserCommon() {UserName=model.UserName ,Email=model.Email,FullName=model.FullName,PasswordHash=model.Password,Status=true };
            var result = _userService.AddUser(user, "Customer");
            if (result)
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);

            }
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(AccountView user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = _userService.GetUser(user.UserName, user.Password);
        //        if (result != null)
        //        {
        //            FormsAuthentication.SetAuthCookie(user.UserName,true);
        //            Session["user"] =user;
        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError("Error", "Username or password not valid.");
        //        return View(user);
        //    }
        //    return View(user);
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountView model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!_userService.IsLocked(model.UserName))
            {
                ModelState.AddModelError("Error", "Invalid Username or Password.");
                return View(model);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.Failure:
                    ModelState.AddModelError("Error", "Invalid Username or Password.");
                    return View(model);
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        public ActionResult Logout()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return Redirect("Login");
        }
    }
}
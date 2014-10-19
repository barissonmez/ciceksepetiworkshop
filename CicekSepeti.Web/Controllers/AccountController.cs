using System.Web;
using System.Web.Mvc;
using CicekSepeti.Model;
using CicekSepeti.Service.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace CicekSepeti.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public AccountController(IUserService userService, IAuthenticationService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login( LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var result = _userService.Login(model);

            if (result.IsSuccess)
            {
                _authService.Login((UserDTO)result.Data, AuthenticationManager);

                return RedirectToAction("Index", "History");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
            }

            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            _authService.Logout(AuthenticationManager);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult SignUp(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(NewUserViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var result = _userService.Register(model);

            if (result.IsSuccess)
            {
                _authService.Login((UserDTO)result.Data, AuthenticationManager);

                return RedirectToAction("Index", "History");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
            }

            return View(model);
        }
    }
}
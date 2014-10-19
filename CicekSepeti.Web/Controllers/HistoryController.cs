using System;
using System.Web;
using System.Web.Mvc;
using CicekSepeti.Service.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace CicekSepeti.Web.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly IActivityService _activityService;

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public HistoryController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public ActionResult Index()
        {
            var userId = AuthenticationManager.User.Identity.GetUserId();
            var model = _activityService.GetActivitiesByUserId(new Guid(userId));
            return View(model);
        }
    }
}
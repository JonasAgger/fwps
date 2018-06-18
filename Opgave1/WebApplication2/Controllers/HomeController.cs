using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication2.Models;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
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
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Contact(VarroCountDTO varroCountDTO)
        {
            try
            {

                var varroCount = varroCountDTO.ToVarroCount();

                var controller = new VarroCountsController();

                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                varroCount.User = User.Identity.GetUserId();

                varroCount.Postnummer = user.Postnummer;

                controller.PostVarroCount(varroCount);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Confirmation");
                }
                return View("Confirmation");
            }
            catch
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Error");
                }
                return View("Error");
            }
        }
    }
}
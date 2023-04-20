using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoleEcIntranet.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult AccessError()
        {
            return View();
        }

        public ActionResult ErrorFormulario107()
        {
            return View();
        }

        public ActionResult ErrorGastosPersonales()
        {
            return View();
        }
    }
}
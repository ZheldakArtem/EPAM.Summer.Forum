using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult DefaultError()
        {
            return View();
        }
        public ActionResult ServerError()
        {
            return View();
        }
    }
}
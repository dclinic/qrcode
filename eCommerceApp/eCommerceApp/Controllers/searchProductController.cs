using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerceApp.Controllers
{
    public class searchProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string txtUID = "")
        {
            return RedirectToAction("Index", "product", new { uid = txtUID });
        }
    }
}
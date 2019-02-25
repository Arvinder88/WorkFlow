using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlow.Controllers
{
    public class VillageProfileController : Controller
    {
        // GET: VillageProfile
        [HttpGet]
        public ActionResult VillageProfile()
        {
            return View();
        }

    }
}
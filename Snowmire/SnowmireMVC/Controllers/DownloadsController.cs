using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnowmireMVC.Controllers
{
    public class DownloadsController : Controller
    {
        // GET: Downloads
        public ActionResult Client()
        {
            return View();
        }
    }
}
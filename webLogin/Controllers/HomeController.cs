using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webLogin.Models;

namespace webLogin.Controllers
{
    public class HomeController : Controller
    {
        private ColegioEntities1 _db = new ColegioEntities1();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

    }
}
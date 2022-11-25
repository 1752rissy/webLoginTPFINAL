using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using webLogin.Models;

namespace webLogin.Controllers
{
    public class RegistroController : Controller
    {
        private ColegioEntities1 _db = new ColegioEntities1();
        // GET: Registro
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Registrar()
        {
            return View("Registrar");
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(Usuario _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Usuario.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMd5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Usuario.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Index", new RouteValueDictionary(
                  new { controller = "Login", action = "Index" }));
                }
                else
                {
                    ViewBag.error = "el email ya existe";
                    return View();
                }
            }
            return View();
        }

        public static string GetMd5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] TargeData = md5.ComputeHash(fromData);

            string byte25 = null;

            for (int i = 0; i < TargeData.Length; i++)
            {
                byte25 += TargeData[i].ToString("x2");
            }

            return byte25;
        }

    }
}
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
    public class LoginController : Controller
    {
        private ColegioEntities1 _db = new ColegioEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View("");
        }

        public ActionResult Login()
        {
            return View();
        }

            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMd5(password);
                var data = _db.Usuario.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["NombreApellido"] = data.FirstOrDefault().Nombre + " " + data.FirstOrDefault().Apellido;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUsuario"] = data.FirstOrDefault().idUsuario;
                    return RedirectToAction("Index", new RouteValueDictionary(
                    new { controller = "Home", action = "Index"}));
                }
                else
                {
                    ViewBag.error = "Fallo el login";
                    return RedirectToAction("Index");
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
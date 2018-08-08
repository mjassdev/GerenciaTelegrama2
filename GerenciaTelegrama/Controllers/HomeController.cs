using System;
using GerenciaTelegrama.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GerenciaTelegrama.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //if (User.Identity.Name != "")
            //{
            //    return RedirectToAction("Dashboard");
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                if (Login(usuario.Username, usuario.Senha))
                {
                        FormsAuthentication.SetAuthCookie(usuario.Username, false);
                        return RedirectToAction("Dashboard", "Home");
                }
                ViewBag.ErroLogin = "Nome de usuário ou senha incorretos";
                return View(usuario);

            }
            ViewBag.ErroLogin = "Preencha todos os campos";
            return View(usuario);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult InternalServerError()
        {
            return View();
        }

        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        public static extern bool LogonUser(string userName, string domainName, string password, int logonType, int logonProvider, ref IntPtr phToken);
        public virtual bool Login(string username, string senha)
        {
            var isValid = IsValidateCredentials(username, senha, "sescto.com.br");
            return isValid;
        }

        public static bool IsValidateCredentials(string userName, string password, string domain)
        {
            var tokenHandler = IntPtr.Zero;
            var isValid = LogonUser(userName, domain, password, 2, 0, ref tokenHandler);
            return isValid;
        }
    }
}
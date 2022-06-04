using CERNA_T3.Models;
using CERNA_T3.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CERNA_T3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository context;
        private readonly IClaimService claim;

        public HomeController(IHomeRepository context, IClaimService claim)
        {
            this.context = context;
            this.claim = claim;
        }
        [Authorize]
        public IActionResult Index()
        {
            var historias = context.GetHistorias();
            return View("Index", historias);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = context.GetUsuario(username, password);

            if (user != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                claim.SetHttpContext(HttpContext);
                claim.Login(claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Login", "Usuario o contraseña incorrectos");
            return View("Login");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            claim.SetHttpContext(HttpContext);
            claim.Logout();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register", new Usuario()); ;
        }

        [HttpPost]
        public IActionResult Register(Usuario usuario, string Confirm)
        {
            if (usuario.Password != Confirm)
                ModelState.AddModelError("Confirm", "Las contraseñas no coinciden");

            var usuarios = context.GetUsuarios().Where(o => o.Username == usuario.Username).FirstOrDefault();
            if (usuarios != null)
                ModelState.AddModelError("Username", "Este usuario ya existe");


            if (ModelState.IsValid)
            {
                context.SaveUsuario(usuario);
                return RedirectToAction("Login");
            }
            else
                return View("Register", usuario);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Sexo = context.GetSexos();
            ViewBag.Especie = context.GetEspecies();
            return View("Create", new Historia());
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(Historia historia)
        {
            historia.FechaRegistro = DateTime.Now;
            if (historia.FechaNacimiento > DateTime.Now)
                ModelState.AddModelError("Fecha", "Fecha erronea");
            if (ModelState.IsValid)
            {
                context.SaveHistory(historia);
                return RedirectToAction("Index");
            }
            ViewBag.Sexo = context.GetSexos();
            ViewBag.Especie = context.GetEspecies();
            return View("Create");
        }
        public IActionResult Privacy()
        {
            return View("Privacy");
        }
        [Authorize]
        public IActionResult Raza(int IdEspecie)
        {
            return Json(context.GetRazas(IdEspecie));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

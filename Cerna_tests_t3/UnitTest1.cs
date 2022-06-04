using CERNA_T3.Controllers;
using CERNA_T3.Models;
using CERNA_T3.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Cerna_tests_t3
{
    public class Tests
    {
        [Test]
        public void Index()
        {
            var repo = new Mock<IHomeRepository>();
            repo.Setup(o => o.GetHistorias()).Returns(new List<Historia>());
            var claim = new Mock<IClaimService>();

            var controller = new HomeController(repo.Object, claim.Object);
            var view = controller.Index() as ViewResult;

            Assert.AreEqual("Index", view.ViewName);
        }

        [Test]
        public void CrearHistoriaPost()
        {
            var repo = new Mock<IHomeRepository>();
            repo.Setup(o => o.SaveHistory(new Historia()));
            var claim = new Mock<IClaimService>();

            var controller = new HomeController(repo.Object, claim.Object);
            var view = controller.Create(new Historia()) as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }
        [Test]
        public void Privacy()
        {
            var repo = new Mock<IHomeRepository>();
            var claim = new Mock<IClaimService>();
            var controller = new HomeController(repo.Object, claim.Object);
            var view = controller.Privacy() as ViewResult;

            Assert.AreEqual("Privacy", view.ViewName);
        }
        [Test]
        public void LoginGet()
        {
            var repo = new Mock<IHomeRepository>();
            var claim = new Mock<IClaimService>();
            var controller = new HomeController(repo.Object, claim.Object);
            var view = controller.Login() as ViewResult;

            Assert.AreEqual("Login", view.ViewName);
        }

        [Test]
        public void LoginPost()
        {
            var repo = new Mock<IHomeRepository>();
            repo.Setup(o => o.GetUsuario("User", "user")).Returns(new Usuario());

            var claim = new Mock<IClaimService>();

            var controller = new HomeController(repo.Object, claim.Object);
            var view = controller.Login("User", "user") as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void Salir()
        {
            var repo = new Mock<IHomeRepository>();
            var claim = new Mock<IClaimService>();

            var controller = new HomeController(repo.Object, claim.Object);
            var view = controller.LogOut() as RedirectToActionResult;

            Assert.AreEqual("Login", view.ActionName);
        }

        [Test]
        public void RegisterPost()
        {
            var repo = new Mock<IHomeRepository>();
            repo.Setup(o => o.GetUsuarios()).Returns(new List<Usuario>());
            repo.Setup(o => o.SaveUsuario(new Usuario()));
            var claim = new Mock<IClaimService>();

            var controller = new HomeController(repo.Object, claim.Object);
            var view = controller.Register(new Usuario() { Password = "cerna" }, "cerna") as RedirectToActionResult;

            Assert.AreEqual("Login", view.ActionName);
        }
    }
}
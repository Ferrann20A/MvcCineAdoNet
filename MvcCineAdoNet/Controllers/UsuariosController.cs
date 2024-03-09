﻿using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;
using MvcCineAdoNet.Extensions;

namespace MvcCineAdoNet.Controllers
{
    public class UsuariosController : Controller
    {
        private IRepositoryCineBook repo;
        public UsuariosController(IRepositoryCineBook repo)
        {
            this.repo = repo;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string email, string password)
        {
            Usuario user = await this.repo.RegisterUsuarioAsync(nombre, email, password);
            return RedirectToAction("LogIn");
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            Usuario user = await this.repo.LogInUserAsync(email, password);
            if (user == null)
            {
                ViewData["mensaje_error"] = "Credenciales incorrectas";
                return View();
            }
            else
            {
                HttpContext.Session.SetObject("usuario", user);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("usuario");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> PerfilUsuario(int idUsuario)
        {
            Usuario user = await this.repo.FindUsuarioAsync(idUsuario);
            return View(user);
        }

        public IActionResult Perfil()
        {
            return View();
        }

        public IActionResult MiListaUsuario()
        {
            return View();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JeffSite.Controllers
{
    public class LeitorController : Controller
    {
        private readonly SocialMidiaService _socialMidia;

        public LeitorController(SocialMidiaService socialMidia)
        {
            _socialMidia = socialMidia;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Redes = _socialMidia.FindAll();
            return View();
        }
    }
}

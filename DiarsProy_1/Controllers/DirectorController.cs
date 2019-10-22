using DiarsProy_1.DB;
using DiarsProy_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiarsProy_1.Controllers
{
    public class DirectorController : Controller
    {
        CieneContext context = new CieneContext();
        //GET: Director
        public ActionResult Index()
        {
            var directores = context.Directors;
            return View(directores);
        }
        [HttpGet]
        public ViewResult Guardar()
        {
            return View("Guardar", new Director());
        }
        [HttpPost]
        public ActionResult DirectorGuardar(Director director)
        {
            context.Directors.Add(director);
            context.SaveChanges();
            return RedirectToActionPermanent("Index", "Pelicula");
        }

    }
}
using DiarsProy_1.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DiarsProy_1.Models;

namespace DiarsProy_1.Controllers
{
    public class PeliculaController : Controller
    {
        CieneContext context = new CieneContext();

        public ActionResult Index()
        {
            var peliculas = context.Peliculas.Include(a => a.Director); ///using System.Data.Entity;

            return View("Index",peliculas);
        }
        [HttpGet]
        public ViewResult Guardar()
        {
            var directores =  context.Directors.ToList(); ///lista diretores

            ViewBag.directores = new SelectList(directores, "IdDirector", "Nombre");
            return View("Guardar", new Pelicula());
        }
        [HttpPost]
        public ActionResult PeliculaGuardar(Pelicula pelicula)
        {
            var directores = context.Directors.ToList();
            ViewBag.directores = new SelectList(directores, "IdDirector", "Nombre");

            if (String.IsNullOrEmpty(pelicula.NombrePelicula))
                ModelState.AddModelError("NombrePelicula", "Nombre de la pelicula es obligatorio");

            //if (!validarLetras(pelicula.NombrePelicula))
            //    ModelState.AddModelError("NombrePelicula", "Ingrese nombre valido");



            if (pelicula.Año== null || pelicula.Año=="")
                ModelState.AddModelError("Año", "El año es obligatorio");

            if (!validarnUMEROS(pelicula.Año))
                ModelState.AddModelError("Año", "El año es obligatorio");

            if (!ModelState.IsValid)
                return View("Guardar",pelicula);

            context.Peliculas.Add(pelicula);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public bool validarnUMEROS(string numString)
        {
            try
            {
                char[] charArr = numString.ToCharArray();
                foreach (char cd in charArr)
                {
                    if (!char.IsNumber(cd))
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return true;
        }

        public bool validarLetras(string numString)
        {
            string parte = numString.Trim();
            int count = parte.Count(s => s == ' ');
            if (parte == "")
            {
                return false;
            }
            else if (count >1)
            {
                return false;
            }

            return true;
        }

    }

}
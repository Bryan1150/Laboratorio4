using Laboratorio4.Handlers;
using Laboratorio4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio4.Controllers
{
    public class PlanetasController : Controller
    {
        // GET: Planetas

        public PlanetasHandler accesoDatos { get; set; }

        public PlanetasController()
        {
            accesoDatos = new PlanetasHandler();
        }

        public PlanetasController(PlanetasHandler planetasHandler)
        {
            accesoDatos = planetasHandler;
        }

        public ActionResult listadoDePlanetas()
        {
            PlanetasHandler accesoDatos = new PlanetasHandler();
            ViewBag.planetas = accesoDatos.obtenerTodoslosPlanetas();
            return View();
        }
        public ActionResult crearPlaneta()
        {
            return View("crearPlaneta");
        }

        [HttpPost]
        public ActionResult crearPlaneta(PlanetaModel planeta)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    PlanetasHandler accesoDatos = new PlanetasHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearPlaneta(planeta);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El planeta " + planeta.nombre + "fue creado con exito! owo";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Algo salio mal y no fue posible crear el planeta unu";
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salio mal y no fue posible crear el planeta unu";
                return View();
            }
        }

        [HttpGet]
        public ActionResult editarPlaneta(int? identificador)
        {
            ActionResult vista;
            try
            {
                PlanetasHandler accesoDatos = new PlanetasHandler();
                PlanetaModel planetaModificar = accesoDatos.obtenerTodoslosPlanetas().Find(smodel => smodel.id == identificador);
                if (planetaModificar == null)
                {
                    vista = RedirectToAction("listadoDePlanetas");
                }
                else
                {
                    vista = View(planetaModificar);
                }
            }
            catch
            {
                vista = RedirectToAction("listadoDePlanetas");
            }
            return vista;
        }

        [HttpPost]
        public ActionResult editarPlaneta(PlanetaModel planeta)
        {
            try
            {
                PlanetasHandler accesoDatos = new PlanetasHandler();
                accesoDatos.modificarPlaneta(planeta);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
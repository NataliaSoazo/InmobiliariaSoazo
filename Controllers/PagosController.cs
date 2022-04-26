using InmobiliariaSoazo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaSoazo.Controllers
{
    public class PagosController : Controller
    {
        RepositorioPagos repositorio;
        RepositorioContrato repoContrato;

        public PagosController()
        {
            repositorio = new RepositorioPagos();
            repoContrato = new RepositorioContrato();
        }

        // GET: Pagos
        public ActionResult Index()
        {
            try
            {
                var lista = repositorio.ObtenerTodos();
                if (TempData.ContainsKey("Id"))
                    ViewBag.Id = TempData["Id"];
                if (TempData.ContainsKey("Mensaje"))
                    ViewBag.Mensaje = TempData["Mensaje"];
                return View(lista);

            }
            catch (Exception ex)
            { throw; }

        }




        public ActionResult Detalles(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);//¿qué falta?
            }
            catch (Exception ex)
            {//poner breakpoints para detectar errores
                throw;
            }
        }
        

        // GET: Create
        public ActionResult Crear()
        {
            ViewBag.Contrato = repoContrato.ObtenerTodos();
         
            return View();
        }

        // POST:Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Pagos entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorio.Alta(entidad);
                    TempData["IdContrato"] = entidad.IdContrato;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Contratos = repoContrato.ObtenerTodos();
                    return View(entidad);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(entidad);
            }
        }




        // GET: Inmueble/Edit/5
        public ActionResult Editar(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(entidad);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Pagos entidad)
        {
            try
            {
                entidad.IdContrato = id;
                repositorio.Modificacion(entidad);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw;
                return View(entidad);
            }
        }

        // GET: Eliminar/5
        public ActionResult Eliminar(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(entidad);
        }

        // POST:Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, Inmueble entidad)
        {
            try
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(entidad);
            }
        }

    }
}

    


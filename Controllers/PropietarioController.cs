using InmobiliariaSoazo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaSoazo.Controllers
{


    public class PropietarioController : Controller
       
    {

        RepositorioPropietario repositorio;
        public PropietarioController()
        {
            repositorio = new RepositorioPropietario();
        }
          
        // GET: PropietarioController
        public ActionResult Index()
        {
          var lista = repositorio.Obtenertodos();
            return View(lista);
        }

        // GET: PropietarioController/Details/5
        public ActionResult Details(int id)
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

        // GET: PropietarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropietarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario p)
        {
            try
            {
                int res = repositorio.Alta(p);
                if (res > 0)
                    return RedirectToAction(nameof(Index));
                else

                    return View();
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "No se pudo agregar. ";
                return View();
            }
        }

        
        // GET: InquilinoController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);//pasa el modelo a la vista
            }
            catch (Exception ex)
            {//poner breakpoints para detectar errores
                throw;
            }
        }


        // POST: PropietarioController/Edit/5
        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            // Si en lugar de IFormCollection ponemos Propietario, el enlace de datos lo hace el sistema
            Propietario p = null;
            try
            {
                p = repositorio.ObtenerPorId(id);
                // En caso de ser necesario usar: 
                //
                //Convert.ToInt32(collection["CAMPO"]);
                //Convert.ToDecimal(collection["CAMPO"]);
                //Convert.ToDateTime(collection["CAMPO"]);
                //int.Parse(collection["CAMPO"]);
                //decimal.Parse(collection["CAMPO"]);
                //DateTime.Parse(collection["CAMPO"]);
                ////////////////////////////////////////
                p.Nombre = collection["Nombre"];
                p.Apellido = collection["Apellido"];
                p.Dni = collection["Dni"];
                p.Email = collection["Email"];
                p.Telefono = collection["Telefono"];
                p.Domicilio = collection["Domicilio"];
                repositorio.Modificacion(p);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {//poner breakpoints para detectar errores
                throw;
            }
        }



        // GET: PropietarioController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {//poner breakpoints para detectar errores
                throw;
            }
        }

        // POST: PropietarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Propietario entidad)
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

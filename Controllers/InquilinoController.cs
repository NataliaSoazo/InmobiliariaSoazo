using InmobiliariaSoazo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaSoazo.Controllers
{
    public class InquilinoController : Controller
    {
        RepositorioInquilino repositorio;
        public InquilinoController()
        {
            repositorio = new RepositorioInquilino();
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
        public ActionResult Create(Inquilino i)
        {
            try
            {
                int res = repositorio.Alta(i);
                if (res > 0)
                    return RedirectToAction(nameof(Index));
                else

                    return View();
            }
            catch (Exception ex)
            {
                throw;
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


       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            // Si en lugar de IFormCollection ponemos Propietario, el enlace de datos lo hace el sistema
            Inquilino i = null;
            try
            {
                i = repositorio.ObtenerPorId(id);
                // En caso de ser necesario usar: 
                //
                //Convert.ToInt32(collection["CAMPO"]);
                //Convert.ToDecimal(collection["CAMPO"]);
                //Convert.ToDateTime(collection["CAMPO"]);
                //int.Parse(collection["CAMPO"]);
                //decimal.Parse(collection["CAMPO"]);
                //DateTime.Parse(collection["CAMPO"]);
                ////////////////////////////////////////
                i.Nombre = collection["Nombre"];
                i.Apellido = collection["Apellido"];
                i.Dni = collection["Dni"];
                i.Telefono = collection["Telefono"];
                i.Email = collection["Email"];
                i.Domicilio = collection["Domicilio"];

                repositorio.Modificacion(i);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
               
            }
            catch (Exception ex)
            {//poner breakpoints para detectar errores
                throw;
            }
        }



        // GET: InquilinoController/Delete/5
    
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

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Inquilino entidad)
        { 
            try
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {//poner breakpoints para detectar errores
                throw;
            }
        }
    }
}

    


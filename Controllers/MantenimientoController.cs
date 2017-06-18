using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireDrink.Models;
using System.IO;
using System.Data;
using System.Data.Entity;

namespace FireDrink.Controllers
{
    public class MantenimientoController : Controller
    {
        //
        // GET: /Marca/

        FireDrinkBDContext BD = new FireDrinkBDContext();

        //======================================================================//
        //                        MANTENIMIENTO LISTADOS                        //
        //======================================================================//
        //LISTADO DE MARCAS
        public ActionResult ListarMarca() {
            var marca = from m in BD.MARCA
                        select new Marca {
                            COD_MARCA = m.COD_MARCA,
                            MAR_PRO = m.MAR_PRO
                        };
            ViewBag.ocultarCategorias = false;
            ViewBag.marcas = marca.ToList();
            return View();
        }

        //LISTADO DE TIPOS
        public ActionResult ListarTipo(){
            var tipo = from m in BD.TIPO
                       select new Tipo {
                           COD_TIPO = m.COD_TIPO,
                           TIP_PRO = m.TIP_PRO
                       };
            ViewBag.ocultarCategorias = false;
            ViewBag.tipos = tipo.ToList();
            return View();
        }

        //======================================================================//
        //                        MANTENIMIENTO REGISTRAR                       //
        //======================================================================//
        //REGISTRAR MARCAS
        public ActionResult RegistrarMarca() {
            MARCA m = new MARCA();
            return View(m);
        }
        [HttpPost]
        public ActionResult RegistrarMarca(MARCA m) {
            if (!ModelState.IsValid) {
                return View(m);
            }
            try {
                BD.MARCA.Add(m);
                BD.SaveChanges();
                return RedirectToAction("ListarMarca");
            }
            catch (Exception ex) {
                ValidateModel(ex.Message);
                return View(m);
            }
        }

        //REGISTRAR TIPOS
        public ActionResult RegistrarTipo() {
            TIPO t = new TIPO();
            return View(t);
        }
        [HttpPost]
        public ActionResult RegistrarTipo(TIPO t) {
            if (!ModelState.IsValid) {
                return View(t);
            }
            try {
                BD.TIPO.Add(t);
                BD.SaveChanges();
                return RedirectToAction("ListarTipo");
            }
            catch (Exception ex) {
                ValidateModel(ex.Message);
                return View(t);
            }
        }

        //======================================================================//
        //                        MANTENIMIENTO DETALLES                        //
        //======================================================================//
        public ActionResult DetalleTipo(int id = 0)
        {
            TIPO tipo = BD.TIPO.Find(id);
            if (tipo == null)
            {
                return HttpNotFound();
            }
            return View(tipo);
        }

        public ActionResult DetalleMarca(int id = 0)
        {
            MARCA marca = BD.MARCA.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        //======================================================================//
        //                        MANTENIMIENTO MODIFICAR                       //
        //======================================================================//
        //EDITAR TIPO
        public ActionResult EditarTipo(int id = 0)
        {
            TIPO tipo = BD.TIPO.Find(id);
            if (tipo == null)
            {
                Session["mensaje"] = "Parece que no existe producto con ese numero";
                return RedirectToAction("ListarTipo");
            }
            return View(tipo);
        }
        [HttpPost]
        public ActionResult EditarTipo(TIPO tipo)
        {
            if (ModelState.IsValid)
            {
                BD.Entry(tipo).State = EntityState.Modified;
                BD.SaveChanges();
                Session["mensaje"] = "Tipo Modificado con Exito";
                return RedirectToAction("ListarTipo");
            }
            return View(tipo);
        }

        //EDITAR MARCA
        public ActionResult EditarMarca(int id = 0)
        {
            MARCA marca = BD.MARCA.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }
        [HttpPost]
        public ActionResult EditarMarca(MARCA marca)
        {
            if (ModelState.IsValid)
            {
                BD.Entry(marca).State = EntityState.Modified;
                BD.SaveChanges();
                return RedirectToAction("ListarMarca");
            }
            return View(marca);
        }

        //======================================================================//
        //                        MANTENIMIENTO ELIMIMAR                        //
        //======================================================================//
        //ELIMINAR TIPO
        public ActionResult EliminarTipo(int id = 0)
        {
            TIPO tipo = BD.TIPO.Find(id);
            if (tipo == null)
            {
                return HttpNotFound();
            }
            return View(tipo);
        }
        [HttpPost, ActionName("EliminarTipo")]
        public ActionResult EliminarTipos(int id)
        {
            TIPO tipo = BD.TIPO.Find(id);
            BD.TIPO.Remove(tipo);
            BD.SaveChanges();
            return RedirectToAction("ListarTipo");
        }

        //ELIMINAR MARCA
        public ActionResult EliminarMarca(int id = 0)
        {
            MARCA marca = BD.MARCA.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }
        [HttpPost, ActionName("EliminarMarca")]
        public ActionResult EliminarMarcas(int id)
        {
            MARCA marca = BD.MARCA.Find(id);
            BD.MARCA.Remove(marca);
            BD.SaveChanges();
            return RedirectToAction("ListarMarca");
        }
    }
}
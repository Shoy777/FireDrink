using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireDrink.Models;
using System.Data;
using System.Data.Entity;
using System.IO;

namespace FireDrink.Controllers
{
    public class ProductoController : Controller
    {
        private FireDrinkBDContext db = new FireDrinkBDContext();

        //
        // GET: /Producto/

        public ActionResult Index()
        {
            var producto = from p in db.PRODUCTO
                           join m in db.MARCA on p.COD_MARCA equals m.COD_MARCA
                           join t in db.TIPO on p.COD_TIPO equals t.COD_TIPO
                           select new Producto
                           {
                               COD_PRO = p.COD_PRO,
                               COD_TIPO = t.TIP_PRO,
                               COD_MARCA = m.MAR_PRO,
                               DES_PRO = p.DES_PRO,
                               PRE_PRO = (double)p.PRE_PRO,
                               STOCK = p.STOCK,
                               NOMFOTO = p.NOMFOTO
                           };

            ViewBag.producto = producto.ToList();
            ViewBag.ocultarCategorias = false;
            return View();
        }

        //
        // GET: /Producto/Details/5

        public ActionResult Details(int id = 0)
        {
            PRODUCTO producto = db.PRODUCTO.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        //
        // GET: /Producto/Create

        public ActionResult Create()
        {
            ViewBag.Categorias = new SelectList(db.TIPO, "COD_TIPO", "TIP_PRO");
            ViewBag.Marcas = new SelectList(db.MARCA, "COD_MARCA", "MAR_PRO");
            return View();
        }

        //
        // POST: /Producto/Create

        [HttpPost]
        public ActionResult Create(PRODUCTO producto, HttpPostedFileBase archivo)
        {
            if (ModelState.IsValid)
            {
                string nombreArchivo = "";
                if (archivo != null/* && archivo.ContentLength > 0*/)
                {
                    nombreArchivo = Path.GetFileName(archivo.FileName);
                    var path = Path.Combine(Server.MapPath("~/imagenes/productosimg/"), nombreArchivo);
                    archivo.SaveAs(path);
                }

                producto.NOMFOTO = nombreArchivo;

                db.PRODUCTO.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categorias = new SelectList(db.TIPO, "COD_TIPO", "TIP_PRO");
            ViewBag.Marcas = new SelectList(db.MARCA, "COD_MARCA", "MAR_PRO");
            return View(producto);
        }

        //
        // GET: /Producto/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PRODUCTO producto = db.PRODUCTO.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categorias = new SelectList(db.TIPO, "COD_TIPO", "TIP_PRO");
            ViewBag.Marcas = new SelectList(db.MARCA, "COD_MARCA", "MAR_PRO");
            return View(producto);
        }

        //
        // POST: /Producto/Edit/5

        [HttpPost]
        public ActionResult Edit(PRODUCTO producto, HttpPostedFileBase archivo)
        {
            if (ModelState.IsValid)
            {
                string nombreArchivo = "";
                if (archivo != null && archivo.ContentLength > 0)
                {
                    nombreArchivo = Path.GetFileName(archivo.FileName);
                    var path = Path.Combine(Server.MapPath("~/imagenes/productosimg/"), nombreArchivo);
                    archivo.SaveAs(path);
                    producto.NOMFOTO = nombreArchivo;
                }

                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = new SelectList(db.TIPO, "COD_TIPO", "TIP_PRO");
            ViewBag.Marcas = new SelectList(db.MARCA, "COD_MARCA", "MAR_PRO");
            return View(producto);
        }

        //
        // GET: /Producto/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PRODUCTO producto = db.PRODUCTO.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        //
        // POST: /Producto/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCTO producto = db.PRODUCTO.Find(id);
            db.PRODUCTO.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
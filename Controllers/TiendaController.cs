﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireDrink.Models;
using System.Threading;

namespace FireDrink.Controllers
{
    public class TiendaController : Controller
    {
        //
        // GET: /Tienda/
        FireDrinkBDContext storeBD = new FireDrinkBDContext();

        public ActionResult Index(int idCategoria=0)
        {
             if (idCategoria == 0)
            {
                var lstProductos = storeBD.PRODUCTO.ToList();
                return View(lstProductos);
            }
             var lstProductos2 = storeBD.PRODUCTO.Where(p=>p.COD_TIPO==idCategoria);

            return View(lstProductos2);
        }

        public ActionResult Detalle(int idproducto)
        {
            var producto = storeBD.PRODUCTO.Find(idproducto);
            return View(producto);
        }

        public ActionResult Carrito()
        {
            return View();
        }

        public ActionResult Buscar(string search_query_top)
        {
            var lstProductos = storeBD.PRODUCTO
                .Where(p => 
                    p.DES_PRO.Contains(search_query_top) || 
                    p.MARCA.MAR_PRO.Contains(search_query_top) ||
                    p.TIPO.TIP_PRO.Contains(search_query_top));

            return View("Index",lstProductos);
        }

    }
}

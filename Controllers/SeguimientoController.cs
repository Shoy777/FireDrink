using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireDrink.Models;
using System.Data.Entity;
using System.Data;
using System.Threading;


namespace FireDrink.Controllers
{
    public class SeguimientoController : Controller
    {
        //
        // GET: /Seguimiento/

        private FireDrinkBDContext db = new FireDrinkBDContext();

        public ActionResult Index()
        {
            var pedido = db.PEDIDO.Include(p => p.USUARIO).Include(p => p.UBIGEO).Include(p => p.estado).Include(p => p.tipo_compPago).Include(p => p.TIPO_PAGO);
           
            return View(pedido.ToList());
        }
        public ActionResult Seguimiento(int idPedido)
        {
            var pedido = db.PEDIDO.Include(p => p.USUARIO).Include(p => p.UBIGEO).Include(p => p.estado).Include(p => p.tipo_compPago).Include(p => p.TIPO_PAGO).FirstOrDefault(p => p.idpedido == idPedido);
            var pro_ped = db.pedido_productos.Include(p => p.PRODUCTO).Where(pp => pp.idpedido == idPedido);
            ViewData["pedido"] = pedido;
            return View(pro_ped);
        }
        public ActionResult cambiaEstado(int idPedido)
        {
            var pedido = db.PEDIDO.Include(p => p.USUARIO).Include(p => p.UBIGEO).Include(p => p.estado).Include(p => p.tipo_compPago).Include(p => p.TIPO_PAGO).FirstOrDefault(p => p.idpedido == idPedido);
            pedido.idestado = pedido.idestado + 1;
            db.Entry(pedido).State = EntityState.Modified;
            db.SaveChanges();

            PEDIDO ped = pedido;
            return RedirectToAction("Index");
        }

        public ActionResult rechazarPedido(int idPedido)
        {
            var pedido = db.PEDIDO.Include(p => p.USUARIO).Include(p => p.UBIGEO).Include(p => p.estado).Include(p => p.tipo_compPago).Include(p => p.TIPO_PAGO).FirstOrDefault(p => p.idpedido == idPedido);
            pedido.idestado = 10;
            db.Entry(pedido).State = EntityState.Modified;
            db.SaveChanges();

            PEDIDO ped = pedido;
            return RedirectToAction("Index");
        }

    }
}

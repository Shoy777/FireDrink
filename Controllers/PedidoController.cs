using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireDrink.Models;
using System.Net.Mail;

namespace FireDrink.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private FireDrinkBDContext db = new FireDrinkBDContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pedido()
        {
            ViewBag.iddistrito = new SelectList(db.UBIGEO, "COD_UBIGEO", "DISTRITO");
            ViewBag.idtipopago = new SelectList(db.TIPO_PAGO, "idtipopago", "descrip");
            ViewBag.idtipo_compPago = new SelectList(db.tipo_compPago, "idtipo_compPago", "descrip");
            USUARIO cl = getClienteLogueado(User.Identity.Name);
            PEDIDO ped = new PEDIDO();
            ped.contacto_nom = cl.NOMBRE;
            ped.contacto_ape = cl.APPATERNO + " " + cl.APEMATERNO;
            ped.contacto_mail = cl.CORREO;

            return View(ped);
        }
        [HttpPost]
        public ActionResult Pedido(PEDIDO pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.idestado = 3;

                if (Request.IsAuthenticated)
                {
                    USUARIO cl = getClienteLogueado(User.Identity.Name);
                    pedido.idCliente = cl.IDUSUARIO;
                }
                else
                {
                    pedido.idCliente = 1;
                }
                pedido.fechaPedido = DateTime.Now;
                return View("Pago", pedido);
            }

            ViewBag.iddistrito = new SelectList(db.UBIGEO, "COD_UBIGEO", "DISTRITO");
            ViewBag.idtipopago = new SelectList(db.TIPO_PAGO, "idtipopago", "descrip");
            ViewBag.idtipo_compPago = new SelectList(db.tipo_compPago, "idtipo_compPago", "descrip");
            return View(pedido);
        }

        public ActionResult Pago()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pago(PEDIDO pedido)
        {
            db.PEDIDO.Add(pedido);
            db.SaveChanges();

            //Process the order
            var cart = CarritodeCompras.GetCart(this.HttpContext);
            string cartDetalle = cart.tablaDetalle();
            PEDIDO ped = cart.CreateOrder(pedido);
            db.Entry(ped).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Completo", new { idPed = pedido.idpedido });
        }

        public ActionResult Completo(int idPed)
        {
            PEDIDO pedido = db.PEDIDO.Find(idPed);
            return View(pedido);
        }

        public USUARIO getClienteLogueado(string email)
        {
            USUARIO cli = db.USUARIO.FirstOrDefault(c => c.CORREO == email);
            return cli;
        }

    }
}

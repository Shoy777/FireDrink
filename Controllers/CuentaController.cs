using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using FireDrink.Models;
using System.Data.Entity;

namespace FireDrink.Controllers
{
   
    public class CuentaController : Controller
    {
        //
        // GET: /Cuenta/
        FireDrinkBDContext bd = new FireDrinkBDContext();

        public ActionResult Logueo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logueo(LoginModel cli)
        {
           var varUsu = from u in bd.USUARIO
                        where u.CORREO == cli.CORREO
                        select new Usuario
                        {
                            IDUSUARIO = u.IDUSUARIO,
                            CORREO = u.CORREO,
                            NOMBRE = u.NOMBRE,
                            APPATERNO = u.APPATERNO,
                            APEMATERNO = u.APEMATERNO,
                            DNI = u.DNI,
                            IDROL = u.IDROL
                        };

           Usuario usuario = (Usuario)varUsu.FirstOrDefault();

           //Verificar que el modelo de datos sea válido en cuanto a la definición de las propiedades
           if (ModelState.IsValid)
           {
               //Verificar que el email y clave exista utilizando el método privado
                if (verificaCliente(cli.CORREO, cli.CLAVE).Equals("USUARIO"))
                {
                    FormsAuthentication.SetAuthCookie(cli.CORREO, false);
                    //crea variable de usuario con el correo del usuario
                    Session["usuario"] = usuario;
                    Session["mensaje"] = "Bienvenido, " + usuario.NOMBRE;
                    return RedirectToAction("Index", "Tienda");
                    //dirigir al controlador home vista Index una vez se a autenticado en el sistema
                }
            }
           //adicionar mensaje de error al model
           Session["mensaje"] = "Correo o contraseña incorrectos, intente nuevamente";
           return View(cli);
        }

     
        public ActionResult Registrar(string email_nuevo)
        {
            ViewData["email_nuevo"] = email_nuevo;
            USUARIO cl = new USUARIO();
            cl.CORREO = email_nuevo;

            ViewBag.ubigeo = new SelectList(bd.UBIGEO, "COD_UBIGEO", "DISTRITO");
            return View(cl);
        }

        [HttpPost]
        public ActionResult Registrar(USUARIO cli)
        {
            if (ModelState.IsValid)
            {
                int i = 0;

                if (cli.CORREO == null)
                {
                    ModelState.AddModelError("", "Formato incorrecto de email");
                    i++;
                }
                if (cli.CLAVE == null)
                {
                    ModelState.AddModelError("", "Formato incorrecto de clave");
                    i++;
                }
                if (cli.NOMBRE == null)
                {
                    ModelState.AddModelError("", "Formato incorrecto de nombre");
                    i++;
                }
                if (cli.APPATERNO == null)
                {
                    ModelState.AddModelError("", "Formato incorrecto de A.Paterno");
                    i++;
                }

                if (cli.APEMATERNO == null)
                {
                    ModelState.AddModelError("", "Formato incorrecto de A. Materno");
                    i++;
                }
                if (cli.DNI == null)
                {
                    ModelState.AddModelError("", "Formato incorrecto de dni");
                    i++;
                }
                if (cli.FECNACIMIENTO == null)
                {
                    ModelState.AddModelError("", "Formato incorrecto de fecha nacimiento");
                    i++;
                }
                if (cli.TELEFONO == null)
                {
                    ModelState.AddModelError("", "Formato incorrecto de Telefono");
                    i++;
                }
                if (cli.CELULAR == null)
                {
                    ModelState.AddModelError("", "Formato incorrecto de Celular");
                    i++;
                }
                if (cli.COD_UBIGEO == -1)
                {
                    ModelState.AddModelError("", "Seleccione Distrito");
                    i++;
                }

                if (i > 0)
                {
                    return View(cli);
                }
                else
                {
                    cli.FECREGISTRO = DateTime.Now;
                    cli.ESTADO = 1;
                    cli.IDROL = 2;
                    bd.USUARIO.Add(cli);
                    bd.SaveChanges();
                    FormsAuthentication.SetAuthCookie(cli.CORREO, false); //crea variable de usuario con el correo del usuario
                    return RedirectToAction("Index", "Tienda");
                }
            }
            ViewBag.ubigeo = new SelectList(bd.UBIGEO, "COD_UBIGEO", "DISTRITO");
            return View();

        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Tienda");
        }

        public ActionResult VerificaCliente(USUARIO cli)
        {
            if (ModelState.IsValid)
            {
                if (cli.CORREO == null)
                {
                    ViewData["error"] = "Por favor escriba un email";
                    return View("Logueo");
                }
                else
                {
                    if (verificaEmailCliente(cli.CORREO))
                    {
                        ViewData["error"] = "Ya existe usuario con el mismo correo electrónico";
                        return View("Logueo");
                    }
                }
            }
            return RedirectToAction("Registrar", "Cuenta", new { email_nuevo = cli.CORREO });
        }

        [Authorize]
        public ActionResult MisPedidos() 
        {
            USUARIO cl = getClienteLogueado(User.Identity.Name);
            var pedido = bd.PEDIDO.Include(p => p.USUARIO).Include(p => p.UBIGEO).Include(p => p.estado).Include(p => p.tipo_compPago).Include(p => p.TIPO_PAGO).Where(p=>p.idCliente==cl.IDUSUARIO);
            return View(pedido.ToList());
        }

        private string verificaCliente(string email, string password)
        {
            string Isvalid = "";

            var user = bd.USUARIO.FirstOrDefault(u => u.CORREO == email); //consultar el primer registro con los el email del usuario
            
            if (user != null)
            {
                if (user.CLAVE == password) //Verificar password del usuario
                {
                    Isvalid = "USUARIO";
                }
            }
            return Isvalid;
        }

        private bool verificaEmailCliente(string email_nuevo)
        {
            bool Isvalid = false;

            var user = bd.USUARIO.FirstOrDefault(u => u.CORREO == email_nuevo); //consultar el primer registro con los el email del usuario
            if (user != null)
            {
                Isvalid = true;
            }

            return Isvalid;
        }

        public USUARIO getClienteLogueado(string email)
        {
            USUARIO cli = bd.USUARIO.FirstOrDefault(c => c.CORREO == email);
            return cli;
        }

    }
}

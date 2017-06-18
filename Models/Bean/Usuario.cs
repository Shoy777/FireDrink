using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireDrink.Models
{
    public class Usuario
    {
        public int IDUSUARIO { get; set; }
        public string CORREO { get; set; }
        public string CLAVE { get; set; }
        public string NOMBRE { get; set; }
        public string APPATERNO { get; set; }
        public string APEMATERNO { get; set; }
        public string DNI { get; set; }
        public DateTime FECNACIMIENTO { get; set; }
        public string TELEFONO { get; set; }
        public string CELULAR { get; set; }
        public string COD_UBIGEO { get; set; }
        public UBIGEO ubigeo { get; set; }
        public int IDROL { get; set; }
        public Rol rol { get; set; }
        public bool VERIFICARUSUARIO { get; set; }
        public bool ESTADO { get; set; }

        public Usuario()
        {
            IDUSUARIO = 0;
            rol = new Rol();
            VERIFICARUSUARIO = false;
            ESTADO = false;
        }
    }
}
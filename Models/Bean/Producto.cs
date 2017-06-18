using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireDrink.Models
{
    public class Producto
    {
        public int COD_PRO { get; set; }
        public string COD_TIPO { get; set; }
        public string COD_MARCA { get; set; }
        public string DES_PRO { get; set; }
        public double PRE_PRO { get; set; }
        public int STOCK { get; set; }
        public string NOMFOTO { get; set; }
        //public byte[] picture1 { get; set; }

        public virtual MARCA MARCA { get; set; }
        public virtual TIPO TIPO { get; set; }

        public int cantidad { get; set; }
    }
}
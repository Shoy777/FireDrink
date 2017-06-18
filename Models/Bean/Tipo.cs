using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireDrink.Models
{
    public class Tipo
    {
        public int COD_TIPO { get; set; }
        public string TIP_PRO { get; set; }

        public virtual List<PRODUCTO> PRODUCTO { get; set; }
    }
}
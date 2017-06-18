using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireDrink.Models
{
    public class Marca
    {
        public int COD_MARCA { get; set; }
        public string MAR_PRO { get; set; }

        public virtual List<PRODUCTO> PRODUCTO { get; set; }
    }
}
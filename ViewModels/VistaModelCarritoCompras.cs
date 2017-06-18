using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FireDrink.Models;

namespace FireDrink.ViewModels
{
    public class VistaModelCarritoCompras
    {
        public List<CART> CarritoItems{ get; set; }
        public decimal CarritoTotal { get; set; }
    }
}
namespace FireDrink.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pedido_productos
    {
        [Key]
        public int idpedido_productos { get; set; }

        public decimal? precio { get; set; }

        public int? cantidad { get; set; }

        public decimal? total { get; set; }

        public int idpedido { get; set; }

        public int idproducto { get; set; }

        public virtual PEDIDO PEDIDO { get; set; }
        public virtual PRODUCTO PRODUCTO { get; set; }
    }
}

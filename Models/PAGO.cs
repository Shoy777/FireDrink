namespace FireDrink.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PAGO")]
    public partial class PAGO
    {
        [Key]
        public int idpago { get; set; }

        public int idpedido { get; set; }

        public byte[] documento { get; set; }

        public virtual PEDIDO PEDIDO { get; set; }
    }
}

namespace FireDrink.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CART")]
    public partial class CART
    {
        [Key]
        public int COD_CART { get; set; }

        [Required]
        [StringLength(50)]
        public string CartId { get; set; }

        public int COD_PRO { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaPedido { get; set; }

        public virtual PRODUCTO PRODUCTO { get; set; }

        [NotMapped]
        public decimal? subTotal { get { return PRODUCTO.PRE_PRO * Cantidad; } }
    }
}

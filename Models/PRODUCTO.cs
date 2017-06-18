namespace FireDrink.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUCTO")]
    public partial class PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTO()
        {
            CART = new HashSet<CART>();
            pedido_productos = new HashSet<pedido_productos>();
        }

        [Key]
        public int COD_PRO { get; set; }

        public int COD_TIPO { get; set; }

        public int COD_MARCA { get; set; }

        [StringLength(100)]
        public string DES_PRO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PRE_PRO { get; set; }

        public int STOCK { get; set; }

        [StringLength(100)]
        public string NOMFOTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CART> CART { get; set; }
        public virtual ICollection<pedido_productos> pedido_productos { get; set; }

        public virtual MARCA MARCA { get; set; }

        public virtual TIPO TIPO { get; set; }
    }
}

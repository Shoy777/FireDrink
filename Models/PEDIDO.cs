namespace FireDrink.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PEDIDO")]
    public partial class PEDIDO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PEDIDO()
        {
            PAGO = new HashSet<PAGO>();
            pedido_estados = new HashSet<pedido_estados>();
            pedido_productos = new HashSet<pedido_productos>();
            Seguimiento = new HashSet<Seguimiento>();
        }

        [Key]
        public int idpedido { get; set; }

        [StringLength(255)]
        public string contacto_nom { get; set; }

        [StringLength(255)]
        public string contacto_ape { get; set; }

        [StringLength(255)]
        public string contacto_mail { get; set; }

        [StringLength(100)]
        public string contacto_fijo { get; set; }

        [StringLength(100)]
        public string contacto_movil { get; set; }

        public int? tipoentrega { get; set; }

        public DateTime? fechaPedido { get; set; }

        public DateTime? fechaentrega { get; set; }

        public int? tcomprobante { get; set; }

        [StringLength(11)]
        public string ruc { get; set; }

        [StringLength(75)]
        public string razonsocial { get; set; }

        [StringLength(255)]
        public string entrega_dir { get; set; }

        [StringLength(255)]
        public string entrega_urba { get; set; }

        public decimal subTotal { get; set; }

        public decimal igv { get; set; }

        public decimal totalpedido { get; set; }

        public string notas { get; set; }

        public int idCliente { get; set; }

        public int iddistrito { get; set; }

        public int idtipo_compPago { get; set; }

        public int idestado { get; set; }

        public int idtipopago { get; set; }

        [StringLength(255)]
        public string entrega_ref { get; set; }

        public virtual estado estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAGO> PAGO { get; set; }

        public virtual USUARIO USUARIO { get; set; }

        public virtual UBIGEO UBIGEO { get; set; }

        public virtual tipo_compPago tipo_compPago { get; set; }

        public virtual TIPO_PAGO TIPO_PAGO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_estados> pedido_estados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_productos> pedido_productos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seguimiento> Seguimiento { get; set; }
    }
}

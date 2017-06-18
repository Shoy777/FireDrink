namespace FireDrink.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seguimiento")]
    public partial class Seguimiento
    {
        [Key]
        [Column(Order = 0)]
        public int idSeguimiento { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idpedido { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idestado { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime fechacambio { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idempleado { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idCliente { get; set; }

        public string notas { get; set; }

        public virtual estado estado { get; set; }

        public virtual PEDIDO PEDIDO { get; set; }
    }
}

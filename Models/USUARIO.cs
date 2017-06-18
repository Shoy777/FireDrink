namespace FireDrink.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USUARIO")]
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            PEDIDO = new HashSet<PEDIDO>();
        }

        [Key]
        public int IDUSUARIO { get; set; }

        [Required]
        [StringLength(100)]
        public string CORREO { get; set; }

        [Required]
        [StringLength(20)]
        public string CLAVE { get; set; }

        [Required]
        [StringLength(50)]
        public string NOMBRE { get; set; }

        [Required]
        [StringLength(50)]
        public string APPATERNO { get; set; }

        [Required]
        [StringLength(50)]
        public string APEMATERNO { get; set; }

        [Required]
        [StringLength(8)]
        public string DNI { get; set; }

        [Column(TypeName = "date")]
        public DateTime FECNACIMIENTO { get; set; }

        [Column(TypeName = "date")]
        public DateTime FECREGISTRO { get; set; }

        [StringLength(9)]
        public string TELEFONO { get; set; }

        [StringLength(11)]
        public string CELULAR { get; set; }

        public int COD_UBIGEO { get; set; }

        public int IDROL { get; set; }

        public int ESTADO { get; set; }

        public virtual estado estado1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO> PEDIDO { get; set; }

        public virtual ROL ROL { get; set; }

        public virtual UBIGEO UBIGEO { get; set; }
    }

    public class LoginModel{
    
        [Required]
        [StringLength(100)]
        public string CORREO { get; set; }

        [Required]
        [StringLength(20)]
        public string CLAVE { get; set; }

}
}

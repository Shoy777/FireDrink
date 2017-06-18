namespace FireDrink.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FireDrinkBDContext : DbContext
    {
        public FireDrinkBDContext()
            : base("name=FireDrinkBDContext")
        {
        }

        public virtual DbSet<CART> CART { get; set; }
        public virtual DbSet<estado> estado { get; set; }
        public virtual DbSet<MARCA> MARCA { get; set; }
        public virtual DbSet<PAGO> PAGO { get; set; }
        public virtual DbSet<PEDIDO> PEDIDO { get; set; }
        public virtual DbSet<pedido_estados> pedido_estados { get; set; }
        public virtual DbSet<pedido_productos> pedido_productos { get; set; }
        public virtual DbSet<PRODUCTO> PRODUCTO { get; set; }
        public virtual DbSet<ROL> ROL { get; set; }
        public virtual DbSet<TIPO> TIPO { get; set; }
        public virtual DbSet<tipo_compPago> tipo_compPago { get; set; }
        public virtual DbSet<TIPO_PAGO> TIPO_PAGO { get; set; }
        public virtual DbSet<UBIGEO> UBIGEO { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<Seguimiento> Seguimiento { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CART>()
                .Property(e => e.CartId)
                .IsUnicode(false);

            modelBuilder.Entity<estado>()
                .Property(e => e.descrip)
                .IsUnicode(false);

            modelBuilder.Entity<estado>()
                .HasMany(e => e.PEDIDO)
                .WithRequired(e => e.estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<estado>()
                .HasMany(e => e.Seguimiento)
                .WithRequired(e => e.estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<estado>()
                .HasMany(e => e.USUARIO)
                .WithRequired(e => e.estado1)
                .HasForeignKey(e => e.ESTADO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MARCA>()
                .Property(e => e.MAR_PRO)
                .IsUnicode(false);

            modelBuilder.Entity<MARCA>()
                .HasMany(e => e.PRODUCTO)
                .WithRequired(e => e.MARCA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.contacto_nom)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.contacto_ape)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.contacto_mail)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.contacto_fijo)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.contacto_movil)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.ruc)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.razonsocial)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.entrega_dir)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.entrega_urba)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.subTotal)
                .HasPrecision(6, 2);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.igv)
                .HasPrecision(5, 2);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.totalpedido)
                .HasPrecision(5, 2);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.notas)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .Property(e => e.entrega_ref)
                .IsUnicode(false);

            modelBuilder.Entity<PEDIDO>()
                .HasMany(e => e.PAGO)
                .WithRequired(e => e.PEDIDO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PEDIDO>()
                .HasMany(e => e.pedido_estados)
                .WithRequired(e => e.PEDIDO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PEDIDO>()
                .HasMany(e => e.pedido_productos)
                .WithRequired(e => e.PEDIDO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PEDIDO>()
                .HasMany(e => e.Seguimiento)
                .WithRequired(e => e.PEDIDO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pedido_productos>()
                .Property(e => e.precio)
                .HasPrecision(12, 2);

            modelBuilder.Entity<pedido_productos>()
                .Property(e => e.total)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PRODUCTO>()
                .Property(e => e.DES_PRO)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTO>()
                .Property(e => e.PRE_PRO)
                .HasPrecision(7, 2);

            modelBuilder.Entity<PRODUCTO>()
                .Property(e => e.NOMFOTO)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTO>()
                .HasMany(e => e.CART)
                .WithRequired(e => e.PRODUCTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCTO>()
                .HasMany(e => e.pedido_productos)
                .WithRequired(e => e.PRODUCTO)
                .HasForeignKey(e => e.idproducto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ROL>()
                .Property(e => e.DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<ROL>()
                .HasMany(e => e.USUARIO)
                .WithRequired(e => e.ROL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TIPO>()
                .Property(e => e.TIP_PRO)
                .IsUnicode(false);

            modelBuilder.Entity<TIPO>()
                .HasMany(e => e.PRODUCTO)
                .WithRequired(e => e.TIPO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipo_compPago>()
                .Property(e => e.descrip)
                .IsUnicode(false);

            modelBuilder.Entity<tipo_compPago>()
                .HasMany(e => e.PEDIDO)
                .WithRequired(e => e.tipo_compPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TIPO_PAGO>()
                .Property(e => e.descrip)
                .IsUnicode(false);

            modelBuilder.Entity<TIPO_PAGO>()
                .HasMany(e => e.PEDIDO)
                .WithRequired(e => e.TIPO_PAGO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UBIGEO>()
                .Property(e => e.REGION)
                .IsUnicode(false);

            modelBuilder.Entity<UBIGEO>()
                .Property(e => e.PROVINCIA)
                .IsUnicode(false);

            modelBuilder.Entity<UBIGEO>()
                .Property(e => e.DISTRITO)
                .IsUnicode(false);

            modelBuilder.Entity<UBIGEO>()
                .HasMany(e => e.PEDIDO)
                .WithRequired(e => e.UBIGEO)
                .HasForeignKey(e => e.iddistrito)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UBIGEO>()
                .HasMany(e => e.USUARIO)
                .WithRequired(e => e.UBIGEO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.CORREO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.CLAVE)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.APPATERNO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.APEMATERNO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.DNI)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.TELEFONO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.CELULAR)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.PEDIDO)
                .WithRequired(e => e.USUARIO)
                .HasForeignKey(e => e.idCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seguimiento>()
                .Property(e => e.notas)
                .IsUnicode(false);
        }
    }
}

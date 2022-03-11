using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApiVentas.Models
{
    public partial class bootcampContext : IdentityDbContext
    {
        public bootcampContext()
        {
        }

        public bootcampContext(DbContextOptions<bootcampContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<DetallesVenta> DetallesVentas { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("defaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.ToTable("articulos");

                entity.HasIndex(e => e.Codigo, "art_codigo_i")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("marca");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            modelBuilder.Entity<DetallesVenta>(entity =>
            {
                entity.ToTable("detalles_ventas");

                entity.HasIndex(e => e.Articulo, "dva_articulo_i");

                entity.HasIndex(e => e.IdVenta, "dva_venta_i");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Articulo).HasColumnName("articulo");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdVenta).HasColumnName("id_venta");

                entity.HasOne(d => d.ArticuloNavigation)
                    .WithMany(p => p.DetallesVenta)
                    .HasForeignKey(d => d.Articulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dva_articulos_fk");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetallesVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dva_venta_fk");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("personas");

                entity.HasIndex(e => e.Dni, "per_dni");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Dni).HasColumnName("dni");

                entity.Property(e => e.Domicilio)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("domicilio");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Nacimiento)
                    .HasColumnType("date")
                    .HasColumnName("nacimiento");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("ventas");

                entity.HasIndex(e => e.Comprador, "vta_comprador_i");

                entity.HasIndex(e => e.Vendedor, "vta_vendedor_i");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comprador).HasColumnName("comprador");

                entity.Property(e => e.Factura)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("factura");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Vendedor).HasColumnName("vendedor");

                entity.HasOne(d => d.CompradorNavigation)
                    .WithMany(p => p.VentaCompradorNavigations)
                    .HasForeignKey(d => d.Comprador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vta_comprador_fk");

                entity.HasOne(d => d.VendedorNavigation)
                    .WithMany(p => p.VentaVendedorNavigations)
                    .HasForeignKey(d => d.Vendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vta_vendedor_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

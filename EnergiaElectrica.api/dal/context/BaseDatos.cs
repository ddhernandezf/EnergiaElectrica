using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EnergiaElectrica.api.dal.modelos;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnergiaElectrica.api.dal.context
{
    public partial class BaseDatos : DbContext
    {
        public BaseDatos()
        {
        }

        public BaseDatos(DbContextOptions<BaseDatos> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Medicion> Medicion { get; set; }
        public virtual DbSet<TipoCliente> TipoCliente { get; set; }
        public virtual DbSet<TipoMedidor> TipoMedidor { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NDTELRU;Database=EnergiaElectrica;User Id=sa;Password=Letmein1.;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(e => e.NumeroContador)
                    .HasName("uqCliente")
                    .IsUnique();

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroContador)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.MedidorNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.Medidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkClienteTipomedidor");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkClienteTipocliente");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.Cliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkFacturaCliente");

                entity.HasOne(d => d.MedicionNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.Medicion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkFacturaUsuario");
            });

            modelBuilder.Entity<Medicion>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.MontoCobrar).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Medicion)
                    .HasForeignKey(d => d.Cliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkMedicionCliente");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.Medicion)
                    .HasForeignKey(d => d.Usuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkMedicionUsuario");
            });

            modelBuilder.Entity<TipoCliente>(entity =>
            {
                entity.HasIndex(e => e.Nombre)
                    .HasName("uqTipoCliente")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TipoMedidor>(entity =>
            {
                entity.HasIndex(e => e.Nombre)
                    .HasName("uqTipoMedidor")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario1)
                    .IsRequired()
                    .HasColumnName("Usuario")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

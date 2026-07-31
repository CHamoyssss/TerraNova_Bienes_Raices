using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Models;

public partial class TerraNovaDbContext : DbContext
{
    public TerraNovaDbContext()
    {
    }

    public TerraNovaDbContext(DbContextOptions<TerraNovaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Propiedad> Propiedades { get; set; }

    public virtual DbSet<Trabajador> Trabajadores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<Visita> Visitas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC07E317ACD1");

            entity.HasIndex(e => e.Ci, "UQ__Clientes__32149A7A785F14DC").IsUnique();

            entity.HasIndex(e => e.Telefono, "UQ__Clientes__4EC504804484003C").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Clientes__A9D10534D9AD21D4").IsUnique();

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Ci)
                .HasMaxLength(20)
                .HasColumnName("CI");
            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(20)
                .HasDefaultValue("Comprador");
        });

        modelBuilder.Entity<Propiedad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Propieda__3214EC07230E0E80");

            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Disponible");
            entity.Property(e => e.FechaPublicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImagenUrl).HasMaxLength(300);
            entity.Property(e => e.Precio).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Tipo).HasMaxLength(50);
            entity.Property(e => e.TipoOperacion)
                .HasMaxLength(20)
                .HasDefaultValue("Venta");
            entity.Property(e => e.Titulo).HasMaxLength(150);
            entity.Property(e => e.Zona).HasMaxLength(100);

            entity.HasOne(d => d.IdTrabajadorNavigation).WithMany(p => p.Propiedades)
                .HasForeignKey(d => d.IdTrabajador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Propiedades_Trabajadores");
        });

        modelBuilder.Entity<Trabajador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trabajad__3214EC0709AD560B");

            entity.HasIndex(e => e.Ci, "UQ__Trabajad__32149A7AD280977B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Trabajad__A9D10534E566E69C").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Cargo).HasMaxLength(50);
            entity.Property(e => e.Ci)
                .HasMaxLength(20)
                .HasColumnName("CI");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FechaContratacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PorcentajeComision).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC0713F400B2");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__6B0F5AE086D65208").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ContraseñaHash).HasMaxLength(300);
            entity.Property(e => e.NombreUsuario).HasMaxLength(100);
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .HasDefaultValue("Agente");

            entity.HasOne(d => d.IdTrabajadorNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTrabajador)
                .HasConstraintName("FK_Usuarios_Trabajadores");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ventas__3214EC071EE7C768");

            entity.Property(e => e.ComisionGenerada).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Completada");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FormaPago)
                .HasMaxLength(20)
                .HasDefaultValue("Contado");
            entity.Property(e => e.Monto).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Observaciones).HasMaxLength(500);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Clientes");

            entity.HasOne(d => d.IdPropiedadNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdPropiedad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Propiedades");

            entity.HasOne(d => d.IdTrabajadorNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdTrabajador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Trabajadores");
        });

        modelBuilder.Entity<Visita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Visitas__3214EC0711EE2058");

            entity.Property(e => e.Comentarios).HasMaxLength(500);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Programada");
            entity.Property(e => e.FechaVisita).HasColumnType("datetime");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Visita)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Visitas_Clientes");

            entity.HasOne(d => d.IdPropiedadNavigation).WithMany(p => p.Visita)
                .HasForeignKey(d => d.IdPropiedad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Visitas_Propiedades");

            entity.HasOne(d => d.IdTrabajadorNavigation).WithMany(p => p.Visita)
                .HasForeignKey(d => d.IdTrabajador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Visitas_Trabajadores");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

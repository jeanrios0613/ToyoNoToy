using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElChenVuelve.Models;

public partial class ToyoNoToyContext : DbContext
{
    public ToyoNoToyContext()
    {
    }

    public ToyoNoToyContext(DbContextOptions<ToyoNoToyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividad> Actividads { get; set; }

    public virtual DbSet<Formulario> Formularios { get; set; }

    public virtual DbSet<Userprofile> Userprofiles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activida__3213E83F51989242");

            entity.ToTable("Actividad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreActividad)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Formulario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Formular__3213E83F4D1883E6");

            entity.ToTable(tb => tb.HasTrigger("trg_atencion"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActividadEconomica).HasMaxLength(200);
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Cedula).HasMaxLength(50);
            entity.Property(e => e.Celular).HasMaxLength(20);
            entity.Property(e => e.DescripcionInversion).HasMaxLength(400);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaAprobacion).HasColumnType("datetime");
            entity.Property(e => e.FechaAtencion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IndicaSolicitud).HasMaxLength(50);
            entity.Property(e => e.Localidad).HasMaxLength(100);
            entity.Property(e => e.MontoInversion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NombreEmpresa).HasMaxLength(100);
            entity.Property(e => e.RedesSociales).HasMaxLength(200);
            entity.Property(e => e.UsuarioAnalista).HasMaxLength(100);
            entity.Property(e => e.UsuarioSupervisor).HasMaxLength(100);
        });

        modelBuilder.Entity<Userprofile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_pro__3213E83FADAEA96E");

            entity.ToTable("userprofile");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Perfil)
                .HasMaxLength(25)
                .HasColumnName("perfil");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__usuarios__B9BE370F2C1E936A");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Correo, "UQ__usuarios__2A586E0B9F3E4B81").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValue("activo")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaUltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_ultima_actualizacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValue("usuario")
                .HasColumnName("rol");
            entity.Property(e => e.UltimoAcceso)
                .HasColumnType("datetime")
                .HasColumnName("ultimo_acceso");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

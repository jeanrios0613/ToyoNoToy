using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace elchenchenvuelvecy.Models;

public partial class ToyoNoToyDbContext : DbContext
{
    public ToyoNoToyDbContext()
    {
    }

    public ToyoNoToyDbContext(DbContextOptions<ToyoNoToyDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Enterprise> Enterprises { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<RequestDetailsCopium> RequestDetailsCopia { get; set; }

    public virtual DbSet<RequestsCopium> RequestsCopia { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FormularioClass>()
      .HasNoKey();


        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasIndex(e => e.RequestId, "IX_Contacts_Include");

            entity.HasIndex(e => e.RequestId, "IX_Contacts_RequestId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IdentificationNumber).HasMaxLength(20);
            entity.Property(e => e.IdentificationType).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(25);

            entity.HasOne(d => d.Request).WithOne(p => p.Contact).HasForeignKey<Contact>(d => d.RequestId);
        });

        modelBuilder.Entity<Enterprise>(entity =>
        {
            entity.HasIndex(e => e.BusinessDescription, "IX_Enterprises_Include");

            entity.HasIndex(e => e.RequestId, "IX_Enterprises_RequestId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BusinessDescription)
                .HasMaxLength(600)
                .IsUnicode(false);
            entity.Property(e => e.BusinessName).HasMaxLength(100);
            entity.Property(e => e.BusinessTime)
                .HasMaxLength(16)
                .HasDefaultValue("");
            entity.Property(e => e.Corregimiento)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.District)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.EconomicActivity).HasMaxLength(100);
            entity.Property(e => e.Instagram).HasMaxLength(50);
            entity.Property(e => e.MonthlySales).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Province)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.ProyectedSales).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Ruc)
                .HasMaxLength(28)
                .IsUnicode(false);
            entity.Property(e => e.WebSite).HasMaxLength(100);

            entity.HasOne(d => d.Request).WithOne(p => p.Enterprise).HasForeignKey<Enterprise>(d => d.RequestId);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Requests_Id");

            entity.HasIndex(e => e.Id, "IX_Requests_Include");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Suggestion)
                .HasMaxLength(50)
                .HasDefaultValue("");
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.HasIndex(e => e.RequestId, "IX_RequestDetails_Include");

            entity.HasIndex(e => e.RequestId, "IX_RequestDetails_RequestId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.QuantityToInvert).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReasonForMoney).HasMaxLength(500);

            entity.HasOne(d => d.Request).WithOne(p => p.RequestDetail).HasForeignKey<RequestDetail>(d => d.RequestId);
        });

        modelBuilder.Entity<RequestDetailsCopium>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RequestDetails_copia");

            entity.Property(e => e.QuantityToInvert).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReasonForMoney).HasMaxLength(500);
        });

        modelBuilder.Entity<RequestsCopium>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Requests_copia");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Suggestion).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

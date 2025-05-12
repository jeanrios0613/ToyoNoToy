using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace managerelchenchenvuelve.Models;

public partial class ToyoNoToyContext : DbContext
{
    public ToyoNoToyContext()
    {
    }

    public ToyoNoToyContext(DbContextOptions<ToyoNoToyContext> options)
        : base(options)
    {
    }

    public DbSet<Comments> Comments { get; set; }
    public DbSet<RequestClass> RequestClasses { get; set; }
    public virtual DbSet<ActionLog> ActionLogs { get; set; }

    public virtual DbSet<ActivityInstance> ActivityInstances { get; set; }

    public virtual DbSet<ActivityInstanceRole> ActivityInstanceRoles { get; set; }

    public virtual DbSet<ActivityStateDefaultTray> ActivityStateDefaultTrays { get; set; }



    public virtual DbSet<ConsultaSoloAmpymeCompleto> ConsultaSoloAmpymeCompletos { get; set; }

    public virtual DbSet<ConsultaSoloAmpymeFechaactualModif> ConsultaSoloAmpymeFechaactualModifs { get; set; }

    public virtual DbSet<ConsultaTodo> ConsultaTodos { get; set; }

    public virtual DbSet<ConsultaTodosChenchen> ConsultaTodosChenchens { get; set; }

    public virtual DbSet<ConsultaTodosPrueba> ConsultaTodosPruebas { get; set; }

    public virtual DbSet<DocumentReference> DocumentReferences { get; set; }

    public virtual DbSet<FormDefinition> FormDefinitions { get; set; }

    public virtual DbSet<ProcessDefinition> ProcessDefinitions { get; set; }

    public virtual DbSet<ProcessDefinitionSequential> ProcessDefinitionSequentials { get; set; }

    public virtual DbSet<ProcessInstance> ProcessInstances { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Secret> Secrets { get; set; }

    public virtual DbSet<SecretLog> SecretLogs { get; set; }

    public virtual DbSet<Tray> Trays { get; set; }

    public virtual DbSet<TrayTheme> TrayThemes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
   //     => optionsBuilder.UseSqlServer("Server=TSIAPP724;Database=PoluxDb;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActionLog>(entity =>
        {
            entity.HasIndex(e => e.ProcessInstanceId, "IX_ActionLogs_ProcessInstanceId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActionLogData)
                .HasMaxLength(1024)
                .HasDefaultValue("");

            entity.HasOne(d => d.ProcessInstance).WithMany(p => p.ActionLogs).HasForeignKey(d => d.ProcessInstanceId);
        });

        modelBuilder.Entity<ActivityInstance>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(32);

            entity.HasOne(d => d.Instance).WithMany(p => p.ActivityInstances).HasForeignKey(d => d.InstanceId);

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.ActivityInstances).HasForeignKey(d => d.State);

            entity.HasOne(d => d.Tray).WithMany(p => p.ActivityInstances).HasForeignKey(d => d.TrayId);
        });

        modelBuilder.Entity<ActivityInstanceRole>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ActivityInstance).WithMany(p => p.ActivityInstanceRoles).HasForeignKey(d => d.ActivityInstanceId);
        });

        modelBuilder.Entity<ActivityStateDefaultTray>(entity =>
        {
            entity.HasKey(e => e.State);

            entity.Property(e => e.State).ValueGeneratedNever();

            entity.HasOne(d => d.Tray).WithMany(p => p.ActivityStateDefaultTrays).HasForeignKey(d => d.TrayId);
        });

       

        modelBuilder.Entity<ConsultaSoloAmpymeCompleto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Consulta_solo_ampyme_completo");

            entity.Property(e => e.ActividadEconómica)
                .HasMaxLength(4000)
                .HasColumnName("Actividad económica");
            entity.Property(e => e.Apellido).HasMaxLength(4000);
            entity.Property(e => e.CodigoDeSolicitud)
                .HasMaxLength(4000)
                .HasColumnName("Codigo de solicitud");
            entity.Property(e => e.CompletaActividad)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Corregimiento)
                .HasMaxLength(4000)
                .HasColumnName("corregimiento");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(4000)
                .HasColumnName("Correo Electronico");
            entity.Property(e => e.CuantoChenchenNecesitas)
                .HasMaxLength(4000)
                .HasColumnName("Cuanto Chenchen necesitas");
            entity.Property(e => e.DescripcionDelNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Descripcion del negocio");
            entity.Property(e => e.Distrito).HasMaxLength(4000);
            entity.Property(e => e.EnQueLoInvertiras)
                .HasMaxLength(4000)
                .HasColumnName("En que lo invertiras");
            entity.Property(e => e.Etapa).HasMaxLength(32);
            entity.Property(e => e.EtapaDelNegocio)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("Etapa del Negocio");
            entity.Property(e => e.FechaDeActualizacion).HasColumnName("Fecha de Actualizacion");
            entity.Property(e => e.FechaDeCreacion)
                .HasMaxLength(4000)
                .HasColumnName("Fecha de Creacion");
            entity.Property(e => e.FechaDeInicioDeOperaciones)
                .HasMaxLength(4000)
                .HasColumnName("Fecha de Inicio de Operaciones");
            entity.Property(e => e.FechaFormateada).HasMaxLength(4000);
            entity.Property(e => e.GestionRealizada)
                .HasMaxLength(4000)
                .HasColumnName("Gestion Realizada");
            entity.Property(e => e.Gestor).HasMaxLength(4000);
            entity.Property(e => e.Instagram).HasMaxLength(4000);
            entity.Property(e => e.Nombre).HasMaxLength(4000);
            entity.Property(e => e.NombreDelNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Nombre del Negocio");
            entity.Property(e => e.NumeroDeIdentificacion)
                .HasMaxLength(4000)
                .HasColumnName("Numero de identificacion");
            entity.Property(e => e.PorQueNoSeContacto)
                .HasMaxLength(4000)
                .HasColumnName("Por que no se contacto");
            entity.Property(e => e.Provincia).HasMaxLength(4000);
            entity.Property(e => e.ProyeccionDeVentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Proyeccion de ventas mensuales");
            entity.Property(e => e.Ruc)
                .HasMaxLength(4000)
                .HasColumnName("RUC");
            entity.Property(e => e.Teléfono).HasMaxLength(4000);
            entity.Property(e => e.TiempoTranscurrido)
                .HasMaxLength(43)
                .IsUnicode(false);
            entity.Property(e => e.TipoDeAtencion)
                .HasMaxLength(4000)
                .HasColumnName("Tipo de atencion");
            entity.Property(e => e.TipoDeIdentificacion)
                .HasMaxLength(4000)
                .HasColumnName("Tipo de identificacion");
            entity.Property(e => e.UsuarioAsignado)
                .HasMaxLength(256)
                .HasColumnName("Usuario Asignado");
            entity.Property(e => e.VentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Ventas mensuales");
            entity.Property(e => e.VerificacionDelCliente)
                .HasMaxLength(4000)
                .HasColumnName("Verificacion del Cliente");
            entity.Property(e => e.WebSite)
                .HasMaxLength(4000)
                .HasColumnName("Web Site");
        });

        modelBuilder.Entity<ConsultaSoloAmpymeFechaactualModif>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Consulta_solo_ampyme_fechaactual_modif");

            entity.Property(e => e.ActividadEconómica)
                .HasMaxLength(4000)
                .HasColumnName("Actividad económica");
            entity.Property(e => e.Apellido).HasMaxLength(4000);
            entity.Property(e => e.Corregimiento)
                .HasMaxLength(4000)
                .HasColumnName("corregimiento");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(4000)
                .HasColumnName("Correo Electronico");
            entity.Property(e => e.CuantoChenchenNecesitas)
                .HasMaxLength(4000)
                .HasColumnName("Cuanto Chenchen necesitas");
            entity.Property(e => e.CódigoDeSolicitud)
                .HasMaxLength(4000)
                .HasColumnName("Código de solicitud");
            entity.Property(e => e.DescripciónDelNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Descripción del negocio");
            entity.Property(e => e.Distrito).HasMaxLength(4000);
            entity.Property(e => e.EnQuéLoInvertirás)
                .HasMaxLength(4000)
                .HasColumnName("En qué lo invertirás");
            entity.Property(e => e.Etapa).HasMaxLength(32);
            entity.Property(e => e.EtapaDelNegocio)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("Etapa del Negocio");
            entity.Property(e => e.FechaDeActualizacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Fecha de Actualizacion");
            entity.Property(e => e.FechaDeCreación)
                .HasMaxLength(4000)
                .HasColumnName("Fecha de Creación");
            entity.Property(e => e.FechaDeInicioDeOperaciones)
                .HasMaxLength(4000)
                .HasColumnName("Fecha de Inicio de Operaciones");
            entity.Property(e => e.GestiónRealizada)
                .HasMaxLength(4000)
                .HasColumnName("Gestión Realizada");
            entity.Property(e => e.Gestor).HasMaxLength(4000);
            entity.Property(e => e.Instagram).HasMaxLength(4000);
            entity.Property(e => e.Nombre).HasMaxLength(4000);
            entity.Property(e => e.NombreDelNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Nombre del Negocio");
            entity.Property(e => e.NúmeroDeIdentificación)
                .HasMaxLength(4000)
                .HasColumnName("Número de identificación");
            entity.Property(e => e.PorQueNoSeContacto)
                .HasMaxLength(4000)
                .HasColumnName("Por que no se contacto");
            entity.Property(e => e.Provincia).HasMaxLength(4000);
            entity.Property(e => e.ProyecciónDeVentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Proyección de ventas mensuales");
            entity.Property(e => e.Ruc)
                .HasMaxLength(4000)
                .HasColumnName("RUC");
            entity.Property(e => e.Teléfono).HasMaxLength(4000);
            entity.Property(e => e.TipoDeAtencion)
                .HasMaxLength(4000)
                .HasColumnName("Tipo de atencion");
            entity.Property(e => e.TipoDeIdentificación)
                .HasMaxLength(4000)
                .HasColumnName("Tipo de identificación");
            entity.Property(e => e.UsuarioAsignado)
                .HasMaxLength(256)
                .HasColumnName("Usuario Asignado");
            entity.Property(e => e.VentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Ventas mensuales");
            entity.Property(e => e.VerificaciónDelCliente)
                .HasMaxLength(4000)
                .HasColumnName("Verificación del Cliente");
            entity.Property(e => e.WebSite)
                .HasMaxLength(4000)
                .HasColumnName("Web Site");
        });

        modelBuilder.Entity<ConsultaTodo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Consulta_todos");

            entity.Property(e => e.ActividadEconomica)
                .HasMaxLength(4000)
                .HasColumnName("Actividad_economica");
            entity.Property(e => e.Apellido).HasMaxLength(4000);
            entity.Property(e => e.CodId).HasColumnName("cod_ID");
            entity.Property(e => e.CodigoDeSolicitud)
                .HasMaxLength(4000)
                .HasColumnName("Codigo_de_solicitud");
            entity.Property(e => e.Corregimiento)
                .HasMaxLength(4000)
                .HasColumnName("corregimiento");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(4000)
                .HasColumnName("Correo_Electronico");
            entity.Property(e => e.CuantoChenchenNecesitas)
                .HasMaxLength(4000)
                .HasColumnName("Cuanto_Chenchen_necesitas");
            entity.Property(e => e.DescripcionNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Descripcion_negocio");
            entity.Property(e => e.Distrito).HasMaxLength(4000);
            entity.Property(e => e.EnQueLoInvertiras)
                .HasMaxLength(4000)
                .HasColumnName("En_que_lo_invertiras");
            entity.Property(e => e.Etapa).HasMaxLength(32);
            entity.Property(e => e.EtapaDelNegocio)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("Etapa_del_Negocio");
            entity.Property(e => e.FechaActualizacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Fecha_Actualizacion");
            entity.Property(e => e.FechaDeCreacion)
                .HasMaxLength(4000)
                .HasColumnName("Fecha_de_Creacion");
            entity.Property(e => e.FechaInicioOperaciones)
                .HasMaxLength(4000)
                .HasColumnName("Fecha_Inicio_Operaciones");
            entity.Property(e => e.GestionRealizada)
                .HasMaxLength(4000)
                .HasColumnName("Gestion_Realizada");
            entity.Property(e => e.Gestor).HasMaxLength(32);
            entity.Property(e => e.IdChen).HasColumnName("id_chen");
            entity.Property(e => e.Instagram).HasMaxLength(4000);
            entity.Property(e => e.Nombre).HasMaxLength(4000);
            entity.Property(e => e.NombreNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Nombre_Negocio");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(4000)
                .HasColumnName("Numero_identificacion");
            entity.Property(e => e.PorqueNoContacto)
                .HasMaxLength(4000)
                .HasColumnName("Porque_no_contacto");
            entity.Property(e => e.Provincia).HasMaxLength(4000);
            entity.Property(e => e.ProyeccionVentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Proyeccion_ventas_mensuales");
            entity.Property(e => e.Ruc)
                .HasMaxLength(4000)
                .HasColumnName("RUC");
            entity.Property(e => e.Telefono).HasMaxLength(4000);
            entity.Property(e => e.TipoAtencion)
                .HasMaxLength(4000)
                .HasColumnName("Tipo_atencion");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(4000)
                .HasColumnName("Tipo_identificacion");
            entity.Property(e => e.UsuarioAsignado)
                .HasMaxLength(256)
                .HasColumnName("Usuario_Asignado");
            entity.Property(e => e.VentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Ventas_mensuales");
            entity.Property(e => e.VerificacionCliente)
                .HasMaxLength(4000)
                .HasColumnName("Verificacion_Cliente");
            entity.Property(e => e.WebSite)
                .HasMaxLength(4000)
                .HasColumnName("Web_Site");
        });

        modelBuilder.Entity<ConsultaTodosChenchen>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Consulta_todos_chenchen");

            entity.Property(e => e.ActividadEconomica)
                .HasMaxLength(4000)
                .HasColumnName("Actividad_economica");
            entity.Property(e => e.Apellido).HasMaxLength(4000);
            entity.Property(e => e.CodId).HasColumnName("cod_ID");
            entity.Property(e => e.CodigoDeSolicitud)
                .HasMaxLength(4000)
                .HasColumnName("Codigo_de_solicitud");
            entity.Property(e => e.Corregimiento)
                .HasMaxLength(4000)
                .HasColumnName("corregimiento");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(4000)
                .HasColumnName("Correo_Electronico");
            entity.Property(e => e.CuantoChenchenNecesitas)
                .HasMaxLength(4000)
                .HasColumnName("Cuanto_Chenchen_necesitas");
            entity.Property(e => e.DescripcionNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Descripcion_negocio");
            entity.Property(e => e.Distrito).HasMaxLength(4000);
            entity.Property(e => e.EnQueLoInvertiras)
                .HasMaxLength(4000)
                .HasColumnName("En_que_lo_invertiras");
            entity.Property(e => e.Etapa).HasMaxLength(32);
            entity.Property(e => e.EtapaDelNegocio)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("Etapa_del_Negocio");
            entity.Property(e => e.FechaActualizacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Fecha_Actualizacion");
            entity.Property(e => e.FechaDeCreacion)
                .HasMaxLength(4000)
                .HasColumnName("Fecha_de_Creacion");
            entity.Property(e => e.FechaInicioOperaciones)
                .HasMaxLength(4000)
                .HasColumnName("Fecha_Inicio_Operaciones");
            entity.Property(e => e.GestionRealizada)
                .HasMaxLength(4000)
                .HasColumnName("Gestion_Realizada");
            entity.Property(e => e.Gestor).HasMaxLength(4000);
            entity.Property(e => e.GestorPoluxdb)
                .HasMaxLength(4000)
                .HasColumnName("Gestor_Poluxdb");
            entity.Property(e => e.IdChen).HasColumnName("id_Chen");
            entity.Property(e => e.Instagram).HasMaxLength(4000);
            entity.Property(e => e.Nombre).HasMaxLength(4000);
            entity.Property(e => e.NombreNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Nombre_Negocio");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(4000)
                .HasColumnName("Numero_identificacion");
            entity.Property(e => e.PorqueNoContacto)
                .HasMaxLength(4000)
                .HasColumnName("Porque_no_contacto");
            entity.Property(e => e.Provincia).HasMaxLength(4000);
            entity.Property(e => e.ProyeccionVentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Proyeccion_ventas_mensuales");
            entity.Property(e => e.Ruc)
                .HasMaxLength(4000)
                .HasColumnName("RUC");
            entity.Property(e => e.Telefono).HasMaxLength(4000);
            entity.Property(e => e.TipoAtencion)
                .HasMaxLength(4000)
                .HasColumnName("Tipo_atencion");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(4000)
                .HasColumnName("Tipo_identificacion");
            entity.Property(e => e.UsuarioAsignado)
                .HasMaxLength(256)
                .HasColumnName("Usuario_Asignado");
            entity.Property(e => e.VentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Ventas_mensuales");
            entity.Property(e => e.VerificacionCliente)
                .HasMaxLength(4000)
                .HasColumnName("Verificacion_Cliente");
            entity.Property(e => e.WebSite)
                .HasMaxLength(4000)
                .HasColumnName("Web_Site");
        });

        modelBuilder.Entity<ConsultaTodosPrueba>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Consulta_todos_prueba");

            entity.Property(e => e.ActividadEconomica)
                .HasMaxLength(4000)
                .HasColumnName("Actividad_economica");
            entity.Property(e => e.Apellido).HasMaxLength(4000);
            entity.Property(e => e.CodId).HasColumnName("cod_ID");
            entity.Property(e => e.CodigoDeSolicitud)
                .HasMaxLength(4000)
                .HasColumnName("Codigo_de_solicitud");
            entity.Property(e => e.Corregimiento)
                .HasMaxLength(4000)
                .HasColumnName("corregimiento");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(4000)
                .HasColumnName("Correo_Electronico");
            entity.Property(e => e.CuantoChenchenNecesitas)
                .HasMaxLength(4000)
                .HasColumnName("Cuanto_Chenchen_necesitas");
            entity.Property(e => e.DescripcionNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Descripcion_negocio");
            entity.Property(e => e.Distrito).HasMaxLength(4000);
            entity.Property(e => e.EnQueLoInvertiras)
                .HasMaxLength(4000)
                .HasColumnName("En_que_lo_invertiras");
            entity.Property(e => e.Etapa).HasMaxLength(32);
            entity.Property(e => e.EtapaDelNegocio)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("Etapa_del_Negocio");
            entity.Property(e => e.FechaActualizacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Fecha_Actualizacion");
            entity.Property(e => e.FechaDeCreacion)
                .HasMaxLength(4000)
                .HasColumnName("Fecha_de_Creacion");
            entity.Property(e => e.FechaInicioOperaciones)
                .HasMaxLength(4000)
                .HasColumnName("Fecha_Inicio_Operaciones");
            entity.Property(e => e.GestionRealizada)
                .HasMaxLength(4000)
                .HasColumnName("Gestion_Realizada");
            entity.Property(e => e.Gestor).HasMaxLength(4000);
            entity.Property(e => e.IdChen).HasColumnName("id_chen");
            entity.Property(e => e.Instagram).HasMaxLength(4000);
            entity.Property(e => e.Nombre).HasMaxLength(4000);
            entity.Property(e => e.NombreNegocio)
                .HasMaxLength(4000)
                .HasColumnName("Nombre_Negocio");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(4000)
                .HasColumnName("Numero_identificacion");
            entity.Property(e => e.PorqueNoContacto)
                .HasMaxLength(4000)
                .HasColumnName("Porque_no_contacto");
            entity.Property(e => e.Provincia).HasMaxLength(4000);
            entity.Property(e => e.ProyeccionVentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Proyeccion_ventas_mensuales");
            entity.Property(e => e.Ruc)
                .HasMaxLength(4000)
                .HasColumnName("RUC");
            entity.Property(e => e.Telefono).HasMaxLength(4000);
            entity.Property(e => e.TipoAtencion)
                .HasMaxLength(4000)
                .HasColumnName("Tipo_atencion");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(4000)
                .HasColumnName("Tipo_identificacion");
            entity.Property(e => e.UsuarioAsignado)
                .HasMaxLength(256)
                .HasColumnName("Usuario_Asignado");
            entity.Property(e => e.VentasMensuales)
                .HasMaxLength(4000)
                .HasColumnName("Ventas_mensuales");
            entity.Property(e => e.VerificacionCliente)
                .HasMaxLength(4000)
                .HasColumnName("Verificacion_Cliente");
            entity.Property(e => e.WebSite)
                .HasMaxLength(4000)
                .HasColumnName("Web_Site");
        });

        modelBuilder.Entity<DocumentReference>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DocumentTitle).HasMaxLength(256);
            entity.Property(e => e.RequiredName).HasMaxLength(256);
            entity.Property(e => e.StageName).HasMaxLength(256);

            entity.HasOne(d => d.ProcessInstance).WithMany(p => p.DocumentReferences).HasForeignKey(d => d.ProcessInstanceId);
        });

        modelBuilder.Entity<FormDefinition>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(512);
            entity.Property(e => e.Title).HasMaxLength(128);
        });

        modelBuilder.Entity<ProcessDefinition>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Icon).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<ProcessDefinitionSequential>(entity =>
        {
            entity.HasKey(e => e.BusinessProcessDefinitionId);

            entity.Property(e => e.BusinessProcessDefinitionId).ValueGeneratedNever();
            entity.Property(e => e.CharacterOnTheLeft).HasMaxLength(1);
            entity.Property(e => e.Prefix).HasMaxLength(50);

            entity.HasOne(d => d.BusinessProcessDefinition).WithOne(p => p.ProcessDefinitionSequential).HasForeignKey<ProcessDefinitionSequential>(d => d.BusinessProcessDefinitionId);
        });

        modelBuilder.Entity<ProcessInstance>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.TransformedSequential)
                .HasMaxLength(100)
                .HasDefaultValue("");

            entity.HasOne(d => d.Definition).WithMany(p => p.ProcessInstances)
                .HasForeignKey(d => d.DefinitionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasOne(d => d.Owner).WithMany(p => p.Roles).HasForeignKey(d => d.OwnerId);
        });

        modelBuilder.Entity<Secret>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(32);

            entity.HasOne(d => d.User).WithMany(p => p.Secrets).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<SecretLog>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Secret).WithMany(p => p.SecretLogs).HasForeignKey(d => d.SecretId);
        });

        modelBuilder.Entity<Tray>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasOne(d => d.Theme).WithMany(p => p.Trays).HasForeignKey(d => d.ThemeId);
        });

        modelBuilder.Entity<TrayTheme>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BackgroundColor).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.TextColor).HasMaxLength(256);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.Lastname).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Password).HasMaxLength(256);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

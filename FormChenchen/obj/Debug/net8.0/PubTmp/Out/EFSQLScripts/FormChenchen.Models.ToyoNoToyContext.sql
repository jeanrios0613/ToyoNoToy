IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [Comments] (
        [Id] uniqueidentifier NOT NULL,
        [ProcessInstanceId] nvarchar(500) NOT NULL,
        [Message] nvarchar(512) NOT NULL,
        [CreatedBy] nvarchar(500) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [StageName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [DocumentReferences] (
        [Id] uniqueidentifier NOT NULL,
        [ProcessInstanceId] nvarchar(500) NOT NULL,
        [DocumentTitle] nvarchar(256) NULL,
        [StageName] nvarchar(256) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedBy] nvarchar(256) NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        CONSTRAINT [PK_DocumentReferences] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [Request_Info] (
        [Codigo_de_solicitud] nvarchar(4000) NULL,
        [Fecha_de_Creacion] datetimeoffset NULL,
        [Fecha_Actualizacion] datetimeoffset NULL,
        [Gestor] nvarchar(32) NULL,
        [Etapa_del_Negocio] nvarchar(17) NULL,
        [Correo_Electronico] nvarchar(4000) NULL,
        [Nombre] nvarchar(4000) NULL,
        [Apellido] nvarchar(4000) NULL,
        [Numero_identificacion] nvarchar(4000) NULL,
        [Tipo_identificacion] nvarchar(4000) NULL,
        [Telefono] nvarchar(4000) NULL,
        [Nombre_Negocio] nvarchar(4000) NULL,
        [Descripcion_negocio] nvarchar(4000) NULL,
        [Actividad_economica] nvarchar(4000) NULL,
        [Instagram] nvarchar(4000) NULL,
        [RUC] nvarchar(4000) NULL,
        [Web_Site] nvarchar(4000) NULL,
        [Provincia] nvarchar(4000) NULL,
        [Distrito] nvarchar(4000) NULL,
        [corregimiento] nvarchar(4000) NULL,
        [Proyeccion_ventas_mensuales] nvarchar(4000) NULL,
        [Ventas_mensuales] nvarchar(4000) NULL,
        [Fecha_Inicio_Operaciones] nvarchar(4000) NULL,
        [Cuanto_Chenchen_necesitas] nvarchar(4000) NULL,
        [En_que_lo_invertiras] nvarchar(4000) NULL,
        [Verificacion_Cliente] nvarchar(4000) NULL,
        [Gestion_Realizada] nvarchar(4000) NULL,
        [Tipo_atencion] nvarchar(4000) NULL,
        [Porque_no_contacto] nvarchar(4000) NULL,
        [Etapa] nvarchar(32) NULL,
        [Usuario_Asignado] nvarchar(256) NULL,
        [cod_ID] uniqueidentifier NULL,
        [id_chen] uniqueidentifier NULL
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [RequestDetails_copia] (
        [Id] uniqueidentifier NOT NULL,
        [QuantityToInvert] decimal(18,2) NOT NULL,
        [ReasonForMoney] nvarchar(500) NOT NULL,
        [RequestId] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [Requests] (
        [Id] uniqueidentifier NOT NULL,
        [Code] nvarchar(50) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [Suggestion] nvarchar(50) NOT NULL DEFAULT N'',
        [Type] int NOT NULL,
        CONSTRAINT [PK_Requests] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [Requests_copia] (
        [Id] uniqueidentifier NOT NULL,
        [Code] nvarchar(50) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [Suggestion] nvarchar(50) NOT NULL
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(30) NOT NULL,
        [RolName] nvarchar(256) NULL,
        [ActiveDirectoryGroup] nvarchar(64) NULL,
        [Description] nvarchar(64) NULL,
        [IsActiveDirectorySync] bit NOT NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [UserRoles] (
        [UserId] nvarchar(200) NOT NULL,
        [RoleId] nvarchar(200) NOT NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] nvarchar(450) NULL,
        [UserName] nvarchar(256) NOT NULL,
        [Email] nvarchar(256) NULL,
        [PasswordHash] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [LockoutEnd] datetimeoffset NULL,
        [Created] datetime2 NULL,
        [UPDATEPASS] datetime2 NULL,
        [Lastname] nvarchar(30) NOT NULL,
        [Names] nvarchar(30) NOT NULL,
        [Status] bit NOT NULL
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [Contacts] (
        [Id] uniqueidentifier NOT NULL,
        [Email] nvarchar(100) NOT NULL,
        [FullName] nvarchar(100) NOT NULL,
        [IdentificationNumber] nvarchar(20) NOT NULL,
        [IdentificationType] nvarchar(50) NOT NULL,
        [Phone] nvarchar(25) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [RequestId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Contacts_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [Enterprises] (
        [Id] uniqueidentifier NOT NULL,
        [BusinessName] nvarchar(100) NULL,
        [BusinessDescription] varchar(600) NULL,
        [EconomicActivity] nvarchar(100) NOT NULL,
        [Instagram] nvarchar(50) NOT NULL,
        [Ruc] varchar(28) NOT NULL,
        [WebSite] nvarchar(100) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [RequestId] uniqueidentifier NOT NULL,
        [BusinessTime] nvarchar(16) NOT NULL DEFAULT N'',
        [Corregimiento] nvarchar(50) NOT NULL DEFAULT N'',
        [District] nvarchar(50) NOT NULL DEFAULT N'',
        [MonthlySales] decimal(18,2) NULL,
        [OperationsStartDate] datetime2 NOT NULL,
        [Province] nvarchar(50) NOT NULL DEFAULT N'',
        [ProyectedSales] decimal(18,2) NULL,
        CONSTRAINT [PK_Enterprises] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Enterprises_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE TABLE [RequestDetails] (
        [Id] uniqueidentifier NOT NULL,
        [QuantityToInvert] decimal(18,2) NOT NULL,
        [ReasonForMoney] nvarchar(500) NOT NULL,
        [RequestId] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [VerifyClient] nvarchar(50) NULL,
        [managementExecuted] nvarchar(500) NULL,
        [TipoAtencion] nvarchar(50) NULL,
        [ContactReason] nvarchar(500) NULL,
        CONSTRAINT [PK_RequestDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RequestDetails_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE INDEX [IX_Contacts_Include] ON [Contacts] ([RequestId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Contacts_RequestId] ON [Contacts] ([RequestId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE INDEX [IX_Enterprises_Include] ON [Enterprises] ([BusinessDescription]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Enterprises_RequestId] ON [Enterprises] ([RequestId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE INDEX [IX_RequestDetails_Include] ON [RequestDetails] ([RequestId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_RequestDetails_RequestId] ON [RequestDetails] ([RequestId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE INDEX [IX_Requests_Id] ON [Requests] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    CREATE INDEX [IX_Requests_Include] ON [Requests] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250526181921_MigracionInicial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250526181921_MigracionInicial', N'9.0.0');
END;

COMMIT;
GO


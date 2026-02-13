IF OBJECT_ID(N'[dbo].[__EFMigrationHistory]') IS NULL
BEGIN
    CREATE TABLE [dbo].[__EFMigrationHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [dbo].[__EFMigrationHistory]
    WHERE [MigrationId] = N'20260213025419_initial1.0'
)
BEGIN
    CREATE TABLE [dbo].[Productos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(100) NOT NULL,
        [Precio] decimal(18,2) NOT NULL,
        [FechaCreacion] datetime2 NOT NULL,
        [Created] datetime2 NOT NULL,
        [Modified] datetime2 NULL,
        [Deleted] datetime2 NULL,
        CONSTRAINT [PK_Productos] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [dbo].[__EFMigrationHistory]
    WHERE [MigrationId] = N'20260213025419_initial1.0'
)
BEGIN
    INSERT INTO [dbo].[__EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260213025419_initial1.0', N'8.0.0');
END;
GO

COMMIT;
GO


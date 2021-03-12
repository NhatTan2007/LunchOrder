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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [IsActive] bit NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [Classes] (
        [ClassId] nvarchar(450) NOT NULL,
        [ClassName] nvarchar(max) NULL,
        CONSTRAINT [PK_Classes] PRIMARY KEY ([ClassId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [Foods] (
        [FoodId] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Price] decimal(18,4) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Foods] PRIMARY KEY ([FoodId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(20) NOT NULL,
        [MiddleName] nvarchar(40) NULL,
        [LastName] nvarchar(80) NOT NULL,
        [ClassId] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUsers_Classes_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [Classes] ([ClassId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [FoodImages] (
        [ImagesId] nvarchar(450) NOT NULL,
        [FileName] nvarchar(max) NOT NULL,
        [FoodId] nvarchar(450) NULL,
        CONSTRAINT [PK_FoodImages] PRIMARY KEY ([ImagesId]),
        CONSTRAINT [FK_FoodImages_Foods_FoodId] FOREIGN KEY ([FoodId]) REFERENCES [Foods] ([FoodId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE TABLE [OrderDetails] (
        [OderDetailId] nvarchar(450) NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [FoodId] nvarchar(450) NOT NULL,
        [DateOrder] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OderDetailId]),
        CONSTRAINT [FK_OrderDetails_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderDetails_Foods_FoodId] FOREIGN KEY ([FoodId]) REFERENCES [Foods] ([FoodId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ClassId', N'ClassName') AND [object_id] = OBJECT_ID(N'[Classes]'))
        SET IDENTITY_INSERT [Classes] ON;
    EXEC(N'INSERT INTO [Classes] ([ClassId], [ClassName])
    VALUES (N''29d075b9-d345-44c9-b2b5-5fc416a6fef1'', N''C1020K1''),
    (N''49da7c2d-251e-43ff-83eb-027ee657098d'', N''C0920G1''),
    (N''49e24f8d-5cf6-4727-a40d-42220be7d6b4'', N''C0221H1'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ClassId', N'ClassName') AND [object_id] = OBJECT_ID(N'[Classes]'))
        SET IDENTITY_INSERT [Classes] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FoodId', N'IsActive', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Foods]'))
        SET IDENTITY_INSERT [Foods] ON;
    EXEC(N'INSERT INTO [Foods] ([FoodId], [IsActive], [Name], [Price])
    VALUES (N''23fbe179-d9bc-4c11-9720-e0501da87da2'', CAST(1 AS bit), N''Cơm gà xối mỡ'', 20000.0),
    (N''0a584914-9cc5-475a-b723-1c1c2a074237'', CAST(1 AS bit), N''Cơm sườn'', 15000.0),
    (N''a12cd2e6-f3a9-4470-b8ef-32f80ada738e'', CAST(1 AS bit), N''Cơm sườn non'', 25000.0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FoodId', N'IsActive', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Foods]'))
        SET IDENTITY_INSERT [Foods] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE INDEX [IX_AspNetUsers_ClassId] ON [AspNetUsers] ([ClassId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE INDEX [IX_FoodImages_FoodId] ON [FoodImages] ([FoodId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE INDEX [IX_OrderDetails_FoodId] ON [OrderDetails] ([FoodId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    CREATE INDEX [IX_OrderDetails_UserId] ON [OrderDetails] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306183912_Init_DB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210306183912_Init_DB', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    EXEC(N'DELETE FROM [Classes]
    WHERE [ClassId] = N''29d075b9-d345-44c9-b2b5-5fc416a6fef1'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    EXEC(N'DELETE FROM [Classes]
    WHERE [ClassId] = N''49da7c2d-251e-43ff-83eb-027ee657098d'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    EXEC(N'DELETE FROM [Classes]
    WHERE [ClassId] = N''49e24f8d-5cf6-4727-a40d-42220be7d6b4'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    EXEC(N'DELETE FROM [Foods]
    WHERE [FoodId] = N''0a584914-9cc5-475a-b723-1c1c2a074237'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    EXEC(N'DELETE FROM [Foods]
    WHERE [FoodId] = N''23fbe179-d9bc-4c11-9720-e0501da87da2'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    EXEC(N'DELETE FROM [Foods]
    WHERE [FoodId] = N''a12cd2e6-f3a9-4470-b8ef-32f80ada738e'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Foods]') AND [c].[name] = N'Price');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Foods] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Foods] ALTER COLUMN [Price] int NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ClassId', N'ClassName') AND [object_id] = OBJECT_ID(N'[Classes]'))
        SET IDENTITY_INSERT [Classes] ON;
    EXEC(N'INSERT INTO [Classes] ([ClassId], [ClassName])
    VALUES (N''e5933481-d982-407b-99d3-57d174b4b955'', N''C1020K1''),
    (N''834fd798-a5e6-400a-8364-71fd46aac4ef'', N''C0920G1''),
    (N''7ed1a59e-39c9-40db-9581-0263479aa37e'', N''C0221H1'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ClassId', N'ClassName') AND [object_id] = OBJECT_ID(N'[Classes]'))
        SET IDENTITY_INSERT [Classes] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FoodId', N'IsActive', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Foods]'))
        SET IDENTITY_INSERT [Foods] ON;
    EXEC(N'INSERT INTO [Foods] ([FoodId], [IsActive], [Name], [Price])
    VALUES (N''88c4ff49-4e20-4a4c-8aa7-60e487814742'', CAST(1 AS bit), N''Cơm gà xối mỡ'', 20000),
    (N''4e9f69be-dbb8-4fdc-a7b9-3eee1180b01d'', CAST(1 AS bit), N''Cơm sườn'', 15000),
    (N''21b81039-21f2-4e0d-abd7-7f072fee0e04'', CAST(1 AS bit), N''Cơm sườn non'', 25000)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FoodId', N'IsActive', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Foods]'))
        SET IDENTITY_INSERT [Foods] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210306194128_ALTER_FOODS_TABLE_PRICE_TYPE', N'5.0.3');
END;
GO

COMMIT;
GO


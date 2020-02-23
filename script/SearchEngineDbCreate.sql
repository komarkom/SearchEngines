IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [SearchRequests] (
    [Id] bigint NOT NULL IDENTITY,
    [SearchText] nvarchar(max) NULL,
    [CreatedOn] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SearchRequests] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [SearchSystems] (
    [Id] bigint NOT NULL IDENTITY,
    [SystemName] nvarchar(150) NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SearchSystems] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [SearchResponses] (
    [Id] bigint NOT NULL IDENTITY,
    [Data] nvarchar(max) NULL,
    [Error] nvarchar(max) NULL,
    [HasError] bit NOT NULL,
    [SearchRequestId] bigint NULL,
    [SearchSystemId] bigint NULL,
    [CreatedOn] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SearchResponses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SearchResponses_SearchRequests_SearchRequestId] FOREIGN KEY ([SearchRequestId]) REFERENCES [SearchRequests] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SearchResponses_SearchSystems_SearchSystemId] FOREIGN KEY ([SearchSystemId]) REFERENCES [SearchSystems] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [SearchResults] (
    [Id] bigint NOT NULL IDENTITY,
    [HeaderLinkText] nvarchar(1000) NULL,
    [Url] nvarchar(2000) NULL,
    [PreviewData] nvarchar(2000) NULL,
    [CreatedOn] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [SearchResponseId] bigint NULL,
    CONSTRAINT [PK_SearchResults] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SearchResults_SearchResponses_SearchResponseId] FOREIGN KEY ([SearchResponseId]) REFERENCES [SearchResponses] ([Id]) ON DELETE NO ACTION
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'SystemName') AND [object_id] = OBJECT_ID(N'[SearchSystems]'))
    SET IDENTITY_INSERT [SearchSystems] ON;
INSERT INTO [SearchSystems] ([Id], [IsDeleted], [SystemName])
VALUES (CAST(1 AS bigint), CAST(0 AS bit), N'yandex');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'SystemName') AND [object_id] = OBJECT_ID(N'[SearchSystems]'))
    SET IDENTITY_INSERT [SearchSystems] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'SystemName') AND [object_id] = OBJECT_ID(N'[SearchSystems]'))
    SET IDENTITY_INSERT [SearchSystems] ON;
INSERT INTO [SearchSystems] ([Id], [IsDeleted], [SystemName])
VALUES (CAST(2 AS bigint), CAST(0 AS bit), N'google');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'SystemName') AND [object_id] = OBJECT_ID(N'[SearchSystems]'))
    SET IDENTITY_INSERT [SearchSystems] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'SystemName') AND [object_id] = OBJECT_ID(N'[SearchSystems]'))
    SET IDENTITY_INSERT [SearchSystems] ON;
INSERT INTO [SearchSystems] ([Id], [IsDeleted], [SystemName])
VALUES (CAST(3 AS bigint), CAST(0 AS bit), N'bing');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'SystemName') AND [object_id] = OBJECT_ID(N'[SearchSystems]'))
    SET IDENTITY_INSERT [SearchSystems] OFF;

GO

CREATE UNIQUE INDEX [IX_SearchResponses_SearchRequestId] ON [SearchResponses] ([SearchRequestId]) WHERE [SearchRequestId] IS NOT NULL;

GO

CREATE INDEX [IX_SearchResponses_SearchSystemId] ON [SearchResponses] ([SearchSystemId]);

GO

CREATE INDEX [IX_SearchResults_SearchResponseId] ON [SearchResults] ([SearchResponseId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200223082028_Initial', N'3.1.2');

GO


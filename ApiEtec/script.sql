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

CREATE TABLE [Equipes] (
    [Id] int NOT NULL IDENTITY,
    [NomeEquipe] nvarchar(max) NULL,
    CONSTRAINT [PK_Equipes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Jogadores] (
    [Rm] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Turma] nvarchar(max) NULL,
    [EquipeId] int NULL,
    CONSTRAINT [PK_Jogadores] PRIMARY KEY ([Rm]),
    CONSTRAINT [FK_Jogadores_Equipes_EquipeId] FOREIGN KEY ([EquipeId]) REFERENCES [Equipes] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NomeEquipe') AND [object_id] = OBJECT_ID(N'[Equipes]'))
    SET IDENTITY_INSERT [Equipes] ON;
INSERT INTO [Equipes] ([Id], [NomeEquipe])
VALUES (1, N'Bastard'),
(2, N'Pxg');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NomeEquipe') AND [object_id] = OBJECT_ID(N'[Equipes]'))
    SET IDENTITY_INSERT [Equipes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Rm', N'EquipeId', N'Nome', N'Turma') AND [object_id] = OBJECT_ID(N'[Jogadores]'))
    SET IDENTITY_INSERT [Jogadores] ON;
INSERT INTO [Jogadores] ([Rm], [EquipeId], [Nome], [Turma])
VALUES (1, NULL, N'Jogador Teste1', N'2º ano'),
(2, NULL, N'Jogador Teste2', N'3º ano');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Rm', N'EquipeId', N'Nome', N'Turma') AND [object_id] = OBJECT_ID(N'[Jogadores]'))
    SET IDENTITY_INSERT [Jogadores] OFF;
GO

CREATE INDEX [IX_Jogadores_EquipeId] ON [Jogadores] ([EquipeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230614012130_EquipeMigracao', N'7.0.4');
GO

COMMIT;
GO


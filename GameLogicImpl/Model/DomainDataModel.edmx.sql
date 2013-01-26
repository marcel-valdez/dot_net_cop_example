
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/22/2011 19:58:07
-- Generated from EDMX file: X:\GameLogicImpl\Model\DomainDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CardGame];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccountCards_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountCards] DROP CONSTRAINT [FK_AccountCards_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountCards_Card]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountCards] DROP CONSTRAINT [FK_AccountCards_Card];
GO
IF OBJECT_ID(N'[dbo].[FK_BattleAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoredBattles] DROP CONSTRAINT [FK_BattleAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_BattleAccount1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoredBattles] DROP CONSTRAINT [FK_BattleAccount1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRankAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRanks] DROP CONSTRAINT [FK_UserRankAccount];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Cards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cards];
GO
IF OBJECT_ID(N'[dbo].[StoredBattles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoredBattles];
GO
IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO
IF OBJECT_ID(N'[dbo].[UserRanks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRanks];
GO
IF OBJECT_ID(N'[dbo].[AccountCards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountCards];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(16)  NOT NULL,
    [Password] nvarchar(32)  NOT NULL,
    [ELO] int  NOT NULL
);
GO

-- Creating table 'Cards'
CREATE TABLE [dbo].[Cards] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [AttackPoints] int  NOT NULL,
    [DefensePoints] int  NOT NULL,
    [Effect_Name] nvarchar(max)  NOT NULL,
    [Effect_Description] nvarchar(max)  NOT NULL,
    [Effect_CardAttackMultiplier] float  NOT NULL,
    [Effect_CardAttackChange] int  NOT NULL,
    [Effect_CardDefenseMultiplier] float  NOT NULL,
    [Effect_CardDefenseChange] int  NOT NULL,
    [Effect_LifePointsChange] float  NOT NULL,
    [Effect_DisableOpponentEffect] bit  NOT NULL,
    [Effect_Affected] int  NOT NULL,
    [Effect_EffectTiming] int  NOT NULL,
    [Effect_ProbabilityOfEffect] float  NOT NULL,
    [ImageUrl] nvarchar(max)  NULL
);
GO

-- Creating table 'StoredBattles'
CREATE TABLE [dbo].[StoredBattles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Result] int  NOT NULL,
    [PlayerA_Id] int  NOT NULL,
    [Account_Id] int  NOT NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserRanks'
CREATE TABLE [dbo].[UserRanks] (
    [Rank] bigint  NOT NULL,
    [Username] nvarchar(16)  NOT NULL
);
GO

-- Creating table 'AccountCards'
CREATE TABLE [dbo].[AccountCards] (
    [AccountCards_Card_Id] int  NOT NULL,
    [Deck_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cards'
ALTER TABLE [dbo].[Cards]
ADD CONSTRAINT [PK_Cards]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoredBattles'
ALTER TABLE [dbo].[StoredBattles]
ADD CONSTRAINT [PK_StoredBattles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Username] in table 'UserRanks'
ALTER TABLE [dbo].[UserRanks]
ADD CONSTRAINT [PK_UserRanks]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [AccountCards_Card_Id], [Deck_Id] in table 'AccountCards'
ALTER TABLE [dbo].[AccountCards]
ADD CONSTRAINT [PK_AccountCards]
    PRIMARY KEY NONCLUSTERED ([AccountCards_Card_Id], [Deck_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AccountCards_Card_Id] in table 'AccountCards'
ALTER TABLE [dbo].[AccountCards]
ADD CONSTRAINT [FK_AccountCards_Account]
    FOREIGN KEY ([AccountCards_Card_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Deck_Id] in table 'AccountCards'
ALTER TABLE [dbo].[AccountCards]
ADD CONSTRAINT [FK_AccountCards_Card]
    FOREIGN KEY ([Deck_Id])
    REFERENCES [dbo].[Cards]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountCards_Card'
CREATE INDEX [IX_FK_AccountCards_Card]
ON [dbo].[AccountCards]
    ([Deck_Id]);
GO

-- Creating foreign key on [PlayerA_Id] in table 'StoredBattles'
ALTER TABLE [dbo].[StoredBattles]
ADD CONSTRAINT [FK_BattleAccount]
    FOREIGN KEY ([PlayerA_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BattleAccount'
CREATE INDEX [IX_FK_BattleAccount]
ON [dbo].[StoredBattles]
    ([PlayerA_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'StoredBattles'
ALTER TABLE [dbo].[StoredBattles]
ADD CONSTRAINT [FK_BattleAccount1]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BattleAccount1'
CREATE INDEX [IX_FK_BattleAccount1]
ON [dbo].[StoredBattles]
    ([Account_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
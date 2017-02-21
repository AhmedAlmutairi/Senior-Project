-- Create tables and populate with seed data
--    This version is customized for initializing the db on Azure
--    Seed data is in populate.sql

IF OBJECT_ID('dbo.AspNetRoles','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetRoles];
GO

IF OBJECT_ID('dbo.AspNetUsers','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetUsers];
GO

IF OBJECT_ID('dbo.AspNetUserClaims','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetUserClaims];
Go

IF OBJECT_ID('dbo.AspNetUserLogins','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetUserLogins];
GO

IF OBJECT_ID('dbo.AspNetUserRoles','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetUserRoles];
GO

IF OBJECT_ID('dbo.CalloborationAccount','U') IS NOT NULL
	DROP TABLE [dbo].[CalloborationAccount];
GO

-- ############# AspNetRoles #############
CREATE TABLE [dbo].[AspNetRoles]
(
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);

-- ############# AspNetUsers #############
CREATE TABLE [dbo].[AspNetUsers]
(
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName] ASC);

-- ############# AspNetUserClaims #############
CREATE TABLE [dbo].[AspNetUserClaims]
(
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId] ASC);

-- ############# AspNetUserLogins #############
CREATE TABLE [dbo].[AspNetUserLogins]
(
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId] ASC);

-- ############# AspNetUserRoles #############
CREATE TABLE [dbo].[AspNetUserRoles]
(
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId] ASC);



-- ########### CalloborationAccount ###########
CREATE TABLE [dbo].[CalloborationAccount]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[Organization] NVARCHAR (50) NOT NULL,
	
	CONSTRAINT [PK_dbo.CalloborationAccount] PRIMARY KEY CLUSTERED ([Id] ASC)

);

-- ########### UserCalloboration ###########
CREATE TABLE [dbo].[UserCalloboration]
(
	[UserId] NVARCHAR (128) NOT NULL,
    [CallobId] INT NOT NULL,
    CONSTRAINT [PK_dbo.UserCalloboration] PRIMARY KEY CLUSTERED ([UserId] ASC, [CallobId] ASC),
    CONSTRAINT [FK_dbo.UserCalloboration_dbo.CalloborationAccount_CallobId] FOREIGN KEY ([CallobId]) REFERENCES [dbo].[CalloborationAccount] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.UserCalloboration_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE


);

-- ########### CalloborationCenters ###########

CREATE TABLE [dbo].[CalloborationCenters]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[Name] NVARCHAR (50) NOT NULL,
	[Tier] NVARCHAR (100) NOT NULL,
	[CallobId]  INT NOT NULL,
	CONSTRAINT [PK_dbo.CalloborationCenters] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.CalloborationCenters_dbo.CalloborationAccount_CallobId] FOREIGN KEY ([CallobId]) REFERENCES [dbo].[CalloborationAccount] ([Id]) ON DELETE CASCADE

);

-- ########### Walls ###########
CREATE TABLE [dbo].[Walls]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[Name] NVARCHAR (50) NOT NULL,
	[UserId] NVARCHAR (128) NOT NULL,
	CONSTRAINT [PK_dbo.Walls] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Walls_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE

);

-- ########### Groups ###########
CREATE TABLE [dbo].[Groups]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[Name] NVARCHAR (50) NOT NULL,
	
	CONSTRAINT [PK_dbo.Groups] PRIMARY KEY CLUSTERED ([Id] ASC),
	
);

-- ########### User_Group ###########
CREATE TABLE [dbo].[User_Group]
(
	[UserId] NVARCHAR (128) NOT NULL,
    [GroupId] INT NOT NULL,
    CONSTRAINT [PK_dbo.User_Group] PRIMARY KEY CLUSTERED ([UserId] ASC, [GroupId] ASC),
    CONSTRAINT [FK_dbo.User_Group_dbo.Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.User_Group_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE


);

-- ########### Wall_Group ###########
CREATE TABLE [dbo].[Wall_Group]
(
	[WallId] INT NOT NULL,
    [GroupId] INT NOT NULL,
    CONSTRAINT [PK_dbo.Wall_Group] PRIMARY KEY CLUSTERED ([WallId] ASC, [GroupId] ASC),
    CONSTRAINT [FK_dbo.Wall_Group_dbo.Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Wall_Group_dbo.Walls_WallId] FOREIGN KEY ([WallId]) REFERENCES [dbo].[Walls] ([Id]) ON DELETE CASCADE


);

-- ########### Posts ###########
CREATE TABLE [dbo].[Posts]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[UserId] NVARCHAR (128) NOT NULL,
	[WallId] INT NOT NULL,
	[CallobId] INT NOT NULL,
	[Title] NVARCHAR (128) NOT NULL,
	[Text] NVARCHAR (1000) NOT NULL,

	
	CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Posts_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Posts_dbo.Walls_WallId] FOREIGN KEY ([WallId]) REFERENCES [dbo].[Walls] ([Id]),
	CONSTRAINT [FK_dbo.Posts_dbo.CalloborationCenters_CallobId] FOREIGN KEY ([CallobId]) REFERENCES [dbo].[CalloborationCenters] ([Id]) ON DELETE CASCADE

);

-- ########### Comments ###########
CREATE TABLE [dbo].[Comments]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[UserId] NVARCHAR (128) NOT NULL,
	[PostId] INT NOT NULL,
	[Comment] NVARCHAR (500) NOT NULL,
	[Vote] NVARCHAR (128) NOT NULL,

	
	CONSTRAINT [PK_dbo.Comments] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Comments_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Comments_dbo.Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([Id]) 
	
);




-- ########### Tags ###########
CREATE TABLE [dbo].[Tags]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[Tag] NVARCHAR (50) NOT NULL,
	
	CONSTRAINT [PK_dbo.Tags] PRIMARY KEY CLUSTERED ([Id] ASC),
	
);

-- ########### Diagrams ###########
CREATE TABLE [dbo].[Diagrams]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[Data] NVARCHAR (500) NOT NULL,
	[UserId] NVARCHAR (128) NOT NULL,
	
	CONSTRAINT [PK_dbo.Diagrams] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Diagrams_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

-- ########### Post_Tag ###########
CREATE TABLE [dbo].[Post_Tag]
(
	[PostId] INT NOT NULL,
    [TagId] INT NOT NULL,
    CONSTRAINT [PK_dbo.Post_Tag] PRIMARY KEY CLUSTERED ([PostId] ASC, [TagId] ASC),
    CONSTRAINT [FK_dbo.Post_Tag_dbo.Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Post_Tag_dbo.Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id]) ON DELETE CASCADE

);

-- ########### Post_Diagram ###########
CREATE TABLE [dbo].[Post_Diagram]
(
	[PostId] INT NOT NULL,
    [DiagramId] INT NOT NULL,
    CONSTRAINT [PK_dbo.Post_Diagram] PRIMARY KEY CLUSTERED ([PostId] ASC, [DiagramId] ASC),
    CONSTRAINT [FK_dbo.Post_Diagram_dbo.Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Post_Diagram_dbo.Diagrams_DiagramId] FOREIGN KEY ([DiagramId]) REFERENCES [dbo].[Diagrams] ([Id]) 

);


-- ########### Answers ###########
CREATE TABLE [dbo].[Answers]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[Answer] NVARCHAR (500) NOT NULL,
	[UserId] NVARCHAR (128) NOT NULL,
	[PostId] INT NOT NULL,
	[Votes] NVARCHAR (128) NOT NULL,
	
	CONSTRAINT [PK_dbo.Answers] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Answers_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Answers_dbo.Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([Id])
    
);

-- ########### Annotations ###########
CREATE TABLE [dbo].[Annotations]
(
	[Id] INT IDENTITY (1,1) NOT NULL,
	[Title] NVARCHAR (128) NOT NULL,
	[Note] NVARCHAR (500) NOT NULL,
	[DiagramId] INT NOT NULL,
	[Votes] NVARCHAR (128) NOT NULL,
	
	CONSTRAINT [PK_dbo.Annotations] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Annotations_dbo.Diagrams_DiagramId] FOREIGN KEY ([DiagramId]) REFERENCES [dbo].[Diagrams] ([Id]) ON DELETE CASCADE
);


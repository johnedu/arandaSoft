-- =========================================
-- Create table template SQL Azure Database 
-- =========================================

IF OBJECT_ID('dbo.AppRolePermission', 'U') IS NOT NULL
  DROP TABLE dbo.AppRolePermission
GO
IF OBJECT_ID('dbo.AppUser', 'U') IS NOT NULL
  DROP TABLE dbo.AppUser
GO
IF OBJECT_ID('dbo.AppPermission', 'U') IS NOT NULL
  DROP TABLE dbo.AppPermission
GO
IF OBJECT_ID('dbo.AppRole', 'U') IS NOT NULL
  DROP TABLE dbo.AppRole
GO

CREATE TABLE dbo.AppRole (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](32) NOT NULL
CONSTRAINT PK_AppRole PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) 
GO

CREATE TABLE dbo.AppPermission (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
CONSTRAINT PK_AppPermission PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) 
GO

CREATE TABLE dbo.AppUser (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[FullName] [nvarchar](128) NOT NULL,
	[EmailAddress] [nvarchar](256) NOT NULL,
	[Address] [nvarchar](512) NULL,
	[PhoneNumber] [nvarchar](32) NULL,
	[Age] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
CONSTRAINT PK_AppUser PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) 
GO

ALTER TABLE dbo.AppUser WITH CHECK ADD  CONSTRAINT [FK_AppUser_AppRole_RoleId] FOREIGN KEY([RoleId])
REFERENCES dbo.AppRole ([Id])
ON DELETE CASCADE
GO

ALTER TABLE dbo.AppUser CHECK CONSTRAINT [FK_AppUser_AppRole_RoleId]
GO

CREATE TABLE dbo.AppRolePermission (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL
CONSTRAINT PK_AppRolePermission PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) 
GO

ALTER TABLE dbo.AppRolePermission WITH CHECK ADD  CONSTRAINT [FK_AppRolePermission_AppRole_RoleId] FOREIGN KEY([RoleId])
REFERENCES dbo.AppRole ([Id])
ON DELETE CASCADE
GO

ALTER TABLE dbo.AppRolePermission CHECK CONSTRAINT [FK_AppRolePermission_AppRole_RoleId]
GO

ALTER TABLE dbo.AppRolePermission WITH CHECK ADD  CONSTRAINT [FK_AppRolePermission_AppPermission_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES dbo.AppPermission ([Id])
ON DELETE CASCADE
GO

ALTER TABLE dbo.AppRolePermission CHECK CONSTRAINT [FK_AppRolePermission_AppPermission_PermissionId]
GO

INSERT INTO dbo.AppRole VALUES ('Visitante')
INSERT INTO dbo.AppRole VALUES ('Asistente')
INSERT INTO dbo.AppRole VALUES ('Editor')
INSERT INTO dbo.AppRole VALUES ('Administrador')
DECLARE @VisitanteRole int = (SELECT Id FROM dbo.AppRole WHERE Name = 'Visitante');
DECLARE @AsistenteRole int = (SELECT Id FROM dbo.AppRole WHERE Name = 'Asistente');
DECLARE @EditorRole int = (SELECT Id FROM dbo.AppRole WHERE Name = 'Editor');
DECLARE @AdministradorRole int = (SELECT Id FROM dbo.AppRole WHERE Name = 'Administrador');
INSERT INTO dbo.AppPermission VALUES ('UserListPermission')
INSERT INTO dbo.AppPermission VALUES ('EditUserPermission')
INSERT INTO dbo.AppPermission VALUES ('DeleteUserPermission')
INSERT INTO dbo.AppPermission VALUES ('CreateUserPermission')
DECLARE @UserListPermission int = (SELECT Id FROM dbo.AppPermission WHERE Name = 'UserListPermission');
DECLARE @EditUserPermission int = (SELECT Id FROM dbo.AppPermission WHERE Name = 'EditUserPermission');
DECLARE @DeleteUserPermission int = (SELECT Id FROM dbo.AppPermission WHERE Name = 'DeleteUserPermission');
DECLARE @CreateUserPermission int = (SELECT Id FROM dbo.AppPermission WHERE Name = 'CreateUserPermission');
INSERT INTO dbo.AppUser VALUES ('admon', '123', 'Administrador', 'admon@arandasoft.com', 'Dg. 97 # 17-60', '7563000', 20, @AdministradorRole)
INSERT INTO dbo.AppRolePermission VALUES (@AsistenteRole, @UserListPermission)
INSERT INTO dbo.AppRolePermission VALUES (@EditorRole, @UserListPermission)
INSERT INTO dbo.AppRolePermission VALUES (@EditorRole, @EditUserPermission)
INSERT INTO dbo.AppRolePermission VALUES (@EditorRole, @DeleteUserPermission)
INSERT INTO dbo.AppRolePermission VALUES (@AdministradorRole, @UserListPermission)
INSERT INTO dbo.AppRolePermission VALUES (@AdministradorRole, @EditUserPermission)
INSERT INTO dbo.AppRolePermission VALUES (@AdministradorRole, @DeleteUserPermission)
INSERT INTO dbo.AppRolePermission VALUES (@AdministradorRole, @CreateUserPermission)

SELECT * FROM dbo.AppUser
USE [master]
GO
/****** Object:  Database [GooglePlusStats.Models.GooglePlusDB]    Script Date: 08/10/2011 16:46:01 ******/
CREATE DATABASE [GooglePlusStats.Models.GooglePlusDB] ON  PRIMARY 
( NAME = N'GooglePlusStats.Models.GooglePlusDB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\GooglePlusStats.Models.GooglePlusDB.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GooglePlusStats.Models.GooglePlusDB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\GooglePlusStats.Models.GooglePlusDB_log.LDF' , SIZE = 504KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GooglePlusStats.Models.GooglePlusDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET ARITHABORT OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET AUTO_CLOSE ON
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET  ENABLE_BROKER
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET  READ_WRITE
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET RECOVERY SIMPLE
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET  MULTI_USER
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [GooglePlusStats.Models.GooglePlusDB] SET DB_CHAINING OFF
GO
USE [GooglePlusStats.Models.GooglePlusDB]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 08/10/2011 16:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[ID] [nvarchar](128) NOT NULL,
	[Subject] [nvarchar](max) NULL,
	[Author] [nvarchar](max) NULL,
	[AuthorId] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[DataCreated] [datetime] NOT NULL,
	[DataUpdated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[People]    Script Date: 08/10/2011 16:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[People](
	[ID] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[PictureUrl] [nvarchar](max) NULL,
	[ProfileUrl] [nvarchar](max) NULL,
	[Introduction] [nvarchar](max) NULL,
	[SubHead] [nvarchar](max) NULL,
	[DataCreated] [datetime] NOT NULL,
	[DataUpdated] [datetime] NOT NULL,
	[FollowersCount] [int] NOT NULL CONSTRAINT [DF_People_FollowersCount]  DEFAULT ((0)),
	[FollowedByCount] [int] NOT NULL CONSTRAINT [DF_People_FollowedByCount]  DEFAULT ((0)),
 CONSTRAINT [PK__People__7E6CC920] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EdmMetadata]    Script Date: 08/10/2011 16:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdmMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModelHash] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Connections]    Script Date: 08/10/2011 16:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Connections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromID] [nvarchar](max) NULL,
	[ToID] [nvarchar](max) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

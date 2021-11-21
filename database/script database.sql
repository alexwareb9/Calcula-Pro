USE [master]
GO
/****** Object:  Database [ops_inhouse]    Script Date: 4/12/2018 3:58:14 AM ******/
CREATE DATABASE [ops_inhouse]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ops_inhouse].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ops_inhouse] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ops_inhouse] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ops_inhouse] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ops_inhouse] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ops_inhouse] SET ARITHABORT OFF 
GO
ALTER DATABASE [ops_inhouse] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ops_inhouse] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ops_inhouse] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ops_inhouse] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ops_inhouse] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ops_inhouse] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ops_inhouse] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ops_inhouse] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ops_inhouse] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ops_inhouse] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ops_inhouse] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ops_inhouse] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ops_inhouse] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ops_inhouse] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ops_inhouse] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [ops_inhouse] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ops_inhouse] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ops_inhouse] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ops_inhouse] SET RECOVERY FULL 
GO
ALTER DATABASE [ops_inhouse] SET  MULTI_USER 
GO
ALTER DATABASE [ops_inhouse] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ops_inhouse] SET DB_CHAINING OFF 
GO
USE [ops_inhouse]
GO
/****** Object:  Table [dbo].[products]    Script Date: 4/12/2018 3:58:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product] [varbinary](max) NOT NULL,
	[name] [varchar](100) NULL,
	[created] [datetime2](7) NULL,
	[modified] [datetime2](7) NULL,
	[created_by] [varchar](100) NULL,
	[modified_by] [varchar](100) NULL,
	[size] [int] NULL,
	[store_id] [uniqueidentifier] NULL,
	[active] [bit] NULL,
	[product_xml] [xml] NULL
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[quotes]    Script Date: 4/12/2018 3:58:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[quotes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime2](7) NULL,
	[total] [smallmoney] NOT NULL,
	[user] [varchar](100) NOT NULL,
	[product_name] [varchar](100) NOT NULL,
	[revisions] [smallint] NOT NULL,
	[quote] [varbinary](max) NOT NULL,
	[item] [varchar](100) NULL,
	[revision] [int] NULL,
	[store_id] [uniqueidentifier] NULL,
	[product_id] [int] NULL,
	[quote_xml] [xml] NULL
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[roles]    Script Date: 4/12/2018 3:58:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[roles](
	[id] [smallint] NOT NULL,
	[name] [varchar](50) NOT NULL
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stores]    Script Date: 4/12/2018 3:58:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stores](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NOT NULL
)

GO
/****** Object:  Table [dbo].[user_accounts]    Script Date: 4/12/2018 3:58:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_accounts](
	[id] [uniqueidentifier] NOT NULL,
	[user] [varchar](100) NOT NULL,
	[pwd] [varchar](25) NOT NULL,
	[first_name] [varchar](60) NOT NULL,
	[last_name] [varchar](60) NOT NULL,
	[phone] [varchar](25) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[company] [varchar](100) NULL,
	[photo] [image] NULL,
	[last_login_date] [datetime] NULL,
	[created_date] [datetime] NOT NULL,
	[store_id] [uniqueidentifier] NULL,
	[role] [smallint] NULL
)

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [ops_inhouse] SET  READ_WRITE 
GO

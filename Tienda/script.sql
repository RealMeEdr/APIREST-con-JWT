USE [master]
GO
/****** Object:  Database [SistemaVentas]    Script Date: 31/10/2022 00:55:38 ******/
CREATE DATABASE [SistemaVentas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SistemaVentas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SistemaVentas.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SistemaVentas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SistemaVentas_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SistemaVentas] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SistemaVentas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SistemaVentas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SistemaVentas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SistemaVentas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SistemaVentas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SistemaVentas] SET ARITHABORT OFF 
GO
ALTER DATABASE [SistemaVentas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SistemaVentas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SistemaVentas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SistemaVentas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SistemaVentas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SistemaVentas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SistemaVentas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SistemaVentas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SistemaVentas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SistemaVentas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SistemaVentas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SistemaVentas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SistemaVentas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SistemaVentas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SistemaVentas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SistemaVentas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SistemaVentas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SistemaVentas] SET RECOVERY FULL 
GO
ALTER DATABASE [SistemaVentas] SET  MULTI_USER 
GO
ALTER DATABASE [SistemaVentas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SistemaVentas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SistemaVentas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SistemaVentas] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SistemaVentas] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SistemaVentas] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SistemaVentas', N'ON'
GO
ALTER DATABASE [SistemaVentas] SET QUERY_STORE = OFF
GO
USE [SistemaVentas]
GO
/****** Object:  Table [dbo].[PRODUCTO]    Script Date: 31/10/2022 00:55:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTO](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[CodigoBarra] [bigint] NULL,
	[Nombre] [varchar](max) NULL,
	[Marca] [varchar](max) NULL,
	[Categoria] [varchar](max) NULL,
	[Precio] [decimal](10, 2) NULL,
 CONSTRAINT [PK_PRODUCTO] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_Editar_Producto]    Script Date: 31/10/2022 00:55:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Editar_Producto](
@IdProducto int,
@codigobarra bigint,
@nombre varchar(MAX),
@marca varchar(MAX),
@categoria varchar(MAX),
@precio decimal(10,2)
)
as
begin
	update PRODUCTO set
	CodigoBarra = ISNULL(@codigobarra, CodigoBarra),
	Nombre = ISNULL(@nombre, Nombre),
	Marca = ISNULL(@marca, Marca),
	Categoria = ISNULL(@categoria, Categoria),
	Precio = ISNULL(@precio, Precio)
Where IdProducto = @IdProducto
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Eliminar_Producto]    Script Date: 31/10/2022 00:55:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Eliminar_Producto](
@IdProducto int
)
as
begin
Delete from PRODUCTO where IdProducto = @IdProducto
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Guardar_Producto]    Script Date: 31/10/2022 00:55:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Guardar_Producto](
@codigobarra bigint,
@nombre varchar(MAX),
@marca varchar(MAX),
@categoria varchar(MAX),
@precio decimal(10,2)
)
as
begin 
insert into PRODUCTO(CodigoBarra, Nombre, Marca, Categoria, Precio)
values(@codigobarra, @nombre, @marca, @categoria, @precio)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Lista_Producto]    Script Date: 31/10/2022 00:55:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Lista_Producto]
as
begin
	select * 
	from PRODUCTO
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Producto]    Script Date: 31/10/2022 00:55:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Producto](
@IdProducto int
)
as
begin 
select * from PRODUCTO where IdProducto = @IdProducto
end
GO
USE [master]
GO
ALTER DATABASE [SistemaVentas] SET  READ_WRITE 
GO

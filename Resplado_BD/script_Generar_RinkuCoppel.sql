USE [master]
GO
/****** Object:  Database [RinkuCoppel]    Script Date: 04/11/2022 04:41:36 p. m. ******/
CREATE DATABASE [RinkuCoppel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RinkuCoppel', FILENAME = N'C:\Users\ssarlis\RinkuCoppel.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RinkuCoppel_log', FILENAME = N'C:\Users\ssarlis\RinkuCoppel_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [RinkuCoppel] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RinkuCoppel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RinkuCoppel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RinkuCoppel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RinkuCoppel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RinkuCoppel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RinkuCoppel] SET ARITHABORT OFF 
GO
ALTER DATABASE [RinkuCoppel] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RinkuCoppel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RinkuCoppel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RinkuCoppel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RinkuCoppel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RinkuCoppel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RinkuCoppel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RinkuCoppel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RinkuCoppel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RinkuCoppel] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RinkuCoppel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RinkuCoppel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RinkuCoppel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RinkuCoppel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RinkuCoppel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RinkuCoppel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RinkuCoppel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RinkuCoppel] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RinkuCoppel] SET  MULTI_USER 
GO
ALTER DATABASE [RinkuCoppel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RinkuCoppel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RinkuCoppel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RinkuCoppel] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RinkuCoppel] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RinkuCoppel] SET QUERY_STORE = OFF
GO
USE [RinkuCoppel]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [RinkuCoppel]
GO
/****** Object:  Table [dbo].[CatRol]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatRol](
	[ide] [int] IDENTITY(1,1) NOT NULL,
	[DescRol] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[ide] [int] IDENTITY(1,1) NOT NULL,
	[NumEmpleado] [varchar](50) NOT NULL,
	[NomEmpleado] [varchar](100) NOT NULL,
	[fkCatRol] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwEmpleados]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwEmpleados]
AS
SELECT        TOP (100) PERCENT dbo.Empleados.ide, dbo.Empleados.NumEmpleado AS [Numero Empleado], dbo.Empleados.NomEmpleado AS [Nombre Empleado], dbo.CatRol.DescRol AS Rol
FROM            dbo.CatRol INNER JOIN
                         dbo.Empleados ON dbo.CatRol.ide = dbo.Empleados.fkCatRol
GO
/****** Object:  Table [dbo].[EntregasEmpleados]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntregasEmpleados](
	[ide] [int] IDENTITY(1,1) NOT NULL,
	[fkEmpleados] [int] NOT NULL,
	[fkMes] [int] NOT NULL,
	[CantidadEntregas] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatMes]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatMes](
	[ide] [int] IDENTITY(1,1) NOT NULL,
	[Mes] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwEntregasEmpleados]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwEntregasEmpleados]
AS
SELECT        dbo.Empleados.ide, dbo.EntregasEmpleados.ide AS ideEntregas, dbo.Empleados.NumEmpleado, dbo.Empleados.NomEmpleado, dbo.CatRol.DescRol, dbo.CatMes.Mes, 
                         dbo.EntregasEmpleados.CantidadEntregas
FROM            dbo.EntregasEmpleados INNER JOIN
                         dbo.CatMes ON dbo.EntregasEmpleados.fkMes = dbo.CatMes.ide INNER JOIN
                         dbo.CatRol INNER JOIN
                         dbo.Empleados ON dbo.CatRol.ide = dbo.Empleados.fkCatRol ON dbo.EntregasEmpleados.fkEmpleados = dbo.Empleados.ide
GO
/****** Object:  Table [dbo].[Configuración_Pago]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuración_Pago](
	[ide] [int] IDENTITY(1,1) NOT NULL,
	[SueldoBase] [float] NOT NULL,
	[HrsJornada] [float] NOT NULL,
	[DiasSemana] [int] NOT NULL,
	[PagoAdicionalEntregaCliente] [float] NOT NULL,
	[BonoChofer] [float] NOT NULL,
	[BonoCargador] [float] NOT NULL,
	[BonoAuxiliar] [float] NOT NULL,
	[ISR_Porcentaje] [float] NOT NULL,
	[ISR_Adicional_Porcentaje] [float] NOT NULL,
	[Vales_Adicional_Porcentaje] [float] NOT NULL,
	[SemanasMes] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwPagos]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwPagos]
AS
SELECT        dbo.Empleados.ide, dbo.Empleados.NumEmpleado, dbo.Empleados.NomEmpleado, dbo.CatRol.DescRol, dbo.CatMes.Mes, dbo.EntregasEmpleados.CantidadEntregas, 
                         dbo.Configuración_Pago.HrsJornada * dbo.Configuración_Pago.DiasSemana * dbo.Configuración_Pago.SemanasMes AS Total_Horas, 
                         dbo.Configuración_Pago.SueldoBase * dbo.Configuración_Pago.HrsJornada * dbo.Configuración_Pago.DiasSemana AS Salario_Semanal, 
                         dbo.Configuración_Pago.PagoAdicionalEntregaCliente * dbo.EntregasEmpleados.CantidadEntregas AS Pago_Total_Entregas, 
                         CASE WHEN dbo.CatRol.DescRol = 'Chofer' THEN dbo.Configuración_Pago.BonoChofer * dbo.Configuración_Pago.HrsJornada * dbo.Configuración_Pago.DiasSemana ELSE CASE WHEN dbo.CatRol.DescRol = 'Cargador'
                          THEN dbo.Configuración_Pago.BonoCargador * dbo.Configuración_Pago.HrsJornada * dbo.Configuración_Pago.DiasSemana ELSE CASE WHEN dbo.CatRol.DescRol = 'Auxiliar' THEN dbo.Configuración_Pago.BonoAuxiliar
                          * dbo.Configuración_Pago.HrsJornada * dbo.Configuración_Pago.DiasSemana END END END AS Pago_Total_Bonos
FROM            dbo.Empleados INNER JOIN
                         dbo.CatRol ON dbo.Empleados.fkCatRol = dbo.CatRol.ide INNER JOIN
                         dbo.EntregasEmpleados ON dbo.Empleados.ide = dbo.EntregasEmpleados.fkEmpleados INNER JOIN
                         dbo.CatMes ON dbo.EntregasEmpleados.fkMes = dbo.CatMes.ide CROSS JOIN
                         dbo.Configuración_Pago
GO
/****** Object:  View [dbo].[vwRetenciones]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwRetenciones]
AS
SELECT        dbo.vwPagos.ide, dbo.vwPagos.NumEmpleado, dbo.vwPagos.NomEmpleado, dbo.vwPagos.DescRol, dbo.vwPagos.Mes, dbo.vwPagos.CantidadEntregas, dbo.vwPagos.Total_Horas, 
                         dbo.vwPagos.Salario_Semanal, dbo.vwPagos.Pago_Total_Entregas, dbo.vwPagos.Pago_Total_Bonos, (dbo.vwPagos.Salario_Semanal + dbo.vwPagos.Pago_Total_Entregas + dbo.vwPagos.Pago_Total_Bonos) 
                         * dbo.Configuración_Pago.SemanasMes AS Salario_Mensual_Bruto, (dbo.vwPagos.Salario_Semanal + dbo.vwPagos.Pago_Total_Entregas + dbo.vwPagos.Pago_Total_Bonos) 
                         * dbo.Configuración_Pago.SemanasMes * dbo.Configuración_Pago.ISR_Porcentaje AS ISR, CASE WHEN (dbo.vwPagos.Salario_Semanal + dbo.vwPagos.Pago_Total_Entregas + dbo.vwPagos.Pago_Total_Bonos) 
                         * dbo.Configuración_Pago.SemanasMes > 10000 THEN (dbo.vwPagos.Salario_Semanal + dbo.vwPagos.Pago_Total_Entregas + dbo.vwPagos.Pago_Total_Bonos) 
                         * dbo.Configuración_Pago.SemanasMes * dbo.Configuración_Pago.ISR_Adicional_Porcentaje ELSE 0 END AS ISR_Adicional
FROM            dbo.vwPagos CROSS JOIN
                         dbo.Configuración_Pago
GO
/****** Object:  View [dbo].[vwSdoMensual_Vales]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwSdoMensual_Vales]
AS
SELECT        dbo.vwRetenciones.ide, dbo.vwRetenciones.NumEmpleado, dbo.vwRetenciones.NomEmpleado, dbo.vwRetenciones.DescRol, dbo.vwRetenciones.Mes, dbo.vwRetenciones.Salario_Mensual_Bruto, 
                         dbo.vwRetenciones.ISR, dbo.vwRetenciones.ISR_Adicional, dbo.vwRetenciones.Salario_Mensual_Bruto - dbo.vwRetenciones.ISR - dbo.vwRetenciones.ISR_Adicional AS Salario_Mensual, 
                         (dbo.vwRetenciones.Salario_Mensual_Bruto - dbo.vwRetenciones.ISR - dbo.vwRetenciones.ISR_Adicional) * dbo.Configuración_Pago.Vales_Adicional_Porcentaje AS Total_Pago_Vales
FROM            dbo.Configuración_Pago CROSS JOIN
                         dbo.vwRetenciones
GO
/****** Object:  View [dbo].[vwReporte]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwReporte]
AS
SELECT        dbo.vwPagos.ide, dbo.vwPagos.NumEmpleado, dbo.vwPagos.NomEmpleado, dbo.vwPagos.DescRol, dbo.vwPagos.Mes, dbo.vwPagos.Total_Horas, dbo.vwPagos.Pago_Total_Entregas, 
                         dbo.vwPagos.Pago_Total_Bonos, dbo.vwRetenciones.ISR, dbo.vwRetenciones.ISR_Adicional, dbo.vwSdoMensual_Vales.Total_Pago_Vales, 
                         dbo.vwSdoMensual_Vales.Salario_Mensual + dbo.vwSdoMensual_Vales.Total_Pago_Vales AS Sueldo_Total
FROM            dbo.vwPagos INNER JOIN
                         dbo.vwRetenciones ON dbo.vwPagos.ide = dbo.vwRetenciones.ide AND dbo.vwPagos.Mes = dbo.vwRetenciones.Mes INNER JOIN
                         dbo.vwSdoMensual_Vales ON dbo.vwPagos.ide = dbo.vwSdoMensual_Vales.ide AND dbo.vwPagos.Mes = dbo.vwSdoMensual_Vales.Mes
GO
/****** Object:  Table [dbo].[PagosEmpleados]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PagosEmpleados](
	[ide] [int] IDENTITY(1,1) NOT NULL,
	[fkEmpleados] [int] NOT NULL,
	[HrsxMes] [int] NOT NULL,
	[PagoTotalxEntregasMes] [float] NOT NULL,
	[PagoTotalxBonosMes] [float] NOT NULL,
	[RetencionesxMes] [float] NOT NULL,
	[ValesxMes] [float] NOT NULL,
	[SueldoTotal] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spGraEmpleado]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGraEmpleado]

	@NumEmpleado varchar(50),
	@NomEmpleado varchar(100),
	@fkCatRol int

AS

BEGIN 
	
			INSERT INTO Empleados (NumEmpleado,NomEmpleado,fkCatRol)
			VALUES (@NumEmpleado,@NomEmpleado,@fkCatRol)
			
END
GO
/****** Object:  StoredProcedure [dbo].[spGraEntrega]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGraEntrega]

	@fkEmpleados int,
	@fkMes int,
	@CantidadEntregas int

AS

BEGIN 
	
			INSERT INTO EntregasEmpleados(fkEmpleados,fkMes,CantidadEntregas)
			VALUES (@fkEmpleados,@fkMes,@CantidadEntregas)
			
END
GO
/****** Object:  StoredProcedure [dbo].[spModEmpleado]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spModEmpleado]
	@NumEmpleado varchar(50),
	@NomEmpleado varchar(100),
	@fkCatRol int,
	@ide int
AS
--DECLARE
--@IdeIns int --Nueva identidad al dar de alta
BEGIN 
	
	--IF @tpMov = 1 
	--	BEGIN
			
			--INSERT INTO Empleados (NumEmpleado,NomEmpleado,fkCatRol)
			--VALUES (@NumEmpleado,@NomEmpleado,@fkCatRol)
			
			--COMMIT TRANSACTION;
		--END
	
		--IF @tpMov = 2
		--BEGIN
			UPDATE Empleados set 	
						NumEmpleado = @NumEmpleado,
						NomEmpleado = @NomEmpleado,
						fkCatRol = @fkCatRol
				    
			WHERE ide=@ide
		--	COMMIT TRANSACTION;

		--END

	
	--IF @@ERROR = 0 
	--BEGIN
	--	COMMIT TRANSACTION
	--END

	--ELSE
	--BEGIN
	--	ROLLBACK TRANSACTION
	--END
END
GO
/****** Object:  StoredProcedure [dbo].[spModEntrega]    Script Date: 04/11/2022 04:41:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spModEntrega]

	@fkEmpleados int,
	@fkMes int,
	@CantidadEntregas int,
	@ide int
AS

BEGIN 
			
			UPDATE EntregasEmpleados set 	
						fkMes = @fkMes,
						CantidadEntregas = @CantidadEntregas
			WHERE ide=@ide
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[10] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CatRol"
            Begin Extent = 
               Top = 31
               Left = 453
               Bottom = 166
               Right = 623
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Empleados"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2430
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwEmpleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwEmpleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CatMes"
            Begin Extent = 
               Top = 37
               Left = 879
               Bottom = 133
               Right = 1049
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Empleados"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 168
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CatRol"
            Begin Extent = 
               Top = 114
               Left = 461
               Bottom = 210
               Right = 631
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "EntregasEmpleados"
            Begin Extent = 
               Top = 6
               Left = 659
               Bottom = 154
               Right = 841
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1590
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwEntregasEmpleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'     End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwEntregasEmpleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwEntregasEmpleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CatRol"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 102
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Empleados"
            Begin Extent = 
               Top = 6
               Left = 735
               Bottom = 162
               Right = 905
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Configuración_Pago"
            Begin Extent = 
               Top = 36
               Left = 958
               Bottom = 313
               Right = 1201
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "EntregasEmpleados"
            Begin Extent = 
               Top = 158
               Left = 406
               Bottom = 288
               Right = 588
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CatMes"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1860
         Width = 1500
         Width = 2760
         Width = 2910
         Width = 2910
      End
   End
   Begin CriteriaPane' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPagos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPagos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPagos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "vwPagos"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 256
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vwRetenciones"
            Begin Extent = 
               Top = 36
               Left = 309
               Bottom = 331
               Right = 517
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vwSdoMensual_Vales"
            Begin Extent = 
               Top = 73
               Left = 624
               Bottom = 334
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2295
         Width = 2430
         Width = 1500
         Width = 1500
         Width = 1695
         Width = 1785
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwReporte'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwReporte'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "vwPagos"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 262
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Configuración_Pago"
            Begin Extent = 
               Top = 6
               Left = 274
               Bottom = 279
               Right = 517
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 14
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1980
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwRetenciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwRetenciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Configuración_Pago"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 291
               Right = 281
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vwRetenciones"
            Begin Extent = 
               Top = 6
               Left = 319
               Bottom = 318
               Right = 527
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2490
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1695
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwSdoMensual_Vales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwSdoMensual_Vales'
GO
USE [master]
GO
ALTER DATABASE [RinkuCoppel] SET  READ_WRITE 
GO

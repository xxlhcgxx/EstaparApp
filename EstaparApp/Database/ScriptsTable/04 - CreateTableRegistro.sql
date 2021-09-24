USE [EstaparDataBase]
GO

/****** Object:  Table [dbo].[Registro]    Script Date: 24/09/2021 17:23:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Registro](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Entrada] [datetime2](7) NOT NULL,
	[HoraEntrada] [time](7) NOT NULL,
	[Saida] [datetime2](7) NULL,
	[HoraSaida] [time](7) NULL,
	[MarcaId] [bigint] NOT NULL,
	[ModeloId] [bigint] NOT NULL,
	[Modelo] [nvarchar](max) NULL,
	[ManobristaId] [bigint] NOT NULL,
	[Manobrista] [nvarchar](max) NULL,
	[Placa] [nvarchar](8) NOT NULL,
	[Manobrado] [bit] NOT NULL,
 CONSTRAINT [PK_Registro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



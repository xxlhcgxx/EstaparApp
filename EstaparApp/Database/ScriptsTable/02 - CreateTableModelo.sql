USE [EstaparDataBase]
GO

/****** Object:  Table [dbo].[Modelo]    Script Date: 24/09/2021 17:23:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Modelo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NOT NULL,
	[MarcaId] [bigint] NOT NULL,
 CONSTRAINT [PK_Modelo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Modelo]  WITH CHECK ADD  CONSTRAINT [FK_Modelo_Marca_MarcaId] FOREIGN KEY([MarcaId])
REFERENCES [dbo].[Marca] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Modelo] CHECK CONSTRAINT [FK_Modelo_Marca_MarcaId]
GO



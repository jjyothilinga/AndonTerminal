USE [IntelligentAndonSystem]
GO

/****** Object:  Table [dbo].[communication]    Script Date: 09/21/2011 09:57:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[communication](
	[slNo] [int] IDENTITY(1,1) NOT NULL,
	[type] [int] NOT NULL,
	[description] [nvarchar](max) NULL
) ON [PRIMARY]

GO


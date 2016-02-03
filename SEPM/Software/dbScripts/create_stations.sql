USE [IntelligentAndonSystem]
GO

/****** Object:  Table [dbo].[stations]    Script Date: 09/21/2011 09:59:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[stations](
	[slNo] [int] IDENTITY(1,1) NOT NULL,
	[id] [int] NOT NULL,
	[line_id] [int] NOT NULL,
	[description] [nvarchar](max) NULL
) ON [PRIMARY]

GO


USE [IntelligentAndonSystem]
GO

/****** Object:  Table [dbo].[departments]    Script Date: 09/21/2011 09:57:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[departments](
	[slNo] [int] IDENTITY(1,1) NOT NULL,
	[id] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
	[message] [nvarchar](max) NULL
) ON [PRIMARY]

GO


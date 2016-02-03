USE [IntelligentAndonSystem]
GO

/****** Object:  Table [dbo].[lines]    Script Date: 09/21/2011 09:58:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[lines](
	[slNo] [int] IDENTITY(1,1) NOT NULL,
	[id] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
	[stages] [int] NOT NULL,
	[idf_id] [int] NOT NULL
) ON [PRIMARY]

GO


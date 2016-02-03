USE [IntelligentAndonSystem]
GO

/****** Object:  Table [dbo].[communication_log]    Script Date: 09/21/2011 09:57:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[communication_log](
	[slNo] [int] IDENTITY(1,1) NOT NULL,
	[id] [int] NOT NULL,
	[type] [int] NOT NULL,
	[timestamp] [datetime] NOT NULL
) ON [PRIMARY]

GO


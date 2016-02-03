USE [IntelligentAndonSystem]
GO

/****** Object:  Table [dbo].[sms_trigger]    Script Date: 09/21/2011 09:58:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[sms_trigger](
	[slNo] [int] IDENTITY(1,1) NOT NULL,
	[entity_id] [int] NOT NULL
) ON [PRIMARY]

GO


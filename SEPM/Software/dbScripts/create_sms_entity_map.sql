USE [IntelligentAndonSystem]
GO

/****** Object:  Table [dbo].[sms_entity_map]    Script Date: 09/21/2011 09:58:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[sms_entity_map](
	[slNo] [int] IDENTITY(1,1) NOT NULL,
	[receiver_id] [int] NOT NULL,
	[entity_id] [int] NOT NULL
) ON [PRIMARY]

GO


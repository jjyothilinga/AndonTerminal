USE [IntelligentAndonSystem]
GO

/****** Object:  Table [dbo].[sms_receiver]    Script Date: 09/21/2011 09:58:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[sms_receiver](
	[slNo] [int] IDENTITY(1,1) NOT NULL,
	[receiver] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO


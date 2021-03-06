USE [IntelligentAndonSystem]
GO
/****** Object:  Trigger [dbo].[onUpdate]    Script Date: 09/21/2011 16:06:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[onUpdate]
   ON  [dbo].[issues]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
   
	INSERT INTO issue_tracker(issue_no , [status] , timestamp) 
	select slNo , [status] , timestamp from inserted
END

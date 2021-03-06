USE [IntelligentAndonSystem]
GO
/****** Object:  Trigger [dbo].[onNewInsert]    Script Date: 09/21/2011 15:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[onNewInsert]
   ON  [dbo].[issues]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
    declare @issueid as int
    declare @status as nvarchar(50)
    declare @tstp as datetime
    declare @dept as int
    
    set @issueid =(select slNo from inserted)
    set @status = (select status from inserted)
    set @tstp = (select [timestamp] from inserted)
    set @dept = (select department from inserted)
    
	INSERT INTO issue_tracker(issue_no , [status] , timestamp) 
	values( @issueid , @status , @tstp)
	
	INSERT INTO sms_trigger(entity_id) values(@dept)
END

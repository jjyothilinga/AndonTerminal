USE [IntelligentAndonSystem]
GO
/****** Object:  Trigger [dbo].[onNewInsert]    Script Date: 01/06/2012 15:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery8.sql|7|0|C:\Documents and Settings\Prabhu\Local Settings\Temp\~vs1BB.sql
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER TRIGGER [dbo].[onNewInsert]
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
    declare @message as varchar(max)
    declare @station as int
    
    set @issueid =(select slNo from inserted)
    set @status = (select status from inserted)
    set @tstp = (select [timestamp] from inserted)
    set @dept = (select department from inserted)
    set @message = (select [message] from inserted)
    set @station = (select station from inserted)
	INSERT INTO issue_tracker(issue_no , [status] , timestamp) 
	values( @issueid , @status , @tstp)
	
	INSERT INTO sms_trigger(receiver,message,status) select receiver ,
	'IDF-8:' + lines.description +':' + stations.description+':'+ @message,1 from sms_entity_map 
	inner join departments on sms_entity_map.entity_id = departments.id
	inner join stations on stations.id = @station
	inner join lines on stations.line_id = lines.id
	inner join sms_receiver on sms_entity_map.receiver_id = sms_receiver.slNo where entity_id = @dept
	and parameter_1 = 0
END

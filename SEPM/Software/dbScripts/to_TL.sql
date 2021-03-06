/*
   Sunday, November 06, 20114:44:27 AM
   User: sa
   Server: SAMBRAHMA\SQLEXPRESS
   Database: IntelligentAndonSystem
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_to_TL
	(
	slno int NOT NULL IDENTITY (1, 1),
	oobid int NULL,
	command nvarchar(50) NULL,
	status int NULL,
	timestamp datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_to_TL SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_to_TL ON
GO
IF EXISTS(SELECT * FROM dbo.to_TL)
	 EXEC('INSERT INTO dbo.Tmp_to_TL (slno, oobid, command, status, timestamp)
		SELECT slno, oobid, command, status, CONVERT(datetime, timestamp) FROM dbo.to_TL WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_to_TL OFF
GO
DROP TABLE dbo.to_TL
GO
EXECUTE sp_rename N'dbo.Tmp_to_TL', N'to_TL', 'OBJECT' 
GO
COMMIT
select Has_Perms_By_Name(N'dbo.to_TL', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.to_TL', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.to_TL', 'Object', 'CONTROL') as Contr_Per 
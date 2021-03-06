/*
   Sunday, November 06, 20114:44:46 AM
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
CREATE TABLE dbo.Tmp_from_TL
	(
	slno int NOT NULL IDENTITY (1, 1),
	oobid int NULL,
	cmd nvarchar(50) NULL,
	status int NULL,
	timestamp datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_from_TL SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_from_TL ON
GO
IF EXISTS(SELECT * FROM dbo.from_TL)
	 EXEC('INSERT INTO dbo.Tmp_from_TL (slno, oobid, cmd, status, timestamp)
		SELECT slno, oobid, cmd, status, CONVERT(datetime, timestamp) FROM dbo.from_TL WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_from_TL OFF
GO
DROP TABLE dbo.from_TL
GO
EXECUTE sp_rename N'dbo.Tmp_from_TL', N'from_TL', 'OBJECT' 
GO
COMMIT
select Has_Perms_By_Name(N'dbo.from_TL', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.from_TL', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.from_TL', 'Object', 'CONTROL') as Contr_Per 
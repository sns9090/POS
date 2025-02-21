USE [dbmstr]
GO
/****** Object:  StoredProcedure [dbo].[Auto_backup_db]    Script Date: 09/01/42 02:52:43 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- Add the parameters for the stored procedure here :dbname,bkppath
ALTER PROCEDURE [dbo].[Auto_backup_db]
	@dbname VARCHAR(50),
	@bkppath VARCHAR(256),
	--@my_pass VARCHAR(256),
	@errstatus int = 0 output,
	@file_name_crtd VARCHAR(256) = '' output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--set @file_name_crtd=''
	declare @fullName  VARCHAR(256) -- filename for backup 
    DECLARE @fileDate VARCHAR(20) -- used for file name
	--SELECT @fileDate = CONVERT(VARCHAR(20),GETDATE(),112) 
	select @fileDate = replace(convert(varchar(8), GETDATE(), 112), ':','') 

    SELECT name FROM master.dbo.sysdatabases WHERE name=@dbname
	if @@rowcount=0  --means database DOES NOT exist
	begin
	   set @errstatus=1  --1=data base does not exist
	   return  --@errstatus
	end
    SET @fullName = @bkppath +'\' + 'Auto_Bak_' + @dbname + '_' + @fileDate + '.bak'  
    BACKUP DATABASE 
	       @dbname 
		   TO DISK = @fullName --with compression --, MEDIAPASSWORD=@my_pass

    IF @@ERROR <> 0  --0 means error
       set @errstatus=2  --2=error during backup
    else 
	  begin
	    set @errstatus=0   --no errors-->successful
	    set @file_name_crtd=@fullName
	    --print @fullName
        --return @errstatus,@fullName
	  end
END
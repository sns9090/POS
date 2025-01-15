USE [db01y2020]
GO

/****** Object:  UserDefinedFunction [dbo].[get_item_bal_by_whno]    Script Date: 15-11-2020 01:44:51 Õ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[get_item_bal_by_whno]
(@itemno varchar(20),@whno varchar(2),@brno varchar(2)) 
RETURNS numeric(8,2)
AS
BEGIN 
    declare @string numeric(8,2)
	--set @string=@strnum
	set @string = (select isnull(sum(qty + openbal),0) from whbins where br_no=@brno and wh_no=@whno and item_no=@itemno)
	return @string

	--select  [dbo].[get_numbers_from_string] ('SA1234M30')
end


GO



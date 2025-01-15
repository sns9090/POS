USE [db01y2018]
GO

/****** Object:  StoredProcedure [dbo].[bld_brprices_all]    Script Date: 15/11/40 10:32:43 Õ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[bld_brprices_all]

@br_no varchar(2)
--@br_no varchar(2),
--@wh_no varchar(2)
--@posted int=1
AS
BEGIN
	
  UPDATE t1 SET 
         t1.item_price = t2.lprice1
  FROM   items t1 
         INNER JOIN brprices t2 
                   ON t1.item_no = t2.itemno
  WHERE  t2.branch=@br_no ; 

  UPDATE t1 SET 
         t1.price = t2.lprice1
  FROM   items_bc t1 
         INNER JOIN brprices t2 
                   ON t1.item_no = t2.itemno
  WHERE  t2.branch=@br_no ; 

END






GO



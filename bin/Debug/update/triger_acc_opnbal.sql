USE [db01y2018]
GO

/****** Object:  Trigger [dbo].[bld_qty_cost_all]    Script Date: 10/07/40 12:33:19 Õ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create trigger [dbo].[beld_acc_balances_opnbal]
on [dbo].[accounts]
after insert,update,delete
as
begin 
exec beld_acc_balances_all_opnbal
--print 'ok biulded'
end

GO



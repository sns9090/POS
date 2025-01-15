USE [db01y2020]
GO

/****** Object:  UserDefinedTableType [dbo].[tblpos_hdr]    Script Date: 17-01-2022 07:40:40 ã ******/
CREATE TYPE [dbo].[tblpos_hdr] AS TABLE(
	[brno] [varchar](2) NOT NULL,
	[slno] [varchar](2) NOT NULL,
	[ref] [int] NOT NULL,
	[contr] [int] NOT NULL,
	[type] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[total] [float] NOT NULL,
	[count] [float] NOT NULL,
	[payed] [float] NOT NULL,
	[total_cost] [float] NULL,
	[saleman] [nvarchar](50) NULL,
	[req_no] [int] NULL,
	[cust_no] [int] NULL,
	[discount] [float] NULL,
	[net_total] [float] NULL,
	[sysdate] [datetime] NULL,
	[gen_ref] [int] NULL,
	[tax_amt] [float] NULL,
	[dscper] [float] NULL,
	[card_type] [int] NULL,
	[card_amt] [float] NULL,
	[rref] [int] NULL,
	[rcontr] [int] NULL,
	[taxfree_amt] [float] NULL,
	[note] [nvarchar](100) NULL,
	[mobile] [nvarchar](20) NULL
)
GO



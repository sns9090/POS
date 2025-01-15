USE [db01y2020]
GO

/****** Object:  UserDefinedTableType [dbo].[tblpos_dtl]    Script Date: 17-01-2022 07:41:07 ã ******/
CREATE TYPE [dbo].[tblpos_dtl] AS TABLE(
	[brno] [varchar](2) NOT NULL,
	[slno] [varchar](2) NULL,
	[ref] [int] NOT NULL,
	[contr] [int] NOT NULL,
	[type] [int] NOT NULL,
	[barcode] [nvarchar](20) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[unit] [nvarchar](3) NOT NULL,
	[price] [float] NOT NULL,
	[qty] [float] NOT NULL,
	[cost] [float] NULL,
	[is_req] [int] NULL,
	[itemno] [nvarchar](16) NOT NULL,
	[srno] [int] NULL,
	[pkqty] [float] NULL,
	[discpc] [float] NULL,
	[tax_id] [int] NULL,
	[tax_amt] [float] NULL,
	[rqty] [float] NULL,
	[offr_amt] [float] NULL
)
GO



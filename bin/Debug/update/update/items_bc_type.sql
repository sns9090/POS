USE [db01y2020]
GO

/****** Object:  UserDefinedTableType [dbo].[tb_items_bc]    Script Date: 15-03-2021 02:06:46 Õ ******/
CREATE TYPE [dbo].[tb_items_bc] AS TABLE(
	[item_no] [nvarchar](16) NOT NULL,
	[barcode] [nvarchar](20) NOT NULL,
	[pack] [int] NOT NULL,
	[pk_qty] [numeric](6, 2) NOT NULL,
	[price] [numeric](7, 2) NOT NULL,
	[note] [nvarchar](50) NULL,
	[pkorder] [int] NULL,
	[price2] [numeric](10, 2) NULL,
	[price3] [numeric](10, 2) NULL,
	[min_price] [numeric](10, 2) NULL,
        [br_no] [nchar](2) NOT NULL,
	[sl_no] [nchar](2) NOT NULL
)
GO



USE [db01y2020]
GO

/****** Object:  UserDefinedTableType [dbo].[tb_items]    Script Date: 15-03-2021 02:06:41 Õ ******/
CREATE TYPE [dbo].[tb_items] AS TABLE(
	[item_no] [nvarchar](16) NOT NULL,
	[item_name] [nvarchar](100) NOT NULL,
	[item_cost] [float] NOT NULL,
	[item_price] [numeric](10, 2) NOT NULL,
	[item_barcode] [nvarchar](20) NOT NULL,
	[item_unit] [int] NULL,
	[item_obalance] [numeric](10, 2) NULL,
	[item_cbalance] [numeric](10, 2) NULL,
	[item_group] [nvarchar](4) NOT NULL,
	[item_image] [nvarchar](200) NULL,
	[item_req] [int] NULL,
	[item_tax] [int] NULL,
	[unit2] [int] NULL,
	[uq2] [numeric](6, 2) NULL,
	[unit2p] [numeric](7, 2) NULL,
	[unit3] [int] NULL,
	[uq3] [numeric](6, 2) NULL,
	[unit3p] [numeric](7, 2) NULL,
	[unit4] [int] NULL,
	[uq4] [numeric](6, 2) NULL,
	[unit4p] [numeric](7, 2) NULL,
	[item_ename] [nvarchar](70) NULL,
	[item_opencost] [float] NULL,
	[item_curcost] [float] NULL,
	[supno] [nvarchar](10) NULL,
	[note] [nvarchar](max) NULL,
	[last_updt] [char](8) NULL,
	[sgroup] [nvarchar](4) NULL,
	[price2] [numeric](10, 2) NULL,
	[price3] [numeric](10, 2) NULL,
	[min_price] [numeric](10, 2) NULL,
	[static_cost] [numeric](10, 4) NULL,
	[inactive] [bit] NULL
)
GO



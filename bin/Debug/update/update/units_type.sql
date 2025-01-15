USE [db01y2020]
GO

/****** Object:  UserDefinedTableType [dbo].[tb_units]    Script Date: 15-03-2021 02:06:55 Õ ******/
CREATE TYPE [dbo].[tb_units] AS TABLE(
	[unit_id] [int] NOT NULL,
	[unit_name] [nvarchar](50) NOT NULL,
	[unit_desc] [nvarchar](100) NULL,
	[unit_order] [int] NULL,
	[unit_qty] [int] NULL
)
GO



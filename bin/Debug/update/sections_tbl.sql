USE [db01y2020]
GO

/****** Object:  Table [dbo].[sctions]    Script Date: 20/03/42 01:10:28 ’ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sctions](
	[sc_code] [varchar](2) NOT NULL,
	[sc_name] [varchar](40) NOT NULL,
	[sc_lname] [varchar](40) NULL,
	[sc_brno] [varchar](2) NOT NULL,
	[sc_whno] [char](2) NULL,
	[sc_arcode] [numeric](6, 0) NULL,
	[sc_nowh] [numeric](1, 0) NULL,
	[sc_m_center] [bit] NULL,
	[lastupdt] [char](8) NULL,
	[rebate_act] [char](19) NULL,
	[lossRebateAct] [char](19) NULL,
	[jv_01] [numeric](6, 0) NULL,
	[jv_02] [numeric](6, 0) NULL,
	[jv_03] [numeric](6, 0) NULL,
	[jv_04] [numeric](6, 0) NULL,
	[jv_05] [numeric](6, 0) NULL,
	[jv_06] [numeric](6, 0) NULL,
	[jv_07] [numeric](6, 0) NULL,
	[jv_08] [numeric](6, 0) NULL,
	[jv_09] [numeric](6, 0) NULL,
	[jv_10] [numeric](6, 0) NULL,
	[jv_11] [numeric](6, 0) NULL,
	[jv_12] [numeric](6, 0) NULL,
	[jv_13] [numeric](6, 0) NULL,
	[jv_14] [numeric](6, 0) NULL,
	[jv_18] [numeric](6, 0) NULL,
	[jv_19] [numeric](6, 0) NULL,
	[jv_20] [numeric](6, 0) NULL,
	[jv_21] [numeric](6, 0) NULL,
	[jv_22] [numeric](6, 0) NULL,
	[jv_23] [numeric](6, 0) NULL,
	[jv_24] [numeric](6, 0) NULL,
	[jv_26] [numeric](6, 0) NULL,
	[jv_27] [numeric](6, 0) NULL,
	[jv_28] [numeric](6, 0) NULL,
	[jv_30] [numeric](6, 0) NULL,
	[jv_31] [numeric](6, 0) NULL,
	[jv_32] [numeric](6, 0) NULL,
	[jv_33] [numeric](6, 0) NULL,
	[jv_34] [numeric](6, 0) NULL,
	[jv_45] [numeric](10, 0) NULL,
	[jv_46] [numeric](6, 0) NULL,
	[jv_16] [numeric](6, 0) NULL,
	[jv_17] [numeric](6, 0) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF_glsction_sc_nowh]  DEFAULT ((1)) FOR [sc_nowh]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF_glsction_sc_m_center]  DEFAULT ((0)) FOR [sc_m_center]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_01__1A74D648]  DEFAULT ((0)) FOR [jv_01]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_02__1B68FA81]  DEFAULT ((0)) FOR [jv_02]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_03__1C5D1EBA]  DEFAULT ((0)) FOR [jv_03]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_04__1D5142F3]  DEFAULT ((0)) FOR [jv_04]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_05__1E45672C]  DEFAULT ((0)) FOR [jv_05]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_06__1F398B65]  DEFAULT ((0)) FOR [jv_06]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_07__202DAF9E]  DEFAULT ((0)) FOR [jv_07]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_08__2121D3D7]  DEFAULT ((0)) FOR [jv_08]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_09__2215F810]  DEFAULT ((0)) FOR [jv_09]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_10__230A1C49]  DEFAULT ((0)) FOR [jv_10]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_11__23FE4082]  DEFAULT ((0)) FOR [jv_11]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_12__24F264BB]  DEFAULT ((0)) FOR [jv_12]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_13__25E688F4]  DEFAULT ((0)) FOR [jv_13]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_14__26DAAD2D]  DEFAULT ((0)) FOR [jv_14]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_18__27CED166]  DEFAULT ((0)) FOR [jv_18]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_19__28C2F59F]  DEFAULT ((0)) FOR [jv_19]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_20__29B719D8]  DEFAULT ((0)) FOR [jv_20]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_21__2AAB3E11]  DEFAULT ((0)) FOR [jv_21]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_22__2B9F624A]  DEFAULT ((0)) FOR [jv_22]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_23__2C938683]  DEFAULT ((0)) FOR [jv_23]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_24__2D87AABC]  DEFAULT ((0)) FOR [jv_24]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_26__2E7BCEF5]  DEFAULT ((0)) FOR [jv_26]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_27__2F6FF32E]  DEFAULT ((0)) FOR [jv_27]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_28__30641767]  DEFAULT ((0)) FOR [jv_28]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_30__31583BA0]  DEFAULT ((0)) FOR [jv_30]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_31__324C5FD9]  DEFAULT ((0)) FOR [jv_31]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_32__33408412]  DEFAULT ((0)) FOR [jv_32]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_33__3434A84B]  DEFAULT ((0)) FOR [jv_33]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_34__3528CC84]  DEFAULT ((0)) FOR [jv_34]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_45__361CF0BD]  DEFAULT ((0)) FOR [jv_45]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_46__371114F6]  DEFAULT ((0)) FOR [jv_46]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_16__3805392F]  DEFAULT ((0)) FOR [jv_16]
GO

ALTER TABLE [dbo].[sctions] ADD  CONSTRAINT [DF__glsction__jv_17__38F95D68]  DEFAULT ((0)) FOR [jv_17]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Õ”«»  Œ›Ì÷« ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sctions', @level2type=N'COLUMN',@level2name=N'rebate_act'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Õ”«» Œ”«∆— «· Œ›Ì÷« ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sctions', @level2type=N'COLUMN',@level2name=N'lossRebateAct'
GO



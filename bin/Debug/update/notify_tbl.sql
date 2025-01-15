USE [db01y2018]
GO

/****** Object:  Table [dbo].[notify]    Script Date: 23/06/40 01:16:35 Õ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[notify](
	[branch] [varchar](2) NULL,
	[n_no] [int] IDENTITY(1,1) NOT NULL,
	[n_subject] [varchar](100) NULL,
	[trtype] [varchar](2) NULL,
	[ref] [numeric](6, 0) NULL,
	[folio] [numeric](5, 0) NULL,
	[trdate] [char](8) NULL,
	[mode] [numeric](1, 0) NULL,
	[usrnotify] [varchar](50) NOT NULL,
	[datenotify] [datetime] NULL,
	[inactive] [bit] NULL,
	[notes] [text] NULL,
	[usrid] [varchar](50) NOT NULL,
	[src] [char](2) NULL,
	[smsaction] [bit] NULL,
	[emailacton] [bit] NULL,
	[loginacton] [bit] NULL,
	[pemail] [varchar](50) NULL,
	[pmobile] [varchar](20) NULL,
	[smsdone] [bit] NULL,
	[emaildone] [bit] NULL,
	[autojv] [bit] NULL,
	[ntperiod] [char](1) NULL,
	[lastprdgnr] [numeric](2, 0) NULL,
	[stop_jv] [bit] NULL,
	[actcode] [char](19) NULL,
	[fcy] [char](3) NULL,
	[expdays] [numeric](4, 0) NULL,
	[isopen] [bit] NULL,
	[del_by_s] [bit] NULL,
	[del_by_r] [bit] NULL,
 CONSTRAINT [PK_notify] PRIMARY KEY CLUSTERED 
(
	[n_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[notify] ADD  CONSTRAINT [DF_notify_datenotify]  DEFAULT (getdate()) FOR [datenotify]
GO

ALTER TABLE [dbo].[notify] ADD  CONSTRAINT [DF_notify_isopen]  DEFAULT ((0)) FOR [isopen]
GO

ALTER TABLE [dbo].[notify] ADD  CONSTRAINT [DF_notify_del_by_sender]  DEFAULT ((0)) FOR [del_by_s]
GO

ALTER TABLE [dbo].[notify] ADD  CONSTRAINT [DF_notify_del_by_r]  DEFAULT ((0)) FOR [del_by_r]
GO



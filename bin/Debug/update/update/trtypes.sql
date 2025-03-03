USE [db01y2020]
GO
/****** Object:  Table [dbo].[trtypes]    Script Date: 01-01-2022 10:20:09 ص ******/
DROP TABLE [dbo].[trtypes]
GO
/****** Object:  Table [dbo].[trtypes]    Script Date: 01-01-2022 10:20:09 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trtypes](
	[tr_no] [varchar](2) NOT NULL,
	[tr_name] [nvarchar](50) NOT NULL,
	[tr_ename] [nvarchar](50) NULL,
	[tr_abriv] [nvarchar](50) NULL,
 CONSTRAINT [PK_trtypes] PRIMARY KEY CLUSTERED 
(
	[tr_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'01', N'قيد عام', N'General Record', N'ق عام')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'02', N'سند قبض', N'Cash In', N'س قبض')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'03', N'سند صرف', N'Cash Out', N'س صرف')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'04', N'فاتورة مبيعات نقدية', N'Cash Sale', N'بيع نقد')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'05', N'فاتورة مبيعات اجلة', N'Credit Sale', N'بيع اجل')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'06', N'فاتورة شراء نقدي', N'Cash Purch.', N'شراء نقد')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'07', N'فاتورة شراء اجل', N'Credit Purch.', N'شراء اجل')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'11', N'اشعار دائن', N'Notice creditor', N'اش دائن')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'12', N'اشعار مدين', N'Notice depitor', N'اش مدين')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'14', N'فاتورة مرتجع نقدي', N'Cash Return Sale', N'م بيع نقد')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'15', N'فاتورة مرتجع اجل', N'Credit Return Sale', N'م بيع اجل')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'16', N'مرتجع شراء نقدي', N'Cash Return Purch.', N'م شراء نقد')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'17', N'مرتجع شراء اجل', N'Credit Return Purch.', N'م شراء اجل')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'21', N'قيد جرد مستمر', N'
Continuous Record', N'جرد مستمر')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'24', N'عرض سعر', N'Price Offer', N'عرض سعر')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'26', N'طلبية شراء', N'Purch. Order', N'طلب شراء')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'31', N'سند استلام كميات', N'Qty Recive', N'سند استلام')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'32', N'سند صرف كميات', N'Qty Out', N'سند صرف')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'33', N'تحويل كمية اصناف', N'Qty Transfer', N'تحويل صنف')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'34', N'تسوية تكلفة اصناف', N'Cost adjust', N'تسوية تكلفة')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'35', N'سند جرد مخزني', N'Inventroy', N'سند جرد')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'36', N'تسوية تكلفة صنف', N'Cost adjust2', N'تسوية تكلفة')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'45', N'تركيب وانتاج صنف', N'Item Combin', N'تركيب صنف')
GO
INSERT [dbo].[trtypes] ([tr_no], [tr_name], [tr_ename], [tr_abriv]) VALUES (N'46', N'فك تركيب صنف', N'Item UnCombin', N'فك صنف')
GO

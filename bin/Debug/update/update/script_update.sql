USE [db01y2020]
GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetNumeric]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[udf_GetNumeric]
GO
/****** Object:  UserDefinedFunction [dbo].[get_numbers_from_string]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[get_numbers_from_string]
GO
/****** Object:  UserDefinedFunction [dbo].[get_lastprice_for_item1]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[get_lastprice_for_item1]
GO
/****** Object:  UserDefinedFunction [dbo].[get_lastprice_for_item]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[get_lastprice_for_item]
GO
/****** Object:  UserDefinedFunction [dbo].[get_lastprice_for_clint]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[get_lastprice_for_clint]
GO
/****** Object:  UserDefinedFunction [dbo].[get_lastcost_for_item]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[get_lastcost_for_item]
GO
/****** Object:  UserDefinedFunction [dbo].[get_item_bal_old]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[get_item_bal_old]
GO
/****** Object:  UserDefinedFunction [dbo].[get_item_bal_by_whno]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[get_item_bal_by_whno]
GO
/****** Object:  UserDefinedFunction [dbo].[get_item_bal]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[get_item_bal]
GO
/****** Object:  UserDefinedFunction [dbo].[get_balanace_for_item]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP FUNCTION [dbo].[get_balanace_for_item]
GO
/****** Object:  StoredProcedure [dbo].[update_pos_inv]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[update_pos_inv]
GO
/****** Object:  StoredProcedure [dbo].[update_items_table]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[update_items_table]
GO
/****** Object:  StoredProcedure [dbo].[update_items]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[update_items]
GO
/****** Object:  StoredProcedure [dbo].[update_acc_inv]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[update_acc_inv]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_lastprice_for_item]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[sp_get_lastprice_for_item]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_custprice_for_item]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[sp_get_custprice_for_item]
GO
/****** Object:  StoredProcedure [dbo].[sp_generate_merge2]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[sp_generate_merge2]
GO
/****** Object:  StoredProcedure [dbo].[sp_generate_merge]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[sp_generate_merge]
GO
/****** Object:  StoredProcedure [dbo].[show_all_tables]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[show_all_tables]
GO
/****** Object:  StoredProcedure [dbo].[send_pos_inv]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[send_pos_inv]
GO
/****** Object:  StoredProcedure [dbo].[Select_Product]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Select_Product]
GO
/****** Object:  StoredProcedure [dbo].[SearchUsers]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[SearchUsers]
GO
/****** Object:  StoredProcedure [dbo].[SearchProduct]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[SearchProduct]
GO
/****** Object:  StoredProcedure [dbo].[SearchAllTables]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[SearchAllTables]
GO
/****** Object:  StoredProcedure [dbo].[Search_Customer]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Search_Customer]
GO
/****** Object:  StoredProcedure [dbo].[Save_Pos_Hdr_Dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Save_Pos_Hdr_Dtl]
GO
/****** Object:  StoredProcedure [dbo].[Save_Hdr_Dtl_Data]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Save_Hdr_Dtl_Data]
GO
/****** Object:  StoredProcedure [dbo].[Save_Detail_Data]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Save_Detail_Data]
GO
/****** Object:  StoredProcedure [dbo].[Move_Tran_To_Other]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Move_Tran_To_Other]
GO
/****** Object:  StoredProcedure [dbo].[move_tr_serial]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[move_tr_serial]
GO
/****** Object:  StoredProcedure [dbo].[Move_Acc_To_Other]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Move_Acc_To_Other]
GO
/****** Object:  StoredProcedure [dbo].[import_inv_from_pos]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[import_inv_from_pos]
GO
/****** Object:  StoredProcedure [dbo].[import_inv_from_branch]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[import_inv_from_branch]
GO
/****** Object:  StoredProcedure [dbo].[Grd_Enter]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Grd_Enter]
GO
/****** Object:  StoredProcedure [dbo].[getTotalCount]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[getTotalCount]
GO
/****** Object:  StoredProcedure [dbo].[GetCurUser]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[GetCurUser]
GO
/****** Object:  StoredProcedure [dbo].[get_whbins_RM]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[get_whbins_RM]
GO
/****** Object:  StoredProcedure [dbo].[get_whbins_bal_cost]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[get_whbins_bal_cost]
GO
/****** Object:  StoredProcedure [dbo].[Get_Order_Dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Get_Order_Dtl]
GO
/****** Object:  StoredProcedure [dbo].[get_cust_bal]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[get_cust_bal]
GO
/****** Object:  StoredProcedure [dbo].[generate_merge]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[generate_merge]
GO
/****** Object:  StoredProcedure [dbo].[formate_db]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[formate_db]
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[DeleteProduct]
GO
/****** Object:  StoredProcedure [dbo].[Delete_Trx]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Delete_Trx]
GO
/****** Object:  StoredProcedure [dbo].[delete_files_data]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[delete_files_data]
GO
/****** Object:  StoredProcedure [dbo].[Del_Product]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[Del_Product]
GO
/****** Object:  StoredProcedure [dbo].[curser_in_curser]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[curser_in_curser]
GO
/****** Object:  StoredProcedure [dbo].[create_messing_tabels]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[create_messing_tabels]
GO
/****** Object:  StoredProcedure [dbo].[create_messing_columns]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[create_messing_columns]
GO
/****** Object:  StoredProcedure [dbo].[create_combin_sale_tran]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[create_combin_sale_tran]
GO
/****** Object:  StoredProcedure [dbo].[create_admin_user]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[create_admin_user]
GO
/****** Object:  StoredProcedure [dbo].[copy_stock_balances_from_old_db]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[copy_stock_balances_from_old_db]
GO
/****** Object:  StoredProcedure [dbo].[copy_data_from_old_db]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[copy_data_from_old_db]
GO
/****** Object:  StoredProcedure [dbo].[copy_data_by_link_test]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[copy_data_by_link_test]
GO
/****** Object:  StoredProcedure [dbo].[copy_data_by_link]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[copy_data_by_link]
GO
/****** Object:  StoredProcedure [dbo].[copy_br_acc]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[copy_br_acc]
GO
/****** Object:  StoredProcedure [dbo].[copy_acc_balances_from_old_db]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[copy_acc_balances_from_old_db]
GO
/****** Object:  StoredProcedure [dbo].[chk_for_taqween]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[chk_for_taqween]
GO
/****** Object:  StoredProcedure [dbo].[check_moved]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[check_moved]
GO
/****** Object:  StoredProcedure [dbo].[change_price_as_tax]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[change_price_as_tax]
GO
/****** Object:  StoredProcedure [dbo].[change_pos_branch]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[change_pos_branch]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_sl_pu]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_sl_pu]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_by_tran_o]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_by_tran_o]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_by_tran]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_by_tran]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_rajba2]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_all_rajba2]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_posted]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_all_posted]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_ok]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_all_ok]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_new]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_all_new]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_by_tran]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_all_by_tran]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_by_sal_tran]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_all_by_sal_tran]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_by_br]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_all_by_br]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins_cost_all]
GO
/****** Object:  StoredProcedure [dbo].[bld_whbins]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_whbins]
GO
/****** Object:  StoredProcedure [dbo].[bld_taxfree_sl_pu]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_taxfree_sl_pu]
GO
/****** Object:  StoredProcedure [dbo].[bld_sal_cost_all]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_sal_cost_all]
GO
/****** Object:  StoredProcedure [dbo].[bld_qty_convert_cost_all]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_qty_convert_cost_all]
GO
/****** Object:  StoredProcedure [dbo].[bld_items_bc_from_items]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_items_bc_from_items]
GO
/****** Object:  StoredProcedure [dbo].[bld_brprices_all]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[bld_brprices_all]
GO
/****** Object:  StoredProcedure [dbo].[beld_sup_balances_all_br]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[beld_sup_balances_all_br]
GO
/****** Object:  StoredProcedure [dbo].[beld_sup_balances]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[beld_sup_balances]
GO
/****** Object:  StoredProcedure [dbo].[beld_cust_balances_all_br]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[beld_cust_balances_all_br]
GO
/****** Object:  StoredProcedure [dbo].[beld_cust_balances]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[beld_cust_balances]
GO
/****** Object:  StoredProcedure [dbo].[beld_acc_su_class_opnbal]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[beld_acc_su_class_opnbal]
GO
/****** Object:  StoredProcedure [dbo].[beld_acc_cu_class_opnbal]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[beld_acc_cu_class_opnbal]
GO
/****** Object:  StoredProcedure [dbo].[beld_acc_balances_all_opnbal]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[beld_acc_balances_all_opnbal]
GO
/****** Object:  StoredProcedure [dbo].[beld_acc_balances_all]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP PROCEDURE [dbo].[beld_acc_balances_all]
GO
/****** Object:  UserDefinedTableType [dbo].[tblpos_hdr]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tblpos_hdr]
GO
/****** Object:  UserDefinedTableType [dbo].[tblpos_dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tblpos_dtl]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_zttk]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_zttk]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_whbins]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_whbins]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_units]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_units]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_tran_items]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_tran_items]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_suppliers]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_suppliers]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sto_hdr]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_sto_hdr]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sto_dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_sto_dtl]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_salofr_dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_salofr_dtl]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sales_hdr]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_sales_hdr]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sales_dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_sales_dtl]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sal_hdr]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_sal_hdr]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_pu_hdr]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_pu_hdr]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_pu_dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_pu_dtl]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_pos_salmen]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_pos_salmen]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_items_offer]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_items_offer]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_items_bc]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_items_bc]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_items]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_items]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_groups]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_groups]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_customers]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_customers]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_brprices]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_brprices]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_accounts]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_accounts]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_acc_hdr]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_acc_hdr]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_acc_dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
DROP TYPE [dbo].[tb_acc_dtl]
GO
/****** Object:  UserDefinedTableType [dbo].[tb_acc_dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_acc_dtl] AS TABLE(
	[a_brno] [varchar](2) NOT NULL,
	[a_type] [varchar](2) NOT NULL,
	[a_ref] [int] NOT NULL,
	[a_key] [nvarchar](50) NOT NULL,
	[a_mdate] [char](8) NOT NULL,
	[a_hdate] [char](8) NOT NULL,
	[a_text] [varchar](200) NULL,
	[a_camt] [numeric](12, 4) NOT NULL,
	[a_damt] [numeric](12, 4) NOT NULL,
	[jddbcr] [char](1) NULL,
	[jdfcamt] [float] NULL,
	[jdcurr] [varchar](2) NULL,
	[a_folio] [numeric](4, 0) NOT NULL,
	[a_sysdate] [datetime] NULL,
	[jdsrc] [char](2) NULL,
	[jdfc_imgval] [float] NULL,
	[jdcstval] [float] NULL,
	[cstkey] [char](22) NULL,
	[brnno] [char](2) NULL,
	[bracc] [char](19) NULL,
	[brxref] [numeric](6, 0) NULL,
	[match] [bit] NULL,
	[rplct_post] [bit] NULL,
	[rowguid] [uniqueidentifier] NULL,
	[taxcatId] [nvarchar](2) NULL,
	[cu_no] [nvarchar](10) NULL,
	[su_no] [nvarchar](10) NULL,
	[sl_no] [varchar](2) NOT NULL,
	[pu_no] [varchar](2) NOT NULL,
	[cc_no] [varchar](2) NULL,
	[qst_ref] [int] NULL,
	[qst_sl] [varchar](2) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_acc_hdr]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_acc_hdr] AS TABLE(
	[a_brno] [varchar](2) NOT NULL,
	[a_type] [varchar](2) NOT NULL,
	[a_ref] [int] NOT NULL,
	[a_mdate] [char](8) NOT NULL,
	[a_hdate] [char](8) NOT NULL,
	[a_text] [varchar](200) NULL,
	[a_amt] [numeric](12, 4) NOT NULL,
	[a_entries] [numeric](4, 0) NULL,
	[jhsrc] [varchar](5) NULL,
	[a_sysdate] [datetime] NULL,
	[jhcurr] [varchar](2) NULL,
	[jhfcflag] [numeric](1, 0) NULL,
	[jhrate] [numeric](10, 5) NULL,
	[jhreleased] [char](8) NULL,
	[a_posted] [bit] NULL,
	[lastupdt] [datetime] NULL,
	[jhlccrttl] [float] NULL,
	[jhlcdbttl] [float] NULL,
	[jhfccrttl] [float] NULL,
	[jhfcdbttl] [float] NULL,
	[jhimgrate] [float] NULL,
	[modified] [bit] NULL,
	[serial_no] [nvarchar](20) NULL,
	[rcvdtrn] [bit] NULL,
	[suspend] [bit] NULL,
	[usrid] [nvarchar](50) NULL,
	[isbrtrx] [bit] NULL,
	[brxref] [char](8) NULL,
	[brxfrm] [char](2) NULL,
	[jhcustno] [varchar](10) NULL,
	[hide_jv] [bit] NULL,
	[rowguid] [uniqueidentifier] NULL,
	[mainkey] [nvarchar](19) NULL,
	[sl_no] [varchar](2) NOT NULL,
	[pu_no] [varchar](2) NOT NULL,
	[aqd_no] [nvarchar](20) NULL,
	[cc_no] [varchar](2) NULL,
	[invsupno] [nvarchar](50) NULL,
	[taxid] [varchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_accounts]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_accounts] AS TABLE(
	[a_key] [nvarchar](50) NOT NULL,
	[a_parent] [int] NOT NULL,
	[a_name] [nvarchar](100) NOT NULL,
	[a_ename] [nvarchar](50) NULL,
	[a_brno] [nvarchar](2) NULL,
	[a_opnbal] [numeric](12, 2) NULL,
	[a_curbal] [numeric](12, 2) NULL,
	[a_crtdate] [char](8) NULL,
	[a_lastupdt] [char](8) NULL,
	[a_group] [char](1) NOT NULL,
	[a_type] [char](1) NOT NULL,
	[a_ptype] [char](1) NOT NULL,
	[a_active] [char](1) NOT NULL,
	[a_level] [int] NOT NULL,
	[glpurge] [char](1) NULL,
	[accontrol] [char](1) NULL,
	[glcurrency] [varchar](2) NOT NULL,
	[fccurbal] [numeric](13, 2) NULL,
	[fcopnbal] [numeric](13, 2) NULL,
	[imopnbal] [float] NULL,
	[imcurbal] [float] NULL,
	[gleval] [bit] NULL,
	[acprotect] [bit] NULL,
	[pkey] [numeric](5, 0) NULL,
	[chkey] [numeric](5, 0) NULL,
	[cashbnk] [bit] NULL,
	[modified] [bit] NULL,
	[section] [varchar](2) NULL,
	[isbracc] [bit] NULL,
	[acckind] [varchar](1) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_brprices]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_brprices] AS TABLE(
	[branch] [varchar](2) NOT NULL,
	[slcenter] [varchar](50) NOT NULL,
	[itemno] [nvarchar](20) NOT NULL,
	[lprice1] [numeric](11, 2) NOT NULL,
	[lprice2] [numeric](11, 2) NULL,
	[lprice3] [numeric](11, 2) NULL,
	[maxdisc1] [numeric](4, 2) NULL,
	[maxdisc2] [numeric](4, 2) NULL,
	[maxdisc3] [numeric](4, 2) NULL,
	[mnmprice] [numeric](11, 2) NULL,
	[pprice1] [numeric](11, 2) NULL,
	[pprice2] [numeric](11, 2) NULL,
	[pprice3] [numeric](11, 2) NULL,
	[barcode] [char](20) NOT NULL,
	[promotion] [bit] NULL,
	[prm_start] [char](20) NULL,
	[prm_end] [char](20) NULL,
	[slsprsnt] [numeric](9, 2) NULL,
	[fprice1] [numeric](11, 2) NULL,
	[fprice2] [numeric](11, 2) NULL,
	[fprice3] [numeric](11, 2) NULL,
	[modified] [bit] NULL,
	[lastupdt] [char](8) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_customers]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_customers] AS TABLE(
	[cu_no] [nvarchar](10) NOT NULL,
	[cu_brno] [varchar](2) NOT NULL,
	[cu_name] [varchar](100) NOT NULL,
	[cu_class] [varchar](2) NOT NULL,
	[cu_addrs] [varchar](50) NULL,
	[cu_tel] [varchar](25) NULL,
	[cu_fax] [char](10) NULL,
	[cu_tlx] [char](10) NULL,
	[cu_email] [varchar](30) NULL,
	[cu_cntactp] [nvarchar](50) NULL,
	[cu_title] [nvarchar](50) NULL,
	[cu_crlmt] [numeric](14, 2) NULL,
	[cu_pymnt] [numeric](3, 0) NULL,
	[cu_status] [numeric](1, 0) NULL,
	[cu_opnbal] [float] NULL,
	[cu_curbal] [float] NULL,
	[cf_fcy] [char](3) NOT NULL,
	[cf_opnfcy] [float] NULL,
	[cf_curfcy] [float] NULL,
	[cu_xrf] [char](6) NULL,
	[cu_alwcr] [bit] NULL,
	[cu_ctlser] [char](19) NULL,
	[lastupdt] [char](8) NULL,
	[cu_lcaloc] [float] NULL,
	[cu_fcaloc] [float] NULL,
	[modified] [bit] NULL,
	[cmncode] [nvarchar](10) NULL,
	[cu_lname] [varchar](40) NULL,
	[cu_city] [varchar](2) NULL,
	[cu_country] [numeric](3, 0) NULL,
	[cu_laddrs] [varchar](50) NULL,
	[cu_mobile] [varchar](25) NULL,
	[cu_sendsms] [bit] NULL,
	[rowguid] [uniqueidentifier] NULL,
	[whno] [nvarchar](10) NULL,
	[vndr_taxcode] [varchar](50) NULL,
	[taxfree] [bit] NULL,
	[cu_kind] [char](1) NULL,
	[section] [char](4) NULL,
	[inactive] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_groups]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_groups] AS TABLE(
	[group_id] [nvarchar](4) NOT NULL,
	[group_name] [nvarchar](50) NOT NULL,
	[group_desc] [nvarchar](100) NULL,
	[group_order] [int] NULL,
	[group_img] [image] NULL,
	[group_pid] [nvarchar](4) NULL,
	[inactive] [bit] NULL,
	[printer] [nvarchar](100) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_items]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_items] AS TABLE(
	[item_no] [nvarchar](20) NOT NULL,
	[item_name] [nvarchar](200) NOT NULL,
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
	[item_ename] [nvarchar](200) NULL,
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
	[inactive] [bit] NULL,
	[dunit] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_items_bc]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_items_bc] AS TABLE(
	[item_no] [nvarchar](20) NOT NULL,
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
/****** Object:  UserDefinedTableType [dbo].[tb_items_offer]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_items_offer] AS TABLE(
	[br_no] [varchar](2) NOT NULL,
	[sl_no] [varchar](2) NOT NULL,
	[itemno] [nvarchar](20) NULL,
	[unicode] [char](6) NULL,
	[barcode] [nvarchar](20) NOT NULL,
	[DiscountP] [numeric](5, 2) NULL,
	[disctype] [char](1) NULL,
	[MinQty] [int] NULL,
	[GivenAmt] [numeric](8, 2) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[InActive] [bit] NULL,
	[lastupdt] [datetime] NULL,
	[MaxQty] [int] NULL,
	[lprice1] [numeric](11, 2) NULL,
	[itemgroup] [char](1) NULL,
	[offer_price] [float] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_pos_salmen]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_pos_salmen] AS TABLE(
	[sl_brno] [varchar](2) NOT NULL,
	[sl_id] [numeric](6, 0) NOT NULL,
	[sl_name] [varchar](50) NOT NULL,
	[slpaddress] [varchar](50) NULL,
	[slptel] [varchar](20) NULL,
	[sl_password] [varchar](100) NOT NULL,
	[sl_chgqty] [bit] NOT NULL,
	[sl_chgprice] [bit] NOT NULL,
	[sl_delline] [bit] NOT NULL,
	[sl_delinv] [bit] NOT NULL,
	[sl_return] [bit] NOT NULL,
	[sl_admin] [bit] NOT NULL,
	[sl_alowdisc] [bit] NOT NULL,
	[slpmaxdisc] [float] NOT NULL,
	[slppricetp] [char](1) NULL,
	[sl_alowcancel] [bit] NULL,
	[slpalowopdr] [bit] NULL,
	[modified] [bit] NULL,
	[sl_alowexit] [bit] NOT NULL,
	[slprtwoinv] [bit] NULL,
	[slpblowbal] [bit] NULL,
	[scr_open] [numeric](2, 0) NULL,
	[alwitmdsc] [bit] NOT NULL,
	[maxitmdsc] [float] NOT NULL,
	[alwuserpls] [bit] NULL,
	[alwuseicrpls] [bit] NULL,
	[slpmagnetic] [bit] NULL,
	[alwusebrrtn] [bit] NULL,
	[alwreprint] [bit] NOT NULL,
	[frcrplinv] [bit] NULL,
	[catch_thief] [bit] NULL,
	[sold_once] [bit] NULL,
	[only_scan_bc] [bit] NULL,
	[stop_item_column] [bit] NULL,
	[slpfpalw] [bit] NULL,
	[slpfpimage] [text] NULL,
	[slcenter] [char](2) NULL,
	[salesquota] [numeric](13, 2) NULL,
	[commissionP] [numeric](9, 3) NULL,
	[bonus] [numeric](13, 2) NULL,
	[supervisor] [bit] NULL,
	[ReadInvByBarcd] [bit] NULL,
	[alwrtncash] [bit] NULL,
	[alwmtchrtn] [bit] NULL,
	[alwinvtrnsfr] [bit] NULL,
	[alwcshcardpay] [bit] NULL,
	[slp_hlldscamtMAX] [numeric](3, 2) NULL,
	[alwprnttmpinv] [bit] NULL,
	[sl_inactive] [bit] NULL,
	[sl_alwcrdit] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_pu_dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_pu_dtl] AS TABLE(
	[branch] [varchar](2) NOT NULL,
	[pucenter] [varchar](2) NOT NULL,
	[invtype] [varchar](2) NOT NULL,
	[ref] [int] NOT NULL,
	[invmdate] [char](8) NULL,
	[invhdate] [char](8) NULL,
	[itemno] [char](20) NULL,
	[unicode] [char](6) NULL,
	[qty] [numeric](10, 2) NULL,
	[fqty] [numeric](10, 2) NULL,
	[price] [numeric](10, 4) NULL,
	[discpc] [float] NULL,
	[cost] [float] NULL,
	[ds_acfm] [numeric](1, 0) NULL,
	[sl_acfm] [numeric](1, 0) NULL,
	[gclass] [char](12) NULL,
	[fullcost] [float] NULL,
	[cur] [varchar](2) NULL,
	[barcode] [nvarchar](20) NULL,
	[imfcval] [float] NULL,
	[pack] [varchar](2) NULL,
	[pkqty] [numeric](8, 2) NULL,
	[shadd] [char](1) NULL,
	[shdpk] [numeric](8, 3) NULL,
	[shdqty] [numeric](11, 3) NULL,
	[frtqty] [numeric](9, 3) NULL,
	[rtnqty] [numeric](12, 3) NULL,
	[whno] [varchar](2) NULL,
	[cmbkey] [char](24) NULL,
	[folio] [numeric](7, 0) NULL,
	[rplct_post] [bit] NULL,
	[sold_item_status] [numeric](1, 0) NULL,
	[tax_amt] [float] NULL,
	[tax_id] [int] NULL,
	[stk_or_ser] [float] NULL,
	[tax_prcent] [float] NULL,
	[expdate] [char](8) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_pu_hdr]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_pu_hdr] AS TABLE(
	[branch] [varchar](2) NOT NULL,
	[pucenter] [varchar](2) NOT NULL,
	[invtype] [varchar](2) NOT NULL,
	[ref] [int] NOT NULL,
	[invmdate] [char](8) NULL,
	[invhdate] [char](8) NULL,
	[supno] [nvarchar](10) NULL,
	[text] [varchar](100) NULL,
	[glser] [varchar](5) NULL,
	[dsctype] [char](1) NULL,
	[pstmode] [numeric](1, 0) NULL,
	[cur] [varchar](2) NULL,
	[currate] [numeric](10, 5) NULL,
	[invttl] [float] NULL,
	[invcst] [float] NULL,
	[invdspc] [float] NULL,
	[invdsvl] [float] NULL,
	[nettotal] [float] NULL,
	[invpaid] [numeric](14, 2) NULL,
	[casher] [nvarchar](16) NULL,
	[entries] [numeric](5, 0) NULL,
	[released] [datetime] NULL,
	[posted] [bit] NULL,
	[fixrate] [numeric](6, 3) NULL,
	[extamt] [numeric](14, 2) NULL,
	[extser] [char](19) NULL,
	[pricetp] [char](1) NULL,
	[ischeque] [bit] NULL,
	[chkno] [char](8) NULL,
	[chkdate] [char](8) NULL,
	[lastupdt] [datetime] NULL,
	[jvgenrt] [bit] NULL,
	[cccommsn] [float] NULL,
	[belowbal] [bit] NULL,
	[invsupno] [nvarchar](50) NULL,
	[ccpayment] [numeric](13, 2) NULL,
	[rplsamt] [nvarchar](50) NULL,
	[pdother] [bit] NULL,
	[slcode] [char](2) NULL,
	[prpaidamt] [numeric](9, 2) NULL,
	[instldays] [numeric](2, 0) NULL,
	[instflag] [bit] NULL,
	[slcmnd] [bit] NULL,
	[inv_printed] [numeric](2, 0) NULL,
	[bendit] [bit] NULL,
	[modified] [bit] NULL,
	[rqstorder] [numeric](6, 0) NULL,
	[rqststld] [bit] NULL,
	[carrier] [char](3) NULL,
	[rcvdtrn] [bit] NULL,
	[usrid] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[wst_key] [nvarchar](20) NULL,
	[wst_amt] [float] NULL,
	[gmark] [float] NULL,
	[tameen] [float] NULL,
	[shahan] [float] NULL,
	[msfr] [float] NULL,
	[mother] [float] NULL,
	[etax] [float] NULL,
	[remarks] [nvarchar](200) NULL,
	[tax_amt_paid] [float] NULL,
	[with_tax] [bit] NULL,
	[taxid] [varchar](50) NULL,
	[tax_percent] [float] NULL,
	[taxfree_amt] [float] NULL,
	[reref] [nvarchar](50) NULL,
	[sabr] [float] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sal_hdr]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_sal_hdr] AS TABLE(
	[branch] [varchar](2) NOT NULL,
	[slcenter] [varchar](2) NOT NULL,
	[invtype] [varchar](2) NOT NULL,
	[ref] [int] NOT NULL,
	[invmdate] [char](8) NULL,
	[invhdate] [char](8) NULL,
	[text] [varchar](200) NULL,
	[remarks] [nvarchar](200) NULL,
	[casher] [nvarchar](16) NULL,
	[entries] [numeric](5, 0) NULL,
	[invttl] [float] NULL,
	[invdsvl] [float] NULL,
	[nettotal] [float] NULL,
	[invdspc] [float] NULL,
	[tax_amt_rcvd] [float] NULL,
	[with_tax] [bit] NULL,
	[usrid] [varchar](50) NULL,
	[custno] [nvarchar](10) NULL,
	[invcst] [float] NULL,
	[suspend] [bit] NULL,
	[glser] [varchar](5) NULL,
	[slcode] [varchar](2) NULL,
	[stkjvno] [numeric](6, 0) NULL,
	[taxid] [varchar](50) NULL,
	[tax_percent] [float] NULL,
	[taxfree_amt] [float] NULL,
	[carrier] [varchar](3) NULL,
	[invpaid] [numeric](14, 2) NULL,
	[chkno] [varchar](10) NULL,
	[reref] [nvarchar](50) NULL,
	[sanedcrd_amt] [numeric](12, 2) NULL,
	[rtncash_dfrpl] [numeric](12, 2) NULL,
	[fcy2] [char](3) NULL,
	[note2] [nvarchar](200) NULL,
	[note3] [nvarchar](200) NULL,
	[inv_mpay] [char](8) NULL,
	[cust_mobil] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sales_dtl]    Script Date: 24-02-2025 7:25:27 PM ******/
CREATE TYPE [dbo].[tb_sales_dtl] AS TABLE(
	[branch] [varchar](2) NOT NULL,
	[slcenter] [varchar](2) NOT NULL,
	[invtype] [varchar](2) NOT NULL,
	[ref] [int] NOT NULL,
	[invmdate] [char](8) NULL,
	[invhdate] [char](8) NULL,
	[itemno] [char](20) NULL,
	[unicode] [char](6) NULL,
	[qty] [numeric](10, 3) NULL,
	[fqty] [numeric](10, 2) NULL,
	[price] [numeric](10, 4) NULL,
	[discpc] [float] NULL,
	[cost] [float] NULL,
	[ds_acfm] [numeric](1, 0) NULL,
	[sl_acfm] [numeric](1, 0) NULL,
	[gclass] [char](12) NULL,
	[custno] [char](6) NULL,
	[fcy] [char](3) NULL,
	[barcode] [nvarchar](20) NULL,
	[imfcval] [float] NULL,
	[pack] [varchar](2) NULL,
	[pkqty] [numeric](10, 2) NULL,
	[shadd] [char](1) NULL,
	[shdpk] [numeric](8, 3) NULL,
	[shdqty] [numeric](11, 3) NULL,
	[frtqty] [numeric](9, 3) NULL,
	[rtnqty] [numeric](12, 3) NULL,
	[whno] [varchar](2) NULL,
	[cmbkey] [nvarchar](max) NULL,
	[folio] [numeric](7, 0) NULL,
	[rplct_post] [bit] NULL,
	[sold_item_status] [numeric](1, 0) NULL,
	[tax_amt] [float] NULL,
	[tax_id] [int] NULL,
	[stk_or_ser] [float] NULL,
	[tax_prcent] [float] NULL,
	[offer_amt] [float] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sales_hdr]    Script Date: 24-02-2025 7:25:28 PM ******/
CREATE TYPE [dbo].[tb_sales_hdr] AS TABLE(
	[branch] [varchar](2) NOT NULL,
	[slcenter] [varchar](2) NOT NULL,
	[invtype] [varchar](2) NOT NULL,
	[ref] [int] NOT NULL,
	[invmdate] [char](8) NULL,
	[invhdate] [char](8) NULL,
	[custno] [nvarchar](10) NULL,
	[text] [varchar](100) NULL,
	[glser] [varchar](5) NULL,
	[dsctype] [char](1) NULL,
	[pstmode] [numeric](1, 0) NULL,
	[fcy] [char](3) NULL,
	[fcyrate] [numeric](6, 3) NULL,
	[invttl] [float] NULL,
	[invcst] [float] NULL,
	[invdspc] [float] NULL,
	[invdsvl] [float] NULL,
	[nettotal] [float] NULL,
	[invpaid] [numeric](14, 2) NULL,
	[casher] [nvarchar](16) NULL,
	[entries] [numeric](5, 0) NULL,
	[released] [datetime] NULL,
	[posted] [bit] NULL,
	[fixrate] [numeric](6, 3) NULL,
	[extamt] [numeric](14, 2) NULL,
	[extser] [char](19) NULL,
	[pricetp] [char](1) NULL,
	[ischeque] [bit] NULL,
	[chkno] [varchar](10) NULL,
	[chkdate] [char](8) NULL,
	[lastupdt] [datetime] NULL,
	[jvgenrt] [bit] NULL,
	[cccommsn] [float] NULL,
	[belowbal] [bit] NULL,
	[fcy2] [char](3) NULL,
	[ccpayment] [numeric](13, 2) NULL,
	[rplsamt] [numeric](13, 2) NULL,
	[pdother] [bit] NULL,
	[slcode] [varchar](2) NULL,
	[prpaidamt] [numeric](9, 2) NULL,
	[instldays] [numeric](2, 0) NULL,
	[instflag] [bit] NULL,
	[slcmnd] [bit] NULL,
	[inv_printed] [numeric](2, 0) NULL,
	[bendit] [bit] NULL,
	[modified] [bit] NULL,
	[rqstorder] [numeric](6, 0) NULL,
	[rqststld] [bit] NULL,
	[carrier] [varchar](3) NULL,
	[rcvdtrn] [bit] NULL,
	[usrid] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[suspend] [bit] NULL,
	[rtnref] [nvarchar](50) NULL,
	[ispurchase] [bit] NULL,
	[stkjvno] [numeric](6, 0) NULL,
	[posinv] [numeric](1, 0) NULL,
	[sanedcrd_amt] [numeric](12, 2) NULL,
	[rtncash_dfrpl] [numeric](12, 2) NULL,
	[invlocked] [bit] NULL,
	[remarks] [nvarchar](200) NULL,
	[tax_amt_rcvd] [float] NULL,
	[with_tax] [bit] NULL,
	[taxid] [varchar](50) NULL,
	[tax_percent] [float] NULL,
	[taxfree_amt] [float] NULL,
	[reref] [nvarchar](50) NULL,
	[note2] [nvarchar](200) NULL,
	[note3] [nvarchar](200) NULL,
	[inv_mpay] [char](8) NULL,
	[cust_mobil] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_salofr_dtl]    Script Date: 24-02-2025 7:25:28 PM ******/
CREATE TYPE [dbo].[tb_salofr_dtl] AS TABLE(
	[branch] [varchar](2) NOT NULL,
	[slcenter] [varchar](2) NOT NULL,
	[invtype] [varchar](2) NOT NULL,
	[ref] [int] NOT NULL,
	[invmdate] [char](8) NULL,
	[invhdate] [char](8) NULL,
	[itemno] [char](20) NULL,
	[unicode] [char](6) NULL,
	[qty] [numeric](10, 2) NULL,
	[fqty] [numeric](10, 2) NULL,
	[price] [numeric](10, 2) NULL,
	[discpc] [float] NULL,
	[cost] [float] NULL,
	[ds_acfm] [numeric](1, 0) NULL,
	[sl_acfm] [numeric](1, 0) NULL,
	[gclass] [char](12) NULL,
	[custno] [char](6) NULL,
	[fcy] [char](3) NULL,
	[barcode] [nvarchar](20) NULL,
	[imfcval] [float] NULL,
	[pack] [varchar](2) NULL,
	[pkqty] [numeric](10, 2) NULL,
	[shadd] [char](1) NULL,
	[shdpk] [numeric](8, 3) NULL,
	[shdqty] [numeric](11, 3) NULL,
	[frtqty] [numeric](9, 3) NULL,
	[rtnqty] [numeric](12, 3) NULL,
	[whno] [varchar](2) NULL,
	[cmbkey] [nvarchar](max) NULL,
	[folio] [numeric](7, 0) NULL,
	[rplct_post] [bit] NULL,
	[sold_item_status] [numeric](1, 0) NULL,
	[tax_amt] [float] NULL,
	[tax_id] [int] NULL,
	[stk_or_ser] [float] NULL,
	[tax_prcent] [float] NULL,
	[offer_amt] [float] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sto_dtl]    Script Date: 24-02-2025 7:25:28 PM ******/
CREATE TYPE [dbo].[tb_sto_dtl] AS TABLE(
	[branch] [varchar](2) NOT NULL,
	[trtype] [varchar](2) NOT NULL,
	[ref] [int] NOT NULL,
	[trmdate] [char](8) NOT NULL,
	[trhdate] [char](8) NOT NULL,
	[itemno] [nvarchar](20) NOT NULL,
	[unicode] [char](6) NULL,
	[qty] [numeric](10, 2) NULL,
	[fqty] [numeric](10, 2) NULL,
	[whno] [varchar](2) NOT NULL,
	[binno] [char](6) NULL,
	[lcost] [float] NOT NULL,
	[sysdate] [char](8) NULL,
	[src] [char](2) NULL,
	[lprice] [float] NULL,
	[fcost] [float] NULL,
	[fprice] [numeric](11, 2) NULL,
	[expdate] [char](8) NULL,
	[towhno] [varchar](2) NULL,
	[tobinno] [char](6) NULL,
	[barcode] [nvarchar](20) NULL,
	[cmbkey] [char](24) NULL,
	[discpc] [numeric](6, 2) NULL,
	[pack] [varchar](2) NULL,
	[pkqty] [numeric](10, 2) NULL,
	[shdpk] [numeric](8, 3) NULL,
	[shdqty] [numeric](14, 3) NULL,
	[folio] [numeric](7, 0) NOT NULL,
	[rplct_post] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_sto_hdr]    Script Date: 24-02-2025 7:25:28 PM ******/
CREATE TYPE [dbo].[tb_sto_hdr] AS TABLE(
	[branch] [varchar](2) NOT NULL,
	[trtype] [varchar](2) NOT NULL,
	[ref] [int] NOT NULL,
	[trmdate] [char](8) NOT NULL,
	[trhdate] [char](8) NOT NULL,
	[text] [varchar](100) NULL,
	[amnttl] [float] NOT NULL,
	[costttl] [float] NULL,
	[sysdate] [datetime] NULL,
	[src] [char](2) NULL,
	[released] [datetime] NULL,
	[posted] [bit] NOT NULL,
	[fcy] [char](3) NULL,
	[fcyrate] [numeric](9, 6) NULL,
	[whno] [varchar](2) NOT NULL,
	[entries] [numeric](6, 0) NULL,
	[lastupdt] [datetime] NULL,
	[towhno] [varchar](2) NULL,
	[modified] [bit] NULL,
	[rcvdtrn] [bit] NULL,
	[supno] [nvarchar](10) NULL,
	[usrid] [varchar](50) NULL,
	[brsupp] [nvarchar](16) NULL,
	[tobrno] [varchar](2) NULL,
	[brxref] [numeric](6, 0) NULL,
	[glref] [numeric](6, 0) NULL,
	[isbrtrx] [bit] NULL,
	[asmtype] [numeric](1, 0) NULL,
	[repost] [bit] NULL,
	[remarks] [nvarchar](200) NULL,
	[stkjvno] [numeric](6, 0) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_suppliers]    Script Date: 24-02-2025 7:25:28 PM ******/
CREATE TYPE [dbo].[tb_suppliers] AS TABLE(
	[su_no] [nvarchar](10) NOT NULL,
	[su_brno] [varchar](2) NOT NULL,
	[su_name] [varchar](100) NOT NULL,
	[su_class] [varchar](2) NOT NULL,
	[su_addrs] [varchar](50) NULL,
	[su_tel] [varchar](25) NULL,
	[su_fax] [char](10) NULL,
	[su_tlx] [char](10) NULL,
	[su_email] [varchar](30) NULL,
	[su_cntactp] [nvarchar](50) NULL,
	[su_title] [nvarchar](50) NULL,
	[su_crlmt] [numeric](14, 2) NULL,
	[su_pymnt] [numeric](3, 0) NULL,
	[su_status] [numeric](1, 0) NULL,
	[su_opnbal] [float] NULL,
	[su_curbal] [float] NULL,
	[su_cur] [varchar](2) NOT NULL,
	[su_opnfcy] [float] NULL,
	[cf_curfcy] [float] NULL,
	[su_xrf] [char](6) NULL,
	[su_alwcr] [bit] NULL,
	[su_ctlser] [char](19) NULL,
	[lastupdt] [char](8) NULL,
	[su_lcaloc] [float] NULL,
	[su_fcaloc] [float] NULL,
	[modified] [bit] NULL,
	[cmncode] [nvarchar](10) NULL,
	[su_lname] [varchar](40) NULL,
	[su_city] [varchar](2) NULL,
	[su_country] [numeric](3, 0) NULL,
	[su_laddrs] [varchar](50) NULL,
	[su_mobile] [varchar](25) NULL,
	[su_sendsms] [bit] NULL,
	[rowguid] [uniqueidentifier] NULL,
	[whno] [nvarchar](10) NULL,
	[vndr_taxcode] [varchar](50) NULL,
	[taxfree] [bit] NULL,
	[su_kind] [char](1) NULL,
	[section] [char](4) NULL,
	[inactive] [bit] NULL,
	[is_shamel_tax] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_tran_items]    Script Date: 24-02-2025 7:25:28 PM ******/
CREATE TYPE [dbo].[tb_tran_items] AS TABLE(
	[item_no] [nvarchar](20) NOT NULL,
	[item_qty] [float] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_units]    Script Date: 24-02-2025 7:25:28 PM ******/
CREATE TYPE [dbo].[tb_units] AS TABLE(
	[unit_id] [int] NOT NULL,
	[unit_name] [nvarchar](50) NOT NULL,
	[unit_desc] [nvarchar](100) NULL,
	[unit_order] [int] NULL,
	[unit_qty] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_whbins]    Script Date: 24-02-2025 7:25:28 PM ******/
CREATE TYPE [dbo].[tb_whbins] AS TABLE(
	[br_no] [varchar](2) NOT NULL,
	[item_no] [nvarchar](20) NOT NULL,
	[unicode] [varchar](6) NOT NULL,
	[wh_no] [varchar](2) NOT NULL,
	[bin_no] [nvarchar](16) NOT NULL,
	[qty] [numeric](10, 2) NOT NULL,
	[rsvqty] [numeric](10, 2) NOT NULL,
	[openbal] [numeric](10, 2) NOT NULL,
	[lcost] [float] NOT NULL,
	[fcost] [float] NOT NULL,
	[openlcost] [float] NOT NULL,
	[openfcost] [float] NOT NULL,
	[expdate] [char](8) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tb_zttk]    Script Date: 24-02-2025 7:25:28 PM ******/
CREATE TYPE [dbo].[tb_zttk] AS TABLE(
	[a] [nvarchar](20) NULL,
	[b] [float] NULL,
	[c] [float] NULL,
	[d] [nvarchar](20) NULL,
	[e] [float] NULL,
	[t] [nvarchar](255) NULL,
	[s] [float] NULL,
	[l] [nvarchar](6) NULL,
	[shdqty] [float] NULL,
	[shdpk] [float] NULL,
	[frtqty] [float] NULL,
	[mqty] [float] NULL,
	[whno] [nvarchar](2) NULL,
	[qtyfound] [bit] NULL,
	[colorit] [bit] NULL,
	[eraseit] [bit] NULL,
	[unit] [nvarchar](20) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tblpos_dtl]    Script Date: 24-02-2025 7:25:28 PM ******/
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
	[itemno] [nvarchar](20) NOT NULL,
	[srno] [int] NULL,
	[pkqty] [float] NULL,
	[discpc] [float] NULL,
	[tax_id] [int] NULL,
	[tax_amt] [float] NULL,
	[rqty] [float] NULL,
	[offr_amt] [float] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tblpos_hdr]    Script Date: 24-02-2025 7:25:28 PM ******/
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
/****** Object:  StoredProcedure [dbo].[beld_acc_balances_all]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[beld_acc_balances_all]

@cur char(3)=''

AS
BEGIN
	declare @Cursor_name Cursor , @dp_key nvarchar(50) ,@TEMP  float
	    -- set @cur=''
         set @Cursor_name = Cursor for
         Select  a_key From accounts where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @dp_key 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

--     insert into stbrprice    select @dp_brno ,'' ,s1.itemno , s1.unicode , s1.lprice1 , s1.lprice2 , s1.lprice3 , s1.maxdisc1 , s1.maxdisc2 , s1.maxdisc3 , s1.mnmprice , s1.fprice1 
--, s1.fprice2 , s1.fprice3 , s1.modelno , 0 , '' , '' , s1.maxdisc1 ,
--0 , 0 , 0 , s1.modified , s1.lastupdt  from stunits s1 , stitems s2
-- where s1.itemno = s2.itemno 
-- and not exists(select itemno from stbrprice s4 where s4.itemno = s1.itemno and s4.company = @dp_brno)

--update accounts set a_curbal= (select round(a.a_opnbal +( isnull(sum(b.a_damt-b.a_camt),0)),2) from acc_dtl b , accounts a where a.a_key=b.a_key and a.glcurrency='' and a.a_key= @dp_key)
                           --  from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key 
                         --   + " and a_brno='" + BL.CLS_Session.brno + "' and a.a_key like '" + textBox3.Text + "%'"
                         --   + " order by a.a_key;";
  set @TEMP=(select round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key),2)
                             from accounts a where a.a_key = @dp_key);
   --set @TEMP=(select isnull(a.a_opnbal + sum(b.a_damt-b.a_camt),0) from accounts a join acc_dtl b on a.a_key=b.a_key
   --                           where a.a_key = @dp_key GROUP BY a.a_key,a.a_opnbal);
  update accounts set a_curbal=@TEMP where a_key=@dp_key
          fetch next
          From @Cursor_name into @dp_key 

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
END








GO
/****** Object:  StoredProcedure [dbo].[beld_acc_balances_all_opnbal]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[beld_acc_balances_all_opnbal]

@cur char(3)=''

AS
BEGIN
	declare @Cursor_name Cursor , @dp_key nvarchar(50) ,@TEMP  float
	    -- set @cur=''
         set @Cursor_name = Cursor for
         Select  a_key From accounts where len(a_key)=6 --and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @dp_key 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

--     insert into stbrprice    select @dp_brno ,'' ,s1.itemno , s1.unicode , s1.lprice1 , s1.lprice2 , s1.lprice3 , s1.maxdisc1 , s1.maxdisc2 , s1.maxdisc3 , s1.mnmprice , s1.fprice1 
--, s1.fprice2 , s1.fprice3 , s1.modelno , 0 , '' , '' , s1.maxdisc1 ,
--0 , 0 , 0 , s1.modified , s1.lastupdt  from stunits s1 , stitems s2
-- where s1.itemno = s2.itemno 
-- and not exists(select itemno from stbrprice s4 where s4.itemno = s1.itemno and s4.company = @dp_brno)

--update accounts set a_curbal= (select round(a.a_opnbal +( isnull(sum(b.a_damt-b.a_camt),0)),2) from acc_dtl b , accounts a where a.a_key=b.a_key and a.glcurrency='' and a.a_key= @dp_key)
                           --  from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key 
                         --   + " and a_brno='" + BL.CLS_Session.brno + "' and a.a_key like '" + textBox3.Text + "%'"
                         --   + " order by a.a_key;";
  set @TEMP=(select isnull(sum(a_opnbal),0) from accounts  where len(a_key) = 9 and a_key like CONCAT(@dp_key, '%'));
   --set @TEMP=(select isnull(a.a_opnbal + sum(b.a_damt-b.a_camt),0) from accounts a join acc_dtl b on a.a_key=b.a_key
   --                           where a.a_key = @dp_key GROUP BY a.a_key,a.a_opnbal);
  update accounts set a_opnbal=@TEMP where a_key=@dp_key -- like CONCAT(@dp_key, '%')
          fetch next
          From @Cursor_name into @dp_key 

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
-------------------------------------------------------------------------------------------------------------------------
		 declare @Cursor_name2 Cursor , @dp_key2 nvarchar(50) ,@TEMP2  float
	    -- set @cur=''
         set @Cursor_name2 = Cursor for
         Select  a_key From accounts where len(a_key)=4 --and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @dp_key2
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

--     insert into stbrprice    select @dp_brno ,'' ,s1.itemno , s1.unicode , s1.lprice1 , s1.lprice2 , s1.lprice3 , s1.maxdisc1 , s1.maxdisc2 , s1.maxdisc3 , s1.mnmprice , s1.fprice1 
--, s1.fprice2 , s1.fprice3 , s1.modelno , 0 , '' , '' , s1.maxdisc1 ,
--0 , 0 , 0 , s1.modified , s1.lastupdt  from stunits s1 , stitems s2
-- where s1.itemno = s2.itemno 
-- and not exists(select itemno from stbrprice s4 where s4.itemno = s1.itemno and s4.company = @dp_brno)

--update accounts set a_curbal= (select round(a.a_opnbal +( isnull(sum(b.a_damt-b.a_camt),0)),2) from acc_dtl b , accounts a where a.a_key=b.a_key and a.glcurrency='' and a.a_key= @dp_key)
                           --  from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key 
                         --   + " and a_brno='" + BL.CLS_Session.brno + "' and a.a_key like '" + textBox3.Text + "%'"
                         --   + " order by a.a_key;";
  ------set @TEMP=(select round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key),2)
  ------                           from accounts a where a.a_key = @dp_key);
  ------ --set @TEMP=(select isnull(a.a_opnbal + sum(b.a_damt-b.a_camt),0) from accounts a join acc_dtl b on a.a_key=b.a_key
  ------ --                           where a.a_key = @dp_key GROUP BY a.a_key,a.a_opnbal);
  ------update accounts set a_curbal=@TEMP where a_key=@dp_key
  set @TEMP2=(select isnull(sum(a_opnbal),0) from accounts  where len(a_key) = 6 and a_key like CONCAT(@dp_key2, '%'));
   --set @TEMP=(select isnull(a.a_opnbal + sum(b.a_damt-b.a_camt),0) from accounts a join acc_dtl b on a.a_key=b.a_key
   --                           where a.a_key = @dp_key GROUP BY a.a_key,a.a_opnbal);
  update accounts set a_opnbal=@TEMP2 where a_key=@dp_key2  --where len(a_key) =6 and a_key like CONCAT(@dp_key2, '%')
          fetch next
          From @Cursor_name2 into @dp_key2 

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2
--------------------------------------------------------------------------------------------------------------------------------------------
		 declare @Cursor_name3 Cursor , @dp_key3 nvarchar(50) ,@TEMP3  float
	    -- set @cur=''
         set @Cursor_name3 = Cursor for
         Select  a_key From accounts where len(a_key)=2 --and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name3
--step2: Fetch Cursor
         fetch next
         From @Cursor_name3 into @dp_key3
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

--     insert into stbrprice    select @dp_brno ,'' ,s1.itemno , s1.unicode , s1.lprice1 , s1.lprice2 , s1.lprice3 , s1.maxdisc1 , s1.maxdisc2 , s1.maxdisc3 , s1.mnmprice , s1.fprice1 
--, s1.fprice2 , s1.fprice3 , s1.modelno , 0 , '' , '' , s1.maxdisc1 ,
--0 , 0 , 0 , s1.modified , s1.lastupdt  from stunits s1 , stitems s2
-- where s1.itemno = s2.itemno 
-- and not exists(select itemno from stbrprice s4 where s4.itemno = s1.itemno and s4.company = @dp_brno)

--update accounts set a_curbal= (select round(a.a_opnbal +( isnull(sum(b.a_damt-b.a_camt),0)),2) from acc_dtl b , accounts a where a.a_key=b.a_key and a.glcurrency='' and a.a_key= @dp_key)
                           --  from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key 
                         --   + " and a_brno='" + BL.CLS_Session.brno + "' and a.a_key like '" + textBox3.Text + "%'"
                         --   + " order by a.a_key;";
  ------set @TEMP=(select round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key),2)
  ------                           from accounts a where a.a_key = @dp_key);
  ------ --set @TEMP=(select isnull(a.a_opnbal + sum(b.a_damt-b.a_camt),0) from accounts a join acc_dtl b on a.a_key=b.a_key
  ------ --                           where a.a_key = @dp_key GROUP BY a.a_key,a.a_opnbal);
  ------update accounts set a_curbal=@TEMP where a_key=@dp_key
  set @TEMP3=(select isnull(sum(a_opnbal),0) from accounts  where len(a_key) = 4 and a_key like CONCAT(@dp_key3, '%'));
   --set @TEMP=(select isnull(a.a_opnbal + sum(b.a_damt-b.a_camt),0) from accounts a join acc_dtl b on a.a_key=b.a_key
   --                           where a.a_key = @dp_key GROUP BY a.a_key,a.a_opnbal);
  update accounts set a_opnbal=@TEMP3 where a_key=@dp_key3 --where len(a_key) =2 and a_key like CONCAT(@dp_key3, '%')
          fetch next
          From @Cursor_name3 into @dp_key3

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name3
--step5: Deallocate Cursor 
         deallocate @Cursor_name3
END

--select * from accounts  where len(a_key) = 9 and a_key like CONCAT('03', '%')


--select isnull(round(sum(a_opnbal),2),0) from accounts  where len(a_key) = 9 and a_key like CONCAT('010205', '%')



GO
/****** Object:  StoredProcedure [dbo].[beld_acc_cu_class_opnbal]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
CREATE PROCEDURE [dbo].[beld_acc_cu_class_opnbal]
	@cust_class varchar(2),
	@branch_no varchar(2)
AS
BEGIN
declare @obnbal  float,@acc_key nvarchar(50)

set	 @obnbal =(select round(isnull(sum(cu_opnbal),0),2) from customers where cu_class=@cust_class and cu_brno=@branch_no);

set @acc_key = (select cl_acc from cuclass where cl_brno=@branch_no and cl_no=@cust_class);

update accounts set a_opnbal=@obnbal where a_key=@acc_key


END








GO
/****** Object:  StoredProcedure [dbo].[beld_acc_su_class_opnbal]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
CREATE PROCEDURE [dbo].[beld_acc_su_class_opnbal]
	@sup_class varchar(2),
	@branch_no varchar(2)
AS
BEGIN
declare @obnbal  float,@acc_key nvarchar(50)

set	 @obnbal =(select round(isnull(sum(su_opnbal),0),2) from suppliers where su_class=@sup_class and su_brno=@branch_no);

set @acc_key = (select cl_acc from suclass where cl_brno=@branch_no and cl_no=@sup_class);

update accounts set a_opnbal=@obnbal where a_key=@acc_key


END








GO
/****** Object:  StoredProcedure [dbo].[beld_cust_balances]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[beld_cust_balances]

@cur char(3)='',
@branch_no varchar(2)

AS
BEGIN
	declare @Cursor_name Cursor , @dp_cuno nvarchar(50) ,@TEMP  float
	    -- set @cur=''
         set @Cursor_name = Cursor for
         Select  cu_no From customers where cf_fcy=@cur and cu_brno=@branch_no
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @dp_cuno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

--     insert into stbrprice    select @dp_brno ,'' ,s1.itemno , s1.unicode , s1.lprice1 , s1.lprice2 , s1.lprice3 , s1.maxdisc1 , s1.maxdisc2 , s1.maxdisc3 , s1.mnmprice , s1.fprice1 
--, s1.fprice2 , s1.fprice3 , s1.modelno , 0 , '' , '' , s1.maxdisc1 ,
--0 , 0 , 0 , s1.modified , s1.lastupdt  from stunits s1 , stitems s2
-- where s1.itemno = s2.itemno 
-- and not exists(select itemno from stbrprice s4 where s4.itemno = s1.itemno and s4.company = @dp_brno)

--update accounts set a_curbal= (select round(a.a_opnbal +( isnull(sum(b.a_damt-b.a_camt),0)),2) from acc_dtl b , accounts a where a.a_key=b.a_key and a.glcurrency='' and a.a_key= @dp_key)
                           --  from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key 
                         --   + " and a_brno='" + BL.CLS_Session.brno + "' and a.a_key like '" + textBox3.Text + "%'"
                         --   + " order by a.a_key;";
  set @TEMP=(select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and a.cu_brno=@branch_no),2)
                             from customers a where a.cu_no = @dp_cuno and a.cu_brno=@branch_no);

							  --select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and a.cu_brno=@branch_no),2)
         --                    from customers a where a.cu_no = @cust_no

  update customers set cu_curbal=@TEMP where cu_no=@dp_cuno and cu_brno=@branch_no
          fetch next
          From @Cursor_name into @dp_cuno 

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
END








GO
/****** Object:  StoredProcedure [dbo].[beld_cust_balances_all_br]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[beld_cust_balances_all_br]

@cur char(3)=''


AS
BEGIN

   -- DECLARE @branch_no varchar(2);

    DECLARE @Cur_br Cursor,@branch_no varchar(2);

	set @Cur_br= CURSOR FOR SELECT br_no From branchs;
    OPEN @Cur_br
    FETCH NEXT FROM @Cur_br INTO @branch_no;

    WHILE @@FETCH_STATUS = 0

    BEGIN
	declare @Cursor_name Cursor , @dp_cuno nvarchar(50) ,@TEMP  float
	    -- set @cur=''
         set @Cursor_name = Cursor for
         Select  cu_no From customers where cf_fcy=@cur and cu_brno=@branch_no
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @dp_cuno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

--     insert into stbrprice    select @dp_brno ,'' ,s1.itemno , s1.unicode , s1.lprice1 , s1.lprice2 , s1.lprice3 , s1.maxdisc1 , s1.maxdisc2 , s1.maxdisc3 , s1.mnmprice , s1.fprice1 
--, s1.fprice2 , s1.fprice3 , s1.modelno , 0 , '' , '' , s1.maxdisc1 ,
--0 , 0 , 0 , s1.modified , s1.lastupdt  from stunits s1 , stitems s2
-- where s1.itemno = s2.itemno 
-- and not exists(select itemno from stbrprice s4 where s4.itemno = s1.itemno and s4.company = @dp_brno)

--update accounts set a_curbal= (select round(a.a_opnbal +( isnull(sum(b.a_damt-b.a_camt),0)),2) from acc_dtl b , accounts a where a.a_key=b.a_key and a.glcurrency='' and a.a_key= @dp_key)
                           --  from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key 
                         --   + " and a_brno='" + BL.CLS_Session.brno + "' and a.a_key like '" + textBox3.Text + "%'"
                         --   + " order by a.a_key;";
  set @TEMP=(select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and a.cu_brno=@branch_no and b.a_type not in ('04','14')),2)
                             from customers a where a.cu_no = @dp_cuno and a.cu_brno=@branch_no);

							  --select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and a.cu_brno=@branch_no),2)
         --                    from customers a where a.cu_no = @cust_no

  update customers set cu_curbal=@TEMP where cu_no=@dp_cuno and cu_brno=@branch_no
          fetch next
          From @Cursor_name into @dp_cuno 

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name

     FETCH NEXT FROM @Cur_br INTO @branch_no;

     END;
     CLOSE @Cur_br;
     DEALLOCATE @Cur_br;
END;






GO
/****** Object:  StoredProcedure [dbo].[beld_sup_balances]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[beld_sup_balances]

@cur char(3)='',
@branch_no varchar(2)

AS
BEGIN
	declare @Cursor_name Cursor , @dp_suno nvarchar(50) ,@TEMP  float
	    -- set @cur=''
         set @Cursor_name = Cursor for
         Select  su_no From suppliers where su_cur=@cur and su_brno=@branch_no
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @dp_suno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

--     insert into stbrprice    select @dp_brno ,'' ,s1.itemno , s1.unicode , s1.lprice1 , s1.lprice2 , s1.lprice3 , s1.maxdisc1 , s1.maxdisc2 , s1.maxdisc3 , s1.mnmprice , s1.fprice1 
--, s1.fprice2 , s1.fprice3 , s1.modelno , 0 , '' , '' , s1.maxdisc1 ,
--0 , 0 , 0 , s1.modified , s1.lastupdt  from stunits s1 , stitems s2
-- where s1.itemno = s2.itemno 
-- and not exists(select itemno from stbrprice s4 where s4.itemno = s1.itemno and s4.company = @dp_brno)

--update accounts set a_curbal= (select round(a.a_opnbal +( isnull(sum(b.a_damt-b.a_camt),0)),2) from acc_dtl b , accounts a where a.a_key=b.a_key and a.glcurrency='' and a.a_key= @dp_key)
                           --  from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key 
                         --   + " and a_brno='" + BL.CLS_Session.brno + "' and a.a_key like '" + textBox3.Text + "%'"
                         --   + " order by a.a_key;";
  set @TEMP=(select round(a.su_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.su_no=b.su_no and a.su_brno=@branch_no),2)
                             from suppliers a where a.su_no = @dp_suno and a.su_brno=@branch_no);

							  --select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.su_no=b.su_no and a.cu_brno=@branch_no),2)
         --                    from customers a where a.su_no = @cust_no

  update suppliers set su_curbal=@TEMP where su_no=@dp_suno and su_brno=@branch_no
          fetch next
          From @Cursor_name into @dp_suno 

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
END








GO
/****** Object:  StoredProcedure [dbo].[beld_sup_balances_all_br]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[beld_sup_balances_all_br]

@cur char(3)=''

AS
BEGIN

    DECLARE @branch_no varchar(2);

    DECLARE Cur_br CURSOR FOR SELECT br_no From branchs;
    OPEN Cur_br
    FETCH NEXT FROM Cur_br INTO @branch_no;

    WHILE @@FETCH_STATUS = 0

    BEGIN
	declare @Cursor_name Cursor , @dp_suno nvarchar(50) ,@TEMP  float ,@TEMPF float
	    -- set @cur=''
         set @Cursor_name = Cursor for
      --   Select  su_no From suppliers where su_cur=@cur and su_brno=@branch_no
	   Select  su_no From suppliers where su_brno=@branch_no
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @dp_suno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
             begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

--     insert into stbrprice    select @dp_brno ,'' ,s1.itemno , s1.unicode , s1.lprice1 , s1.lprice2 , s1.lprice3 , s1.maxdisc1 , s1.maxdisc2 , s1.maxdisc3 , s1.mnmprice , s1.fprice1 
--, s1.fprice2 , s1.fprice3 , s1.modelno , 0 , '' , '' , s1.maxdisc1 ,
--0 , 0 , 0 , s1.modified , s1.lastupdt  from stunits s1 , stitems s2
-- where s1.itemno = s2.itemno 
-- and not exists(select itemno from stbrprice s4 where s4.itemno = s1.itemno and s4.company = @dp_brno)

--update accounts set a_curbal= (select round(a.a_opnbal +( isnull(sum(b.a_damt-b.a_camt),0)),2) from acc_dtl b , accounts a where a.a_key=b.a_key and a.glcurrency='' and a.a_key= @dp_key)
                           --  from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.a_key=b.a_key 
                         --   + " and a_brno='" + BL.CLS_Session.brno + "' and a.a_key like '" + textBox3.Text + "%'"
                         --   + " order by a.a_key;";
  set @TEMP=(select round(a.su_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.su_no=b.su_no and a.su_brno=@branch_no and b.a_type not in ('06','16')),2)
                             from suppliers a where a.su_no = @dp_suno and a.su_brno=@branch_no);
  set @TEMPF=(select round(a.su_opnfcy +(select isnull(sum((case b.jddbcr when 'D' then b.jdfcamt else - b.jdfcamt end)),0) from acc_dtl b where a.su_no=b.su_no and a.su_brno=@branch_no and b.a_type not in ('06','16')),2)
                             from suppliers a where a.su_no = @dp_suno and a.su_brno=@branch_no);
							  --select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.su_no=b.su_no and a.cu_brno=@branch_no),2)
         --                    from customers a where a.su_no = @cust_no

  update suppliers set su_curbal=@TEMP, cf_curfcy=@TEMPF where su_no=@dp_suno and su_brno=@branch_no
          fetch next
          From @Cursor_name into @dp_suno 

		 -- print @TEMP
             end;
--step4: Close cursor
             close @Cursor_name
--step5: Deallocate Cursor 
             deallocate @Cursor_name

     FETCH NEXT FROM Cur_br INTO @branch_no;

     END;
     CLOSE Cur_br;
     DEALLOCATE Cur_br;

END;







GO
/****** Object:  StoredProcedure [dbo].[bld_brprices_all]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_brprices_all]

@br_no varchar(2),
@br_sl_pr int
--@br_no varchar(2),
--@wh_no varchar(2)
--@posted int=1
AS
BEGIN
	
  UPDATE t1 SET 
         t1.item_price = t2.lprice1
  FROM   items t1 
         INNER JOIN brprices t2 
                   ON t1.item_no = t2.itemno and t1.item_price <> t2.lprice1
  WHERE  iif(@br_sl_pr=1,t2.branch,t2.slcenter)=@br_no ; 
  ---------------------------------------------------------------------------------
  UPDATE t1 SET 
         t1.price = t2.lprice1
  FROM   items_bc t1 
         INNER JOIN brprices t2 
                   ON t1.item_no = t2.itemno and t1.price <> t2.lprice1
  WHERE  iif(@br_sl_pr=1,t2.branch,t2.slcenter)=@br_no ; 

END







GO
/****** Object:  StoredProcedure [dbo].[bld_items_bc_from_items]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_items_bc_from_items]

AS
BEGIN
	
merge items_bc as t
 using (SELECT [item_no] item_no,[item_barcode] barcode,[item_unit] pack,1 pk_qty,item_price price,'' note,1 pkorder, price2, price3, min_price FROM items) as s
                ON t.barcode=s.barcode and t.item_no=s.item_no
                WHEN MATCHED THEN
                UPDATE SET t.price = s.price
                WHEN NOT MATCHED THEN
                 -- +' INSERT VALUES(s.item_no, s.barcode, s.pack, s.pk_qty, s.price, s.note, s.pkorder)'
                INSERT VALUES(s.[item_no],s.[barcode],s.[pack],s.[pk_qty],s.[price],s.[note],s.[pkorder],s.price2, s.price3, s.min_price,'01','01');

END







GO
/****** Object:  StoredProcedure [dbo].[bld_qty_convert_cost_all]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_qty_convert_cost_all]

@br_no varchar(2),
--@br_no varchar(2),
--@wh_no varchar(2)
@posted int=1
AS
BEGIN
	
  UPDATE t1 SET 
         t1.lcost = round((t2.item_curcost * t1.pkqty),4),t1.lprice = round((t2.item_curcost * t1.pkqty),4)
  FROM   sto_dtl t1 
         INNER JOIN items t2 
                   ON t1.itemno = t2.item_no and t1.trtype in('33') ;
  


UPDATE t1
SET t1.costttl = t2.cost,t1.amnttl = t2.cost
FROM sto_hdr t1
INNER JOIN
(
  SELECT branch,trtype,ref, round(sum(qty * lcost),4) cost
  FROM sto_dtl
 -- GROUP BY  ID, CCY 
 GROUP BY  branch,trtype,ref
) t2  ON t1.branch = t2.branch and t1.trtype=t2.trtype and t1.ref=t2.ref ;

END







GO
/****** Object:  StoredProcedure [dbo].[bld_sal_cost_all]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_sal_cost_all]

@br_no varchar(2),
--@br_no varchar(2),
--@wh_no varchar(2)
@posted int=1
AS
BEGIN
	
  UPDATE t1 SET 
         t1.cost = t2.item_curcost
  FROM   sales_dtl t1 
         INNER JOIN items t2 
                   ON t1.itemno = t2.item_no
  --WHERE  t2.item_curcost<>t1.cost and t1.branch=@br_no;


 --UPDATE t1 SET 
 --        t1.invcst = sum(t2.qty * t2.pkqty * t2.cost)
 -- FROM   sales_hdr t1 
 --        INNER JOIN sales_dtl t2 
 --                  ON t1.branch = t2.branch and t1.ref=t2.ref and t1.invtype=t2.invtype and t1.slcenter=t2.slcenter;
 ---- WHERE  t2.item_curcost<>t1.cost and t1.branch=@br_no;


 -- UPDATE sales_hdr SET 
 --        sales_hdr.invcst = sum(sales_dtl.qty * sales_dtl.pkqty * sales_dtl.cost)
 -- FROM   sales_hdr,sales_dtl
 --       where  sales_hdr.branch = sales_dtl.branch and sales_hdr.ref=sales_dtl.ref and sales_hdr.invtype=sales_dtl.invtype and sales_hdr.slcenter=sales_dtl.slcenter;



UPDATE t1
SET t1.invcst = t2.cost
FROM sales_hdr t1
INNER JOIN
(
  SELECT branch,slcenter,invtype,ref, round(sum(qty * pkqty * cost),2) cost
  FROM sales_dtl
 -- GROUP BY  ID, CCY 
 GROUP BY  branch,slcenter,invtype,ref
) t2  ON t1.branch = t2.branch and t1.slcenter=t2.slcenter and t1.invtype=t2.invtype and t1.ref=t2.ref  ;

END







GO
/****** Object:  StoredProcedure [dbo].[bld_taxfree_sl_pu]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_taxfree_sl_pu]

@br_no varchar(2),
--@br_no varchar(2),
--@wh_no varchar(2)
@posted int=1
AS
BEGIN
	


UPDATE t1
--SET t1.taxfree_amt =(case when t1.with_tax=0 then t2.taxfree - (t2.taxfree * (t1.invdspc/100 )) else ((t2.taxfree - (t2.taxfree*3/23)) - ((t2.taxfree - (t2.taxfree*3/23)) * (t1.invdspc/100 ))) end)
--SET t1.taxfree_amt =round ((case when t1.invdsvl<>0 then t2.taxfree - (t2.taxfree * ((t1.invdsvl * 100/(case when with_tax=1 then (t1.invttl - (t1.invttl*3/23)) else t1.invttl  end))/100 )) else t2.taxfree - (t2.taxfree * (t1.invdspc/100 )) end),2)
SET t1.taxfree_amt =case when t1.with_tax=0 then round ((case when t1.invdspc <>0 then (t2.taxfree - (t2.taxfree * t1.invdspc/100)) else (t2.taxfree - (t2.taxfree * (t1.invdsvl*100/t1.invttl)/100)) end),2 ) else round ( (case when t1.invdspc <>0 then (t2.taxfree - t2.taxamt) - ((t2.taxfree - t2.taxamt) * t1.invdspc/100) else (t2.taxfree - t2.taxamt) - ((t2.taxfree - t2.taxamt) * (t1.invdsvl*100/t1.invttl)/100) end),2 ) end
FROM sales_hdr t1
INNER JOIN
(
  SELECT branch,slcenter,invtype,ref, round(sum(case when tax_amt=0 then qty*pkqty* price  else 0 end),2) taxfree, round(sum(case when tax_amt=0 then 0  else tax_amt end),2) taxamt
  FROM sales_dtl --where tax_amt=0
 -- GROUP BY  ID, CCY 
 GROUP BY  branch,slcenter,invtype,ref
) t2  ON t1.branch = t2.branch and t1.slcenter=t2.slcenter and t1.invtype=t2.invtype and t1.ref=t2.ref ;

select taxfree_amt from sales_hdr
--taxfree_amt
--485.36
--878.31

------------------------pos_hdr bld

----UPDATE t1
----SET t1.tax_amt =t2.taxamt
----FROM pos_hdr t1
----INNER JOIN
----(
----  SELECT brno,slno,contr,ref,  round(sum(case when tax_amt=0 then 0  else tax_amt end),2) taxamt
----  FROM pos_dtl --where tax_amt=0
---- -- GROUP BY  ID, CCY 
---- GROUP BY  brno,slno,contr,ref
----) t2  ON t1.brno = t2.brno and t1.slno=t2.slno and t1.contr=t2.contr and t1.ref=t2.ref where t1.ref=80;


END









GO
/****** Object:  StoredProcedure [dbo].[bld_whbins]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[bld_whbins]
AS
BEGIN

--merge whbins as t
--using(select a.item_no,a.item_cost,(a.item_obalance - isnull(sum(b.qty),0)) as qty  from items a left join sales_dtl b
--      on a.item_no=b.itemno
--	 group by a.item_no,a.item_cost,a.item_obalance) as s
--	 ON t.item_no = s.item_no
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_cost
--	 WHEN NOT MATCHED then
--	 insert values('01',s.item_no,'01',s.qty,0,0,s.item_cost,0,'');












	  --select a.item_no,a.item_cost,a.item_obalance + (select  isnull(sum(c.qty),0) from recv_dtl c where a.item_no=c.itemn_no) - (select  isnull(sum(b.qty),0) from sales_dtl b where a.item_no=b.itemno) as qty  from items a 
	  --group by a.item_no,a.item_cost,a.item_obalance









select getdate();

END;


GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_whbins_cost_all]

--@br_no varchar(2),
--@wh_no varchar(2)
@posted int=1
AS
BEGIN

DECLARE @bld_temp TABLE
(
 branch varchar(2),trtype varchar(2),ref int ,itemno varchar(16),qty float,pkqty float , cost float ,whno varchar(2),towhno varchar(2),expdate varchar(8)
);

--IF EXISTS(SELECT TABLE_NAME  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME  = '@bld_temp') BEGIN
--   DROP TABLE @bld_temp;
--END;

--Select branch,trtype,ref,itemno,qty,pkqty,lprice cost,whno,towhno,expdate into @bld_temp from sto_dtl;
insert into @bld_temp Select branch,trtype,ref,itemno,qty,pkqty,lprice cost,whno,towhno,expdate from sto_dtl union all select branch,invtype,ref,itemno,qty,pkqty,cost cost,whno,'' towhno,'' expdate from sales_dtl
union all select branch,invtype,ref,itemno,qty,pkqty,iif(fullcost>0,fullcost, price/pkqty) cost,whno,'' towhno,expdate from pu_dtl;

--select * from @bld_temp where itemno='02-006';




update items set item_curcost= 0 , item_cbalance=0,item_obalance=0,item_opencost=0;--where br_no= @brno and wh_no=@whno;

	declare @Cursor_name Cursor , @brno nvarchar(2) ,@whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_name = Cursor LOCAL FAST_FORWARD for
         Select  wh_brno,wh_no From whouses -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @brno,@whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
update whbins set qty= 0,lcost=0 where br_no= @brno and wh_no=@whno;
merge whbins as t
using(select a.item_no,(case when ((select  isnull(count(*),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('36')))=0 then ( ( (case when  (
 (select  isnull(sum(c.qty*c.pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31','45','33','46'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )<>0 then

  ((( select  isnull(sum(c.qty*pkqty*cost),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty*cost),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31','45','33','46'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno='' and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty*lcost),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )/

  ( (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31','45','33','46'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno='' and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  ))  when (select  isnull(avg(cost),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno )> 0 then (select  isnull(avg(cost),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno )
  else (select  isnull(avg(cost),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno ) end

  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) 
  ))) else (select  top 1 isnull(b.cost,0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('36') group by b.ref,b.cost order by b.ref desc) end) as lcost,( 0
  + (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
 -- - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('46'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty,(select  isnull(expdate,'') from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno ) expdate  from items a 
     where
	 ( (select  count(*) from @bld_temp c where a.item_no=c.itemno  and c.branch=@brno ) + 
	   (select  count(*) from @bld_temp b where a.item_no=b.itemno  and b.branch=@brno) + 
	   (select  count(*) from @bld_temp d where a.item_no=d.itemno and d.branch=@brno) > 0
  --+ (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
  --- (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  --+ (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) )<>0
 ------ ((select isnull(sum(d.openbal),0) from whbins d where a.item_no=d.item_no)
 ------ + (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
 -------- - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

 -------- - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
 -------- + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

 ------ + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
 ------ - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
 ------ )<>0
 ------ and
 /*
  ((select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
   - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) )<>0
  */
  )

  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) 

	 ) as s
	 ON t.item_no = s.item_no and t.wh_no = @whno and t.br_no=@brno--and s.qty<>0
	-- ON t.item_no = s.item_no and t.wh_no=@whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=s.qty,t.lcost=s.lcost 
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.lcost,0,0,0,'');
     --WHEN NOT MATCHED BY SOURCE THEN 
     --DELETE;
     --WHEN NOT MATCHED BY SOURCE
     --THEN DELETE;


	   fetch next
          From @Cursor_name into @brno,@whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------

 declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float,@osumqty float, @oevrcost float
	    -- set @cur=''
         set @Cursor_name2 = Cursor LOCAL FAST_FORWARD for
         Select  item_no From items --a where exists(select item_no from whbins b where a.item_no=b.item_no) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
set @sumqty= (select isnull(sum(qty + openbal),0) from whbins  where item_no=@itemno);
set @evrcost=case when (select (sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<> 0 then
 (select ( sum((iif(qty>0 , qty,0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else (select isnull(avg(lcost),0) from whbins  where item_no=@itemno and qty + openbal + lcost  <> 0) end;

set @osumqty= (select isnull(sum(openbal),0) from whbins  where item_no=@itemno);
set @oevrcost=case when (select (sum(openbal)) from whbins  where item_no=@itemno and openbal <> 0)<> 0 then
 (select ( sum((openbal * openlcost)))/(sum(openbal)) from whbins  where item_no=@itemno and  openbal <> 0) else 0 end;

update items set item_cbalance = @sumqty,item_curcost=@evrcost,    item_obalance = @osumqty,item_opencost=@oevrcost,static_cost=iif(static_cost=0 , @evrcost,static_cost)  where item_no=@itemno;
update items set item_cbalance = 0,item_curcost=0,    item_obalance = 0,item_opencost=0  where item_cost=0;
--merge whbins as t
--using(select a.item_no,a.item_curcost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_curcost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_curcost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2

		

END


GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_by_br]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_whbins_cost_all_by_br]

@br_no varchar(2),
--@wh_no varchar(2)
@posted int=1
AS
BEGIN

update items set item_curcost= 0 , item_cbalance=0,item_obalance=0,item_opencost=0;--where br_no= @brno and wh_no=@whno;

	declare @Cursor_whno Cursor , @whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_whno = Cursor for
         Select wh_no From whouses where  wh_brno=@br_no -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_whno
--step2: Fetch Cursor
         fetch next
         From @Cursor_whno into @whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
update whbins set qty= 0 where br_no= @br_no and wh_no=@whno;
merge whbins as t
using(select a.item_no,( (0
  + (case when  ((select isnull(sum(d.openbal),0) from whbins d where a.item_no=d.item_no and d.br_no=@br_no)
  + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@br_no and c.invtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('31','45','46'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )<>0 then

  ((( select  isnull(sum(c.qty*pkqty*fullcost),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@br_no and c.invtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty*lcost),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('31','45','46'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno='' and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty*lcost),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )/

  (0
  + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@br_no and c.invtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('31','45','46'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno='' and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )) else 0 end

  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) 
  ))) as item_curcost,( 0
  + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@br_no and c.invtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@br_no and c.invtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@br_no and b.invtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@br_no and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@br_no and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('33'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@br_no and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('45','46'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
 -- - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('46'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('32')) ) as qty  from items a 
     where
	 ( 
  --+ (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
  --- (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

  --- (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  --+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) )<>0
 ------ ((select isnull(sum(d.openbal),0) from whbins d where a.item_no=d.item_no)
 ------ + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
 -------- - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

 -------- - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
 -------- + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

 ------ + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
 ------ - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
 ------ )<>0
 ------ and
  ((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@br_no and c.invtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@br_no and c.invtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@br_no and b.invtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@br_no and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@br_no and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('33'))
   - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@br_no and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@br_no and b.trtype in ('32')) )<>0)

  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) 

	 group by a.item_no,a.item_curcost,a.item_obalance) as s
	 ON t.item_no  = s.item_no  and t.wh_no= @whno and t.br_no=@br_no--and s.qty<>0
	-- ON t.item_no = s.item_no and t.wh_no=@whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=s.qty,t.lcost=s.item_curcost 
	 WHEN NOT MATCHED then
	 insert values(@br_no,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');
     --WHEN NOT MATCHED BY SOURCE THEN 
     --DELETE;
     --WHEN NOT MATCHED BY SOURCE
     --THEN DELETE;


	   fetch next
          From @Cursor_whno into @whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_whno
--step5: Deallocate Cursor 
         deallocate @Cursor_whno
------------------------------------------------------------------

 declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float,@osumqty float, @oevrcost float
	    -- set @cur=''
         set @Cursor_name2 = Cursor for
         Select  item_no From items --a where exists(select item_no from whbins b where a.item_no=b.item_no) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
set @sumqty= (select isnull(sum(qty + openbal),0) from whbins  where item_no=@itemno and br_no=@br_no);
set @evrcost=case when (select (sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0 and br_no=@br_no)<> 0 then
 (select ( sum((iif(qty>0 , qty,0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0 and br_no=@br_no) else (select isnull(sum(lcost),0) from whbins  where item_no=@itemno and qty + openbal + lcost <> 0 and br_no=@br_no) end;

set @osumqty= (select isnull(sum(openbal),0) from whbins  where item_no=@itemno and br_no=@br_no);
set @oevrcost=case when (select (sum(openbal)) from whbins  where item_no=@itemno and openbal <> 0 and br_no=@br_no)<> 0 then
 (select ( sum((openbal * openlcost)))/(sum(openbal)) from whbins  where item_no=@itemno and  openbal <> 0 and br_no=@br_no) else 0 end;

update items set item_cbalance = @sumqty,item_curcost=@evrcost,  item_obalance = @osumqty,item_opencost=@oevrcost  where item_no=@itemno;

--merge whbins as t
--using(select a.item_no,a.item_curcost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_curcost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_curcost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2

END













	  














GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_by_sal_tran]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[bld_whbins_cost_all_by_sal_tran]

--@br_no varchar(2),
--@wh_no varchar(2)
@items_tran_tb tb_tran_items READONLY
AS
BEGIN


--IF EXISTS(SELECT TABLE_NAME  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME  = '@bld_temp') BEGIN
--   DROP TABLE @bld_temp;
--END;
DECLARE @bld_temp TABLE
(
 branch varchar(2),trtype varchar(2),ref int ,itemno varchar(20),qty float,pkqty float , cost float ,whno varchar(2),towhno varchar(2),expdate varchar(8)
);

--Select branch,trtype,ref,itemno,qty,pkqty,lprice cost,whno,towhno into @bld_temp from sto_dtl where exists(select * from @items_tran_tb b where sto_dtl.itemno= b.item_no);
--insert into @bld_temp select branch,invtype,ref,itemno,qty,pkqty,cost cost,whno,'' towhno from sales_dtl where exists(select * from @items_tran_tb b where sales_dtl.itemno= b.item_no);
--insert into @bld_temp select branch,invtype,ref,itemno,qty,pkqty,iif(fullcost>0,fullcost, price/pkqty) cost,whno,'' towhno from pu_dtl where exists(select * from @items_tran_tb b where pu_dtl.itemno= b.item_no);

insert into @bld_temp Select branch,trtype,ref,itemno,qty,pkqty,lprice cost,whno,towhno,expdate from sto_dtl where exists(select * from @items_tran_tb b where sto_dtl.itemno= b.item_no) union all select branch,invtype,ref,itemno,qty,pkqty,cost cost,whno,'' towhno,'' expdate from sales_dtl where exists(select * from @items_tran_tb b where sales_dtl.itemno= b.item_no)
union all select branch,invtype,ref,itemno,qty,pkqty,iif(fullcost>0,fullcost, price/pkqty) cost,whno,'' towhno,expdate from pu_dtl where exists(select * from @items_tran_tb b where pu_dtl.itemno= b.item_no);


--select * from @bld_temp where itemno='02-006';




--update items set item_curcost= 0 , item_cbalance=0,item_obalance=0,item_opencost=0;--where br_no= @brno and wh_no=@whno;

	declare @Cursor_name Cursor , @brno nvarchar(2) ,@whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_name = Cursor LOCAL FAST_FORWARD for
         Select  wh_brno,wh_no From whouses -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @brno,@whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
--update whbins set qty= 0,lcost=0 where br_no= @brno and wh_no=@whno;
merge whbins as t
using(select a.item_no,0 as lcost,( 0
  + (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
 -- - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('46'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
     where
	 ( (select  count(*) from @bld_temp c where a.item_no=c.itemno  and c.branch=@brno ) + 
	   (select  count(*) from @bld_temp b where a.item_no=b.itemno  and b.branch=@brno) + 
	   (select  count(*) from @bld_temp d where a.item_no=d.itemno and d.branch=@brno) > 0
  --+ (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
  --- (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  --+ (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) )<>0
 ------ ((select isnull(sum(d.openbal),0) from whbins d where a.item_no=d.item_no)
 ------ + (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
 -------- - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

 -------- - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
 -------- + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

 ------ + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
 ------ - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
 ------ )<>0
 ------ and
 /*
  ((select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
   - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) )<>0
  */
  )

  --- (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) 

	 ) as s
	 ON t.item_no = s.item_no and t.wh_no = @whno and t.br_no=@brno--and s.qty<>0
	-- ON t.item_no = s.item_no and t.wh_no=@whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=s.qty --,t.lcost=s.lcost 
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.lcost,0,0,0,'');
     --WHEN NOT MATCHED BY SOURCE THEN 
     --DELETE;
     --WHEN NOT MATCHED BY SOURCE
     --THEN DELETE;


	   fetch next
          From @Cursor_name into @brno,@whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------

 declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float --, @evrcost float,@osumqty float, @oevrcost float
	    -- set @cur=''
         set @Cursor_name2 = Cursor LOCAL FAST_FORWARD for
         Select  item_no From items --a where exists(select item_no from whbins b where a.item_no=b.item_no) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
set @sumqty= (select isnull(sum(qty + openbal),0) from whbins  where item_no=@itemno);
--set @evrcost=case when (select (sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<> 0 then
-- (select ( sum((iif(qty>0 , qty,0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else (select isnull(sum(lcost),0) from whbins  where item_no=@itemno and qty + openbal  <> 0) end;

--set @osumqty= (select isnull(sum(openbal),0) from whbins  where item_no=@itemno);
--set @oevrcost=case when (select (sum(openbal)) from whbins  where item_no=@itemno and openbal <> 0)<> 0 then
-- (select ( sum((openbal * openlcost)))/(sum(openbal)) from whbins  where item_no=@itemno and  openbal <> 0) else 0 end;

update items set item_cbalance = @sumqty --,item_curcost=@evrcost,    item_obalance = @osumqty,item_opencost=@oevrcost  
where item_no=@itemno;
update items set item_cbalance = 0,item_curcost=0,    item_obalance = 0,item_opencost=0  where item_cost=0;
--merge whbins as t
--using(select a.item_no,a.item_curcost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_curcost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_curcost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2

		

END





GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_by_tran]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[bld_whbins_cost_all_by_tran]

--@br_no varchar(2),
--@wh_no varchar(2)
@items_tran_tb tb_tran_items READONLY
AS
BEGIN


--IF EXISTS(SELECT TABLE_NAME  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME  = '@bld_temp') BEGIN
--   DROP TABLE @bld_temp;
--END;
DECLARE @bld_temp TABLE
(
 branch varchar(2),trtype varchar(2),ref int ,itemno varchar(20),qty float,pkqty float , cost float ,whno varchar(2),towhno varchar(2),expdate varchar(8)
);

--Select branch,trtype,ref,itemno,qty,pkqty,lprice cost,whno,towhno into @bld_temp from sto_dtl where exists(select * from @items_tran_tb b where sto_dtl.itemno= b.item_no);
--insert into @bld_temp select branch,invtype,ref,itemno,qty,pkqty,cost cost,whno,'' towhno from sales_dtl where exists(select * from @items_tran_tb b where sales_dtl.itemno= b.item_no);
--insert into @bld_temp select branch,invtype,ref,itemno,qty,pkqty,iif(fullcost>0,fullcost, price/pkqty) cost,whno,'' towhno from pu_dtl where exists(select * from @items_tran_tb b where pu_dtl.itemno= b.item_no);

insert into @bld_temp Select branch,trtype,ref,itemno,qty,pkqty,(lprice/pkqty) cost,whno,towhno,expdate from sto_dtl where exists(select * from @items_tran_tb b where sto_dtl.itemno= b.item_no) union all select branch,invtype,ref,itemno,qty,pkqty,cost cost,whno,'' towhno,'' expdate from sales_dtl where exists(select * from @items_tran_tb b where sales_dtl.itemno= b.item_no)
union all select branch,invtype,ref,itemno,qty,pkqty,iif(fullcost>0,fullcost, price/pkqty) cost,whno,'' towhno,expdate from pu_dtl where exists(select * from @items_tran_tb b where pu_dtl.itemno= b.item_no);


--select * from @bld_temp where itemno='02-006';




--update items set item_curcost= 0 , item_cbalance=0,item_obalance=0,item_opencost=0;--where br_no= @brno and wh_no=@whno;

	declare @Cursor_name Cursor , @brno nvarchar(2) ,@whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_name = Cursor LOCAL FAST_FORWARD for
         Select  wh_brno,wh_no From whouses -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @brno,@whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
--update whbins set qty= 0,lcost=0 where br_no= @brno and wh_no=@whno;
merge whbins as t
using(


--select @lpkqty =(coalesce(stp.openbal,0) + coalesce(pur.puqty,0) -coalesce(sal.salqty,0) + coalesce(sto.stoqty,0) - coalesce(sto.stoqty1,0) - coalesce(sto2.stoqty2,0)) from items a
	-- left join
	select a.item_no ,--(coalesce(round((avg(fullcost)),2),0) qty*pkqty>0 and  branch=@brno and whno=@whno and itemno=@itemno) as lcost,
	
	coalesce(round((avg(NULLIF(case when trtype in ('06','07','31','45','33') and (t.whno=@whno or t.towhno=@whno ) and t.branch=@brno then cost else 0 end,0))),2),0)  as lcost,
	--isnull((sum(NULLIF(case when trtype in ('06','07','31','45','33','46') and (t.whno=@brno or t.towhno=@brno ) and t.branch=@brno then (cost*pkqty*qty) else 0 end,0))/sum(NULLIF(case when trtype in ('06','07','31','45','33','46') and (t.whno=@brno or t.towhno=@brno ) and t.branch=@brno then (pkqty*qty) else 0 end,0))),0)  as lcost,
	--coalesce((sum(case when t.trtype in ('06','07','31') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty*t.cost else 0 end))/(sum(case when t.trtype in ('06','07','31') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 1 end)),0)  as lcost,
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 (coalesce(sum(case when t.trtype in ('06','07') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty when t.trtype in ('16','17') then -t.qty*t.pkqty else 0 end),0) --as puqty 
     -- from pu_dtl t where t.whno=@whno
      
     +  coalesce(sum(case when t.trtype in ('14','15') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty when t.trtype in ('04','05') then -t.qty*t.pkqty else 0 end),0) --as salqty  
     -- from sales_dtl t where t.whno=@whno
     
	--+ coalesce(sum(case when t.trtype in ('31','45','46','35',case when trtype='45' and whno='' and towhno<>'' and t.branch=@brno then '45' else '' end,case when t.trtype in ('33') and t.towhno=@whno then '33' else '' end) and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty 
	-- - coalesce(sum(case when t.trtype in ('32',case when trtype='45' and towhno='' and whno<>'' and t.branch=@brno then '45' else '' end,case when t.trtype in ('33') and t.whno=@whno  then '33' else '' end) and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty1 
	
	+ coalesce(sum(case when t.trtype in ('31',case when trtype='45' and whno='' and towhno<>'' and t.branch=@brno then '45' else '' end,'35',case when trtype='46' and whno='' and towhno<>'' and t.branch=@brno then '46' else '' end,case when t.trtype in ('33') and t.towhno=@whno then '33' else '' end) and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty 
	 - coalesce(sum(case when t.trtype in ('32',case when trtype='45' and towhno='' and whno<>'' and t.branch=@brno then '45' else '' end,case when trtype='46' and towhno='' and whno<>'' and t.branch=@brno then '46' else '' end,case when t.trtype in ('33') and t.whno=@whno  then '33' else '' end) and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty1 


-- ,sum(case when t.trtype in ('31','33','45','46') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end) as stoqty2
	 --,sum(case when t.trtype in ('33') and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty  when t.trtype in ('33') and t.whno=@whno and t.branch=@brno then -(t.qty*t.pkqty) else 0 end) as stoqty2
	 -- from sto_dtl t where t.towhno=@whno
     
    --	- coalesce(sum(case when t.trtype in ('33') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty2
	 -- from sto_dtl t where t.whno=@whno
    
--	+ (select coalesce(sum(b.openbal),0) openbal from whbins b where b.wh_no=@whno and b.br_no=@brno and b.item_no=a.item_no)
	
	) as qty
      --group by t.item_no,t.wh_no
     --) stp
	 -- on a.item_no = stp.item_no and stp.wh_no=@whno
	 -- where a.item_no = @itemid;
	 from items a join @bld_temp t  on a.item_no=t.itemno
	 where branch=@brno and (whno=@whno or towhno=@whno)  --and itemno=a.item_no
	 group by a.item_no
--);


	 ) as s
	 ON t.item_no = s.item_no and t.wh_no = @whno and t.br_no=@brno--and s.qty<>0
	-- ON t.item_no = s.item_no and t.wh_no=@whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=s.qty,t.lcost=s.lcost 
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.lcost,0,0,0,'');
     --WHEN NOT MATCHED BY SOURCE THEN 
     --DELETE;
     --WHEN NOT MATCHED BY SOURCE
     --THEN DELETE;


	   fetch next
          From @Cursor_name into @brno,@whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------

 declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float,@osumqty float, @oevrcost float
	    -- set @cur=''
         set @Cursor_name2 = Cursor LOCAL FAST_FORWARD for
         Select  item_no From items --a where exists(select item_no from whbins b where a.item_no=b.item_no) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
set @sumqty= (select isnull(sum(qty + openbal),0) from whbins  where item_no=@itemno);
set @evrcost=case when (select (sum(iif(qty>0 , iif(lcost<>0,qty,0),0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<> 0 then
 (select ( sum((iif(qty>0 , iif(lcost<>0,qty,0),0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , iif(lcost<>0,qty,0),0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else (select isnull(avg(NULLIF(lcost,0)),0) from whbins where item_no=@itemno) end;

set @osumqty= (select isnull(sum(openbal),0) from whbins  where item_no=@itemno);
set @oevrcost=case when (select (sum(openbal)) from whbins  where item_no=@itemno and openbal <> 0)<> 0 then
 (select ( sum((openbal * openlcost)))/(sum(openbal)) from whbins  where item_no=@itemno and  openbal <> 0) else (select isnull(avg(NULLIF(openlcost,0)),0) from whbins where item_no=@itemno) end;

--update items set item_cbalance = @sumqty,item_curcost=@evrcost,    item_obalance = @osumqty,item_opencost=@oevrcost,static_cost=iif(static_cost=0 , @evrcost,static_cost)  where item_no=@itemno;
--update items set item_cbalance =iif(item_cost=0 , 0,@sumqty),item_curcost=iif(item_cost=0 , 0,iif(@evrcost=0,@oevrcost,@evrcost)),    item_obalance =iif(item_cost=0 , 0,@osumqty) ,item_opencost=iif(item_cost=0 , 0,@oevrcost),static_cost=iif((static_cost=0 or static_cost is null) , iif(@evrcost=0,@oevrcost,@evrcost),static_cost)  where item_no=@itemno;
update items set item_cbalance =iif(item_cost=0 , 0,@sumqty),item_curcost=iif(item_cost=0 , 0,iif(@evrcost=0,@oevrcost,@evrcost)),    item_obalance =iif(item_cost=0 , 0,@osumqty) ,item_opencost=iif(item_cost=0 , 0,@oevrcost)  --where item_no=@itemno;
,static_cost=iif((static_cost=0 or static_cost is null) , iif(@evrcost=0,@oevrcost,@evrcost),static_cost)  where item_no=@itemno;

update whbins set lcost=openlcost where lcost=0 and openlcost<>0 and item_no=@itemno;
--update items set item_cbalance = 0,item_curcost=0,    item_obalance = 0,item_opencost=0  where item_cost=0;
--merge whbins as t
--using(select a.item_no,a.item_curcost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_curcost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_curcost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2

		

END






GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_new]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_whbins_cost_all_new]

--@br_no varchar(2),
--@wh_no varchar(2)
@posted int=1
AS
BEGIN


IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '%#bld_temp%') BEGIN
   DROP TABLE #bld_temp;
END;

Select branch,trtype,ref,itemno,qty,pkqty,lprice cost,whno,towhno into #bld_temp from sto_dtl;
insert into #bld_temp select branch,invtype,ref,itemno,qty,pkqty,cost cost,whno,'' towhno from sales_dtl;
insert into #bld_temp select branch,invtype,ref,itemno,qty,pkqty,fullcost cost,whno,'' towhno from pu_dtl;

--select * from #bld_temp;




update items set item_curcost= 0 , item_cbalance=0,item_obalance=0,item_opencost=0;--where br_no= @brno and wh_no=@whno;

	declare @Cursor_name Cursor , @brno nvarchar(2) ,@whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_name = Cursor LOCAL FAST_FORWARD for
         Select  wh_brno,wh_no From whouses -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @brno,@whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
update whbins set qty= 0,lcost=0 where br_no= @brno and wh_no=@whno;
merge whbins as t
using(select a.item_no,(case when ((select  isnull(count(*),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('36')))=0 then ( ( (case when  (
 (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31','45','33','46'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )<>0 then

  ((( select  isnull(sum(c.qty*pkqty*cost),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty*cost),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31','45','33','46'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno='' and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty*lcost),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )/

  ( (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31','45','33','46'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno='' and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  ))  when (select  isnull(avg(cost),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno )> 0 then (select  isnull(avg(cost),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno )
  else (select  isnull(avg(cost),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno ) end

  --- (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) 
  ))) else (select  top 1 isnull(b.cost,0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('36') group by b.ref,b.cost order by b.ref desc) end) as lcost,( 0
  + (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
  - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  --- (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
 -- - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('46'))
  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
     where
	 ( (select  count(*) from #bld_temp c where a.item_no=c.itemno  and c.branch=@brno ) + 
	   (select  count(*) from #bld_temp b where a.item_no=b.itemno  and b.branch=@brno) + 
	   (select  count(*) from #bld_temp d where a.item_no=d.itemno and d.branch=@brno) > 0
  --+ (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
  --- (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

  --- (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  --+ (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  --- (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
  --- (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) )<>0
 ------ ((select isnull(sum(d.openbal),0) from whbins d where a.item_no=d.item_no)
 ------ + (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
 -------- - (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

 -------- - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
 -------- + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

 ------ + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
 ------ - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
 ------ )<>0
 ------ and
 /*
  ((select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
   - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) )<>0
  */
  )

  --- (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) 

	 ) as s
	 ON t.item_no = s.item_no and t.wh_no = @whno and t.br_no=@brno--and s.qty<>0
	-- ON t.item_no = s.item_no and t.wh_no=@whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=s.qty,t.lcost=s.lcost 
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.lcost,0,0,0,'');
     --WHEN NOT MATCHED BY SOURCE THEN 
     --DELETE;
     --WHEN NOT MATCHED BY SOURCE
     --THEN DELETE;


	   fetch next
          From @Cursor_name into @brno,@whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------

 declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float,@osumqty float, @oevrcost float
	    -- set @cur=''
         set @Cursor_name2 = Cursor LOCAL FAST_FORWARD for
         Select  item_no From items --a where exists(select item_no from whbins b where a.item_no=b.item_no) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
set @sumqty= (select isnull(sum(qty + openbal),0) from whbins  where item_no=@itemno);
set @evrcost=case when (select (sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<> 0 then
 (select ( sum((iif(qty>0 , qty,0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else (select isnull(sum(lcost),0) from whbins  where item_no=@itemno and qty + openbal + lcost <> 0) end;

set @osumqty= (select isnull(sum(openbal),0) from whbins  where item_no=@itemno);
set @oevrcost=case when (select (sum(openbal)) from whbins  where item_no=@itemno and openbal <> 0)<> 0 then
 (select ( sum((openbal * openlcost)))/(sum(openbal)) from whbins  where item_no=@itemno and  openbal <> 0) else 0 end;

update items set item_cbalance = @sumqty,item_curcost=@evrcost,    item_obalance = @osumqty,item_opencost=@oevrcost  where item_no=@itemno;

--merge whbins as t
--using(select a.item_no,a.item_curcost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from #bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from #bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_curcost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_curcost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2

		

END



GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_ok]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[bld_whbins_cost_all_ok]

--@br_no varchar(2),
--@wh_no varchar(2)
@posted int
AS
BEGIN

DECLARE @bld_temp TABLE
(
 branch varchar(2),trtype varchar(2),ref int ,itemno varchar(16),qty float,pkqty float , cost float ,whno varchar(2),towhno varchar(2),expdate varchar(8)
);

--IF EXISTS(SELECT TABLE_NAME  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME  = '@bld_temp') BEGIN
--   DROP TABLE @bld_temp;
--END;

--Select branch,trtype,ref,itemno,qty,pkqty,lprice cost,whno,towhno,expdate into @bld_temp from sto_dtl;
if @posted=0
BEGIN
insert into @bld_temp Select branch,trtype,ref,itemno,qty,pkqty,(lprice/pkqty) cost,whno,towhno,expdate from sto_dtl union all select branch,invtype,ref,itemno,qty,pkqty,cost cost,whno,'' towhno,'' expdate from sales_dtl
union all select branch,invtype,ref,itemno,qty,pkqty,iif(fullcost>0,fullcost, price/pkqty) cost,whno,'' towhno,expdate from pu_dtl;
END
ELSE
BEGIN
insert into @bld_temp Select a.branch,a.trtype,a.ref,itemno,qty,pkqty,(lprice/pkqty) cost,a.whno,a.towhno,expdate from sto_dtl a join sto_hdr b on a.branch=b.branch and a.trtype=b.trtype and a.ref=b.ref where b.posted=1
union all select a.branch,a.invtype,a.ref,itemno,qty,pkqty,cost cost,whno,'' towhno,'' expdate from sales_dtl a join sales_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.ref=b.ref and a.slcenter=b.slcenter where b.posted=1
union all select a.branch,a.invtype,a.ref,itemno,qty,pkqty,iif(fullcost>0,fullcost, price/pkqty) cost,a.whno,'' towhno,expdate from pu_dtl a join pu_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.ref=b.ref and a.pucenter=b.pucenter where b.posted=1 ;
END

--select * from @bld_temp where itemno='02-006';




update items set item_curcost= 0 , item_cbalance=0,item_obalance=0,item_opencost=0;--where br_no= @brno and wh_no=@whno;

	declare @Cursor_name Cursor , @brno nvarchar(2) ,@whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_name = Cursor LOCAL FAST_FORWARD for
         Select  wh_brno,wh_no From whouses -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @brno,@whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
update whbins set qty= 0,lcost=0 where br_no= @brno and wh_no=@whno;



--set @sumqty= (
merge whbins as t
using(
--select @lpkqty =(coalesce(stp.openbal,0) + coalesce(pur.puqty,0) -coalesce(sal.salqty,0) + coalesce(sto.stoqty,0) - coalesce(sto.stoqty1,0) - coalesce(sto2.stoqty2,0)) from items a
	-- left join
	select a.item_no ,--(coalesce(round((avg(fullcost)),2),0) qty*pkqty>0 and  branch=@brno and whno=@whno and itemno=@itemno) as lcost,
	coalesce(round((avg(NULLIF(case when trtype in ('06','07','31','45','33') and (t.whno=@whno or t.towhno=@whno ) and t.branch=@brno then cost else 0 end,0))),2),0)  as lcost,
	--(select isnull((sum(NULLIF(case when trtype in ('06','07','31','45','33','46') and (whno=@brno or towhno=@brno ) and branch=@brno then (cost*pkqty*qty) else 0 end,0))/sum(NULLIF(case when trtype in ('06','07','31','45','33','46') and (whno=@brno or towhno=@brno ) and branch=@brno  then (pkqty*qty) else 0 end,0))),0) from   @bld_temp bt join items on a.item_no=bt.itemno ) as lcost,
	-- coalesce((sum(case when t.trtype in ('06','07','31') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty*t.cost else 0 end))/(sum(case when t.trtype in ('06','07','31') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 1 end)),0)  as lcost,
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 
	 (coalesce(sum(case when t.trtype in ('06','07') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty when t.trtype in ('16','17') then -t.qty*t.pkqty else 0 end),0) --as puqty 
     -- from pu_dtl t where t.whno=@whno
      
     +  coalesce(sum(case when t.trtype in ('14','15') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty when t.trtype in ('04','05') then -t.qty*t.pkqty else 0 end),0) --as salqty  
     -- from sales_dtl t where t.whno=@whno
     
	+ coalesce(sum(case when t.trtype in ('31',case when trtype='45' and whno='' and towhno<>'' and t.branch=@brno then '45' else '' end,'35',case when trtype='46' and whno='' and towhno<>'' and t.branch=@brno then '46' else '' end,case when t.trtype in ('33') and t.towhno=@whno then '33' else '' end) and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty 
	 - coalesce(sum(case when t.trtype in ('32',case when trtype='45' and towhno='' and whno<>'' and t.branch=@brno then '45' else '' end,case when trtype='46' and towhno='' and whno<>'' and t.branch=@brno then '46' else '' end,case when t.trtype in ('33') and t.whno=@whno  then '33' else '' end) and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty1 


	
	 -- - coalesce(sum(case when t.trtype in ('33') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty1 
	 -- + coalesce(sum(case when t.trtype in ('33') and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty 

	 -- ,sum(case when t.trtype in ('31','33','45','46') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end) as stoqty2
	 --,sum(case when t.trtype in ('33') and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty  when t.trtype in ('33') and t.whno=@whno and t.branch=@brno then -(t.qty*t.pkqty) else 0 end) as stoqty2
	 -- from sto_dtl t where t.towhno=@whno
     
    --	- coalesce(sum(case when t.trtype in ('33') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty2
	 -- from sto_dtl t where t.whno=@whno
    
--	+ (select coalesce(sum(b.openbal),0) openbal from whbins b where b.wh_no=@whno and b.br_no=@brno and b.item_no=a.item_no)
	
	) as qty
      --group by t.item_no,t.wh_no
     --) stp
	 -- on a.item_no = stp.item_no and stp.wh_no=@whno
	 -- where a.item_no = @itemid;
	 from items a join @bld_temp t  on a.item_no=t.itemno
	 where branch=@brno and (whno=@whno or towhno=@whno) --and itemno=a.item_no
	 group by a.item_no
--);

) as s
	 ON t.item_no = s.item_no and t.wh_no = @whno and t.br_no=@brno--and s.qty<>0
	-- ON t.item_no = s.item_no and t.wh_no=@whno --and s.qty<>0
	 WHEN MATCHED then
	 --update set t.qty=s.qty,t.lcost=(select round(avg(cost),2) cst from  @bld_temp where branch=@brno and whno=@whno and itemno=@itemno), @sumqty =s.qty
	 update set t.qty=s.qty,t.lcost=s.lcost
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.lcost,0,0,0,'');

declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float,@osumqty float, @oevrcost float
	    -- set @cur=''
         set @Cursor_name2 = Cursor LOCAL FAST_FORWARD for
         Select  item_no From items --a where exists(select item_no from whbins b where a.item_no=b.item_no) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
--set @evrcost=(select round(avg(cost),2) cst from  @bld_temp where branch=@brno and whno=@whno and itemno=@itemno);
--set @osumqty= (0);
--set @oevrcost=(0);
set @sumqty= (select isnull(sum(qty+openbal),0) from whbins  where item_no=@itemno);
set @evrcost=case when (select (sum(iif(qty>0 , iif(lcost<>0,qty,0),0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<> 0 then
 (select ( sum((iif(qty>0 , iif(lcost<>0,qty,0),0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , iif(lcost<>0,qty,0),0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else (select isnull(avg(NULLIF(lcost,0)),0) from whbins where item_no=@itemno) end;

set @osumqty= (select isnull(sum(openbal),0) from whbins  where item_no=@itemno);
set @oevrcost=case when (select (sum(openbal)) from whbins  where item_no=@itemno and openbal <> 0)<> 0 then
 (select ( sum((openbal * openlcost)))/(sum(openbal)) from whbins  where item_no=@itemno and  openbal <> 0) else (select isnull(avg(NULLIF(openlcost,0)),0) from whbins where item_no=@itemno) end;

--update items set item_cbalance = @sumqty,item_curcost=@evrcost,    item_obalance = @osumqty,item_opencost=@oevrcost,static_cost=iif(static_cost=0 , @evrcost,static_cost)  where item_no=@itemno;

--update items set item_cbalance =iif(item_cost=0 , 0,@sumqty),item_curcost=iif(item_cost=0 , 0,iif(@evrcost=0,@oevrcost,@evrcost)),    item_obalance =iif(item_cost=0 , 0,@osumqty) ,item_opencost=iif(item_cost=0 , 0,@oevrcost),static_cost=iif((static_cost=0 or static_cost is null) , iif(@evrcost=0,@oevrcost,@evrcost),static_cost)  where item_no=@itemno;
update items set item_cbalance =iif(item_cost=0 , 0,@sumqty),item_curcost=iif(item_cost=0 , 0,iif(@evrcost=0,@oevrcost,@evrcost))
,item_obalance =iif(item_cost=0 , 0,@osumqty) ,item_opencost=iif(item_cost=0 , 0,@oevrcost)  --where item_no=@itemno
,static_cost=iif((static_cost=0 or static_cost is null) , iif(@evrcost=0,@oevrcost,@evrcost),static_cost)  where item_no=@itemno;

--update items set item_cbalance = 0,item_curcost=0,    item_obalance = 0,item_opencost=0  where item_cost=0 and item_no=@itemno;
--update items set item_cbalance = 0,item_curcost=0,    item_obalance = 0,item_opencost=0  where item_cost=0;
--merge whbins as t
--using(select a.item_no,a.item_curcost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_curcost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_curcost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2


	   fetch next
          From @Cursor_name into @brno,@whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------
update whbins set lcost=openlcost where lcost=0 and openlcost<>0 ;
END


GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_posted]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_whbins_cost_all_posted]

--@br_no varchar(2),
--@wh_no varchar(2)
@posted int=1
AS
BEGIN

update items set item_curcost= 0 , item_cbalance=0,item_obalance=0,item_opencost=0;--where br_no= @brno and wh_no=@whno;

	declare @Cursor_name Cursor , @brno nvarchar(2) ,@whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_name = Cursor for
         Select  wh_brno,wh_no From whouses -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @brno,@whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
update whbins set qty= 0,lcost=0 where br_no= @brno and wh_no=@whno;
merge whbins as t
using(select a.item_no,( ( (case when  (
 (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c join pu_hdr h on c.branch=h.branch and c.pucenter=h.pucenter and c.invtype=h.invtype and c.ref=h.ref and h.posted=1 where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31','45','33','46'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )<>0 then

  ((( select  isnull(sum(c.qty*pkqty*fullcost),0) from pu_dtl c join pu_hdr h on c.branch=h.branch and c.pucenter=h.pucenter and c.invtype=h.invtype and c.ref=h.ref and h.posted=1 where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty*lcost),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31','45','33','46'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno='' and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty*lcost),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )/

  ( (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c join pu_hdr h on c.branch=h.branch and c.pucenter=h.pucenter and c.invtype=h.invtype and c.ref=h.ref and h.posted=1 where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
 -- - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

 -- - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
 -- + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31','45','33','46'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno='' and b.towhno=@whno and b.branch=@brno and b.trtype in ('45'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
  )) else 0 end

  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) 
  ))) as lcost,( 0
  + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c join pu_hdr h on c.branch=h.branch and c.pucenter=h.pucenter and c.invtype=h.invtype and c.ref=h.ref and h.posted=1 where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c join pu_hdr h on c.branch=h.branch and c.pucenter=h.pucenter and c.invtype=h.invtype and c.ref=h.ref and h.posted=1 where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b join sales_hdr h on b.branch=b.branch and b.slcenter=h.slcenter and b.invtype=h.invtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b join sales_hdr h on b.branch=b.branch and b.slcenter=h.slcenter and b.invtype=h.invtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
 -- - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('46'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
     where
	 ( 
  --+ (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
  --- (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

  --- (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  --+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  --+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) )<>0
 ------ ((select isnull(sum(d.openbal),0) from whbins d where a.item_no=d.item_no)
 ------ + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
 -------- - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

 -------- - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
 -------- + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

 ------ + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
 ------ - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32'))
 ------ )<>0
 ------ and
  ((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c join pu_hdr h on c.branch=h.branch and c.pucenter=h.pucenter and c.invtype=h.invtype and c.ref=h.ref and h.posted=1 where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
  - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c join pu_hdr h on c.branch=h.branch and c.pucenter=h.pucenter and c.invtype=h.invtype and c.ref=h.ref and h.posted=1 where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

  - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b join sales_hdr h on b.branch=b.branch and b.slcenter=h.slcenter and b.invtype=h.invtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
  + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b join sales_hdr h on b.branch=b.branch and b.slcenter=h.slcenter and b.invtype=h.invtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('33'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('33'))
   - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('45','46'))
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('35'))
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b join sto_hdr h on b.branch=h.branch  and b.trtype=h.trtype and b.ref=h.ref and h.posted=1 where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) )<>0)

  --- (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) 

	 group by a.item_no,a.item_curcost,a.item_obalance) as s
	 ON t.item_no + t.wh_no = s.item_no + @whno and t.br_no=@brno--and s.qty<>0
	-- ON t.item_no = s.item_no and t.wh_no=@whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=s.qty,t.lcost=s.lcost 
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.lcost,0,0,0,'');
     --WHEN NOT MATCHED BY SOURCE THEN 
     --DELETE;
     --WHEN NOT MATCHED BY SOURCE
     --THEN DELETE;


	   fetch next
          From @Cursor_name into @brno,@whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------

 declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float,@osumqty float, @oevrcost float
	    -- set @cur=''
         set @Cursor_name2 = Cursor for
         Select  item_no From items --a where exists(select item_no from whbins b where a.item_no=b.item_no) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
set @sumqty= (select isnull(sum(qty + openbal),0) from whbins  where item_no=@itemno);
set @evrcost=case when (select (sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<> 0 then
 (select ( sum((iif(qty>0 , qty,0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else (select isnull(avg(lcost),0) from whbins  where item_no=@itemno and qty + openbal + lcost  <> 0) end;

set @osumqty= (select isnull(sum(openbal),0) from whbins  where item_no=@itemno);
set @oevrcost=case when (select (sum(openbal)) from whbins  where item_no=@itemno and openbal <> 0)<> 0 then
 (select ( sum((openbal * openlcost)))/(sum(openbal)) from whbins  where item_no=@itemno and  openbal <> 0) else 0 end;

update items set item_cbalance = @sumqty,item_curcost=@evrcost,    item_obalance = @osumqty,item_opencost=@oevrcost  where item_no=@itemno;

--merge whbins as t
--using(select a.item_no,a.item_curcost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_curcost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_curcost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2

END













	  














GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_all_rajba2]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[bld_whbins_cost_all_rajba2]

--@br_no varchar(2),
--@wh_no varchar(2)
@posted int=1
AS
BEGIN

DECLARE @bld_temp TABLE
(
 branch varchar(2),trtype varchar(2),ref int ,itemno varchar(16),qty float,pkqty float , cost float ,whno varchar(2),towhno varchar(2),expdate varchar(8)
);

--IF EXISTS(SELECT TABLE_NAME  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME  = '@bld_temp') BEGIN
--   DROP TABLE @bld_temp;
--END;

--Select branch,trtype,ref,itemno,qty,pkqty,lprice cost,whno,towhno,expdate into @bld_temp from sto_dtl;
insert into @bld_temp Select branch,trtype,ref,itemno,qty,pkqty,lprice cost,whno,towhno,expdate from sto_dtl union all select branch,invtype,ref,itemno,qty,pkqty,cost cost,whno,'' towhno,'' expdate from sales_dtl
union all select branch,invtype,ref,itemno,qty,pkqty,iif(fullcost>0,fullcost, price/pkqty) cost,whno,'' towhno,expdate from pu_dtl;

--select * from @bld_temp where itemno='02-006';




update items set item_curcost= 0 , item_cbalance=0,item_obalance=0,item_opencost=0;--where br_no= @brno and wh_no=@whno;

	declare @Cursor_name Cursor , @brno nvarchar(2) ,@whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_name = Cursor LOCAL FAST_FORWARD for
         Select  wh_brno,wh_no From whouses -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @brno,@whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
update whbins set qty= 0,lcost=0 where br_no= @brno and wh_no=@whno;


declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float,@osumqty float, @oevrcost float
	    -- set @cur=''
         set @Cursor_name2 = Cursor LOCAL FAST_FORWARD for
         Select  item_no From items --a where exists(select item_no from whbins b where a.item_no=b.item_no) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
--set @sumqty= (
merge whbins as t
using(
--select @lpkqty =(coalesce(stp.openbal,0) + coalesce(pur.puqty,0) -coalesce(sal.salqty,0) + coalesce(sto.stoqty,0) - coalesce(sto.stoqty1,0) - coalesce(sto2.stoqty2,0)) from items a
	-- left join
	select
	 (coalesce(sum(case when t.trtype in ('06','07') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else -t.qty*t.pkqty end),0) --as puqty 
     -- from pu_dtl t where t.whno=@whno
      
     +  coalesce(sum(case when t.trtype in ('14','15') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else -t.qty*t.pkqty end),0) --as salqty  
     -- from sales_dtl t where t.whno=@whno
     
	+ coalesce(sum(case when t.trtype in ('31','45','46','35',case when trtype='45' and whno='' and towhno<>'' then '45' else '' end) and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty 
	 - coalesce(sum(case when t.trtype in ('32',case when trtype='45' and towhno='' and whno<>'' then '45' else '' end) and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty1 
	 -- ,sum(case when t.trtype in ('31','33','45','46') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end) as stoqty2
	 --,sum(case when t.trtype in ('33') and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty  when t.trtype in ('33') and t.whno=@whno and t.branch=@brno then -(t.qty*t.pkqty) else 0 end) as stoqty2
	 -- from sto_dtl t where t.towhno=@whno
     
	-- - coalesce(sum(case when t.trtype in ('33') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end),0) --as stoqty2
	 -- from sto_dtl t where t.whno=@whno
    
	+ (select coalesce(sum(b.openbal),0) openbal from whbins b where b.wh_no=@whno and b.br_no=@brno and b.item_no=@itemno)) as qty
      --group by t.item_no,t.wh_no
     --) stp
	 -- on a.item_no = stp.item_no and stp.wh_no=@whno
	 -- where a.item_no = @itemid;
	 from @bld_temp t
	 where branch=@brno and whno=@whno and itemno=@itemno

--);

) as s
	 ON t.item_no = @itemno and t.wh_no = @whno and t.br_no=@brno --and s.qty<>0
	-- ON t.item_no = s.item_no and t.wh_no=@whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=s.qty,t.lcost=(select round(avg(cost),2) cst from  @bld_temp where branch=@brno and whno=@whno and itemno=@itemno)
	 WHEN NOT MATCHED then
	 insert values(@brno,@itemno,'',@whno,'',s.qty,0,0,(select round(avg(cost),2) cst from  @bld_temp where branch=@brno and whno=@whno and itemno=@itemno),0,0,0,'');

set @sumqty= (select isnull(sum(qty + openbal),0) from whbins  where item_no=@itemno);
set @evrcost=case when (select (sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<> 0 then
 (select ( sum((iif(qty>0 , qty,0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else (select isnull(avg(lcost),0) from whbins  where item_no=@itemno and qty + openbal + lcost  <> 0) end;

set @osumqty= (select isnull(sum(openbal),0) from whbins  where item_no=@itemno);
set @oevrcost=case when (select (sum(openbal)) from whbins  where item_no=@itemno and openbal <> 0)<> 0 then
 (select ( sum((openbal * openlcost)))/(sum(openbal)) from whbins  where item_no=@itemno and  openbal <> 0) else 0 end;

update items set item_cbalance = @sumqty,item_curcost=@evrcost,    item_obalance = @osumqty,item_opencost=@oevrcost,static_cost=iif(static_cost=0 , @evrcost,static_cost)  where item_no=@itemno;
--update items set item_cbalance = 0,item_curcost=0,    item_obalance = 0,item_opencost=0  where item_cost=0;
--merge whbins as t
--using(select a.item_no,a.item_curcost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from @bld_temp c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.trtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.trtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from @bld_temp b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_curcost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_curcost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2


	   fetch next
          From @Cursor_name into @brno,@whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------

 
 update items set item_cbalance = 0,item_curcost=0,    item_obalance = 0,item_opencost=0  where item_cost=0;
		

END

GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_by_tran]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[bld_whbins_cost_by_tran]

@cond varchar(20),
@br_no varchar(2),
@inv_type varchar(2),
@wh_no varchar(2),
@ref int,
@depart varchar(2),

@posted int=1
AS
BEGIN
	declare @Cursor_name Cursor , @brno nvarchar(2) ,@whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_name = Cursor for
         Select  wh_brno,wh_no From whouses where wh_no=@wh_no -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @brno,@whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
if @cond = 'sal' 
 begin
  merge whbins as t
  using(select a.item_no,a.item_curcost,(case when @inv_type in ('04','05') then -isnull(sum(c.qty*pkqty),0) else isnull(sum(c.qty*pkqty),0) end)  as qty from items a join sales_dtl c on a.item_no=c.itemno where c.whno=@wh_no and c.branch=@br_no and c.invtype in ('04','05','14','15') and c.ref=@ref and c.slcenter=@depart
  group by a.item_no,a.item_curcost,a.item_obalance,c.invtype) as s
	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty= t.qty+s.qty,t.lcost=s.item_curcost  
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');
  end

else if @cond = 'pu'
  BEGIN
  merge whbins as t
  using(select a.item_no,a.item_curcost,(case when @inv_type in ('06','07') then isnull(sum(c.qty*pkqty),0) else -isnull(sum(c.qty*pkqty),0) end)  as qty from items a join pu_dtl c on a.item_no=c.itemno where c.whno=@wh_no and c.branch=@br_no and c.invtype in ('06','07','16','17') and c.ref=@ref and c.pucenter=@depart
  group by a.item_no,a.item_curcost,a.item_obalance,c.invtype) as s
	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=t.qty+s.qty,t.lcost=s.item_curcost
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');
  end

else if @cond = 'sto'
  begin

  merge whbins as t
  using( select a.item_no,a.item_curcost,(case when @inv_type in ('31') then isnull(sum(c.qty*pkqty),0) else -isnull(sum(c.qty*pkqty),0) end)  as qty from items a join sto_dtl c on a.item_no=c.itemno where c.whno=@wh_no and c.branch=@br_no and c.trtype in ('31','32') and c.ref=@ref
  group by a.item_no,a.item_curcost,a.item_obalance,c.trtype) as s
	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=t.qty+s.qty,t.lcost=s.item_curcost
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_curcost,0,0,0,'');

  end

	   fetch next
          From @Cursor_name into @brno,@whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------

  declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float,@osumqty float, @oevrcost float
	    -- set @cur=''
       -- set @Cursor_name2 = Cursor for 
        -- Select  item_no From items a where exists(select itemno from (CASE when @cond='sal' then sales_dtl b when @cond = 'pu' then pu_dtl b end) where a.item_no=b.itemno) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
if @cond = 'sal' 
begin
  -- declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float
   set @Cursor_name2 = Cursor for 
   Select  item_no From items a where exists(select itemno from sales_dtl b where a.item_no=b.itemno and b.branch=@br_no and b.invtype=@inv_type and b.ref=@ref and b.slcenter=@depart)
     -- FROM   T 
end

else if @cond = 'pu'
BEGIN
    set @Cursor_name2 = Cursor for
      Select  item_no From items a where exists(select itemno from pu_dtl b where a.item_no=b.itemno and b.branch=@br_no and b.invtype=@inv_type and b.ref=@ref and b.pucenter=@depart)
     -- FROM   T 
END 
 
else if @cond = 'sto'
  begin
   set @Cursor_name2 = Cursor for
      Select  item_no From items a where exists(select itemno from sto_dtl b where a.item_no=b.itemno and b.branch=@br_no and b.trtype=@inv_type and b.ref=@ref and b.towhno=@wh_no)
  --   -- FROM   T 
  end
  
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
--------set @sumqty= (select sum(qty + openbal) from whbins  where item_no=@itemno);
----------set @evrcost=case when (select (sum(qty + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<>0 then (select ( sum((qty * lcost) + (openbal * openlcost)))/(sum(qty + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else 0 end ;
--------set @evrcost=case when (select (sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<> 0 then
-------- (select ( sum((iif(qty>0 , qty,0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else (select isnull(avg(lcost),0) from whbins  where item_no=@itemno and qty + openbal + lcost  <> 0) end;

--------update items set item_cbalance = @sumqty,item_curcost=@evrcost where item_no=@itemno;
set @sumqty= (select isnull(sum(qty + openbal),0) from whbins  where item_no=@itemno);
set @evrcost=case when (select (sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0)<> 0 then
 (select ( sum((iif(qty>0 , qty,0) * lcost) + (openbal * openlcost)))/(sum(iif(qty>0 , qty,0) + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0) else (select isnull(avg(lcost),0) from whbins  where item_no=@itemno and qty + openbal + lcost  <> 0) end;

set @osumqty= (select isnull(sum(openbal),0) from whbins  where item_no=@itemno);
set @oevrcost=case when (select (sum(openbal)) from whbins  where item_no=@itemno and openbal <> 0)<> 0 then
 (select ( sum((openbal * openlcost)))/(sum(openbal)) from whbins  where item_no=@itemno and  openbal <> 0) else 0 end;

update items set item_cbalance = @sumqty,item_curcost=@evrcost,    item_obalance = @osumqty,item_opencost=@oevrcost  where item_no=@itemno;

--merge whbins as t
--using(select a.item_no,a.item_cost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_cost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_cost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_cost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2

END













	  














GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_cost_by_tran_o]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_whbins_cost_by_tran_o]

@cond varchar(20),
@br_no varchar(2),
@inv_type varchar(2),
@wh_no varchar(2),
@ref int,
@depart varchar(2),

@posted int=1
AS
BEGIN
	declare @Cursor_name Cursor , @brno nvarchar(2) ,@whno nvarchar(2)
	    -- set @cur=''
         set @Cursor_name = Cursor for
         Select  wh_brno,wh_no From whouses where wh_no=@wh_no -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @brno,@whno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
if @cond = 'sal' 
 begin
  merge whbins as t
  using(select a.item_no,a.item_cost,( 0
 
  - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05') and b.ref=@ref)
  + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15') and b.ref=@ref)

                                     ) as qty  from items a where exists(select * from sales_dtl b where a.item_no=b.itemno and b.branch=@brno and b.invtype in ('04','05','14','15') and b.ref=@ref) 
	 group by a.item_no,a.item_cost,a.item_obalance) as s
	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=t.qty + s.qty,t.lcost=s.item_cost 
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_cost,0,0,0,'');
  end

else if @cond = 'pu'
  BEGIN
  merge whbins as t
  using(select a.item_no,a.item_cost,( 0
  + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07') and c.ref=@ref) 
  - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17') and c.ref=@ref) 

                                     ) as qty  from items a where exists(select * from pu_dtl b where a.item_no=b.itemno and b.branch=@brno and b.invtype in ('06','07','16','17') and b.ref=@ref)
     group by a.item_no,a.item_cost,a.item_obalance) as s
	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=t.qty + s.qty,t.lcost=s.item_cost 
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_cost,0,0,0,'');
  end

else if @cond = 'sto'
  begin

  merge whbins as t
  using(select a.item_no,a.item_cost,( 0
 
  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31') and b.ref=@ref)
  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32') and b.ref=@ref) 
  
                                     ) as qty  from items a where exists(select * from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('31','32') and b.ref=@ref)
	  group by a.item_no,a.item_cost,a.item_obalance) as s
	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
	 WHEN MATCHED then
	 update set t.qty=t.qty + s.qty,t.lcost=s.item_cost 
	 WHEN NOT MATCHED then
	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_cost,0,0,0,'');

  end

	   fetch next
          From @Cursor_name into @brno,@whno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------

  declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float
	    -- set @cur=''
       -- set @Cursor_name2 = Cursor for 
        -- Select  item_no From items a where exists(select itemno from (CASE when @cond='sal' then sales_dtl b when @cond = 'pu' then pu_dtl b end) where a.item_no=b.itemno) -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
if @cond = 'sal' 
begin
  -- declare @Cursor_name2 Cursor , @itemno nvarchar(20), @sumqty float, @evrcost float
   set @Cursor_name2 = Cursor for 
   Select  item_no From items a where exists(select itemno from sales_dtl b where a.item_no=b.itemno and b.branch=@br_no and b.invtype=@inv_type and b.ref=@ref and b.slcenter=@depart)
     -- FROM   T 
end

else if @cond = 'pu'
BEGIN
    set @Cursor_name2 = Cursor for
      Select  item_no From items a where exists(select itemno from pu_dtl b where a.item_no=b.itemno and b.branch=@br_no and b.invtype=@inv_type and b.ref=@ref and b.pucenter=@depart)
     -- FROM   T 
END 
 
else if @cond = 'sto'
  begin
   set @Cursor_name2 = Cursor for
      Select  item_no From items a where exists(select itemno from sto_dtl b where a.item_no=b.itemno and b.branch=@br_no and b.trtype=@inv_type and b.ref=@ref and b.towhno=@wh_no)
  --   -- FROM   T 
  end
  
--step1: open cursor 
         open @Cursor_name2
--step2: Fetch Cursor
         fetch next
         From @Cursor_name2 into @itemno 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
set @sumqty= (select sum(qty + openbal) from whbins  where item_no=@itemno);
set @evrcost= (select ( sum((qty * lcost) + (openbal * openlcost)))/(sum(qty + openbal)) from whbins  where item_no=@itemno and qty + openbal <> 0);
update items set item_cbalance = @sumqty,item_curcost=@evrcost where item_no=@itemno;

--merge whbins as t
--using(select a.item_no,a.item_cost,( 0
--  + (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('06','07')) 
--  - (select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.whno=@whno and c.branch=@brno and c.invtype in ('16','17')) 

--  - (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('04','05'))
--  + (select  isnull(sum(b.qty*pkqty),0) from sales_dtl b where a.item_no=b.itemno and b.whno=@whno and b.branch=@brno and b.invtype in ('14','15'))

--  + (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('31'))
--  - (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.towhno=@whno and b.branch=@brno and b.trtype in ('32')) ) as qty  from items a 
--	  group by a.item_no,a.item_cost,a.item_obalance) as s
--	 ON t.item_no + t.wh_no = s.item_no + @whno --and s.qty<>0
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_cost 
--	 WHEN NOT MATCHED then
--	 insert values(@brno,s.item_no,'',@whno,'',s.qty,0,0,s.item_cost,0,0,0,'');


	   fetch next
          From @Cursor_name2 into @itemno

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name2
--step5: Deallocate Cursor 
         deallocate @Cursor_name2

END













	  














GO
/****** Object:  StoredProcedure [dbo].[bld_whbins_sl_pu]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[bld_whbins_sl_pu]
AS
BEGIN

--merge whbins as t
--using(select a.item_no,a.item_cost,a.item_obalance + (select  isnull(sum(c.qty),0) from recv_dtl c where a.item_no=c.itemn_no) - (select  isnull(sum(b.qty),0) from sales_dtl b where a.item_no=b.itemno) as qty  from items a 
--	  group by a.item_no,a.item_cost,a.item_obalance) as s
--	 ON t.item_no = s.item_no
--	 WHEN MATCHED then
--	 update set t.qty=s.qty,t.lcost=s.item_cost
--	 WHEN NOT MATCHED then
--	 insert values('01',s.item_no,'01',s.qty,0,0,s.item_cost,0,'');
select getdate();
END;










	  














GO
/****** Object:  StoredProcedure [dbo].[change_pos_branch]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[change_pos_branch] 

@brno nvarchar(2)

AS
BEGIN

	begin transaction
	
	update branchs set br_no=@brno;
	update slcenters set sl_brno=@brno,sl_no=@brno;
	--update pucenters set pu_no=@brno;
	update pos_salmen set sl_brno=@brno;
	update pos_hdr set brno=@brno,slno=@brno;
	update pos_dtl set brno=@brno,slno=@brno;
	update contrs set c_brno=@brno,c_slno=@brno;
	update users set brkey=' ' + @brno, slkey=' ' + @brno + @brno ;

	if @@error<>0
	begin
		rollback transaction
		return 
	end
	commit transaction

END

GO
/****** Object:  StoredProcedure [dbo].[change_price_as_tax]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[change_price_as_tax] 
--  @price float,
@newp  float,@oldp float

as
begin

--DECLARE  @price float,@newp  float,@oldp float;

--set @price=115;set @oldp=15; set @newp=5;

--select round((@price - @price/((100 + @oldp)/@oldp)) + (@price - @price/((100 + @oldp)/@oldp)) * (@newp/100),2) vat;
if(@oldp=0)
begin
update items set item_price=round((item_price) + (item_price * (@newp/100)),2) ;
update items_bc set price=round((price ) + (price  * (@newp/100)),2) ;
update brprices set lprice1=round((lprice1) + (lprice1  * (@newp/100)),2) ;
end
else
begin
update items set item_price=round((item_price - item_price/((100 + @oldp)/@oldp)) + (item_price - item_price/((100 + @oldp)/@oldp)) * (@newp/100),2) ;
update items_bc set price=round((price - price/((100 + @oldp)/@oldp)) + (price - price/((100 + @oldp)/@oldp)) * (@newp/100),2) ;
update brprices set lprice1=round((lprice1 - lprice1/((100 + @oldp)/@oldp)) + (lprice1 - lprice1/((100 + @oldp)/@oldp)) * (@newp/100),2) ;
end
end
GO
/****** Object:  StoredProcedure [dbo].[check_moved]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[check_moved]

--@counti int,
@itemno varchar(50),
@NO_items int output,
@errstatus int = 0 output

AS
BEGIN

--SELECT @counti = count (*) from sales_dtl where itemno = @itemno

declare   
@sqlmsg nvarchar(max)

set @sqlmsg='SELECT * from sales_dtl where itemno = ' + @itemno

begin transaction
	exec sp_executesql @sqlmsg
	set @No_items=@@ROWCOUNT
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=1
		return 
	end
	commit transaction

--return @counti
 
END;






GO
/****** Object:  StoredProcedure [dbo].[chk_for_taqween]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[chk_for_taqween]
@from_date varchar(23),
@to_date varchar(23),
@br_no varchar(2),
@sl_no varchar(2),
@shmel_tax int

--,@no_of_invoices int=0 output

AS
BEGIN


--Select a.brno,a.contr,a.ref,a.slno,new_valafds=sum((b.qty*(b.price-(b.price*a.dscper/100)))-b.offr_amt),a.net_total
--from  pos_hdr a with (NOLOCK) inner Join pos_dtl b with (NOLOCK) 
--on  a.brno = b.brno and a.Contr = b.Contr and a.ref = b.ref and a.slno=b.slno
--where [date]  between ''+ @from_date +'' and ''+ @to_date +'' and a.brno=''+@br_no+'' and a.slno=''+@sl_no+''
--group by a.brno,a.contr,a.ref,a.slno,a.net_total having
--round(net_total,2) - round(sum((b.qty*(b.price-(b.price*a.dscper/100)))-b.offr_amt),2) >0.20
if(@shmel_tax=2)
BEGIN
--Select a.brno,a.contr,a.ref,a.slno,new_valafds=sum((b.qty*(b.price-(b.price*(a.discount/(case when a.total<>0 then a.total else 1 end)))))-b.offr_amt),a.net_total
Select a.brno,a.contr,a.ref,a.slno,new_valafds=sum((b.qty*(b.price))-b.offr_amt)-a.discount,a.net_total
from  pos_hdr a with (NOLOCK) inner Join pos_dtl b with (NOLOCK) 
on  a.brno = b.brno and a.Contr = b.Contr and a.ref = b.ref and a.slno=b.slno
where [date]  between ''+ @from_date +'' and ''+ @to_date +'' and a.brno=''+@br_no+'' and a.slno=''+@sl_no+''
group by a.brno,a.contr,a.ref,a.slno,a.discount,a.net_total having
--round(net_total,2) - round(sum((b.qty*(b.price-(b.price*(a.discount/(case when a.total<>0 then a.total else 1 end)))))-b.offr_amt),2) >0.50
round(net_total,2) - round(sum((b.qty*(b.price))-b.offr_amt)-a.discount,2) >0.50
end
else
BEGIN
--Select a.brno,a.contr,a.ref,a.slno,new_valafds=sum((b.qty*(b.price-(b.price*(a.discount/(case when a.total<>0 then a.total else 1 end)))))-b.offr_amt),a.net_total
Select a.brno,a.contr,a.ref,a.slno,new_valafds=sum((b.qty*(b.price))-b.offr_amt)-a.discount,a.net_total,a.tax_amt
from  pos_hdr a with (NOLOCK) inner Join pos_dtl b with (NOLOCK) 
on  a.brno = b.brno and a.Contr = b.Contr and a.ref = b.ref and a.slno=b.slno
where [date]  between ''+ @from_date +'' and ''+ @to_date +'' and a.brno=''+@br_no+'' and a.slno=''+@sl_no+''
group by a.brno,a.contr,a.ref,a.slno,a.discount,a.net_total,a.tax_amt having
--round(net_total,2) - round(sum((b.qty*(b.price-(b.price*(a.discount/(case when a.total<>0 then a.total else 1 end)))))-b.offr_amt),2) >0.50
round(net_total-a.tax_amt,2) - round(sum((b.qty*(b.price))-b.offr_amt)-a.discount,2) >0.50

end

--set @no_of_invoices=@@ROWCOUNT

--return @no_of_invoices
end

GO
/****** Object:  StoredProcedure [dbo].[copy_acc_balances_from_old_db]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[copy_acc_balances_from_old_db]
 @db nvarchar(50),
 @cond varchar(5),
 --@multibarcode bit,
 --@uniqu_bcs bit , -- multi branch prices
 @errstatus int = 0 output
	
AS
BEGIN
	declare @sqlmsg nvarchar(max)
	declare @tbl nvarchar(50)

	
	-----------------------------copy acc

	set @errstatus=0

if @cond = 'acc' 
    begin
	--set @tbl=@db+'.dbo.'+'accounts as s '
	set @tbl=@db+'.dbo.'+'accounts '
	set @sqlmsg=' merge accounts as t '
	--set @sqlmsg= @sqlmsg+' using '+@tbl
	set @sqlmsg= @sqlmsg+' using (select * from '+ @tbl + ' where a_type not in (''2'',''3'')) as s'
	set @sqlmsg= @sqlmsg+' on t.a_key = s.a_key AND t.glcurrency = s.glcurrency '
	set @sqlmsg= @sqlmsg+' when matched then '
	set @sqlmsg= @sqlmsg+' update set t.a_opnbal=s.a_curbal, t.a_curbal=s.a_curbal + t.a_curbal '
	set @sqlmsg= @sqlmsg+' when not matched then '
	set @sqlmsg= @sqlmsg+' INSERT VALUES(s.a_key,s.a_parent,s.a_name,s.a_ename,s.a_brno,s.a_curbal,s.a_curbal,s.a_crtdate,s.a_lastupdt,s.a_group,s.a_type,s.a_ptype,s.a_active,s.a_level,s.glpurge,s.accontrol,s.glcurrency,s.fccurbal,s.fcopnbal,s.imopnbal,s.imcurbal,s.gleval,s.acprotect,s.pkey,s.chkey,s.cashbnk,s.modified,s.section,s.isbracc,s.acckind,s.a_note);'


		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=-1
			return 
		end
		else commit transaction
   end
	


	---------------------  copy sup

	if @cond = 'sup' 
    begin
	set  @tbl=@db+'.dbo.'+'suppliers as s '
	set  @sqlmsg=' merge suppliers as t '
	set  @sqlmsg= @sqlmsg+' using '+@tbl
	set  @sqlmsg= @sqlmsg+' on t.su_cur = s.su_cur AND t.su_brno = s.su_brno AND t.su_no = s.su_no'
	set  @sqlmsg= @sqlmsg+' when matched then '
	set  @sqlmsg= @sqlmsg+' update set t.su_opnbal=s.su_curbal,t.su_curbal=s.su_curbal + t.su_curbal,t.su_opnfcy=s.cf_curfcy,t.cf_curfcy=s.cf_curfcy + t.cf_curfcy '
	set  @sqlmsg= @sqlmsg+' when not matched then '
	set  @sqlmsg= @sqlmsg+' insert VALUES(s.su_no,s.su_brno,s.su_name,s.su_class,s.su_addrs,s.su_tel,s.su_fax,s.su_tlx,s.su_email,s.su_cntactp,s.su_title,s.su_crlmt,s.su_pymnt,s.su_status,s.su_curbal,s.su_curbal,s.su_cur,s.su_opnfcy,s.cf_curfcy,s.su_xrf,s.su_alwcr,s.su_ctlser,s.lastupdt,s.su_lcaloc,s.su_fcaloc,s.modified,s.cmncode,s.su_lname,s.su_city,s.su_country,s.su_laddrs,s.su_mobile,s.su_sendsms,s.rowguid,s.whno,s.vndr_taxcode,s.taxfree,s.su_kind,s.section,s.inactive,s.is_shamel_tax, s.s_bulding_no, s.s_street, 
                            s.s_site_name, s.s_city, s.s_cuntry, s.s_postal_code, s.s_ex_postalcode, s.s_other_id,s.su_note);'

		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=-2
			return 
		end
		else commit transaction
		end

	---------------------  copy cust

	
	if @cond = 'cus' 
    begin	
	set @tbl=@db+'.dbo.'+'customers as s '
	set @sqlmsg=' merge customers as t '
	set @sqlmsg= @sqlmsg+' using '+@tbl
	set @sqlmsg= @sqlmsg+' on t.cf_fcy = s.cf_fcy AND t.cu_brno = s.cu_brno AND t.cu_no = s.cu_no '
	set @sqlmsg= @sqlmsg+' when matched then '
	set @sqlmsg= @sqlmsg+' update set t.cu_opnbal=s.cu_curbal,t.cu_curbal=s.cu_curbal + t.cu_curbal '
	set @sqlmsg= @sqlmsg+' when not matched then '
	set @sqlmsg= @sqlmsg+' INSERT VALUES(s.cu_no,s.cu_brno,s.cu_name,s.cu_class,s.cu_addrs,s.cu_tel,s.cu_fax,s.cu_tlx,s.cu_email,s.cu_cntactp,s.cu_title,s.cu_crlmt,s.cu_pymnt,s.cu_status,s.cu_curbal,s.cu_curbal,s.cf_fcy,s.cf_opnfcy,s.cf_curfcy,s.cu_xrf,s.cu_alwcr,s.cu_ctlser,s.lastupdt,s.cu_lcaloc,s.cu_fcaloc,s.modified,s.cmncode,s.cu_lname,s.cu_city,s.cu_country,s.cu_laddrs,s.cu_mobile,s.cu_sendsms,s.rowguid,s.whno,s.vndr_taxcode,s.taxfree,s.cu_kind,s.section,s.inactive, s.c_bulding_no, s.c_street, s.c_site_name, 
                         s.c_city, s.c_cuntry, s.c_postal_code, s.c_ex_postalcode, s.c_other_id, s.cu_salman,s.cu_disc,s.cu_note);'
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=-3
			return 
		end
		else commit transaction
		end


end








GO
/****** Object:  StoredProcedure [dbo].[copy_br_acc]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[copy_br_acc]

--@counti int,
@brno varchar(2),
@brname nvarchar(50),
@NO_acc int output,
@errstatus int = 0 output

AS
BEGIN

    BEGIN TRANSACTION
--BEGIN TRAN

   BEGIN TRY

    -- drop table #temptb;
    select * into #temptb from acccpy;

	UPDATE #temptb
    SET a_key = REPLACE( a_key, '010201001', '0102010'+@brno )
	----where not exists (select a_key
 ----                       from accounts
 ----                       where '0102010'+@brno = accounts.a_key)
	;--,a_parent=REPLACE( a_parent, '501', '5'+ @brno) where a_key like '0501%';

	UPDATE #temptb
    SET a_key = REPLACE( a_key, '010203001', '0102030'+@brno );

	UPDATE #temptb
    SET a_key = REPLACE( a_key, '010207001', '0102070'+@brno );

	UPDATE #temptb
    SET a_key = REPLACE( a_key, '020201001', '0202010'+@brno );

	UPDATE #temptb
    SET a_key = REPLACE( a_key, '020206001', '0202060'+@brno );

    UPDATE #temptb
    SET a_key = REPLACE( a_key, '0501', '05'+@brno ),a_parent=REPLACE( a_parent, '501', '5'+ @brno) where a_key like '0501%';

    UPDATE #temptb
    SET a_key = REPLACE( a_key, '030101', '0301'+ @brno ),a_parent=REPLACE( a_parent, '30101', '301'+ @brno ) where a_key like '030101%';;

    UPDATE #temptb
    SET a_key = REPLACE( a_key, '040101', '0401'+ @brno ),a_parent=REPLACE( a_parent, '40101', '401'+ @brno ) where a_key like '040101%';;

    UPDATE #temptb
    set a_brno=@brno,a_name=REPLACE( a_name, 'الفرع الرئيسي', '' ) + ' ' + @brname where a_brno='01';

    insert into accounts select * from #temptb
	where not exists(select a_key from accounts where accounts.a_key = #temptb.a_key);
	--;
	set @NO_acc = @@rowcount
	--if (select count(*) from contrs where c_brno=@brno)<1
	--if 1=1
	if @NO_acc>0
	   BEGIN
	  -- INSERT [dbo].[contrs] ([c_brno], [c_slno], [contr_id], [contr_name], [msg1], [msg2], [usecd], [portno], [cdmsg], [last_login],[print_type]) VALUES (@brno, @brno, cast (@brno as int), N'كاشير' + cast(cast (@brno as int) as varchar), N'مرحبا بكم', N'شكرا لتسوقكم لدينا', 0, 5, N'Wlcome To Our Store', getdate(),1);
	  INSERT [dbo].[contrs] ([c_brno], [c_slno], [contr_id], [contr_name], [msg1], [msg2], [usecd], [portno], [cdmsg], [last_login],[print_type]) VALUES (@brno, @brno, (select top 1 contr_id from contrs), N'كاشير' + cast(cast (@brno as int) as varchar), N'مرحبا بكم', N'شكرا لتسوقكم لدينا', 0, 5, N'Wlcome To Our Store', getdate(),1);
	   INSERT [dbo].[slcenters] ([sl_brno], [sl_no], [sl_name], [dp_cashser], [dp_slcash], [dp_slcrdt], [dp_reslcash], [dp_reslcrdt], [dp_descont], [dp_taxacc], [dp_salcust_acc], [dp_mkhzon_acc], [dp_okdiff], [dp_diffser], [dp_mainwh], [dp_rtrnwh], [lastupdt], [dp_comp], [dp_fccrdt], [nochg_print], [modified], [srcst], [cstcode], [dp_crdcardac], [dp_rplsac], [dp_cardcomm], [dp_lname], [slwhsrc], [prpaidcrdac], [FRC_CRSL_FC], [inv_form_no], [sc_code], [sanedcrd_act], [no_of_invCopies], [is_shamel_tax]) VALUES (@brno, @brno, @brno + ' مركز بيع ' , N'0102010'+@brno, N'0401'+@brno+'001', N'0401'+@brno+'002', N'0401'+@brno+'003', N'0401'+@brno+'004', N'0401'+@brno+'005', N'0202060'+@brno, N'0401'+@brno+'006', N'', NULL, N'', @brno, @brno, NULL, NULL, NULL, NULL, NULL, NULL, @brno, N'', NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, N'                   ', CAST(1 AS Numeric(1, 0)), 0);
	   INSERT [dbo].[pucenters] ([pu_brno], [pu_no], [pu_name], [dp_cashser], [dp_pucash], [dp_pucrdt], [dp_repucash], [dp_repucrdt], [dp_descont], [dp_taxacc], [dp_custser], [dp_mkhzon_acc], [dp_okdiff], [dp_diffser], [dp_mainwh], [dp_rtrnwh], [lastupdt], [dp_comp], [modified], [cstcode], [serfrt], [serins], [sercst], [serpvl], [sersmp], [serfn], [sertkt], [serfre], [sertrn], [seroth], [dp_lname], [dp_sabr_acc]) VALUES (@brno, @brno, @brno + ' مركز شراء ', N'0102010'+@brno, N'0301'+@brno+'001', N'0301'+@brno+'002', N'0301'+@brno+'003', N'0301'+@brno+'004', N'0301'+@brno+'005', N'0102070'+@brno, NULL, N'', NULL, NULL, @brno, @brno, NULL, NULL, NULL, @brno, N'0301'+@brno+'008', N'0301'+@brno+'009', N'0301'+@brno+'010', N'0301'+@brno+'011', N'0301'+@brno+'012', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
	   INSERT [dbo].[whouses] ([wh_brno], [wh_no], [wh_name], [manager], [phone], [address], [lastupdt], [mkhzon_acc], [modified], [lname], [fax], [prnt_fsh], [ac_end_prd], [cstcode], [no_autosales], [sc_code], [frq_sar_acc]) VALUES (@brno, @brno, @brno + ' مخزن ', NULL, NULL, NULL, NULL, N'', NULL, NULL, NULL, NULL, NULL, @brno, 0, @brno, N'');
	   INSERT [dbo].[costcenters] ([cc_brno], [cc_id], [cc_name]) VALUES (@brno, @brno, @brno + ' مركز تكلفة فرع ');
	   update branchs set acc_coped=1 where br_no= @brno;
	   end
	drop table #temptb;

   COMMIT TRANSACTION
   set @errstatus=1
   END TRY

BEGIN CATCH

  ROLLBACK TRANSACTION
  set @errstatus=0
  set @NO_acc = 0
END CATCH

END


GO
/****** Object:  StoredProcedure [dbo].[copy_data_by_link]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[copy_data_by_link]
	@fmdate varchar(8),
	@todate varchar(8),
	@link nvarchar(100),
	@db_nam nvarchar(20),

	@ok_as bit,
	@ok_ac bit,
	@ok_sl bit,
	@ok_pu bit,
	@ok_st bit,
		
	@ok_it bit,
	--@ok_gr bit,
	@ok_su bit,
	@ok_cu bit,
	@ok_pstd bit,
	@ok_dtl bit,

	@errstatus int = 0 output
 
AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

   Declare @dbc nvarchar(50),
		   @sqlmsg nvarchar(max)

   set @dbc=@link+'.'+@db_nam+'.dbo.'

----  Process accounts & acc_hdr & acc_dtl Table 

  if @ok_as=1
	begin
	 		
     set @sqlmsg='merge accounts as t 
		using  '+@dbc+'accounts  as s
		 ON t.[a_key] = s.[a_key] AND t.[glcurrency] = s.[glcurrency]
		 when not matched then
		INSERT([a_key],[a_parent],[a_name],[a_ename],[a_brno],[a_opnbal],[a_curbal],[a_crtdate],[a_lastupdt],[a_group],[a_type],[a_ptype],[a_active],[a_level],[glpurge],[accontrol],[glcurrency],[fccurbal],[fcopnbal],[imopnbal],[imcurbal],[gleval],[acprotect],[pkey],[chkey],[cashbnk],[modified],[section],[isbracc],[acckind],[a_note])
         VALUES(s.[a_key],s.[a_parent],s.[a_name],s.[a_ename],s.[a_brno],s.[a_opnbal],s.[a_curbal],s.[a_crtdate],s.[a_lastupdt],s.[a_group],s.[a_type],s.[a_ptype],s.[a_active],s.[a_level],s.[glpurge],s.[accontrol],s.[glcurrency],s.[fccurbal],s.[fcopnbal],s.[imopnbal],s.[imcurbal],s.[gleval],s.[acprotect],s.[pkey],s.[chkey],s.[cashbnk],s.[modified],s.[section],s.[isbracc],s.[acckind],s.[a_note]);'
		
		begin transaction

		BEGIN TRY

		    exec sp_executesql @sqlmsg
		    COMMIT TRANSACTION

        END TRY

        BEGIN CATCH

            ROLLBACK TRANSACTION
		    set @errstatus=1
		    return

        END CATCH
				
			

	end

    if @ok_ac=1
	begin

		
		set @sqlmsg='merge acc_hdr as t 
		using (select * from '+@dbc+'acc_hdr with (NOLOCK) where 1=1 '+ iif(@ok_pstd=1,' and a_posted=1 ','')
		+' and a_mdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[a_brno] = s.[a_brno] AND t.[a_ref] = s.[a_ref] AND t.[a_type] = s.[a_type] AND t.[pu_no] = s.[pu_no] AND t.[sl_no] = s.[sl_no]
		 when not matched then
		 INSERT([a_brno],[a_type],[a_ref],[a_mdate],[a_hdate],[a_text],[a_amt],[a_entries],[jhsrc],[a_sysdate],[jhcurr],[jhfcflag],[jhrate],[jhreleased],[a_posted],[lastupdt],[jhlccrttl],[jhlcdbttl],[jhfccrttl],[jhfcdbttl],[jhimgrate],[modified],[serial_no],[rcvdtrn],[suspend],[usrid],[isbrtrx],[brxref],[brxfrm],[jhcustno],[hide_jv],[rowguid],[mainkey],[sl_no],[pu_no],[aqd_no],[cc_no],[invsupno],[taxid])
         VALUES(s.[a_brno],s.[a_type],s.[a_ref],s.[a_mdate],s.[a_hdate],s.[a_text],s.[a_amt],s.[a_entries],s.[jhsrc],s.[a_sysdate],s.[jhcurr],s.[jhfcflag],s.[jhrate],s.[jhreleased],s.[a_posted],s.[lastupdt],s.[jhlccrttl],s.[jhlcdbttl],s.[jhfccrttl],s.[jhfcdbttl],s.[jhimgrate],s.[modified],s.[serial_no],s.[rcvdtrn],s.[suspend],s.[usrid],s.[isbrtrx],s.[brxref],s.[brxfrm],s.[jhcustno],s.[hide_jv],s.[rowguid],s.[mainkey],s.[sl_no],s.[pu_no],s.[aqd_no],s.[cc_no],s.[invsupno],s.[taxid]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=2	
			return	--@errstatus		 				
		end
		else commit transaction

		set @sqlmsg='merge acc_dtl as t 
		 using (select a.* from '+@dbc+'acc_dtl a with (NOLOCK) inner join '+@dbc+'acc_hdr b with (NOLOCK)
		 ON a.[a_brno] = b.[a_brno]  AND a.[a_ref] = b.[a_ref] AND a.[a_type] = b.[a_type] AND a.[pu_no] = b.[pu_no] AND a.[sl_no] = b.[sl_no]
		 where 1=1 ' + iif(@ok_pstd=1,' and b.a_posted=1 ','')
		+' and a.a_mdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[a_brno] = s.[a_brno] AND t.[a_folio] = s.[a_folio] AND t.[a_ref] = s.[a_ref] AND t.[a_type] = s.[a_type] AND t.[pu_no] = s.[pu_no] AND t.[sl_no] = s.[sl_no]
		 when not matched then
		 INSERT([a_brno],[a_type],[a_ref],[a_key],[a_mdate],[a_hdate],[a_text],[a_camt],[a_damt],[jddbcr],[jdfcamt],[jdcurr],[a_folio],[a_sysdate],[jdsrc],[jdfc_imgval],[jdcstval],[cstkey],[brnno],[bracc],[brxref],[match],[rplct_post],[rowguid],[taxcatId],[cu_no],[su_no],[sl_no],[pu_no],[cc_no])
         VALUES(s.[a_brno],s.[a_type],s.[a_ref],s.[a_key],s.[a_mdate],s.[a_hdate],s.[a_text],s.[a_camt],s.[a_damt],s.[jddbcr],s.[jdfcamt],s.[jdcurr],s.[a_folio],s.[a_sysdate],s.[jdsrc],s.[jdfc_imgval],s.[jdcstval],s.[cstkey],s.[brnno],s.[bracc],s.[brxref],s.[match],s.[rplct_post],s.[rowguid],s.[taxcatId],s.[cu_no],s.[su_no],s.[sl_no],s.[pu_no],s.[cc_no]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=3	
			return --@errstatus			 				
		end
		else commit transaction
	end
---  Sales Processing 
    if @ok_sl=1
	begin

------ Process Sales_hdr && Sales_dtl Table
		set @sqlmsg='merge sales_hdr as t 
		using (select * from '+@dbc+'sales_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
		+' and invmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[slcenter] = s.[slcenter]
		 when not matched then
		 INSERT([branch],[slcenter],[invtype],[ref],[invmdate],[invhdate],[custno],[text],[glser],[dsctype],[pstmode],[fcy],[fcyrate],[invttl],[invcst],[invdspc],[invdsvl],[nettotal],[invpaid],[casher],[entries],[released],[posted],[fixrate],[extamt],[extser],[pricetp],[ischeque],[chkno],[chkdate],[lastupdt],[jvgenrt],[cccommsn],[belowbal],[fcy2],[ccpayment],[rplsamt],[pdother],[slcode],[prpaidamt],[instldays],[instflag],[slcmnd],[inv_printed],[bendit],[modified],[rqstorder],[rqststld],[carrier],[rcvdtrn],[usrid],[address],[suspend],[rtnref],[ispurchase],[stkjvno],[posinv],[sanedcrd_amt],[rtncash_dfrpl],[invlocked],[remarks],[tax_amt_rcvd],[with_tax],[taxid],[tax_percent],[taxfree_amt],[reref],note2,note3,inv_mpay,cust_mobil)
         VALUES(s.[branch],s.[slcenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[custno],s.[text],s.[glser],s.[dsctype],s.[pstmode],s.[fcy],s.[fcyrate],s.[invttl],s.[invcst],s.[invdspc],s.[invdsvl],s.[nettotal],s.[invpaid],s.[casher],s.[entries],s.[released],s.[posted],s.[fixrate],s.[extamt],s.[extser],s.[pricetp],s.[ischeque],s.[chkno],s.[chkdate],s.[lastupdt],s.[jvgenrt],s.[cccommsn],s.[belowbal],s.[fcy2],s.[ccpayment],s.[rplsamt],s.[pdother],s.[slcode],s.[prpaidamt],s.[instldays],s.[instflag],s.[slcmnd],s.[inv_printed],s.[bendit],s.[modified],s.[rqstorder],s.[rqststld],s.[carrier],s.[rcvdtrn],s.[usrid],s.[address],s.[suspend],s.[rtnref],s.[ispurchase],s.[stkjvno],s.[posinv],s.[sanedcrd_amt],s.[rtncash_dfrpl],s.[invlocked],s.[remarks],s.[tax_amt_rcvd],s.[with_tax],s.[taxid],s.[tax_percent],s.[taxfree_amt],s.[reref],s.note2,s.note3,s.inv_mpay,s.cust_mobil);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=4	
			return	--@errstatus		 				
		end
		else commit transaction

		if @ok_dtl=1
	    begin
		set @sqlmsg='merge sales_dtl as t 
		using (select a.* from '+@dbc+'sales_dtl a with (NOLOCK) inner join '+@dbc+'sales_hdr b with (NOLOCK)
		 ON a.[branch] = b.[branch] AND a.[invtype] = b.[invtype] AND a.[ref] = b.[ref] AND a.[slcenter] = b.[slcenter]
	   	where 1=1 ' + iif(@ok_pstd=1,' and b.posted=1 ','')
		+' and a.invmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[slcenter] = s.[slcenter]
		 when not matched then
		 INSERT([branch],[slcenter],[invtype],[ref],[invmdate],[invhdate],[itemno],[unicode],[qty],[fqty],[price],[discpc],[cost],[ds_acfm],[sl_acfm],[gclass],[custno],[fcy],[barcode],[imfcval],[pack],[pkqty],[shadd],[shdpk],[shdqty],[frtqty],[rtnqty],[whno],[cmbkey],[folio],[rplct_post],[sold_item_status],[tax_amt],[tax_id],[stk_or_ser],[tax_prcent],[offer_amt])
         VALUES(s.[branch],s.[slcenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[price],s.[discpc],s.[cost],s.[ds_acfm],s.[sl_acfm],s.[gclass],s.[custno],s.[fcy],s.[barcode],s.[imfcval],s.[pack],s.[pkqty],s.[shadd],s.[shdpk],s.[shdqty],s.[frtqty],s.[rtnqty],s.[whno],s.[cmbkey],s.[folio],s.[rplct_post],s.[sold_item_status],s.[tax_amt],s.[tax_id],s.[stk_or_ser],s.[tax_prcent],s.[offer_amt]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=5	
			return	--@errstatus		 				
		end
		else commit transaction
		end

	end
----  Process pu_hdr & pu_dtl Table  
 	if @ok_pu=1 
	begin
------ sdaad_hd Table
		set @sqlmsg='merge pu_hdr as t 
		using (select * from '+@dbc+'pu_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
		+' and invmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[pucenter] = s.[pucenter]
		 when not matched then
		INSERT([branch],[pucenter],[invtype],[ref],[invmdate],[invhdate],[supno],[text],[glser],[dsctype],[pstmode],[cur],[currate],[invttl],[invcst],[invdspc],[invdsvl],[nettotal],[invpaid],[casher],[entries],[released],[posted],[fixrate],[extamt],[extser],[pricetp],[ischeque],[chkno],[chkdate],[lastupdt],[jvgenrt],[cccommsn],[belowbal],[invsupno],[ccpayment],[rplsamt],[pdother],[slcode],[prpaidamt],[instldays],[instflag],[slcmnd],[inv_printed],[bendit],[modified],[rqstorder],[rqststld],[carrier],[rcvdtrn],[usrid],[address],[wst_key],[wst_amt],[gmark],[tameen],[shahan],[msfr],[mother],[etax],[remarks],[tax_amt_paid],[with_tax],[taxid],[tax_percent],[taxfree_amt],[reref],[sabr])
        VALUES(s.[branch],s.[pucenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[supno],s.[text],s.[glser],s.[dsctype],s.[pstmode],s.[cur],s.[currate],s.[invttl],s.[invcst],s.[invdspc],s.[invdsvl],s.[nettotal],s.[invpaid],s.[casher],s.[entries],s.[released],s.[posted],s.[fixrate],s.[extamt],s.[extser],s.[pricetp],s.[ischeque],s.[chkno],s.[chkdate],s.[lastupdt],s.[jvgenrt],s.[cccommsn],s.[belowbal],s.[invsupno],s.[ccpayment],s.[rplsamt],s.[pdother],s.[slcode],s.[prpaidamt],s.[instldays],s.[instflag],s.[slcmnd],s.[inv_printed],s.[bendit],s.[modified],s.[rqstorder],s.[rqststld],s.[carrier],s.[rcvdtrn],s.[usrid],s.[address],s.[wst_key],s.[wst_amt],s.[gmark],s.[tameen],s.[shahan],s.[msfr],s.[mother],s.[etax],s.[remarks],s.[tax_amt_paid],s.[with_tax],s.[taxid],s.[tax_percent],s.[taxfree_amt],s.[reref],s.[sabr]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=6	
			return	--@errstatus		 				
		end
		else commit transaction

		if @ok_dtl=1
	    begin
		set @sqlmsg='merge pu_dtl as t 
		using (select a.* from '+@dbc+'pu_dtl a with (NOLOCK) inner join '+@dbc+'pu_hdr b with (NOLOCK)
		 ON a.[branch] = b.[branch] AND a.[invtype] = b.[invtype] AND a.[ref] = b.[ref] AND a.[pucenter] = b.[pucenter]
	   	where 1=1 ' + iif(@ok_pstd=1,' and b.posted=1 ','')
		+' and a.invmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[pucenter] = s.[pucenter]
		 when not matched then
		INSERT([branch],[pucenter],[invtype],[ref],[invmdate],[invhdate],[itemno],[unicode],[qty],[fqty],[price],[discpc],[cost],[ds_acfm],[sl_acfm],[gclass],[fullcost],[cur],[barcode],[imfcval],[pack],[pkqty],[shadd],[shdpk],[shdqty],[frtqty],[rtnqty],[whno],[cmbkey],[folio],[rplct_post],[sold_item_status],[tax_amt],[tax_id],[stk_or_ser],[tax_prcent],[expdate])
        VALUES(s.[branch],s.[pucenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[price],s.[discpc],s.[cost],s.[ds_acfm],s.[sl_acfm],s.[gclass],s.[fullcost],s.[cur],s.[barcode],s.[imfcval],s.[pack],s.[pkqty],s.[shadd],s.[shdpk],s.[shdqty],s.[frtqty],s.[rtnqty],s.[whno],s.[cmbkey],s.[folio],s.[rplct_post],s.[sold_item_status],s.[tax_amt],s.[tax_id],s.[stk_or_ser],s.[tax_prcent],s.[expdate]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=7	
			return	--@errstatus		 				
		end
		else commit transaction
		end

	end
----  Process Sto_hdr & Sto_dtl Table   
	if @ok_st=1 
	begin
	  
	  	set @sqlmsg='merge sto_hdr as t 
		using (select * from '+@dbc+'sto_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
		+' and trmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[ref] = s.[ref] AND t.[trtype] = s.[trtype]
		 when not matched then
		INSERT([branch],[trtype],[ref],[trmdate],[trhdate],[text],[amnttl],[costttl],[sysdate],[src],[released],[posted],[fcy],[fcyrate],[whno],[entries],[lastupdt],[towhno],[modified],[rcvdtrn],[supno],[usrid],[brsupp],[tobrno],[brxref],[glref],[isbrtrx],[asmtype],[repost],[remarks],[stkjvno])
        VALUES(s.[branch],s.[trtype],s.[ref],s.[trmdate],s.[trhdate],s.[text],s.[amnttl],s.[costttl],s.[sysdate],s.[src],s.[released],s.[posted],s.[fcy],s.[fcyrate],s.[whno],s.[entries],s.[lastupdt],s.[towhno],s.[modified],s.[rcvdtrn],s.[supno],s.[usrid],s.[brsupp],s.[tobrno],s.[brxref],s.[glref],s.[isbrtrx],s.[asmtype],s.[repost],s.[remarks],s.[stkjvno]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=8	
			return	--@errstatus		 				
		end
		else commit transaction

		if @ok_dtl=1
	    begin
		set @sqlmsg='merge sto_dtl as t 
		using (select a.* from '+@dbc+'sto_dtl a with (NOLOCK) inner join '+@dbc+'sto_hdr b with (NOLOCK)
		 ON a.[branch] = b.[branch] AND a.[trtype] = b.[trtype] AND a.[ref] = b.[ref]
	   	where 1=1 ' + iif(@ok_pstd=1,' and b.posted=1 ','')
		+' and a.trmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[ref] = s.[ref] AND t.[trtype] = s.[trtype]
		 when not matched then
		INSERT([branch],[trtype],[ref],[trmdate],[trhdate],[itemno],[unicode],[qty],[fqty],[whno],[binno],[lcost],[sysdate],[src],[lprice],[fcost],[fprice],[expdate],[towhno],[tobinno],[barcode],[cmbkey],[discpc],[pack],[pkqty],[shdpk],[shdqty],[folio],[rplct_post])
        VALUES(s.[branch],s.[trtype],s.[ref],s.[trmdate],s.[trhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[whno],s.[binno],s.[lcost],s.[sysdate],s.[src],s.[lprice],s.[fcost],s.[fprice],s.[expdate],s.[towhno],s.[tobinno],s.[barcode],s.[cmbkey],s.[discpc],s.[pack],s.[pkqty],s.[shdpk],s.[shdqty],s.[folio],s.[rplct_post]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=9	
			return	--@errstatus		 				
		end
		else commit transaction
		end

	end
-----  Process customers Table
	 if @ok_cu=1
	begin
	 		
     set @sqlmsg='merge customers as t 
		using  '+@dbc+'customers as s
		 ON t.[cf_fcy] = s.[cf_fcy] AND t.[cu_brno] = s.[cu_brno] AND t.[cu_no] = s.[cu_no]
		 when not matched then
		INSERT([cu_no],[cu_brno],[cu_name],[cu_class],[cu_addrs],[cu_tel],[cu_fax],[cu_tlx],[cu_email],[cu_cntactp],[cu_title],[cu_crlmt],[cu_pymnt],[cu_status],[cu_opnbal],[cu_curbal],[cf_fcy],[cf_opnfcy],[cf_curfcy],[cu_xrf],[cu_alwcr],[cu_ctlser],[lastupdt],[cu_lcaloc],[cu_fcaloc],[modified],[cmncode],[cu_lname],[cu_city],[cu_country],[cu_laddrs],[cu_mobile],[cu_sendsms],[rowguid],[whno],[vndr_taxcode],[taxfree],[cu_kind],[section],[inactive],[cu_note])
        VALUES(s.[cu_no],s.[cu_brno],s.[cu_name],s.[cu_class],s.[cu_addrs],s.[cu_tel],s.[cu_fax],s.[cu_tlx],s.[cu_email],s.[cu_cntactp],s.[cu_title],s.[cu_crlmt],s.[cu_pymnt],s.[cu_status],s.[cu_opnbal],s.[cu_curbal],s.[cf_fcy],s.[cf_opnfcy],s.[cf_curfcy],s.[cu_xrf],s.[cu_alwcr],s.[cu_ctlser],s.[lastupdt],s.[cu_lcaloc],s.[cu_fcaloc],s.[modified],s.[cmncode],s.[cu_lname],s.[cu_city],s.[cu_country],s.[cu_laddrs],s.[cu_mobile],s.[cu_sendsms],s.[rowguid],s.[whno],s.[vndr_taxcode],s.[taxfree],s.[cu_kind],s.[section],s.[inactive],s.[cu_note]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=10	
			return	--@errstatus		 				
		end
		else commit transaction

	end

-----  Process suppliers Table

	 if @ok_su=1
	begin
	 		
     set @sqlmsg='merge suppliers as t 
		using  '+@dbc+'suppliers as s
		 ON t.[su_brno] = s.[su_brno] AND t.[su_cur] = s.[su_cur] AND t.[su_no] = s.[su_no]
		 when not matched then
		INSERT([su_no],[su_brno],[su_name],[su_class],[su_addrs],[su_tel],[su_fax],[su_tlx],[su_email],[su_cntactp],[su_title],[su_crlmt],[su_pymnt],[su_status],[su_opnbal],[su_curbal],[su_cur],[su_opnfcy],[cf_curfcy],[su_xrf],[su_alwcr],[su_ctlser],[lastupdt],[su_lcaloc],[su_fcaloc],[modified],[cmncode],[su_lname],[su_city],[su_country],[su_laddrs],[su_mobile],[su_sendsms],[rowguid],[whno],[vndr_taxcode],[taxfree],[su_kind],[section],[inactive],[is_shamel_tax],[su_note])
        VALUES(s.[su_no],s.[su_brno],s.[su_name],s.[su_class],s.[su_addrs],s.[su_tel],s.[su_fax],s.[su_tlx],s.[su_email],s.[su_cntactp],s.[su_title],s.[su_crlmt],s.[su_pymnt],s.[su_status],s.[su_opnbal],s.[su_curbal],s.[su_cur],s.[su_opnfcy],s.[cf_curfcy],s.[su_xrf],s.[su_alwcr],s.[su_ctlser],s.[lastupdt],s.[su_lcaloc],s.[su_fcaloc],s.[modified],s.[cmncode],s.[su_lname],s.[su_city],s.[su_country],s.[su_laddrs],s.[su_mobile],s.[su_sendsms],s.[rowguid],s.[whno],s.[vndr_taxcode],s.[taxfree],s.[su_kind],s.[section],s.[inactive],s.[is_shamel_tax],s.[su_note]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=11	
			return	--@errstatus		 				
		end
		else commit transaction

	end

-----  Process items & items_bc & units Table

	if @ok_it=1
	begin
	 		
     set @sqlmsg='merge items as t 
		using  '+@dbc+'items  as s
		 ON t.[item_no] = s.[item_no]
		 when not matched then
		INSERT([item_no],[item_name],[item_cost],[item_price],[item_barcode],[item_unit],[item_obalance],[item_cbalance],[item_group],[item_image],[item_req],[item_tax],[unit2],[uq2],[unit2p],[unit3],[uq3],[unit3p],[unit4],[uq4],[unit4p],[item_ename],[item_opencost],[item_curcost],[supno],[note],[last_updt],[sgroup],[price2],[price3],[min_price],[static_cost],[inactive],dunit)
        VALUES(s.[item_no],s.[item_name],s.[item_cost],s.[item_price],s.[item_barcode],s.[item_unit],s.[item_obalance],s.[item_cbalance],s.[item_group],s.[item_image],s.[item_req],s.[item_tax],s.[unit2],s.[uq2],s.[unit2p],s.[unit3],s.[uq3],s.[unit3p],s.[unit4],s.[uq4],s.[unit4p],s.[item_ename],s.[item_opencost],s.[item_curcost],s.[supno],s.[note],s.[last_updt],s.[sgroup],s.[price2],s.[price3],s.[min_price],s.[static_cost],s.[inactive],s.dunit);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=12	
			return	--@errstatus		 				
		end
		else commit transaction

		set @sqlmsg='merge items_bc as t 
		using  '+@dbc+'items_bc as s
		 ON t.[barcode] = s.[barcode] AND t.[br_no] = s.[br_no] AND t.[sl_no] = s.[sl_no]
		 when not matched then
		INSERT([item_no],[barcode],[pack],[pk_qty],[price],[note],[pkorder],[price2],[price3],[min_price],[br_no],[sl_no])
         VALUES(s.[item_no],s.[barcode],s.[pack],s.[pk_qty],s.[price],s.[note],s.[pkorder],s.[price2],s.[price3],s.[min_price],s.[br_no],s.[sl_no]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=13	
			return	--@errstatus		 				
		end
		else commit transaction

		set @sqlmsg='merge units as t 
		using  '+@dbc+'units  as s
		 ON t.[unit_id] = s.[unit_id]
		 when not matched then
		INSERT([unit_id],[unit_name],[unit_desc],[unit_order],[unit_qty])
        VALUES(s.[unit_id],s.[unit_name],s.[unit_desc],s.[unit_order],s.[unit_qty]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=14	
			return	--@errstatus		 				
		end
		else commit transaction

	end
	return	@errstatus
end
GO
/****** Object:  StoredProcedure [dbo].[copy_data_by_link_test]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[copy_data_by_link_test]
	@fmdate varchar(8),
	@todate varchar(8),
	@link nvarchar(100),
	@db_nam nvarchar(20),

	@ok_as bit,
	@ok_ac bit,
	@ok_sl bit,
	@ok_pu bit,
	@ok_st bit,
		
	@ok_it bit,
	--@ok_gr bit,
	@ok_su bit,
	@ok_cu bit,
	@ok_pstd bit,

	@errstatus int = 0 output,@timestamp float = 0 output
 
AS
BEGIN

DECLARE @StartTime datetime,@EndTime datetime   
        set @StartTime=GETDATE() 
    -- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

   Declare @dbc nvarchar(50),
		   @sqlmsg nvarchar(max)

   set @dbc=@link+'.'+@db_nam+'.dbo.'

----  Process accounts & acc_hdr & acc_dtl Table 

  if @ok_as=1
	begin
	 		
     set @sqlmsg='merge accounts as t 
		using  '+@dbc+'accounts  as s
		 ON t.[a_key] = s.[a_key] AND t.[glcurrency] = s.[glcurrency]
		 when not matched then
		INSERT([a_key],[a_parent],[a_name],[a_ename],[a_brno],[a_opnbal],[a_curbal],[a_crtdate],[a_lastupdt],[a_group],[a_type],[a_ptype],[a_active],[a_level],[glpurge],[accontrol],[glcurrency],[fccurbal],[fcopnbal],[imopnbal],[imcurbal],[gleval],[acprotect],[pkey],[chkey],[cashbnk],[modified],[section],[isbracc],[acckind],[a_note])
         VALUES(s.[a_key],s.[a_parent],s.[a_name],s.[a_ename],s.[a_brno],s.[a_opnbal],s.[a_curbal],s.[a_crtdate],s.[a_lastupdt],s.[a_group],s.[a_type],s.[a_ptype],s.[a_active],s.[a_level],s.[glpurge],s.[accontrol],s.[glcurrency],s.[fccurbal],s.[fcopnbal],s.[imopnbal],s.[imcurbal],s.[gleval],s.[acprotect],s.[pkey],s.[chkey],s.[cashbnk],s.[modified],s.[section],s.[isbracc],s.[acckind],s.[a_note]);'
		
		begin transaction

		BEGIN TRY

		    exec sp_executesql @sqlmsg
		    COMMIT TRANSACTION

        END TRY

        BEGIN CATCH

            ROLLBACK TRANSACTION
		    set @errstatus=1
		    return

        END CATCH
				
			

	end

    if @ok_ac=1
	begin

		
		set @sqlmsg='merge acc_hdr as t 
		using (select * from '+@dbc+'acc_hdr with (NOLOCK) where 1=1 '+ iif(@ok_pstd=1,' and a_posted=1 ','')
		+' and a_mdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[a_brno] = s.[a_brno] AND t.[a_ref] = s.[a_ref] AND t.[a_type] = s.[a_type] AND t.[pu_no] = s.[pu_no] AND t.[sl_no] = s.[sl_no]
		 when not matched then
		 INSERT([a_brno],[a_type],[a_ref],[a_mdate],[a_hdate],[a_text],[a_amt],[a_entries],[jhsrc],[a_sysdate],[jhcurr],[jhfcflag],[jhrate],[jhreleased],[a_posted],[lastupdt],[jhlccrttl],[jhlcdbttl],[jhfccrttl],[jhfcdbttl],[jhimgrate],[modified],[serial_no],[rcvdtrn],[suspend],[usrid],[isbrtrx],[brxref],[brxfrm],[jhcustno],[hide_jv],[rowguid],[mainkey],[sl_no],[pu_no],[aqd_no],[cc_no],[invsupno],[taxid])
         VALUES(s.[a_brno],s.[a_type],s.[a_ref],s.[a_mdate],s.[a_hdate],s.[a_text],s.[a_amt],s.[a_entries],s.[jhsrc],s.[a_sysdate],s.[jhcurr],s.[jhfcflag],s.[jhrate],s.[jhreleased],s.[a_posted],s.[lastupdt],s.[jhlccrttl],s.[jhlcdbttl],s.[jhfccrttl],s.[jhfcdbttl],s.[jhimgrate],s.[modified],s.[serial_no],s.[rcvdtrn],s.[suspend],s.[usrid],s.[isbrtrx],s.[brxref],s.[brxfrm],s.[jhcustno],s.[hide_jv],s.[rowguid],s.[mainkey],s.[sl_no],s.[pu_no],s.[aqd_no],s.[cc_no],s.[invsupno],s.[taxid]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=2	
			return	--@errstatus		 				
		end
		else commit transaction

		set @sqlmsg='merge acc_dtl as t 
		using (select * from '+@dbc+'acc_dtl with (NOLOCK) where 1=1 ' -- + iif(@ok_pstd=1,' and a_posted=1 ','')
		+' and a_mdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[a_brno] = s.[a_brno] AND t.[a_folio] = s.[a_folio] AND t.[a_ref] = s.[a_ref] AND t.[a_type] = s.[a_type] AND t.[pu_no] = s.[pu_no] AND t.[sl_no] = s.[sl_no]
		 when not matched then
		 INSERT([a_brno],[a_type],[a_ref],[a_key],[a_mdate],[a_hdate],[a_text],[a_camt],[a_damt],[jddbcr],[jdfcamt],[jdcurr],[a_folio],[a_sysdate],[jdsrc],[jdfc_imgval],[jdcstval],[cstkey],[brnno],[bracc],[brxref],[match],[rplct_post],[rowguid],[taxcatId],[cu_no],[su_no],[sl_no],[pu_no],[cc_no])
         VALUES(s.[a_brno],s.[a_type],s.[a_ref],s.[a_key],s.[a_mdate],s.[a_hdate],s.[a_text],s.[a_camt],s.[a_damt],s.[jddbcr],s.[jdfcamt],s.[jdcurr],s.[a_folio],s.[a_sysdate],s.[jdsrc],s.[jdfc_imgval],s.[jdcstval],s.[cstkey],s.[brnno],s.[bracc],s.[brxref],s.[match],s.[rplct_post],s.[rowguid],s.[taxcatId],s.[cu_no],s.[su_no],s.[sl_no],s.[pu_no],s.[cc_no]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=3	
			return --@errstatus			 				
		end
		else commit transaction
	end
---  Sales Processing 
    if @ok_sl=1
	begin

------ Process Sales_hdr && Sales_dtl Table
		set @sqlmsg='merge sales_hdr as t 
		using (select * from '+@dbc+'sales_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
		+' and invmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[slcenter] = s.[slcenter]
		 when not matched then
		 INSERT([branch],[slcenter],[invtype],[ref],[invmdate],[invhdate],[custno],[text],[glser],[dsctype],[pstmode],[fcy],[fcyrate],[invttl],[invcst],[invdspc],[invdsvl],[nettotal],[invpaid],[casher],[entries],[released],[posted],[fixrate],[extamt],[extser],[pricetp],[ischeque],[chkno],[chkdate],[lastupdt],[jvgenrt],[cccommsn],[belowbal],[fcy2],[ccpayment],[rplsamt],[pdother],[slcode],[prpaidamt],[instldays],[instflag],[slcmnd],[inv_printed],[bendit],[modified],[rqstorder],[rqststld],[carrier],[rcvdtrn],[usrid],[address],[suspend],[rtnref],[ispurchase],[stkjvno],[posinv],[sanedcrd_amt],[rtncash_dfrpl],[invlocked],[remarks],[tax_amt_rcvd],[with_tax],[taxid],[tax_percent],[taxfree_amt],[reref])
         VALUES(s.[branch],s.[slcenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[custno],s.[text],s.[glser],s.[dsctype],s.[pstmode],s.[fcy],s.[fcyrate],s.[invttl],s.[invcst],s.[invdspc],s.[invdsvl],s.[nettotal],s.[invpaid],s.[casher],s.[entries],s.[released],s.[posted],s.[fixrate],s.[extamt],s.[extser],s.[pricetp],s.[ischeque],s.[chkno],s.[chkdate],s.[lastupdt],s.[jvgenrt],s.[cccommsn],s.[belowbal],s.[fcy2],s.[ccpayment],s.[rplsamt],s.[pdother],s.[slcode],s.[prpaidamt],s.[instldays],s.[instflag],s.[slcmnd],s.[inv_printed],s.[bendit],s.[modified],s.[rqstorder],s.[rqststld],s.[carrier],s.[rcvdtrn],s.[usrid],s.[address],s.[suspend],s.[rtnref],s.[ispurchase],s.[stkjvno],s.[posinv],s.[sanedcrd_amt],s.[rtncash_dfrpl],s.[invlocked],s.[remarks],s.[tax_amt_rcvd],s.[with_tax],s.[taxid],s.[tax_percent],s.[taxfree_amt],s.[reref]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=4	
			return	--@errstatus		 				
		end
		else commit transaction

		set @sqlmsg='merge sales_dtl as t 
		using (select * from '+@dbc+'sales_dtl with (NOLOCK) where 1=1 ' -- + iif(@ok_pstd=1,' and a_posted=1 ','')
		+' and invmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[slcenter] = s.[slcenter]
		 when not matched then
		 INSERT([branch],[slcenter],[invtype],[ref],[invmdate],[invhdate],[itemno],[unicode],[qty],[fqty],[price],[discpc],[cost],[ds_acfm],[sl_acfm],[gclass],[custno],[fcy],[barcode],[imfcval],[pack],[pkqty],[shadd],[shdpk],[shdqty],[frtqty],[rtnqty],[whno],[cmbkey],[folio],[rplct_post],[sold_item_status],[tax_amt],[tax_id],[stk_or_ser],[tax_prcent],[offer_amt])
         VALUES(s.[branch],s.[slcenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[price],s.[discpc],s.[cost],s.[ds_acfm],s.[sl_acfm],s.[gclass],s.[custno],s.[fcy],s.[barcode],s.[imfcval],s.[pack],s.[pkqty],s.[shadd],s.[shdpk],s.[shdqty],s.[frtqty],s.[rtnqty],s.[whno],s.[cmbkey],s.[folio],s.[rplct_post],s.[sold_item_status],s.[tax_amt],s.[tax_id],s.[stk_or_ser],s.[tax_prcent],s.[offer_amt]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=5	
			return	--@errstatus		 				
		end
		else commit transaction
		
	end
----  Process pu_hdr & pu_dtl Table  
 	if @ok_pu=1 
	begin
------ sdaad_hd Table
		set @sqlmsg='merge pu_hdr as t 
		using (select * from '+@dbc+'pu_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
		+' and invmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[pucenter] = s.[pucenter]
		 when not matched then
		INSERT([branch],[pucenter],[invtype],[ref],[invmdate],[invhdate],[supno],[text],[glser],[dsctype],[pstmode],[cur],[currate],[invttl],[invcst],[invdspc],[invdsvl],[nettotal],[invpaid],[casher],[entries],[released],[posted],[fixrate],[extamt],[extser],[pricetp],[ischeque],[chkno],[chkdate],[lastupdt],[jvgenrt],[cccommsn],[belowbal],[invsupno],[ccpayment],[rplsamt],[pdother],[slcode],[prpaidamt],[instldays],[instflag],[slcmnd],[inv_printed],[bendit],[modified],[rqstorder],[rqststld],[carrier],[rcvdtrn],[usrid],[address],[wst_key],[wst_amt],[gmark],[tameen],[shahan],[msfr],[mother],[etax],[remarks],[tax_amt_paid],[with_tax],[taxid],[tax_percent],[taxfree_amt],[reref],[sabr])
        VALUES(s.[branch],s.[pucenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[supno],s.[text],s.[glser],s.[dsctype],s.[pstmode],s.[cur],s.[currate],s.[invttl],s.[invcst],s.[invdspc],s.[invdsvl],s.[nettotal],s.[invpaid],s.[casher],s.[entries],s.[released],s.[posted],s.[fixrate],s.[extamt],s.[extser],s.[pricetp],s.[ischeque],s.[chkno],s.[chkdate],s.[lastupdt],s.[jvgenrt],s.[cccommsn],s.[belowbal],s.[invsupno],s.[ccpayment],s.[rplsamt],s.[pdother],s.[slcode],s.[prpaidamt],s.[instldays],s.[instflag],s.[slcmnd],s.[inv_printed],s.[bendit],s.[modified],s.[rqstorder],s.[rqststld],s.[carrier],s.[rcvdtrn],s.[usrid],s.[address],s.[wst_key],s.[wst_amt],s.[gmark],s.[tameen],s.[shahan],s.[msfr],s.[mother],s.[etax],s.[remarks],s.[tax_amt_paid],s.[with_tax],s.[taxid],s.[tax_percent],s.[taxfree_amt],s.[reref],s.[sabr]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=6	
			return	--@errstatus		 				
		end
		else commit transaction

		set @sqlmsg='merge pu_dtl as t 
		using (select * from '+@dbc+'pu_dtl with (NOLOCK) where 1=1 ' --+ iif(@ok_pstd=1,' and posted=1 ','')
		+' and invmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[pucenter] = s.[pucenter]
		 when not matched then
		INSERT([branch],[pucenter],[invtype],[ref],[invmdate],[invhdate],[itemno],[unicode],[qty],[fqty],[price],[discpc],[cost],[ds_acfm],[sl_acfm],[gclass],[fullcost],[cur],[barcode],[imfcval],[pack],[pkqty],[shadd],[shdpk],[shdqty],[frtqty],[rtnqty],[whno],[cmbkey],[folio],[rplct_post],[sold_item_status],[tax_amt],[tax_id],[stk_or_ser],[tax_prcent],[expdate])
        VALUES(s.[branch],s.[pucenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[price],s.[discpc],s.[cost],s.[ds_acfm],s.[sl_acfm],s.[gclass],s.[fullcost],s.[cur],s.[barcode],s.[imfcval],s.[pack],s.[pkqty],s.[shadd],s.[shdpk],s.[shdqty],s.[frtqty],s.[rtnqty],s.[whno],s.[cmbkey],s.[folio],s.[rplct_post],s.[sold_item_status],s.[tax_amt],s.[tax_id],s.[stk_or_ser],s.[tax_prcent],s.[expdate]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=7	
			return	--@errstatus		 				
		end
		else commit transaction
		

	end
----  Process Sto_hdr & Sto_dtl Table   
	if @ok_st=1 
	begin
	  
	  	set @sqlmsg='merge sto_hdr as t 
		using (select * from '+@dbc+'sto_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
		+' and trmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[ref] = s.[ref] AND t.[trtype] = s.[trtype]
		 when not matched then
		INSERT([branch],[trtype],[ref],[trmdate],[trhdate],[text],[amnttl],[costttl],[sysdate],[src],[released],[posted],[fcy],[fcyrate],[whno],[entries],[lastupdt],[towhno],[modified],[rcvdtrn],[supno],[usrid],[brsupp],[tobrno],[brxref],[glref],[isbrtrx],[asmtype],[repost],[remarks],[stkjvno])
        VALUES(s.[branch],s.[trtype],s.[ref],s.[trmdate],s.[trhdate],s.[text],s.[amnttl],s.[costttl],s.[sysdate],s.[src],s.[released],s.[posted],s.[fcy],s.[fcyrate],s.[whno],s.[entries],s.[lastupdt],s.[towhno],s.[modified],s.[rcvdtrn],s.[supno],s.[usrid],s.[brsupp],s.[tobrno],s.[brxref],s.[glref],s.[isbrtrx],s.[asmtype],s.[repost],s.[remarks],s.[stkjvno]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=8	
			return	--@errstatus		 				
		end
		else commit transaction

		set @sqlmsg='merge sto_dtl as t 
		using (select * from '+@dbc+'sto_dtl with (NOLOCK) where 1=1 '
		+' and trmdate between '+@fmdate+' and '+@todate+') as s
		 ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[ref] = s.[ref] AND t.[trtype] = s.[trtype]
		 when not matched then
		INSERT([branch],[trtype],[ref],[trmdate],[trhdate],[itemno],[unicode],[qty],[fqty],[whno],[binno],[lcost],[sysdate],[src],[lprice],[fcost],[fprice],[expdate],[towhno],[tobinno],[barcode],[cmbkey],[discpc],[pack],[pkqty],[shdpk],[shdqty],[folio],[rplct_post])
        VALUES(s.[branch],s.[trtype],s.[ref],s.[trmdate],s.[trhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[whno],s.[binno],s.[lcost],s.[sysdate],s.[src],s.[lprice],s.[fcost],s.[fprice],s.[expdate],s.[towhno],s.[tobinno],s.[barcode],s.[cmbkey],s.[discpc],s.[pack],s.[pkqty],s.[shdpk],s.[shdqty],s.[folio],s.[rplct_post]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=9	
			return	--@errstatus		 				
		end
		else commit transaction

	end
-----  Process customers Table
	 if @ok_cu=1
	begin
	 		
     set @sqlmsg='merge customers as t 
		using  '+@dbc+'customers as s
		 ON t.[cf_fcy] = s.[cf_fcy] AND t.[cu_brno] = s.[cu_brno] AND t.[cu_no] = s.[cu_no]
		 when not matched then
		INSERT([cu_no],[cu_brno],[cu_name],[cu_class],[cu_addrs],[cu_tel],[cu_fax],[cu_tlx],[cu_email],[cu_cntactp],[cu_title],[cu_crlmt],[cu_pymnt],[cu_status],[cu_opnbal],[cu_curbal],[cf_fcy],[cf_opnfcy],[cf_curfcy],[cu_xrf],[cu_alwcr],[cu_ctlser],[lastupdt],[cu_lcaloc],[cu_fcaloc],[modified],[cmncode],[cu_lname],[cu_city],[cu_country],[cu_laddrs],[cu_mobile],[cu_sendsms],[rowguid],[whno],[vndr_taxcode],[taxfree],[cu_kind],[section],[inactive],[cu_note])
        VALUES(s.[cu_no],s.[cu_brno],s.[cu_name],s.[cu_class],s.[cu_addrs],s.[cu_tel],s.[cu_fax],s.[cu_tlx],s.[cu_email],s.[cu_cntactp],s.[cu_title],s.[cu_crlmt],s.[cu_pymnt],s.[cu_status],s.[cu_opnbal],s.[cu_curbal],s.[cf_fcy],s.[cf_opnfcy],s.[cf_curfcy],s.[cu_xrf],s.[cu_alwcr],s.[cu_ctlser],s.[lastupdt],s.[cu_lcaloc],s.[cu_fcaloc],s.[modified],s.[cmncode],s.[cu_lname],s.[cu_city],s.[cu_country],s.[cu_laddrs],s.[cu_mobile],s.[cu_sendsms],s.[rowguid],s.[whno],s.[vndr_taxcode],s.[taxfree],s.[cu_kind],s.[section],s.[inactive],s.[cu_note]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=10	
			return	--@errstatus		 				
		end
		else commit transaction

	end

-----  Process suppliers Table

	 if @ok_su=1
	begin
	 		
     set @sqlmsg='merge suppliers as t 
		using  '+@dbc+'suppliers as s
		 ON t.[su_brno] = s.[su_brno] AND t.[su_cur] = s.[su_cur] AND t.[su_no] = s.[su_no]
		 when not matched then
		INSERT([su_no],[su_brno],[su_name],[su_class],[su_addrs],[su_tel],[su_fax],[su_tlx],[su_email],[su_cntactp],[su_title],[su_crlmt],[su_pymnt],[su_status],[su_opnbal],[su_curbal],[su_cur],[su_opnfcy],[cf_curfcy],[su_xrf],[su_alwcr],[su_ctlser],[lastupdt],[su_lcaloc],[su_fcaloc],[modified],[cmncode],[su_lname],[su_city],[su_country],[su_laddrs],[su_mobile],[su_sendsms],[rowguid],[whno],[vndr_taxcode],[taxfree],[su_kind],[section],[inactive],[is_shamel_tax],[su_note)
        VALUES(s.[su_no],s.[su_brno],s.[su_name],s.[su_class],s.[su_addrs],s.[su_tel],s.[su_fax],s.[su_tlx],s.[su_email],s.[su_cntactp],s.[su_title],s.[su_crlmt],s.[su_pymnt],s.[su_status],s.[su_opnbal],s.[su_curbal],s.[su_cur],s.[su_opnfcy],s.[cf_curfcy],s.[su_xrf],s.[su_alwcr],s.[su_ctlser],s.[lastupdt],s.[su_lcaloc],s.[su_fcaloc],s.[modified],s.[cmncode],s.[su_lname],s.[su_city],s.[su_country],s.[su_laddrs],s.[su_mobile],s.[su_sendsms],s.[rowguid],s.[whno],s.[vndr_taxcode],s.[taxfree],s.[su_kind],s.[section],s.[inactive],s.[is_shamel_tax],s.[su_note]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=11	
			return	--@errstatus		 				
		end
		else commit transaction

	end

-----  Process items & items_bc & units Table

	if @ok_it=1
	begin
	 		
     set @sqlmsg='merge items as t 
		using  '+@dbc+'items  as s
		 ON t.[item_no] = s.[item_no]
		 when not matched then
		INSERT([item_no],[item_name],[item_cost],[item_price],[item_barcode],[item_unit],[item_obalance],[item_cbalance],[item_group],[item_image],[item_req],[item_tax],[unit2],[uq2],[unit2p],[unit3],[uq3],[unit3p],[unit4],[uq4],[unit4p],[item_ename],[item_opencost],[item_curcost],[supno],[note],[last_updt],[sgroup],[price2],[price3],[min_price],[static_cost],[inactive])
        VALUES(s.[item_no],s.[item_name],s.[item_cost],s.[item_price],s.[item_barcode],s.[item_unit],s.[item_obalance],s.[item_cbalance],s.[item_group],s.[item_image],s.[item_req],s.[item_tax],s.[unit2],s.[uq2],s.[unit2p],s.[unit3],s.[uq3],s.[unit3p],s.[unit4],s.[uq4],s.[unit4p],s.[item_ename],s.[item_opencost],s.[item_curcost],s.[supno],s.[note],s.[last_updt],s.[sgroup],s.[price2],s.[price3],s.[min_price],s.[static_cost],s.[inactive]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=12	
			return	--@errstatus		 				
		end
		else commit transaction

		set @sqlmsg='merge items_bc as t 
		using  '+@dbc+'items_bc as s
		 ON t.[barcode] = s.[barcode] AND t.[br_no] = s.[br_no] AND t.[sl_no] = s.[sl_no]
		 when not matched then
		INSERT([item_no],[barcode],[pack],[pk_qty],[price],[note],[pkorder],[price2],[price3],[min_price],[br_no],[sl_no])
         VALUES(s.[item_no],s.[barcode],s.[pack],s.[pk_qty],s.[price],s.[note],s.[pkorder],s.[price2],s.[price3],s.[min_price],s.[br_no],s.[sl_no]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=13	
			return	--@errstatus		 				
		end
		else commit transaction

		set @sqlmsg='merge units as t 
		using  '+@dbc+'units  as s
		 ON t.[unit_id] = s.[unit_id]
		 when not matched then
		INSERT([unit_id],[unit_name],[unit_desc],[unit_order],[unit_qty])
        VALUES(s.[unit_id],s.[unit_name],s.[unit_desc],s.[unit_order],s.[unit_qty]);'
		
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=14	
			return	--@errstatus		 				
		end
		else commit transaction

	end
	SET     @EndTime = GETDATE()
	set @timestamp=(SELECT DATEDIFF(ms,@StartTime,@EndTime)/1000)
	return	@errstatus
	return @timestamp




--SELECT DATEDIFF(ms,@StartTime,@EndTime)/1000 AS [Duration in milliseconds] 
end
GO
/****** Object:  StoredProcedure [dbo].[copy_data_from_old_db]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[copy_data_from_old_db]

@old_db nvarchar(10),
@errstatus int = 0 output
--@branch_no varchar(2)

AS
BEGIN

--  EXEC [dbo].[delete_files_data]
	declare @Cursor_name Cursor , @db_tb nvarchar(50) --,@TEMP  float
	    -- set @cur=''
         set @Cursor_name = Cursor for
         select tbl_name from dbmstr.dbo.tbl_to_copy order by tbl_id -- where cf_fcy=@cur and su_brno=@branch_no
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @db_tb 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

   
	
declare   
@sqlmsg nvarchar(max)='' --, @tb nvarchar(50)=@db_tb

--  set @sqlmsg=' delete from '+ @db_tb + ' ;'

set @sqlmsg= @sqlmsg +  'insert into '+@db_tb+' select * from '+@old_db+'.dbo.'+@db_tb+' ;'

	begin transaction
	exec sp_executesql @sqlmsg
	--set @No_items_updated=@@ROWCOUNT
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=1
		return 
	end
	commit transaction

          fetch next
          From @Cursor_name into @db_tb 

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
END









GO
/****** Object:  StoredProcedure [dbo].[copy_stock_balances_from_old_db]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[copy_stock_balances_from_old_db]
 @db nvarchar(50),
 @whno nvarchar(2),
 @allwh bit,
 --@multibarcode bit,
 --@uniqu_bcs bit , -- multi branch prices
 @errstatus int = 0 output
	
AS
BEGIN
	declare @sqlmsg nvarchar(max)
	declare @tbl nvarchar(50)

	
	-----------------------------copy_items

	set @errstatus=0

	set @tbl=@db+'.dbo.'+'items as s '
	set @sqlmsg=' merge ITEMS as t '
	set @sqlmsg= @sqlmsg+' using '+@tbl
	set @sqlmsg= @sqlmsg+' on t.item_no = s.item_no '
	set @sqlmsg= @sqlmsg+' when matched then '
	set @sqlmsg= @sqlmsg+' update set t.item_cost = s.item_cost '
	set @sqlmsg= @sqlmsg+' when not matched then '
	set @sqlmsg= @sqlmsg+' INSERT VALUES(s.[item_no],s.[item_name],s.[item_cost],s.[item_price],s.[item_barcode],s.[item_unit],s.[item_obalance],s.[item_cbalance],s.[item_group],s.[item_image],s.[item_req],s.[item_tax],s.[unit2],s.[uq2],s.[unit2p],s.[unit3],s.[uq3],s.[unit3p],s.[unit4],s.[uq4],s.[unit4p],s.[item_ename],s.[item_opencost],s.[item_curcost],s.[supno],s.[note],s.[last_updt],s.[sgroup],s.[price2],s.[price3],s.[min_price],s.[static_cost],s.[inactive],s.[dunit]);'


		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=-1
			return 
		end
		else commit transaction

	


	---------------------  copy items_bc

	set @tbl=@db+'.dbo.'+'items_bc as s '
	set @sqlmsg=' merge items_bc as t '
	set  @sqlmsg= @sqlmsg+' using '+@tbl
	set  @sqlmsg= @sqlmsg+' on  t.barcode=s.barcode and t.[br_no]=s.[br_no] and t.[sl_no]=s.[sl_no]'
	set  @sqlmsg= @sqlmsg+' when not matched then '
	set  @sqlmsg= @sqlmsg+' insert VALUES(s.[item_no],s.[barcode],s.[pack],s.[pk_qty],s.[price],s.[note],s.[pkorder],s.[price2],s.[price3],s.[min_price],s.[br_no],s.[sl_no]);'

		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=-2
			return 
		end
		else commit transaction


	---------------------  copy brprices

		
	set @tbl=@db+'.dbo.'+'brprices as s '
	set @sqlmsg='merge brprices as t '
	set @sqlmsg= @sqlmsg+' using '+@tbl
	set @sqlmsg= @sqlmsg+' on t.branch=s.branch and t.slcenter=s.slcenter and t.itemno=s.itemno '
	set @sqlmsg= @sqlmsg+' when not matched then '
	set @sqlmsg= @sqlmsg+' INSERT([branch],[slcenter],[itemno],[lprice1],[barcode]) VALUES(s.[branch],s.[slcenter],s.[itemno],s.[lprice1],s.[barcode]);'
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=-3
			return 
		end
		else commit transaction

	--------------------- copy whbins

	set @tbl=@db+'.dbo.'+'whbins as s '
	set @sqlmsg=' merge whbins as t '
	--set @sqlmsg= @sqlmsg+' using '+@tbl
	set @sqlmsg= @sqlmsg+' using (select br_no, a.item_no, unicode, wh_no, bin_no, qty, rsvqty, openbal,' + iif(@allwh=0,' (round((case when item_curcost>0 then item_curcost when item_curcost=0 then static_cost else item_curcost end),2)) ',' lcost ') + '  lcost, fcost, openlcost, openfcost, expdate from '+@db+'.dbo.'+'whbins a join '+@db+'.dbo.'+'items b on a.item_no=b.item_no ' + iif(@allwh=0,'',' and a.wh_no='''+@whno+'''') + ') as s'
	set @sqlmsg= @sqlmsg+' on t.br_no=s.br_no and t.item_no=s.item_no and t.unicode=s.unicode and t.wh_no=s.wh_no and t.bin_no=s.bin_no and t.expdate=s.expdate '
	set @sqlmsg= @sqlmsg+' when matched then '
	set @sqlmsg= @sqlmsg+' update set t.openbal=s.qty+ s.[openbal],t.openlcost=s.lcost,t.openfcost=s.fcost '
	set @sqlmsg= @sqlmsg+' when not matched then '
	set @sqlmsg= @sqlmsg+' insert VALUES(s.[br_no],s.[item_no],s.[unicode],s.[wh_no],s.[bin_no],s.[qty] + s.[openbal],s.[rsvqty],s.[qty] + s.[openbal],s.[lcost],s.[fcost],s.[lcost],s.[openfcost],s.[expdate]);'
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=-4
			return 
		end
		else commit transaction

end







GO
/****** Object:  StoredProcedure [dbo].[create_admin_user]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[create_admin_user]

AS
BEGIN
	
	INSERT [dbo].[users] ([user_name], [full_name], [password], [user_type], [modify_price], [language], [use_sal_edt], [use_dsc], [ch_itmpr], [formkey], [post_btn], [slkey], [pukey], [stkey], [brkey], [acckey], [trkey], [chng_date], [modfy_othr_tr], [item_desc], [inv_desc], [shw_sal_icost], [inactive]) VALUES (N'admin', N'admin', N'rlUnpygPz9k=', 1, 1, N'ع', 1, 1, 1, N' T100 F100 F110 F120 F130 F150 F140 F160 A100 A110 A111_111 A112_111 A114_111 A113_111 A120 A121_111 A122_111 A123_111 A125_111 A124_111 A130 A131_111 A132_111 A133_111 A139_111 A134_111 A135_111 A136_111 A13A_111 A13B_111 A13C_111 A13D_111 A138_111 A137_111 B100 B110 B111_111 B112_111 B120 B121_111 B122_111 B123_111 B124_111 B130 B131_111 B132_111 B133_111 B134_111 B135_111 B136_111 B137_111 S100 S110 S111_111 S112_111 S113_111 S114_111 S115_111 S120 S121_111 S122_111 S123_111 S124_111 S125_111 S126_111 S130 S131_111 S132_111 S133_111 S134_111 S135_111 S137_111 S138_111 S139_111 S13A_111 C100 C110 C111_111 C112_111 C113_111 C114_111 C120 C121_111 C122_111 C130 C131_111 C132_111 C133_111 C135_111 C138_111 C134_111 C136_111 C137_111 C139_111 P100 P110 P111_111 P120 P121_111 P122_111 P123_111 P130 P131_111 P132_111 P133_111 P134_111 D100 D110 D111_111 D112_111 D120 D121_111 D122_111 D123_111 D130 D131_111 D132_111 D133_111 D135_111 D134_111 D136_111 E100 E110 E111_111 E112_111 E113_111 E115_111 E116_111 E117_111 E11A_111 E120 E121_111 E122_111 E123_111 E130 E131_111 E133_111 E135_111 E132_111 E134_111 E140 E141_111 E142_111 E143_111 E144_111 E145_111 M100 M180 M181_111 M182_111 M183_111 M184_111 M110 M111_111 M112_111 M113_111 M114_111 M120 M130 M140 M150 M160 M170', 1, N' 0101 0102', N' 0101', N' 0101 0102', N' 01', N'', N' 01 02 03 04 05 06 07 11 12 14 15 16 17 21 24 26 31 32 33 34 35 45 46', 1, 1, 10, 10, 1, 0)

END








GO
/****** Object:  StoredProcedure [dbo].[create_combin_sale_tran]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[create_combin_sale_tran]

@br_no varchar(2),
@sl_no varchar(2),
@invtype varchar(2),
@invref int --,

--@items_tran_tb tb_tran_items READONLY

AS

BEGIN

DECLARE @bld_temp TABLE
(
 branch varchar(2),trtype varchar(2),ref int,invmdate char(8), invhdate char(8) ,itemno varchar(20),qty float,pkqty float , cost float ,whno varchar(2),towhno varchar(2),expdate varchar(8)
);


INSERT INTO sto_hdr(branch,trtype,ref,trmdate, trhdate,text,remarks,entries,amnttl,costttl,usrid,whno,towhno,supno,brsupp)
select branch,invtype,ref,invmdate, invhdate,text,remarks,entries,0,0,usrid,dp_mainwh,dp_mainwh,'' supno,'' brsupp from sales_hdr join slcenters on sales_hdr.branch=slcenters.sl_brno and sales_hdr.slcenter=slcenters.sl_no where branch=@br_no and slcenter=@sl_no and invtype=@invtype and ref=@invref


insert into @bld_temp select branch,invtype,ref,invmdate, invhdate,itemno,qty,pkqty,cost cost,whno,whno towhno,'' expdate from sales_dtl where branch=@br_no and slcenter=@sl_no and invtype=@invtype and ref=@invref and stk_or_ser=2;

INSERT INTO sto_dtl(branch,trtype,ref,trmdate, trhdate, itemno, qty, lcost, pack, pkqty,folio,towhno,barcode,whno,lprice) 
select branch,invtype,ref,invmdate, invhdate, itemno, qty, cost, pack, pkqty,ROW_NUMBER() OVER(ORDER BY itemno) AS folio,dp_mainwh,barcode,'' whno,price from sales_dtl join slcenters on sales_dtl.branch=slcenters.sl_brno and sales_dtl.slcenter=slcenters.sl_no where branch=@br_no and slcenter=@sl_no and invtype=@invtype and ref=@invref and stk_or_ser=2;


declare @Cursor_name Cursor , @item_no nvarchar(2) ,@n_qty nvarchar(2),@cnt int 
	    -- set @cur=''
	     set @cnt = 1
         set @Cursor_name = Cursor LOCAL FAST_FORWARD for
         Select  [itemno],[qty] From @bld_temp -- where len(a_key)=9 and glcurrency=@cur
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @item_no,@n_qty
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin

		  set @cnt =@cnt +  1
INSERT INTO sto_dtl(branch,trtype,ref,trmdate, trhdate, itemno, qty, lcost, pack, pkqty,folio,whno,barcode,towhno,lprice)
select branch,trtype,ref,invmdate, invhdate, itemno_cmbin itemno, qty*nqty qty, cost lcost, 1 pack,1 pkqty,ROW_NUMBER() OVER(ORDER BY a.itemno) + @cnt + 100 as folio ,whno,barcode,''towhno,0 lprice from items_cmbin a join @bld_temp b on a.itemno=b.itemno where a.itemno=@item_no;

--select itemno_cmbin itemno, nqty qty, 1 pack,1 pkqty,ROW_NUMBER() OVER(ORDER BY itemno) + 10 as folio  from items_cmbin



	   fetch next
          From @Cursor_name into @item_no,@n_qty 
		  set @cnt =@cnt +  1
		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
------------------------------------------------------------------

END

GO
/****** Object:  StoredProcedure [dbo].[create_messing_columns]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[create_messing_columns]
 
 @errstatus int = 0 output
	
AS
BEGIN


 if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'items' and COLUMN_NAME='price2')
		begin 
		    ALTER TABLE dbo.items ADD  price2  numeric(10, 2) NULL default 0
		
		end

 if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'users' and COLUMN_NAME='show_win')
		begin 
		    ALTER TABLE dbo.users ADD  show_win  bit NULL default 0
		 
		end



 if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'users' and COLUMN_NAME='suspend_inv')
		begin 
		    ALTER TABLE dbo.users ADD  suspend_inv  bit NULL default 0
		  
		end



 if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'users' and COLUMN_NAME='belw_bal')
		begin 
		    ALTER TABLE dbo.users ADD  belw_bal  bit NULL default 0
		 
		end





 if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'pu_dtl' and COLUMN_NAME='expdate')
		begin 
		    ALTER TABLE dbo.pu_dtl ADD  expdate  char(8) NULL
		
		end

 if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'units' and COLUMN_NAME='unit_qty')
		begin 
		    ALTER TABLE dbo.units ADD  unit_qty  int NULL
		
		end

 if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'contrs' and COLUMN_NAME='print_type')
		begin 
		    ALTER TABLE dbo.contrs ADD  print_type  int NULL
		
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'sales_hdr' and COLUMN_NAME='tax_percent')
		begin 
		    ALTER TABLE dbo.sales_hdr ADD  tax_percent  float NULL default 0
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'pu_hdr' and COLUMN_NAME='tax_percent')
		begin 
		    ALTER TABLE dbo.pu_hdr ADD  tax_percent  float NULL default 0
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'salofr_hdr' and COLUMN_NAME='tax_percent')
		begin 
		    ALTER TABLE dbo.salofr_hdr ADD  tax_percent  float NULL default 0
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'sales_hdr' and COLUMN_NAME='taxfree_amt')
		begin 
		    ALTER TABLE dbo.sales_hdr ADD  taxfree_amt  float NULL default 0
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'pu_hdr' and COLUMN_NAME='taxfree_amt')
		begin 
		    ALTER TABLE dbo.pu_hdr ADD  taxfree_amt  float NULL default 0
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'salofr_hdr' and COLUMN_NAME='taxfree_amt')
		begin 
		    ALTER TABLE dbo.salofr_hdr ADD  taxfree_amt  float NULL default 0
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'pos_hdr' and COLUMN_NAME='taxfree_amt')
		begin 
		    ALTER TABLE dbo.pos_hdr ADD  taxfree_amt  float NULL default 0
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'pos_hdr' and COLUMN_NAME='note')
		begin 
		    ALTER TABLE dbo.pos_hdr ADD  note  nvarchar(100) NULL default N''
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'pos_hdr' and COLUMN_NAME='mobile')
		begin 
		    ALTER TABLE dbo.pos_hdr ADD  mobile  nvarchar(20) NULL default N''
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'contrs' and COLUMN_NAME='print_rtn')
		begin 
		    ALTER TABLE dbo.contrs ADD  print_rtn  bit NULL default 0
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'taxperiods' and COLUMN_NAME='tax_percent')
		begin 
		    ALTER TABLE dbo.taxperiods ADD  tax_percent  float NULL default 0
		 
		end

if not exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'suppliers' and COLUMN_NAME='is_shamel_tax')
		begin 
		    ALTER TABLE dbo.suppliers ADD  is_shamel_tax  bit NULL default 0
		 
		end


--****************************************************************************************************************************

 if exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'slcenter' and COLUMN_NAME='dp_custser')
		begin 
        exec sp_rename 'slcenter.dp_custser','dp_salcust_acc', 'COLUMN'
        ALTER TABLE slcenter ALTER COLUMN dp_salcust_acc varchar(20);
		end

 if exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'slcenter' and COLUMN_NAME='dp_expser')
		begin 
        exec sp_rename 'slcenter.dp_expser','dp_mkhzon_acc', 'COLUMN'
        ALTER TABLE slcenter ALTER COLUMN dp_mkhzon_acc varchar(20);
		end

 if exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'branchs' and COLUMN_NAME='stkxtoser')
		begin 
        exec sp_rename 'branchs.stkxtoser','stkinacc', 'COLUMN'
        ALTER TABLE branchs ALTER COLUMN stkinacc varchar(20);
		end

 if exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'branchs' and COLUMN_NAME='stkxfmser')
		begin 
        exec sp_rename 'branchs.stkxfmser','stkoutacc', 'COLUMN'
        ALTER TABLE branchs ALTER COLUMN stkoutacc varchar(20);
		end

 if exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'branchs' and COLUMN_NAME='calgl')
		begin 
        exec sp_rename 'branchs.calgl','jv_36', 'COLUMN'
        ALTER TABLE branchs ALTER COLUMN jv_36 numeric(6, 0);
		end

if exists(SELECT COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'pu_hdr' and COLUMN_NAME='fcy2')
		begin 
        exec sp_rename 'pu_hdr.fcy2','invsupno', 'COLUMN'
        ALTER TABLE pu_hdr ALTER COLUMN invsupno nvarchar(50);
		end







--***************************************************************************************************************************




ALTER TABLE sales_hdr ALTER COLUMN released datetime;
ALTER TABLE sales_hdr ALTER COLUMN lastupdt datetime;

ALTER TABLE pu_hdr ALTER COLUMN released datetime;
ALTER TABLE pu_hdr ALTER COLUMN lastupdt datetime;

ALTER TABLE sto_hdr ALTER COLUMN released datetime;
ALTER TABLE sto_hdr ALTER COLUMN lastupdt datetime;

ALTER TABLE salofr_hdr ALTER COLUMN released datetime;
ALTER TABLE salofr_hdr ALTER COLUMN lastupdt datetime;
ALTER TABLE acc_hdr ALTER COLUMN serial_no nvarchar(20);

ALTER TABLE taxs ALTER COLUMN tax_percent float;

update users set show_win = 0 where show_win is null;
update users set suspend_inv = 0 where suspend_inv is null;
update users set belw_bal = 0 where belw_bal is null;

update units set unit_qty = 0 where unit_qty is null;
update contrs set print_type = 0 where print_type is null;
update contrs set print_rtn = 1 where print_rtn is null;

update taxperiods set tax_percent = 5 where tax_percent is null;
  		
--exec create_messing_tabels;
			
		return @errstatus
end



GO
/****** Object:  StoredProcedure [dbo].[create_messing_tabels]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[create_messing_tabels]
 
 @errstatus int = 0 output
	
AS
BEGIN

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'items_altr')
BEGIN
    

CREATE TABLE [dbo].[items_altr](
	[itemno] [nvarchar](16) NOT NULL,
	[rplitemno] [nvarchar](16) NOT NULL,
	[cmbkey] [char](24) NULL,
	[iorder] [numeric](3, 0) NULL,
	[lastupdt] [date] NULL
) ON [PRIMARY]



ALTER TABLE [dbo].[items_altr]  WITH CHECK ADD  CONSTRAINT [FK_items_altr_items] FOREIGN KEY([itemno])
REFERENCES [dbo].[items] ([item_no])
ON DELETE CASCADE

ALTER TABLE [dbo].[items_altr] CHECK CONSTRAINT [FK_items_altr_items]


END

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'sltrans')
BEGIN
--SET ANSI_PADDING ON
CREATE TABLE [dbo].[sltrans](
	[code] [varchar](3) NOT NULL,
	[cname] [varchar](50) NOT NULL,
	[pname] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[tel] [varchar](20) NULL,
	[fax] [char](10) NULL,
	[mobile] [char](10) NULL,
	[modified] [bit] NULL,
	[lcname] [varchar](40) NULL,
	[lpname] [varchar](30) NULL,
	[lastupdt] [char](8) NULL,
	[laddress] [varchar](50) NULL,
	[pricePerUnit] [numeric](10, 2) NULL,
 CONSTRAINT [PK_sltrans] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


--SET ANSI_PADDING OFF

ALTER TABLE [dbo].[sltrans] ADD  CONSTRAINT [DF_sltrans_pricePerUnit]  DEFAULT ((0)) FOR [pricePerUnit]

END

update dbmstr.dbo.years set update_date=FORMAT (getdate(), 'yyyyMMdd', 'en-us') where dbmstr.dbo.years.comp_id=substring(db_name(),3,2) and dbmstr.dbo.years.yrcode=substring(db_name(),6,4);


		return @errstatus
end

GO
/****** Object:  StoredProcedure [dbo].[curser_in_curser]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[curser_in_curser]

AS
BEGIN
Declare @ClientID int;
Declare @UID int;

DECLARE Cur1 CURSOR FOR
    SELECT ClientID From Folder;

OPEN Cur1
FETCH NEXT FROM Cur1 INTO @ClientID;
WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Processing ClientID: ' + Cast(@ClientID as Varchar);
    DECLARE Cur2 CURSOR FOR
        SELECT UID FROM Attend Where ClientID=@ClientID;
    OPEN Cur2;
    FETCH NEXT FROM Cur2 INTO @UID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT 'Found UID: ' + Cast(@UID as Varchar);
        FETCH NEXT FROM Cur2 INTO @UID;
    END;
    CLOSE Cur2;
    DEALLOCATE Cur2;
    FETCH NEXT FROM Cur1 INTO @ClientID;
END;
PRINT 'DONE';
CLOSE Cur1;
DEALLOCATE Cur1;
END







GO
/****** Object:  StoredProcedure [dbo].[Del_Product]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Del_Product]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
	@no nvarchar(16),
	@barcode nvarchar(16)
AS
BEGIN
delete from items where item_no=@no and item_barcode=@barcode
	/* SET NOCOUNT ON */
	RETURN








END;

GO
/****** Object:  StoredProcedure [dbo].[delete_files_data]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[delete_files_data]

--@old_db nvarchar(10),
@errstatus int = 0 output
--@branch_no varchar(2)

AS
BEGIN
	declare @Cursor_name Cursor , @db_tb nvarchar(50) --,@TEMP  float
	    -- set @cur=''
         set @Cursor_name = Cursor for
         select tbl_name from dbmstr.dbo.tbl_to_del order by tbl_id -- where cf_fcy=@cur and su_brno=@branch_no
         --( You use your query statement) 
--step1: open cursor 
         open @Cursor_name
--step2: Fetch Cursor
         fetch next
         From @Cursor_name into @db_tb 
--step3: process cursor 
          while @@FETCH_STATUS = 0 
          begin
       --   print '@@FETCH_STATUS'+convert(varchar,@@FETCH_STATUS)

   
	
declare   
@sqlmsg nvarchar(max)='' --, @tb nvarchar(50)=@db_tb

set @sqlmsg=' delete from '+ @db_tb + ' ;'
--set @sqlmsg= @sqlmsg +  ' insert into '+@db_tb+' select * from '+@old_db+'.dbo.'+@db_tb+' ;'

	begin transaction
	exec sp_executesql @sqlmsg
	--set @No_items_updated=@@ROWCOUNT
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=1
		return 
	end
	commit transaction

          fetch next
          From @Cursor_name into @db_tb 

		 -- print @TEMP
          end 
--step4: Close cursor
          close @Cursor_name
--step5: Deallocate Cursor 
         deallocate @Cursor_name
END









GO
/****** Object:  StoredProcedure [dbo].[Delete_Trx]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Trx]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
BEGIN
delete from pos_hdr;
--delete from users where user_id<>1;
delete from groups where group_id<>1;
delete from items;
delete from units where unit_id<>1;
-- access --ALTER TABLE hdr ALTER COLUMN ref COUNTER(1,1);
DBCC CHECKIDENT (pos_hdr, RESEED, 0)--means start with 0+1=1 if 5 whil start with 5+1=6
	/* SET NOCOUNT ON */
	RETURN

END;











GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DeleteProduct]
@ID varchar(50)
AS
BEGIN
Delete From items where item_no=@ID











END;


GO
/****** Object:  StoredProcedure [dbo].[formate_db]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[formate_db]
	
AS
BEGIN

delete from sales_hdr;
delete from d_sales_hdr;
delete from d_sales_dtl;
delete from pu_hdr;
delete from d_pu_hdr;
delete from d_pu_dtl;
delete from pos_hdr;
delete from d_pos_hdr;
delete from d_pos_dtl;
delete from salofr_hdr;
delete from sto_hdr;
delete from acc_hdr;
delete from d_acc_hdr;
delete from d_acc_dtl;
delete from ttk_hdr;
delete from ttk_dtl;
delete from items --where item_no > 100;
delete from users where [user_name] <> 'admin';
delete from groups where group_id<>'01';
delete from units where unit_id not in (1,2);
--delete from pos_salmen;
delete from suppliers where su_no<>'1';
delete from customers;
delete from notify;
delete from pos_esend;

update accounts set a_opnbal=0,a_curbal=0;
--update suppliers set su_opnbal=0,su_opnfcy=0,su_curbal=0;
--update customers set cu_opnbal=0,cf_opnfcy=0,cu_curbal=0;

END


GO
/****** Object:  StoredProcedure [dbo].[generate_merge]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[generate_merge]

 -- @table_name nvarchar(50) ,
 -- @schema nvarchar(50)
 
AS
BEGIN
EXEC dbo.sp_generate_merge 'table_name', @schema='dbo'
END








GO
/****** Object:  StoredProcedure [dbo].[get_cust_bal]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
CREATE PROCEDURE [dbo].[get_cust_bal]
	@cust_no nvarchar(10),
	@branch_no varchar(2)
AS
BEGIN

	  select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and a.cu_brno=@branch_no and b.a_type not in ('04','14')),2)
                             from customers a where a.cu_no = @cust_no ;

END








GO
/****** Object:  StoredProcedure [dbo].[Get_Order_Dtl]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Get_Order_Dtl]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
	@ref int
AS
BEGIN

--select dtl.*,items.item_name from dtl
--inner join items
--on 
--dtl.barcode=items.item_barcode
-- where dtl.ref=@ref   
select a.barcode,a.qty,a.price,b.item_name from dtl as a
inner join items as b
on 
a.barcode=b.item_barcode
 where a.ref=@ref


--select dtl.barcode,dtl.qty,dtl.price,items.item_name from dtl,items
--where dtl.barcode=items.item_barcode and dtl.ref=@ref


	/* SET NOCOUNT ON */
	RETURN









END;


GO
/****** Object:  StoredProcedure [dbo].[get_whbins_bal_cost]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[get_whbins_bal_cost]

@conit varchar(100),
@consup varchar(100),
@conwh varchar(100),
@conall varchar(100),
@congrp varchar(100),
@congrsp varchar(100),
@condopnbalonly varchar(100),
@condminslonly varchar(100),
@conaball varchar(100),
@congplc varchar(100),
@consupc varchar(100)

AS
BEGIN

select a.item_no a1, a.item_name b1,0.00 trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd
,0.00 frt,[dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0) totalq,round(a.item_curcost,2) d1,a.item_price price
,0.00 e1,0.00 totalp,b.wh_no whno,a.uq2 from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id 
join whbins b WITH (NOLOCK) on a.item_no=b.item_no  
+ @conit + @consup + @conwh + @conall + @congrp + @congrsp + @condopnbalonly + @condminslonly + @conaball + @congplc + @consupc  ;

END;
GO
/****** Object:  StoredProcedure [dbo].[get_whbins_RM]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[get_whbins_RM]

@r_or_m varchar(1),
@brno varchar(2),
@errstatus int = 0 output
--@posted int=1
AS
BEGIN

declare @sqlmsg nvarchar(max)='',@txt_sort varchar(5)=''


if @r_or_m='0' set @txt_sort='DESC'
else if @r_or_m='1' set @txt_sort='ASC';
--begin
--set @txt_sort='DESC';
--select top 100 a.item_no a1,a.item_name a2,a.item_obalance a3,(
--(select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('06','07')) 
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('14','15'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('31','45'))) a4,
--((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('16','17')) 
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('04','05'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('32','46'))
--) a5,0 a6, round(a.item_curcost,2) a7,b.su_name a8  from items a left join suppliers b on a.supno = b.su_no where b.su_brno=@brno 
-- and 
--a.item_obalance + ((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('06','07'))
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('14','15'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('31','45')))
--+((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('16','17')) 
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('04','05'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('32','46'))) <>0
-- order by 
--a.item_obalance + ((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('06','07'))
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('14','15'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('31','45')))
---((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('16','17')) 
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('04','05'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('32','46'))) DESC;
            
--end


--if @r_or_m = '1'
--begin

--set @txt_sort='ASC';
--select top 100 a.item_no a1,a.item_name a2,a.item_obalance a3,(
--(select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('06','07')) 
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('14','15'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('31','45'))) a4,
--((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('16','17')) 
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('04','05'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('32','46'))
--) a5,0 a6, round(a.item_curcost,2) a7,b.su_name a8  from items a left join suppliers b on a.supno = b.su_no where b.su_brno=@brno 
-- and 
--a.item_obalance + ((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('06','07'))
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('14','15'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('31','45')))
--+((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('16','17')) 
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('04','05'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('32','46'))) <>0
-- order by 
--a.item_obalance + ((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('06','07'))
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('14','15'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('31','45')))
---((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('16','17')) 
--+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch=@brno and c.invtype in ('04','05'))
--+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch=@brno and b.trtype in ('32','46'))) ASC;
--end

set @sqlmsg= 'select top 100 a.item_no a1,a.item_name a2,a.item_obalance a3,(
(select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''06'',''07'')) 
+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''14'',''15''))
+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch='+@brno+' and b.trtype in (''31'',''45''))) a4,
((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''16'',''17'')) 
+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''04'',''05''))
+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch='+@brno+' and b.trtype in (''32'',''46''))
) a5,0 a6, round(a.item_curcost,2) a7,b.su_name a8  from items a left join suppliers b on a.supno = b.su_no where b.su_brno='+@brno+' 
 and 
a.item_obalance + ((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''06'',''07''))
+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''14'',''15''))
+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch='+@brno+' and b.trtype in (''31'',''45'')))
+((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''16'',''17'')) 
+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''04'',''05''))
+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch='+@brno+' and b.trtype in (''32'',''46''))) <>0
 order by 
a.item_obalance + ((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''06'',''07''))
+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''14'',''15''))
+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch='+@brno+' and b.trtype in (''31'',''45'')))
-((select  isnull(sum(c.qty*pkqty),0) from pu_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''16'',''17'')) 
+ (select  isnull(sum(c.qty*pkqty),0) from sales_dtl c where a.item_no=c.itemno and c.branch='+@brno+' and c.invtype in (''04'',''05''))
+ (select  isnull(sum(b.qty*pkqty),0) from sto_dtl b where a.item_no=b.itemno and b.branch='+@brno+' and b.trtype in (''32'',''46'')))' +  @txt_sort;

----begin transaction
----	exec sp_executesql @sqlmsg
----	if @@error<>0
----	begin
----		rollback transaction
----		set @errstatus=-5
----		return 
----	end
----	else commit transaction

--begin transaction
	exec sp_executesql @sqlmsg
	--if @@error<>0
	--begin
		--rollback transaction
		--set @errstatus=-5
		--return 
	--end
	--else commit transaction

end

GO
/****** Object:  StoredProcedure [dbo].[GetCurUser]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetCurUser]
@ID int
AS
BEGIN
SELECT * FROM users
--where user_id=@ID








END;


GO
/****** Object:  StoredProcedure [dbo].[getTotalCount]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[getTotalCount]

 @itemno varchar(50)

AS
BEGIN

  -- Create integer values
    DECLARE @tablesalCount INT, @tablepuCount INT,@tableCount INT,@tablesloCount INT,@tablestCount INT

  -- Get the number of rows from all table
    SELECT @tablesalCount = (SELECT COUNT(*) FROM sales_dtl WHERE itemno=@itemno)
    SELECT @tablepuCount = (SELECT COUNT(*) FROM pu_dtl WHERE itemno=@itemno)
	SELECT @tablesloCount = (SELECT COUNT(*) FROM salofr_dtl WHERE itemno=@itemno)
    SELECT @tablestCount = (SELECT COUNT(*) FROM sto_dtl WHERE itemno=@itemno)

  -- Return the sum of all table
    SELECT TotalCount = @tablesalCount + @tablepuCount + @tablesloCount + @tablestCount

 END ;







GO
/****** Object:  StoredProcedure [dbo].[Grd_Enter]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Grd_Enter] 

@zttk_tb tb_zttk READONLY

AS
BEGIN

--declare   
-- @sqlmsg nvarchar(max)

--set @sqlmsg=' delete from zttk ; 
--               MERGE Zttk as t 
--               using ' + @zttk_tb + ' as s 
--               ON t.d = s.a and t.s=s.s
--               WHEN MATCHED THEN
--               UPDATE SET t.d=s.d , t.b=s.b
--               WHEN NOT MATCHED THEN                                      
--               INSERT VALUES(s.A,s.B,s.C,s.D,s.E,s.T,s.S,s.L,s.SHDQTY,s.SHDPK,s.FRTQTY,s.MQTY,s.WHNO,s.QTYFOUND,s.COLORIT,s.ERASEIT,s.UNIT)
--			   WHEN NOT MATCHED BY SOURCE
--               THEN DELETE;'
	begin transaction
	--exec sp_executesql @sqlmsg
	delete from zttk ; 
               MERGE Zttk as t 
               using  @zttk_tb  as s 
               ON t.d = s.a and t.s=s.s
               WHEN MATCHED THEN
               UPDATE SET t.d=s.d , t.b=s.b
               WHEN NOT MATCHED THEN                                      
               INSERT VALUES(s.A,s.B,s.C,s.D,s.E,s.T,s.S,s.L,s.SHDQTY,s.SHDPK,s.FRTQTY,s.MQTY,s.WHNO,s.QTYFOUND,s.COLORIT,s.ERASEIT,s.UNIT)
			   WHEN NOT MATCHED BY SOURCE
               THEN DELETE;
	if @@error<>0
	begin
		rollback transaction
		return 
	end
	commit transaction

END

GO
/****** Object:  StoredProcedure [dbo].[import_inv_from_branch]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[import_inv_from_branch]
 @whr_condn nvarchar(200),
 @brlink nvarchar(15),
 @no_of_invoices int=0 output,
 @errstatus int = 0 output
	
AS
BEGIN
	declare @sqlmsg nvarchar(max)='',
	@dbc nvarchar(15),
	@lcalcomp char(2),
	@yrcode char(4)--,
	--@syscode int =2,
	--@lcauseslctr bit=0,
	--@it_is_pos bit=0
---------------------------------------------------------------------------------------------
	set @dbc=db_name()
	set @lcalcomp=substring(@dbc,3,2)
	set @yrcode=substring(@dbc,6,4)
	-- sa SET @yrcode=iif(cast(@yrcode as int)>33,'14','20')+rtrim(@yrcode)

	
------------------------------ Get syscode from Main office glcalend  ------------------------------------------------------
	--select @syscode=sysno,@lcauseslctr=causeslctr  from sysdb.dbo.glcalend 
	--where calcomp=@lcalcomp and yrcode=@yrcode
------------------------------------------------------------------------------------------------------------------------------

    set @errstatus=0
 
------------------------------ Get Yrcode & Calcomp from Branch Boteequ  ----------------------------------------------
		declare @dynsql nvarchar(300),
				@params nvarchar(100)

		set @yrcode=''
		set @lcalcomp=''
		set @dynsql='select top 1 @yrcd=yrcode,@lclcmp=comp_id  from '+@brlink+'.dbmstr.dbo.years'
		+ ' order by yrcode desc';
		set @params='@yrcd char(4) output,@lclcmp char(2) output';
		exec sp_executesql @dynsql,@params,@yrcd=@yrcode output,@lclcmp=@lcalcomp output
		if @@error<>0 or @@rowcount=0
		begin
		  	set @errstatus=-1
			return 
		end
		--if @yrcode<>'' and @lcalcomp<>'' set @dbc='.pos'+@lcalcomp+'y'+substring(@yrcode,3,2)+'.dbo.'
		if @yrcode<>'' and @lcalcomp<>'' set @dbc='.db'+@lcalcomp+'y'+@yrcode+'.dbo.'
		else
		begin
		  	set @errstatus=-2
			return 		  
		end
		--set @it_is_pos=1
------------------------------------------------------------------------------------------------------------------------
	
    


	set @sqlmsg='merge pos_hdr as t '
	+ ' using '
	+ ' (select * from '+@brlink+@dbc+'pos_hdr b where '+@whr_condn+') as s'
	+ ' on t.brno=s.brno and t.slno=s.slno and t.ref=s.ref and t.contr=s.contr '
	+ ' when not matched then '
	+ ' insert (brno,slno,ref,contr,type,date,total,count,payed,total_cost,saleman,req_no,cust_no,'
	+ ' discount,net_total,sysdate,gen_ref,tax_amt,dscper,card_type,card_amt,rref,rcontr,taxfree_amt,note,mobile)'
	+ ' values (s.brno,s.slno, s.ref, s.contr,s.type,s.date,s.total,s.count,s.payed,s.total_cost,s.saleman,s.req_no,s.cust_no,'
	+ ' s.discount,s.net_total,s.sysdate,0,s.tax_amt,s.dscper,s.card_type,s.card_amt,s.rref,s.rcontr,s.taxfree_amt,s.note,s.mobile);'	
	
	begin transaction
	exec sp_executesql @sqlmsg
	set @no_of_invoices=@@ROWCOUNT
	if @@error<>0 or @no_of_invoices=0
	begin
		rollback transaction
		set @errstatus=-3
		if @no_of_invoices=0 set @errstatus=0
		return 
	end
	

	
	set @sqlmsg='merge pos_dtl as t '
	+ ' using '
	+ ' (select a.* from '+@brlink+@dbc+'pos_dtl a inner join '+@brlink+@dbc+'pos_hdr b'
	+ ' on a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr '
	+ ' where '+@whr_condn+') as s'
	+ ' on t.brno=s.brno and t.slno=s.slno and t.ref=s.ref and t.contr=s.contr and t.srno=s.srno'
	+ ' when not matched then '
	+ ' insert values (s.brno,s.slno, s.ref, s.contr,s.type,s.barcode,s.name,s.unit,s.price,s.qty,s.cost,s.is_req,s.itemno,s.srno,'
	+ ' s.pkqty,s.discpc,s.tax_id,s.tax_amt,s.rqty,s.offr_amt);'


	exec sp_executesql @sqlmsg
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=-4
		return 
	end

	else commit transaction


	----set @sqlmsg='merge POSRTNINV as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'POSRTNINV a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' and b.invcst>0) as s'
	----+ ' on t.company=s.company and t.rcontr=s.rcontr and t.rref=s.rref '
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.rcontr,s.rref,s.Contr,s.ref,s.rtnamt);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-5
	----	return 
	----end

	----set @sqlmsg='merge POSPPCARD as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'POSPPCARD a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' and b.prepaidamt>0) as s'
	----+ ' on t.company=s.company and t.contr=s.contr and t.ref=s.ref and t.disccard=s.disccard'
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.ref,s.disccard,s.paytype,s.prepaidamt,s.paycard);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-9
	----	return 
	----end
-------------------------------------------------------------------------------
	----set @sqlmsg='merge POSCCCARD as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'POSCCCARD a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' and b.ccpaid>0) as s'
	----+ ' on t.company=s.company and t.contr=s.contr and t.ref=s.ref and t.cccardno=s.cccardno and t.ccsrlno=s.ccsrlno'
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.ref,s.cccardno,s.ccsrlno,s.cardtype,s.ccpaid,
	----    s.ccconus,s.chargepctg,s.chargeamt,s.cctype,s.approvl_no,s.TransDate,s.TransTime,s.TransNo,s.terminal_id);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-9
	----	return 
	----end

	----set @sqlmsg='merge poschrtinv as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'poschrtinv a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' ) as s'
	----+ ' on t.company=s.company and t.contr=s.contr and t.ref=s.ref '
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.ref,s.hllamt,s.chrcode);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-5
	----	return 
	----end
-----------------------------------------------------------------------------


	----set @sqlmsg='merge posfcamt as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'posfcamt a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' and b.invcst>0) as s'
	----+ ' on t.company=s.company and t.contr=s.contr and t.ref=s.ref and t.srlno=s.srlno '
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.ref,s.fcy,s.fcamt,s.fcyrate,s.lcamt,s.srlno);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-6
	----	return 
	----end
	----else commit transaction
	
	----declare @new_cndn nvarchar(250),
	----        @lpos int =0

	----set @new_cndn=replace(replace(@whr_condn,'tdatetime','mnydate'),'smalldatetime','date')
	----set @sqlmsg='merge pos_mny as t '
	----+ ' using '
	----+ ' (select * from '+@poslink+@dbc+'pos_mny where '+@new_cndn+') as s'	
	----+ ' on t.company=s.company and t.contr=s.contr and t.mnydate=s.mnydate and t.slpcode=s.slpcode'
 ----   + ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.mnydate,s.slpcode,s.oner,s.fiver,s.tenr,s.twntyr,s.fiftyr,s.hundredr'
 ----   + ' ,s.twohandr,s.fivehandr,s.modified,s.rtnval,s.network,s.prepaidamt,0,0,0,0,s.saned_amt,0);'
	

	----begin transaction
	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=0
	----end
	----else commit transaction
	
end 







GO
/****** Object:  StoredProcedure [dbo].[import_inv_from_pos]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[import_inv_from_pos]
 @whr_condn nvarchar(200),
 @poslink nvarchar(15),
 @no_of_invoices int=0 output,
 @errstatus int = 0 output
	
AS
BEGIN
	declare @sqlmsg nvarchar(max)='',
	@dbc nvarchar(15),
	@lcalcomp char(2),
	@yrcode char(4)--,
	--@syscode int =2,
	--@lcauseslctr bit=0,
	--@it_is_pos bit=0
---------------------------------------------------------------------------------------------
	set @dbc=db_name()
	set @lcalcomp=substring(@dbc,3,2)
	set @yrcode=substring(@dbc,6,4)
	-- sa SET @yrcode=iif(cast(@yrcode as int)>33,'14','20')+rtrim(@yrcode)

	
------------------------------ Get syscode from Main office glcalend  ------------------------------------------------------
	--select @syscode=sysno,@lcauseslctr=causeslctr  from sysdb.dbo.glcalend 
	--where calcomp=@lcalcomp and yrcode=@yrcode
------------------------------------------------------------------------------------------------------------------------------

    set @errstatus=0
 
------------------------------ Get Yrcode & Calcomp from Branch Boteequ  ----------------------------------------------
		declare @dynsql nvarchar(300),
				@params nvarchar(100)

		set @yrcode=''
		set @lcalcomp=''
		set @dynsql='select top 1 @yrcd=yrcode,@lclcmp=comp_id  from '+@poslink+'.dbmstr.dbo.years'
		+ ' order by yrcode desc';
		set @params='@yrcd char(4) output,@lclcmp char(2) output';
		exec sp_executesql @dynsql,@params,@yrcd=@yrcode output,@lclcmp=@lcalcomp output
		if @@error<>0 or @@rowcount=0
		begin
		  	set @errstatus=-1
			return 
		end
		--if @yrcode<>'' and @lcalcomp<>'' set @dbc='.pos'+@lcalcomp+'y'+substring(@yrcode,3,2)+'.dbo.'
		if @yrcode<>'' and @lcalcomp<>'' set @dbc='.db'+@lcalcomp+'y'+@yrcode+'.dbo.'
		else
		begin
		  	set @errstatus=-2
			return 		  
		end
		--set @it_is_pos=1
------------------------------------------------------------------------------------------------------------------------
	
    


	set @sqlmsg='merge pos_hdr as t '
	+ ' using '
	+ ' (select * from '+@poslink+@dbc+'pos_hdr b where '+@whr_condn+') as s'
	+ ' on t.brno=s.brno and t.slno=s.slno and t.ref=s.ref and t.contr=s.contr '
	+ ' when not matched then '
	+ ' insert (brno,slno,ref,contr,type,date,total,count,payed,total_cost,saleman,req_no,cust_no,'
	+ ' discount,net_total,sysdate,gen_ref,tax_amt,dscper,card_type,card_amt,rref,rcontr)'
	+ ' values (s.brno,s.slno, s.ref, s.contr,s.type,s.date,s.total,s.count,s.payed,s.total_cost,s.saleman,s.req_no,s.cust_no,'
	+ ' s.discount,s.net_total,s.sysdate,0,s.tax_amt,s.dscper,s.card_type,s.card_amt,s.rref,s.rcontr);'	
	
	begin transaction
	exec sp_executesql @sqlmsg
	set @no_of_invoices=@@ROWCOUNT
	if @@error<>0 or @no_of_invoices=0
	begin
		rollback transaction
		set @errstatus=-3
		if @no_of_invoices=0 set @errstatus=0
		return 
	end
	

	
	set @sqlmsg='merge pos_dtl as t '
	+ ' using '
	+ ' (select a.* from '+@poslink+@dbc+'pos_dtl a inner join '+@poslink+@dbc+'pos_hdr b'
	+ ' on a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr '
	+ ' where '+@whr_condn+') as s'
	+ ' on t.brno=s.brno and t.slno=s.slno and t.ref=s.ref and t.contr=s.contr '
	+ ' when not matched then '
	+ ' insert values (s.brno,s.slno, s.ref, s.contr,s.type,s.barcode,s.name,s.unit,s.price,s.qty,s.cost,s.is_req,s.itemno,s.srno,'
	+ ' s.pkqty,s.discpc,s.tax_id,s.tax_amt,s.rqty,s.offr_amt);'


	exec sp_executesql @sqlmsg
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=-4
		return 
	end

	else commit transaction


	----set @sqlmsg='merge POSRTNINV as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'POSRTNINV a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' and b.invcst>0) as s'
	----+ ' on t.company=s.company and t.rcontr=s.rcontr and t.rref=s.rref '
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.rcontr,s.rref,s.Contr,s.ref,s.rtnamt);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-5
	----	return 
	----end

	----set @sqlmsg='merge POSPPCARD as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'POSPPCARD a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' and b.prepaidamt>0) as s'
	----+ ' on t.company=s.company and t.contr=s.contr and t.ref=s.ref and t.disccard=s.disccard'
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.ref,s.disccard,s.paytype,s.prepaidamt,s.paycard);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-9
	----	return 
	----end
-------------------------------------------------------------------------------
	----set @sqlmsg='merge POSCCCARD as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'POSCCCARD a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' and b.ccpaid>0) as s'
	----+ ' on t.company=s.company and t.contr=s.contr and t.ref=s.ref and t.cccardno=s.cccardno and t.ccsrlno=s.ccsrlno'
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.ref,s.cccardno,s.ccsrlno,s.cardtype,s.ccpaid,
	----    s.ccconus,s.chargepctg,s.chargeamt,s.cctype,s.approvl_no,s.TransDate,s.TransTime,s.TransNo,s.terminal_id);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-9
	----	return 
	----end

	----set @sqlmsg='merge poschrtinv as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'poschrtinv a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' ) as s'
	----+ ' on t.company=s.company and t.contr=s.contr and t.ref=s.ref '
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.ref,s.hllamt,s.chrcode);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-5
	----	return 
	----end
-----------------------------------------------------------------------------


	----set @sqlmsg='merge posfcamt as t '
	----+ ' using '
	----+ ' (select a.* from '+@poslink+@dbc+'posfcamt a inner join '+@poslink+@dbc+'pos_hd b'
	----+ ' on a.company=b.company and a.contr=b.contr and a.ref=b.ref'
	----+ ' where '+@whr_condn+' and b.invcst>0) as s'
	----+ ' on t.company=s.company and t.contr=s.contr and t.ref=s.ref and t.srlno=s.srlno '
	----+ ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.ref,s.fcy,s.fcamt,s.fcyrate,s.lcamt,s.srlno);'

	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=-6
	----	return 
	----end
	----else commit transaction
	
	----declare @new_cndn nvarchar(250),
	----        @lpos int =0

	----set @new_cndn=replace(replace(@whr_condn,'tdatetime','mnydate'),'smalldatetime','date')
	----set @sqlmsg='merge pos_mny as t '
	----+ ' using '
	----+ ' (select * from '+@poslink+@dbc+'pos_mny where '+@new_cndn+') as s'	
	----+ ' on t.company=s.company and t.contr=s.contr and t.mnydate=s.mnydate and t.slpcode=s.slpcode'
 ----   + ' when not matched then '
	----+ ' insert values (s.company,s.contr,s.mnydate,s.slpcode,s.oner,s.fiver,s.tenr,s.twntyr,s.fiftyr,s.hundredr'
 ----   + ' ,s.twohandr,s.fivehandr,s.modified,s.rtnval,s.network,s.prepaidamt,0,0,0,0,s.saned_amt,0);'
	

	----begin transaction
	----exec sp_executesql @sqlmsg
	----if @@error<>0
	----begin
	----	rollback transaction
	----	set @errstatus=0
	----end
	----else commit transaction
	
end 






GO
/****** Object:  StoredProcedure [dbo].[Move_Acc_To_Other]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Move_Acc_To_Other]
	
	@br_no nvarchar(2),
	@old_acc nvarchar(50),
	@new_acc nvarchar(50),
	
	@ok_ac bit,	
	@ok_it bit,	
	@ok_su bit,
	@ok_cu bit,
	

	@errstatus int = 0 output
 
AS
BEGIN

  
   Declare @sqlmsg nvarchar(max)

  



  if @ok_ac=1
	begin
	 		
     set @sqlmsg='update acc_dtl set a_key='''+@new_acc+''' where a_key=''' + @old_acc +''' ;'
		        + ' update sales_hdr set casher='''+@new_acc+''' where casher=''' + @old_acc +''' ;'
				+ ' update pu_hdr set casher='''+@new_acc+''' where casher=''' + @old_acc +''' ;'
				+ ' update acc_hdr set mainkey='''+@new_acc+''' where mainkey=''' + @old_acc +''' ;'
		begin transaction

		BEGIN TRY

		    exec sp_executesql @sqlmsg
		    COMMIT TRANSACTION

        END TRY

        BEGIN CATCH

            ROLLBACK TRANSACTION
		    set @errstatus=1
		    return

        END CATCH
				
			

	end

    if @ok_it=1
	begin

		
	   set @sqlmsg='update sales_dtl set itemno='''+@new_acc+''' where itemno=''' + @old_acc +''' ;'
		      + ' update pu_dtl set itemno='''+@new_acc+''' where itemno=''' + @old_acc +''' ;'
		      + ' update sto_dtl set itemno='''+@new_acc+''' where itemno=''' + @old_acc +''' ;'
			  + ' update pos_dtl set itemno='''+@new_acc+''' where itemno=''' + @old_acc +''' ;'
			 -- + ' update whbins set item_no='+@new_acc+' where  item_no=' + @old_acc +' ;'
			  + ' delete from whbins where item_no=''' + @old_acc +''' ;'
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=2	
			return	--@errstatus		 				
		end
		else commit transaction

	end

    if @ok_su=1
	begin

	   set @sqlmsg='update acc_dtl set su_no='''+@new_acc+''' where su_no=''' + @old_acc +''' and a_brno='''+@br_no+''' ;'
		          + ' update pu_hdr set supno='''+@new_acc+''' where supno=''' + @old_acc +''' and branch='''+@br_no+''' ;'
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=3	
			return	--@errstatus		 				
		end
		else commit transaction


	end


 	if @ok_cu=1 
	begin

		set @sqlmsg='update acc_dtl set cu_no='''+@new_acc+''' where cu_no=''' + @old_acc +''' and a_brno='''+@br_no+''' ;'
		           + ' update sales_hdr set custno='''+@new_acc+''' where custno=''' + @old_acc +''' and branch='''+@br_no+''' ;'
		begin transaction
		exec sp_executesql @sqlmsg
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=4	
			return	--@errstatus		 				
		end
		else commit transaction


	end
	return	@errstatus
end
GO
/****** Object:  StoredProcedure [dbo].[move_tr_serial]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[move_tr_serial] 
	@old_db nvarchar(10),@move_serial bit,
    @errstatus int = 0 output

AS
BEGIN

if(@move_serial=1)
BEGIN
declare @db_brno nvarchar(2), @Cursor_brno Cursor, @sqlmsg nvarchar(max) 
set @Cursor_brno = Cursor LOCAL FAST_FORWARD for
Select  br_no From branchs

open @Cursor_brno

         fetch next
         From @Cursor_brno into @db_brno 

          while @@FETCH_STATUS = 0 
          begin

		    set @sqlmsg ='update branchs set  jv_01=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a  where a.a_brno='+@db_brno+' and a.a_type=''01'' ), jv_02=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''02'' ), jv_03=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''03'' ),
			            jv_04=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''04'' ), jv_05=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''05'' ), jv_06=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''06'' ), jv_07=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''07'' ), 
                        jv_11=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''11'' ), jv_12=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''12'' ), jv_14=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''14'' ), jv_15=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''15'' ), jv_16=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''16'' ), jv_17=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''17'' ), jv_21=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''21'' ), jv_24=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''24'' ), jv_26=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''26'' ),jv_31=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''31'' ), jv_32=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''32'' ), jv_33=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''33'' ), jv_34=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''34'' ), 
                        jv_35=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''35'' ),jv_45=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''45'' ),jv_46=(select isnull(max(a.a_ref),0) from '+@old_db+'.dbo.acc_hdr a where a.a_brno='+@db_brno+' and a.a_type=''46'' ) where br_no='+@db_brno+' ;'
		     begin transaction
	         exec sp_executesql @sqlmsg
	         if @@error<>0
	         begin
	         rollback transaction
		     set @errstatus=1
		     return 
	         end
	         commit transaction

	        fetch next
            From @Cursor_brno into @db_brno


          end 
close @Cursor_brno

deallocate @Cursor_brno

END

else
begin
declare @db_brno2 nvarchar(2), @Cursor_brno2 Cursor, @sqlmsg2 nvarchar(max) 
set @Cursor_brno2 = Cursor LOCAL FAST_FORWARD for
Select  br_no From branchs

open @Cursor_brno2

         fetch next
         From @Cursor_brno2 into @db_brno2 

          while @@FETCH_STATUS = 0 
          begin

		    set @sqlmsg2 ='update branchs set  jv_01=0, jv_02=0, jv_03=0,jv_04=0, jv_05=0, jv_06=0, jv_07=0,jv_11=0, jv_12=0, jv_14=0,
			               jv_15=0, jv_16=0, jv_17=0, jv_21=0, jv_24=0, jv_26=0,jv_31=0, jv_32=0, jv_33=0, jv_34=0,jv_35=0,jv_45=0,
						   jv_46=0 where br_no='+@db_brno2+' ;'
		     begin transaction
	         exec sp_executesql @sqlmsg2
	         if @@error<>0
	         begin
	         rollback transaction
		     set @errstatus=1
		     return 
	         end
	         commit transaction

	        fetch next
            From @Cursor_brno2 into @db_brno2


          end 
close @Cursor_brno2

deallocate @Cursor_brno2
end
End

GO
/****** Object:  StoredProcedure [dbo].[Move_Tran_To_Other]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Move_Tran_To_Other]
	
	@br_no nvarchar(2),
	@type_no nvarchar(2),
	@sl_no nvarchar(2),
	@pu_no nvarchar(2),
	@old_acc nvarchar(50),
	@new_acc nvarchar(50),
	@sqlmsg1 nvarchar(max),
	
	@ok_ac bit,	
	@ok_it bit,	
	@ok_su bit,
	@ok_cu bit,
	

	@errstatus int = 0 output
 
AS
BEGIN

  
   Declare @sqlmsg nvarchar(max),
   @spce nvarchar(2),@neww  INT ,@oldd  INT


  if @ok_ac=1
	begin
	 set @spce =' ' 
    -- set @sqlmsg= ' update acc_hdr set a_ref='+@new_acc+ @spce + ' where a_ref=' + @old_acc + @spce + ' and a_type=''' + @type_no +''' and a_brno='''+@br_no+''' ;'
				--+ ' update acc_dtl set a_ref='+@new_acc+ @spce + ' where a_ref=' + @old_acc + @spce + ' and a_type=''' + @type_no +''' and a_brno='''+@br_no+''' ;'
		  ----+ ' update sales_hdr set casher='''+@new_acc+''' where casher=''' + @old_acc +''' ;'
		  --   --+ ' update sales_hdr set casher='''+@new_acc+''' where casher=''' + @old_acc +''' ;'
				----+ ' update pu_hdr set casher='''+@new_acc+''' where casher=''' + @old_acc +''' ;'
				----+ ' update sales_hdr set casher='''+@new_acc+''' where casher=''' + @old_acc +''' ;'

		begin transaction

		BEGIN TRY

		    exec sp_executesql @sqlmsg1
		    COMMIT TRANSACTION

        END TRY

        BEGIN CATCH

            ROLLBACK TRANSACTION
		    set @errstatus=1
		    return

        END CATCH
				
			

	end

    if @ok_it=1
	begin

		
	   --set @sqlmsg=' update acc_hdr set a_ref='+@new_acc+ @spce + ' where a_ref=' + @old_acc + @spce + ' and a_type=''' + @type_no +''' and a_brno='''+@br_no+''' ;'
				--+ ' update acc_dtl set a_ref='+@new_acc+ @spce + ' where a_ref=' + @old_acc + @spce + ' and a_type=''' + @type_no +''' and a_brno='''+@br_no+''' ;'
				--+ ' update sto_hdr set ref='+@new_acc+ @spce + ' where ref=' + @old_acc + @spce + ' and trtype=''' + @type_no +''' and branch='''+@br_no+''' ;'
				--+ ' update sto_dtl set ref='+@new_acc+ @spce + ' where ref=' + @old_acc + @spce + ' and trtype=''' + @type_no +''' and branch='''+@br_no+''' ;'
				
			
		begin transaction
		exec sp_executesql @sqlmsg1
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=2	
			return	--@errstatus		 				
		end
		else commit transaction

	end

    if @ok_su=1
	begin

	   --set @sqlmsg=' update acc_hdr set a_ref='+@new_acc+ @spce + ' where a_ref=' + @old_acc + @spce + ' and a_type=''' + @type_no +''' and a_brno='''+@br_no+''' and pu_no='''+@pu_no+''' ;'
				--+ ' update acc_dtl set a_ref='+@new_acc+ @spce + ' where a_ref=' + @old_acc + @spce + ' and a_type=''' + @type_no +''' and a_brno='''+@br_no+''' and pu_no='''+@pu_no+''' ;'
				--+ ' update pu_hdr set ref='+@new_acc+ @spce + ' where ref=' + @old_acc + @spce + ' and invtype=''' + @type_no +''' and branch='''+@br_no+''' and pucenter='''+@pu_no+''' ;'
				--+ ' update pu_dtl set ref='+@new_acc+ @spce + ' where ref=' + @old_acc + @spce + ' and invtype=''' + @type_no +''' and branch='''+@br_no+''' and pucenter='''+@pu_no+''' ;'
				
		begin transaction
		exec sp_executesql @sqlmsg1
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=3	
			return	--@errstatus		 				
		end
		else commit transaction


	end


 	if @ok_cu=1 
	begin

	
  

	  --set @sqlmsg=' update acc_hdr set a_ref=@neww where a_ref=@oldd and a_type=''' + @type_no +''' and a_brno='''+@br_no+''' and sl_no='''+@sl_no+''' ;'
			--	+ ' update acc_dtl set a_ref=@neww where a_ref=@oldd and a_type=''' + @type_no +''' and a_brno='''+@br_no+''' and sl_no='''+@sl_no+''' ;'
			--	+ ' update sales_hdr set ref=@neww where ref=@oldd and invtype=''' + @type_no +''' and branch='''+@br_no+''' and slcenter='''+@sl_no+''' ;'
			--	+ ' update sales_dtl set ref=@neww where ref=@oldd and invtype=''' + @type_no +''' and branch='''+@br_no+''' and slcenter='''+@sl_no+''' ;'
				
		begin transaction
		--set @neww=@new_acc; set @oldd=@old_acc;
		--exec sp_executesql @sqlmsg ,N'@neww INT, @oldd INT', @neww=@new_acc, @oldd=@old_acc;
		exec sp_executesql @sqlmsg1
		if @@error<>0
		begin
			rollback transaction
			set @errstatus=4	
			return	--@errstatus		 				
		end
		else commit transaction


	end
	return	@errstatus
end
GO
/****** Object:  StoredProcedure [dbo].[Save_Detail_Data]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Save_Detail_Data]

	@ok_sl_dtl bit,
	@ok_pu_dtl bit,
	@ok_sto_dtl bit,

	@br_no nvarchar(2),	
	@sl_no nvarchar(2),	
	@trtype nvarchar(2),
	@trref int,

	@sales_hdr_tb tb_sal_hdr  READONLY,
	@pu_hdr_tb tb_pu_hdr  READONLY,
	@sto_hdr_tb tb_sto_hdr  READONLY,

	@sales_dtl_tb tb_sales_dtl  READONLY,
	@pu_dtl_tb tb_pu_dtl  READONLY,
	@sto_dtl_tb tb_sto_dtl  READONLY,


	@errstatus int = 0 output
 
AS
BEGIN

	if (@ok_sl_dtl=1 and exists(select * from @sales_dtl_tb))
	   BEGIN 
	   BEGIN TRANSACTION;  
  
       BEGIN TRY 

	    delete from  sales_dtl where branch=@br_no and slcenter=@sl_no and invtype=@trtype and ref=@trref;

		merge sales_dtl as t 
		using (select * from @sales_dtl_tb where branch=@br_no and slcenter=@sl_no and invtype=@trtype and ref=@trref) as s
	    ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[slcenter] = s.[slcenter]
	    when not matched then
		INSERT([branch],[slcenter],[invtype],[ref],[invmdate],[invhdate],[itemno],[unicode],[qty],[fqty],[price],[discpc],[cost],[ds_acfm],[sl_acfm],[gclass],[custno],[fcy],[barcode],[imfcval],[pack],[pkqty],[shadd],[shdpk],[shdqty],[frtqty],[rtnqty],[whno],[cmbkey],[folio],[rplct_post],[sold_item_status],[tax_amt],[tax_id],[stk_or_ser],[tax_prcent],[offer_amt])
        VALUES(s.[branch],s.[slcenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[price],s.[discpc],s.[cost],s.[ds_acfm],s.[sl_acfm],s.[gclass],s.[custno],s.[fcy],s.[barcode],s.[imfcval],s.[pack],s.[pkqty],s.[shadd],s.[shdpk],s.[shdqty],s.[frtqty],s.[rtnqty],s.[whno],s.[cmbkey],s.[folio],s.[rplct_post],s.[sold_item_status],s.[tax_amt],s.[tax_id],s.[stk_or_ser],s.[tax_prcent],s.[offer_amt]);
		
        set @errstatus=1
	    COMMIT;  	  
	   END TRY

       BEGIN CATCH
	     set @errstatus=0
         ROLLBACK; 
       END CATCH
	   END
------  Process pu_hdr & pu_dtl Table  
-- 	if @ok_pu_dtl=1 
--	begin
------------ sdaad_hd Table
------		set @sqlmsg='merge pu_hdr as t 
------		using (select * from '+@dbc+'pu_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
------		+' and invmdate between '+@fmdate+' and '+@todate+') as s
------		 ON t.[branch] = s.[branch] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[pucenter] = s.[pucenter]
------		 when not matched then
------		INSERT([branch],[pucenter],[invtype],[ref],[invmdate],[invhdate],[supno],[text],[glser],[dsctype],[pstmode],[cur],[currate],[invttl],[invcst],[invdspc],[invdsvl],[nettotal],[invpaid],[casher],[entries],[released],[posted],[fixrate],[extamt],[extser],[pricetp],[ischeque],[chkno],[chkdate],[lastupdt],[jvgenrt],[cccommsn],[belowbal],[invsupno],[ccpayment],[rplsamt],[pdother],[slcode],[prpaidamt],[instldays],[instflag],[slcmnd],[inv_printed],[bendit],[modified],[rqstorder],[rqststld],[carrier],[rcvdtrn],[usrid],[address],[wst_key],[wst_amt],[gmark],[tameen],[shahan],[msfr],[mother],[etax],[remarks],[tax_amt_paid],[with_tax],[taxid],[tax_percent],[taxfree_amt],[reref],[sabr])
------        VALUES(s.[branch],s.[pucenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[supno],s.[text],s.[glser],s.[dsctype],s.[pstmode],s.[cur],s.[currate],s.[invttl],s.[invcst],s.[invdspc],s.[invdsvl],s.[nettotal],s.[invpaid],s.[casher],s.[entries],s.[released],s.[posted],s.[fixrate],s.[extamt],s.[extser],s.[pricetp],s.[ischeque],s.[chkno],s.[chkdate],s.[lastupdt],s.[jvgenrt],s.[cccommsn],s.[belowbal],s.[invsupno],s.[ccpayment],s.[rplsamt],s.[pdother],s.[slcode],s.[prpaidamt],s.[instldays],s.[instflag],s.[slcmnd],s.[inv_printed],s.[bendit],s.[modified],s.[rqstorder],s.[rqststld],s.[carrier],s.[rcvdtrn],s.[usrid],s.[address],s.[wst_key],s.[wst_amt],s.[gmark],s.[tameen],s.[shahan],s.[msfr],s.[mother],s.[etax],s.[remarks],s.[tax_amt_paid],s.[with_tax],s.[taxid],s.[tax_percent],s.[taxfree_amt],s.[reref],s.[sabr]);'
		
------		begin transaction
------		exec sp_executesql @sqlmsg
------		if @@error<>0
------		begin
------			rollback transaction
------			set @errstatus=6	
------			return	--@errstatus		 				
------		end
------		else commit transaction

     if (@ok_pu_dtl=1 and exists(select * from @pu_dtl_tb))
	    BEGIN
	   BEGIN TRANSACTION;  
  
       BEGIN TRY 

	    delete from  pu_dtl where branch=@br_no and pucenter=@sl_no and invtype=@trtype and ref=@trref;

		merge pu_dtl as t 
		using (select * from @pu_dtl_tb where branch=@br_no and pucenter=@sl_no and invtype=@trtype and ref=@trref) as s
		ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[pucenter] = s.[pucenter]
		when not matched then
		INSERT([branch],[pucenter],[invtype],[ref],[invmdate],[invhdate],[itemno],[unicode],[qty],[fqty],[price],[discpc],[cost],[ds_acfm],[sl_acfm],[gclass],[fullcost],[cur],[barcode],[imfcval],[pack],[pkqty],[shadd],[shdpk],[shdqty],[frtqty],[rtnqty],[whno],[cmbkey],[folio],[rplct_post],[sold_item_status],[tax_amt],[tax_id],[stk_or_ser],[tax_prcent],[expdate])
        VALUES(s.[branch],s.[pucenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[price],s.[discpc],s.[cost],s.[ds_acfm],s.[sl_acfm],s.[gclass],s.[fullcost],s.[cur],s.[barcode],s.[imfcval],s.[pack],s.[pkqty],s.[shadd],s.[shdpk],s.[shdqty],s.[frtqty],s.[rtnqty],s.[whno],s.[cmbkey],s.[folio],s.[rplct_post],s.[sold_item_status],s.[tax_amt],s.[tax_id],s.[stk_or_ser],s.[tax_prcent],s.[expdate]);
		
        set @errstatus=1
	    COMMIT;  	  
	   END TRY

       BEGIN CATCH
	     set @errstatus=0
         ROLLBACK; 
       END CATCH
	   END
--	end
------  Process Sto_hdr & Sto_dtl Table   
--	if @ok_st=1 
--	begin
	  
--	 ---- 	set @sqlmsg='merge sto_hdr as t 
--		----using (select * from '+@dbc+'sto_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
--		----+' and trmdate between '+@fmdate+' and '+@todate+') as s
--		---- ON t.[branch] = s.[branch] AND t.[ref] = s.[ref] AND t.[trtype] = s.[trtype]
--		---- when not matched then
--		----INSERT([branch],[trtype],[ref],[trmdate],[trhdate],[text],[amnttl],[costttl],[sysdate],[src],[released],[posted],[fcy],[fcyrate],[whno],[entries],[lastupdt],[towhno],[modified],[rcvdtrn],[supno],[usrid],[brsupp],[tobrno],[brxref],[glref],[isbrtrx],[asmtype],[repost],[remarks],[stkjvno])
--  ----      VALUES(s.[branch],s.[trtype],s.[ref],s.[trmdate],s.[trhdate],s.[text],s.[amnttl],s.[costttl],s.[sysdate],s.[src],s.[released],s.[posted],s.[fcy],s.[fcyrate],s.[whno],s.[entries],s.[lastupdt],s.[towhno],s.[modified],s.[rcvdtrn],s.[supno],s.[usrid],s.[brsupp],s.[tobrno],s.[brxref],s.[glref],s.[isbrtrx],s.[asmtype],s.[repost],s.[remarks],s.[stkjvno]);'
		
--		----begin transaction
--		----exec sp_executesql @sqlmsg
--		----if @@error<>0
--		----begin
--		----	rollback transaction
--		----	set @errstatus=8	
--		----	return	--@errstatus		 				
--		----end
--		----else commit transaction

      if (@ok_sto_dtl=1 and exists(select * from @sto_dtl_tb))
	    BEGIN

	   BEGIN TRANSACTION;  
  
       BEGIN TRY 

	    delete from  sto_dtl where branch=@br_no  and trtype=@trtype and ref=@trref;
        merge sto_dtl as t 
 		using ( select * from @sto_dtl_tb where branch=@br_no  and trtype=@trtype and ref=@trref) as s
 		ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[ref] = s.[ref] AND t.[trtype] = s.[trtype]
 		when not matched then
 		INSERT([branch],[trtype],[ref],[trmdate],[trhdate],[itemno],[unicode],[qty],[fqty],[whno],[binno],[lcost],[sysdate],[src],[lprice],[fcost],[fprice],[expdate],[towhno],[tobinno],[barcode],[cmbkey],[discpc],[pack],[pkqty],[shdpk],[shdqty],[folio],[rplct_post])
        VALUES(s.[branch],s.[trtype],s.[ref],s.[trmdate],s.[trhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[whno],s.[binno],s.[lcost],s.[sysdate],s.[src],s.[lprice],s.[fcost],s.[fprice],s.[expdate],s.[towhno],s.[tobinno],s.[barcode],s.[cmbkey],s.[discpc],s.[pack],s.[pkqty],s.[shdpk],s.[shdqty],s.[folio],s.[rplct_post]);
		
        set @errstatus=1
	    COMMIT;  	  
	   END TRY

       BEGIN CATCH
	     set @errstatus=0
         ROLLBACK; 
       END CATCH
	   END
--	end


--	return	@errstatus
END








GO
/****** Object:  StoredProcedure [dbo].[Save_Hdr_Dtl_Data]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Save_Hdr_Dtl_Data]

	@ok_sl_dtl bit,
	@ok_pu_dtl bit,
	@ok_sto_dtl bit,

	@br_no nvarchar(2),	
	@sl_no nvarchar(2),	
	@trtype nvarchar(2),
	@trref int,

	@sales_hdr_tb tb_sal_hdr  READONLY,
	@pu_hdr_tb tb_pu_hdr  READONLY,
	@sto_hdr_tb tb_sto_hdr  READONLY,

	@sales_dtl_tb tb_sales_dtl  READONLY,
	@pu_dtl_tb tb_pu_dtl  READONLY,
	@sto_dtl_tb tb_sto_dtl  READONLY,


	@errstatus int=0 output
 
AS
BEGIN

	if (@ok_sl_dtl=1 and exists(select * from @sales_dtl_tb))
	   BEGIN 
	   BEGIN TRANSACTION;  
  
       BEGIN TRY 

	    merge sales_hdr as t 
	    using (select * from @sales_hdr_tb where branch=@br_no and slcenter=@sl_no and invtype=@trtype and ref=@trref) as s
		ON t.[branch] = s.[branch] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[slcenter] = s.[slcenter]
	    when not matched then
		INSERT(branch,slcenter,invtype,ref,invmdate, invhdate,[text],remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,[suspend],glser,slcode,stkjvno,taxid,tax_percent,taxfree_amt,carrier,invpaid,chkno,reref,sanedcrd_amt,rtncash_dfrpl,fcy2,note2,note3,inv_mpay,cust_mobil)
        VALUES(s.branch,s.slcenter,s.invtype,s.ref,s.invmdate, s.invhdate,s.[text],s.remarks,s.casher,s.entries,round(s.invttl,2),round(s.invdsvl,2),round(s.nettotal,2),s.invdspc,round(s.tax_amt_rcvd,2),s.with_tax,s.usrid,s.custno,s.invcst,s.[suspend],s.glser,s.slcode,s.stkjvno,s.taxid,s.tax_percent,round(s.taxfree_amt,2),s.carrier,s.invpaid,s.chkno,s.reref,s.sanedcrd_amt,s.rtncash_dfrpl,s.fcy2,s.note2,s.note3,s.inv_mpay,s.cust_mobil);		

	   -- delete from  sales_dtl where branch=@br_no and slcenter=@sl_no and invtype=@trtype and ref=@trref;

		merge sales_dtl as t 
		using (select * from @sales_dtl_tb where branch=@br_no and slcenter=@sl_no and invtype=@trtype and ref=@trref) as s
	    ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[slcenter] = s.[slcenter]
	    when not matched then
		INSERT([branch],[slcenter],[invtype],[ref],[invmdate],[invhdate],[itemno],[unicode],[qty],[fqty],[price],[discpc],[cost],[ds_acfm],[sl_acfm],[gclass],[custno],[fcy],[barcode],[imfcval],[pack],[pkqty],[shadd],[shdpk],[shdqty],[frtqty],[rtnqty],[whno],[cmbkey],[folio],[rplct_post],[sold_item_status],[tax_amt],[tax_id],[stk_or_ser],[tax_prcent],[offer_amt])
        VALUES(s.[branch],s.[slcenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[price],s.[discpc],s.[cost],s.[ds_acfm],s.[sl_acfm],s.[gclass],s.[custno],s.[fcy],s.[barcode],s.[imfcval],s.[pack],s.[pkqty],s.[shadd],s.[shdpk],s.[shdqty],s.[frtqty],s.[rtnqty],s.[whno],s.[cmbkey],s.[folio],s.[rplct_post],s.[sold_item_status],s.[tax_amt],s.[tax_id],s.[stk_or_ser],s.[tax_prcent],s.[offer_amt]);
		
        set @errstatus=1
	    COMMIT;  	  
	   END TRY

       BEGIN CATCH
	     set @errstatus=0 --ERROR_MESSAGE()
         ROLLBACK; 
       END CATCH
	   END
------  Process pu_hdr & pu_dtl Table  
-- 	if @ok_pu_dtl=1 
--	begin
------------ sdaad_hd Table
------		set @sqlmsg='merge pu_hdr as t 
------		using (select * from '+@dbc+'pu_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
------		+' and invmdate between '+@fmdate+' and '+@todate+') as s
------		 ON t.[branch] = s.[branch] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[pucenter] = s.[pucenter]
------		 when not matched then
------		INSERT([branch],[pucenter],[invtype],[ref],[invmdate],[invhdate],[supno],[text],[glser],[dsctype],[pstmode],[cur],[currate],[invttl],[invcst],[invdspc],[invdsvl],[nettotal],[invpaid],[casher],[entries],[released],[posted],[fixrate],[extamt],[extser],[pricetp],[ischeque],[chkno],[chkdate],[lastupdt],[jvgenrt],[cccommsn],[belowbal],[invsupno],[ccpayment],[rplsamt],[pdother],[slcode],[prpaidamt],[instldays],[instflag],[slcmnd],[inv_printed],[bendit],[modified],[rqstorder],[rqststld],[carrier],[rcvdtrn],[usrid],[address],[wst_key],[wst_amt],[gmark],[tameen],[shahan],[msfr],[mother],[etax],[remarks],[tax_amt_paid],[with_tax],[taxid],[tax_percent],[taxfree_amt],[reref],[sabr])
------        VALUES(s.[branch],s.[pucenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[supno],s.[text],s.[glser],s.[dsctype],s.[pstmode],s.[cur],s.[currate],s.[invttl],s.[invcst],s.[invdspc],s.[invdsvl],s.[nettotal],s.[invpaid],s.[casher],s.[entries],s.[released],s.[posted],s.[fixrate],s.[extamt],s.[extser],s.[pricetp],s.[ischeque],s.[chkno],s.[chkdate],s.[lastupdt],s.[jvgenrt],s.[cccommsn],s.[belowbal],s.[invsupno],s.[ccpayment],s.[rplsamt],s.[pdother],s.[slcode],s.[prpaidamt],s.[instldays],s.[instflag],s.[slcmnd],s.[inv_printed],s.[bendit],s.[modified],s.[rqstorder],s.[rqststld],s.[carrier],s.[rcvdtrn],s.[usrid],s.[address],s.[wst_key],s.[wst_amt],s.[gmark],s.[tameen],s.[shahan],s.[msfr],s.[mother],s.[etax],s.[remarks],s.[tax_amt_paid],s.[with_tax],s.[taxid],s.[tax_percent],s.[taxfree_amt],s.[reref],s.[sabr]);'
		
------		begin transaction
------		exec sp_executesql @sqlmsg
------		if @@error<>0
------		begin
------			rollback transaction
------			set @errstatus=6	
------			return	--@errstatus		 				
------		end
------		else commit transaction

     if (@ok_pu_dtl=1 and exists(select * from @pu_dtl_tb))
	    BEGIN
	   BEGIN TRANSACTION;  
  
       BEGIN TRY 

	    merge pu_hdr as t 
		using (select * from @pu_hdr_tb where branch=@br_no and pucenter=@sl_no and invtype=@trtype and ref=@trref) as s
	    ON t.[branch] = s.[branch] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[pucenter] = s.[pucenter]
	    when not matched then
		INSERT([branch],[pucenter],[invtype],[ref],[invmdate],[invhdate],[supno],[text],[glser],[dsctype],[pstmode],[cur],[currate],[invttl],[invcst],[invdspc],[invdsvl],[nettotal],[invpaid],[casher],[entries],[released],[posted],[fixrate],[extamt],[extser],[pricetp],[ischeque],[chkno],[chkdate],[lastupdt],[jvgenrt],[cccommsn],[belowbal],[invsupno],[ccpayment],[rplsamt],[pdother],[slcode],[prpaidamt],[instldays],[instflag],[slcmnd],[inv_printed],[bendit],[modified],[rqstorder],[rqststld],[carrier],[rcvdtrn],[usrid],[address],[wst_key],[wst_amt],[gmark],[tameen],[shahan],[msfr],[mother],[etax],[remarks],[tax_amt_paid],[with_tax],[taxid],[tax_percent],[taxfree_amt],[reref],[sabr])
        VALUES(s.[branch],s.[pucenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[supno],s.[text],s.[glser],s.[dsctype],s.[pstmode],s.[cur],s.[currate],s.[invttl],s.[invcst],s.[invdspc],s.[invdsvl],s.[nettotal],s.[invpaid],s.[casher],s.[entries],s.[released],s.[posted],s.[fixrate],s.[extamt],s.[extser],s.[pricetp],s.[ischeque],s.[chkno],s.[chkdate],s.[lastupdt],s.[jvgenrt],s.[cccommsn],s.[belowbal],s.[invsupno],s.[ccpayment],s.[rplsamt],s.[pdother],s.[slcode],s.[prpaidamt],s.[instldays],s.[instflag],s.[slcmnd],s.[inv_printed],s.[bendit],s.[modified],s.[rqstorder],s.[rqststld],s.[carrier],s.[rcvdtrn],s.[usrid],s.[address],s.[wst_key],s.[wst_amt],s.[gmark],s.[tameen],s.[shahan],s.[msfr],s.[mother],s.[etax],s.[remarks],s.[tax_amt_paid],s.[with_tax],s.[taxid],s.[tax_percent],s.[taxfree_amt],s.[reref],s.[sabr]);
		

	   -- delete from  pu_dtl where branch=@br_no and pucenter=@sl_no and invtype=@trtype and ref=@trref;

		merge pu_dtl as t 
		using (select * from @pu_dtl_tb where branch=@br_no and pucenter=@sl_no and invtype=@trtype and ref=@trref) as s
		ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[invtype] = s.[invtype] AND t.[ref] = s.[ref] AND t.[pucenter] = s.[pucenter]
		when not matched then
		INSERT([branch],[pucenter],[invtype],[ref],[invmdate],[invhdate],[itemno],[unicode],[qty],[fqty],[price],[discpc],[cost],[ds_acfm],[sl_acfm],[gclass],[fullcost],[cur],[barcode],[imfcval],[pack],[pkqty],[shadd],[shdpk],[shdqty],[frtqty],[rtnqty],[whno],[cmbkey],[folio],[rplct_post],[sold_item_status],[tax_amt],[tax_id],[stk_or_ser],[tax_prcent],[expdate])
        VALUES(s.[branch],s.[pucenter],s.[invtype],s.[ref],s.[invmdate],s.[invhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[price],s.[discpc],s.[cost],s.[ds_acfm],s.[sl_acfm],s.[gclass],s.[fullcost],s.[cur],s.[barcode],s.[imfcval],s.[pack],s.[pkqty],s.[shadd],s.[shdpk],s.[shdqty],s.[frtqty],s.[rtnqty],s.[whno],s.[cmbkey],s.[folio],s.[rplct_post],s.[sold_item_status],s.[tax_amt],s.[tax_id],s.[stk_or_ser],s.[tax_prcent],s.[expdate]);
		
        set @errstatus=1
	    COMMIT;  	  
	   END TRY

       BEGIN CATCH
	     set @errstatus=0 --ERROR_MESSAGE()
         ROLLBACK; 
       END CATCH
	   END
--	end
------  Process Sto_hdr & Sto_dtl Table   
--	if @ok_st=1 
--	begin
	  
--	 ---- 	set @sqlmsg='merge sto_hdr as t 
--		----using (select * from '+@dbc+'sto_hdr with (NOLOCK) where 1=1 ' + iif(@ok_pstd=1,' and posted=1 ','')
--		----+' and trmdate between '+@fmdate+' and '+@todate+') as s
--		---- ON t.[branch] = s.[branch] AND t.[ref] = s.[ref] AND t.[trtype] = s.[trtype]
--		---- when not matched then
--		----INSERT([branch],[trtype],[ref],[trmdate],[trhdate],[text],[amnttl],[costttl],[sysdate],[src],[released],[posted],[fcy],[fcyrate],[whno],[entries],[lastupdt],[towhno],[modified],[rcvdtrn],[supno],[usrid],[brsupp],[tobrno],[brxref],[glref],[isbrtrx],[asmtype],[repost],[remarks],[stkjvno])
--  ----      VALUES(s.[branch],s.[trtype],s.[ref],s.[trmdate],s.[trhdate],s.[text],s.[amnttl],s.[costttl],s.[sysdate],s.[src],s.[released],s.[posted],s.[fcy],s.[fcyrate],s.[whno],s.[entries],s.[lastupdt],s.[towhno],s.[modified],s.[rcvdtrn],s.[supno],s.[usrid],s.[brsupp],s.[tobrno],s.[brxref],s.[glref],s.[isbrtrx],s.[asmtype],s.[repost],s.[remarks],s.[stkjvno]);'
		
--		----begin transaction
--		----exec sp_executesql @sqlmsg
--		----if @@error<>0
--		----begin
--		----	rollback transaction
--		----	set @errstatus=8	
--		----	return	--@errstatus		 				
--		----end
--		----else commit transaction

      if (@ok_sto_dtl=1 and exists(select * from @sto_dtl_tb))
	    BEGIN

	   BEGIN TRANSACTION;  
  
       BEGIN TRY 

	    merge sto_hdr as t 
		using ( select * from @sto_hdr_tb where branch=@br_no  and trtype=@trtype and ref=@trref) as s
		 ON t.[branch] = s.[branch] AND t.[ref] = s.[ref] AND t.[trtype] = s.[trtype]
		 when not matched then
		INSERT([branch],[trtype],[ref],[trmdate],[trhdate],[text],[amnttl],[costttl],[sysdate],[src],[released],[posted],[fcy],[fcyrate],[whno],[entries],[lastupdt],[towhno],[modified],[rcvdtrn],[supno],[usrid],[brsupp],[tobrno],[brxref],[glref],[isbrtrx],[asmtype],[repost],[remarks],[stkjvno])
        VALUES(s.[branch],s.[trtype],s.[ref],s.[trmdate],s.[trhdate],s.[text],s.[amnttl],s.[costttl],s.[sysdate],s.[src],s.[released],s.[posted],s.[fcy],s.[fcyrate],s.[whno],s.[entries],s.[lastupdt],s.[towhno],s.[modified],s.[rcvdtrn],s.[supno],s.[usrid],s.[brsupp],s.[tobrno],s.[brxref],s.[glref],s.[isbrtrx],s.[asmtype],s.[repost],s.[remarks],s.[stkjvno]);
		
	   -- delete from  sto_dtl where branch=@br_no  and trtype=@trtype and ref=@trref;

        merge sto_dtl as t 
 		using ( select * from @sto_dtl_tb where branch=@br_no  and trtype=@trtype and ref=@trref) as s
 		ON t.[branch] = s.[branch] AND t.[folio] = s.[folio] AND t.[ref] = s.[ref] AND t.[trtype] = s.[trtype]
 		when not matched then
 		INSERT([branch],[trtype],[ref],[trmdate],[trhdate],[itemno],[unicode],[qty],[fqty],[whno],[binno],[lcost],[sysdate],[src],[lprice],[fcost],[fprice],[expdate],[towhno],[tobinno],[barcode],[cmbkey],[discpc],[pack],[pkqty],[shdpk],[shdqty],[folio],[rplct_post])
        VALUES(s.[branch],s.[trtype],s.[ref],s.[trmdate],s.[trhdate],s.[itemno],s.[unicode],s.[qty],s.[fqty],s.[whno],s.[binno],s.[lcost],s.[sysdate],s.[src],s.[lprice],s.[fcost],s.[fprice],s.[expdate],s.[towhno],s.[tobinno],s.[barcode],s.[cmbkey],s.[discpc],s.[pack],s.[pkqty],s.[shdpk],s.[shdqty],s.[folio],s.[rplct_post]);
		
        set @errstatus=1
	    COMMIT;  	  
	   END TRY

       BEGIN CATCH
	     set @errstatus=0 --ERROR_MESSAGE()
         ROLLBACK; 
       END CATCH
	   END
--	end


--	return	@errstatus
END






GO
/****** Object:  StoredProcedure [dbo].[Save_Pos_Hdr_Dtl]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Save_Pos_Hdr_Dtl]

	--@ok_sl_dtl bit,
	--@ok_pu_dtl bit,
	--@ok_sto_dtl bit,

	@br_no nvarchar(2),	
	@sl_no nvarchar(2),	
	@contr int,
	@ref int,

	@pos_hdr_tb tblpos_hdr  READONLY,
	
	@pos_dtl_tb tblpos_dtl  READONLY,



	@errstatus int=0 output
 
AS
BEGIN

	if 1=1
	   BEGIN 
	   BEGIN TRANSACTION;  
  
       BEGIN TRY 

	     MERGE pos_hdr as t
         USING @pos_hdr_tb as s
         ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr
         WHEN MATCHED THEN
         UPDATE SET T.total = S.total,T.discount = S.discount,T.net_total = S.net_total,t.tax_amt=s.tax_amt
         WHEN NOT MATCHED THEN
         INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.date,round(s.total,2),s.count,s.payed,s.total_cost,s.saleman,s.req_no,s.cust_no,s.discount,round(s.net_total,2),s.sysdate,s.gen_ref,round(s.tax_amt,4),s.dscper,s.card_type,s.card_amt,s.rref,s.rcontr,s.taxfree_amt,s.note,s.mobile);

	   ----------------------------------------------------------------------------------------

		 MERGE pos_dtl as t
         USING @pos_dtl_tb as s
         ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr and T.srno=S.srno
         WHEN MATCHED THEN
         UPDATE SET T.price = S.price,T.qty=S.qty,t.itemno=s.itemno,t.srno=s.srno
         WHEN NOT MATCHED THEN
         INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.barcode,s.name,s.unit,s.price,s.qty,s.cost,s.is_req,s.itemno,s.srno,s.pkqty,s.discpc,s.tax_id,s.tax_amt,s.rqty,s.offr_amt);
                
        set @errstatus=1
	    COMMIT;  	  
	   END TRY

       BEGIN CATCH
	     set @errstatus=0 --ERROR_MESSAGE()
         ROLLBACK; 
       END CATCH
	   END


END


GO
/****** Object:  StoredProcedure [dbo].[Search_Customer]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[Search_Customer]
@Criterion varchar(50)
AS
BEGIN
SELECT [ID_CUSTOMER]
      ,[FIRST_NAME] as 'الاسم الشخصي'
      ,[LAST_NAME] as 'الاسم العائلي'
      ,[TEL] as 'الهاتف'
      ,[EMAIL] as 'البريد الالكتروني'
      ,[IMAGE_CUSTOMER]
  FROM [CUSTOMERS00]
where [FIRST_NAME]+[LAST_NAME]+[TEL]+[EMAIL] like '%'+@Criterion+'%'









END;


GO
/****** Object:  StoredProcedure [dbo].[SearchAllTables]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchAllTables]
(
    @SearchStr nvarchar(100)
)
AS
BEGIN

-- Copyright © 2002 Narayana Vyas Kondreddi. All rights reserved.
-- Purpose: To search all columns of all tables for a given search string
-- Written by: Narayana Vyas Kondreddi
-- Site: http://vyaskn.tripod.com
-- Tested on: SQL Server 7.0 and SQL Server 2000
-- Date modified: 28th July 2002 22:50 GMT

DECLARE @Results TABLE(ColumnName nvarchar(370), ColumnValue nvarchar(3630))

SET NOCOUNT ON

DECLARE @TableName nvarchar(256), @ColumnName nvarchar(128), @SearchStr2 nvarchar(110)
SET  @TableName = ''
SET @SearchStr2 = QUOTENAME('%' + @SearchStr + '%','''')

WHILE @TableName IS NOT NULL
BEGIN
    SET @ColumnName = ''
    SET @TableName = 
    (
        SELECT MIN(QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME))
        FROM    INFORMATION_SCHEMA.TABLES
        WHERE       TABLE_TYPE = 'BASE TABLE'
            AND QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) > @TableName
            AND OBJECTPROPERTY(
                    OBJECT_ID(
                        QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME)
                         ), 'IsMSShipped'
                           ) = 0
    )

    WHILE (@TableName IS NOT NULL) AND (@ColumnName IS NOT NULL)
    BEGIN
        SET @ColumnName =
        (
            SELECT MIN(QUOTENAME(COLUMN_NAME))
            FROM    INFORMATION_SCHEMA.COLUMNS
            WHERE       TABLE_SCHEMA    = PARSENAME(@TableName, 2)
                AND TABLE_NAME  = PARSENAME(@TableName, 1)
                AND DATA_TYPE IN ('char', 'varchar', 'nchar', 'nvarchar')
                AND QUOTENAME(COLUMN_NAME) > @ColumnName
        )

        IF @ColumnName IS NOT NULL
        BEGIN
            INSERT INTO @Results
            EXEC
            (
                'SELECT ''' + @TableName + '.' + @ColumnName + ''', LEFT(' + @ColumnName + ', 3630) 
                FROM ' + @TableName + ' (NOLOCK) ' +
                ' WHERE ' + @ColumnName + ' LIKE ' + @SearchStr2
            )
        END
    END 
END

SELECT ColumnName, ColumnValue FROM @Results
END




GO
/****** Object:  StoredProcedure [dbo].[SearchProduct]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SearchProduct]
@ID varchar(50)
AS
BEGIN
SELECT [item_no] as 'رقم الصنف'
      ,[item_name]as 'اسم الصنف'
      ,[item_cost] as 'تكلفة الصنف'
      ,[item_price]as 'السعر'
      ,[item_barcode]as 'الباركود'
      
      
      ,[item_group] as 'مجموعة'
      ,[item_image] as 'صورة'

  FROM [dbo].[items]
where 
[item_no]+
[item_name]
Like '%'+@ID+'%'








END;



GO
/****** Object:  StoredProcedure [dbo].[SearchUsers]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SearchUsers]
@Criterion varchar(50)
AS
BEGIN
SELECT [ID] as 'اسم المستخدم'
       ,[FullName] as 'الاسم الكامل'
     ,[PWD] as 'كلمة السر'
      ,[UserType] as 'نوع المستخدم'
  FROM [Product_DB].[dbo].[TBL_USERS]
where ID+FullName+PWD+UserType	like '%' + @Criterion + '%'










END;


GO
/****** Object:  StoredProcedure [dbo].[Select_Product]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Select_Product]

(
@no nvarchar(16),
@barcode nvarchar(16)
)
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
BEGIN
select * from items where item_no=@no and item_barcode=@barcode
	/* SET NOCOUNT ON */
	RETURN










END;


GO
/****** Object:  StoredProcedure [dbo].[send_pos_inv]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[send_pos_inv] 
@reff int,
@dbname nvarchar(50),
@errstatus int = 0 output

AS
BEGIN

declare @sqlmsg nvarchar(max)

 set  @sqlmsg='MERGE [main].[db04y2020].dbo.pos_hdr  as t'
                +' USING (select * from pos_hdr where ref=38) as s'
                +' ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr'
                +' WHEN MATCHED THEN'
                +' UPDATE SET T.total = S.total,T.discount = S.discount,T.net_total = S.net_total,t.tax_amt=s.tax_amt'
                +' WHEN NOT MATCHED THEN'
                +' INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.date,s.total,s.count,s.payed,s.total_cost,s.saleman,s.req_no,s.cust_no,s.discount,s.net_total,s.sysdate,s.gen_ref,s.tax_amt,s.dscper,s.card_type,s.card_amt,s.rref,s.rcontr,s.taxfree_amt,s.note,s.mobile);'
   begin transaction
	exec sp_executesql @sqlmsg
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=1
		return 
	end
	commit transaction
 

 END


 BEGIN
 
 set  @sqlmsg= 'MERGE [main].[db04y2020].dbo.pos_dtl  as t'
              +' USING (select * from pos_dtl where ref=38) as s'
              +' ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr'
              +' WHEN MATCHED THEN'
              +' UPDATE SET T.price = S.price,T.qty=S.qty,t.itemno=s.itemno,t.srno=s.srno'
              +' WHEN NOT MATCHED THEN'
              +' INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.barcode,s.name,s.unit,s.price,s.qty,s.cost,s.is_req,s.itemno,s.srno,s.pkqty,s.discpc,s.tax_id,s.tax_amt,s.rqty,s.offr_amt);'
                 
   begin transaction
	exec sp_executesql @sqlmsg
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=2
		return 
	end
	commit transaction

  END


GO
/****** Object:  StoredProcedure [dbo].[show_all_tables]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[show_all_tables]
AS
BEGIN
	CREATE TABLE #counts
(
    table_name varchar(255),
    row_count int
)

EXEC sp_MSForEachTable @command1='INSERT #counts (table_name, row_count) SELECT ''?'', COUNT(*) FROM ?'
SELECT table_name, row_count FROM #counts ORDER BY  row_count DESC
DROP TABLE #counts

END

GO
/****** Object:  StoredProcedure [dbo].[sp_generate_merge]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- TO EXECUTE ================ exec sp_generate_merge 'Table_Name',@schema='dbo'

--Turn system object marking on

CREATE PROCEDURE [dbo].[sp_generate_merge]
(
 @table_name varchar(776), -- The table/view for which the MERGE statement will be generated using the existing data
 @target_table varchar(776) = NULL, -- Use this parameter to specify a different table name into which the data will be inserted/updated/deleted
 @from nvarchar(max) = NULL, -- Use this parameter to filter the rows based on a filter condition (using WHERE)
 @include_timestamp bit = 0, -- Specify 1 for this parameter, if you want to include the TIMESTAMP/ROWVERSION column's data in the MERGE statement
 @debug_mode bit = 0, -- If @debug_mode is set to 1, the SQL statements constructed by this procedure will be printed for later examination
 @schema varchar(64) = NULL, -- Use this parameter if you are not the owner of the table
 @ommit_images bit = 0, -- Use this parameter to generate MERGE statement by omitting the 'image' columns
 @ommit_identity bit = 0, -- Use this parameter to ommit the identity columns
 @top int = NULL, -- Use this parameter to generate a MERGE statement only for the TOP n rows
 @cols_to_include varchar(8000) = NULL, -- List of columns to be included in the MERGE statement
 @cols_to_exclude varchar(8000) = NULL, -- List of columns to be excluded from the MERGE statement
 @update_only_if_changed bit = 1, -- When 1, only performs an UPDATE operation if an included column in a matched row has changed.
 @delete_if_not_matched bit = 1, -- When 1, deletes unmatched source rows from target, when 0 source rows will only be used to update existing rows or insert new.
 @disable_constraints bit = 0, -- When 1, disables foreign key constraints and enables them after the MERGE statement
 @ommit_computed_cols bit = 0, -- When 1, computed columns will not be included in the MERGE statement
 @include_use_db bit = 1, -- When 1, includes a USE [DatabaseName] statement at the beginning of the generated batch
 @results_to_text bit = 0, -- When 1, outputs results to grid/messages window. When 0, outputs MERGE statement in an XML fragment.
 @include_rowsaffected bit = 1, -- When 1, a section is added to the end of the batch which outputs rows affected by the MERGE
 @nologo bit = 0, -- When 1, the "About" comment is suppressed from output
 @batch_separator VARCHAR(50) = 'GO' -- Batch separator to use
)
AS
BEGIN

/***********************************************************************************************************
Procedure: sp_generate_merge (Version 0.93)
 (Adapted by Daniel Nolan for SQL Server 2008/2012)

Adapted from: sp_generate_inserts (Build 22) 
 (Copyright © 2002 Narayana Vyas Kondreddi. All rights reserved.)

Purpose: To generate a MERGE statement from existing data, which will INSERT/UPDATE/DELETE data based
 on matching primary key values in the source/target table.
 
 The generated statements can be executed to replicate the data in some other location.
 
 Typical use cases:
 * Generate statements for static data tables, store the .SQL file in source control and use 
 it as part of your Dev/Test/Prod deployment. The generated statements are re-runnable, so 
 you can make changes to the file and migrate those changes between environments.
 
 * Generate statements from your Production tables and then run those statements in your 
 Dev/Test environments. Schedule this as part of a SQL Job to keep all of your environments 
 in-sync.
 
 * Enter test data into your Dev environment, and then generate statements from the Dev
 tables so that you can always reproduce your test database with valid sample data.
 

Written by: Narayana Vyas Kondreddi
 http://vyaskn.tripod.com

 Daniel Nolan
 http://danere.com
 @dan3r3

Acknowledgements (sp_generate_merge):
 Nathan Skerl -- StackOverflow answer that provided a workaround for the output truncation problem
 http://stackoverflow.com/a/10489767/266882

 Bill Gibson -- Blog that detailed the static data table use case; the inspiration for this proc
 http://blogs.msdn.com/b/ssdt/archive/2012/02/02/including-data-in-an-sql-server-database-project.aspx
 
 Bill Graziano -- Blog that provided the groundwork for MERGE statement generation
 http://weblogs.sqlteam.com/billg/archive/2011/02/15/generate-merge-statements-from-a-table.aspx 

Acknowledgements (sp_generate_inserts):
 Divya Kalra -- For beta testing
 Mark Charsley -- For reporting a problem with scripting uniqueidentifier columns with NULL values
 Artur Zeygman -- For helping me simplify a bit of code for handling non-dbo owned tables
 Joris Laperre -- For reporting a regression bug in handling text/ntext columns

Tested on: SQL Server 2008 (10.50.1600), SQL Server 2012 (11.0.2100)

Date created: January 17th 2001 21:52 GMT
Modified: May 1st 2002 19:50 GMT
Last Modified: September 27th 2012 10:00 AEDT

Email: dan@danere.com, vyaskn@hotmail.com

NOTE: This procedure may not work with tables with a large number of columns (> 500).
 Results can be unpredictable with huge text columns or SQL Server 2000's sql_variant data types
 IMPORTANT: This procedure has not been extensively tested with international data (Extended characters or Unicode). If needed
 you might want to convert the datatypes of character variables in this procedure to their respective unicode counterparts
 like nchar and nvarchar

Get Started: Ensure that your SQL client is configured to send results to grid (default SSMS behaviour).
This ensures that the generated MERGE statement can be output in full, getting around SSMS's 4000 nchar limit.
After running this proc, click the hyperlink within the single row returned to copy the generated MERGE statement.

Example 1: To generate a MERGE statement for table 'titles':
 
 EXEC sp_generate_merge 'titles'

Example 2: To generate a MERGE statement for 'titlesCopy' table from 'titles' table:

 EXEC sp_generate_merge 'titles', 'titlesCopy'

Example 3: To generate a MERGE statement for table 'titles' that will unconditionally UPDATE matching rows 
 (ie. not perform a "has data changed?" check prior to going ahead with an UPDATE):
 
 EXEC sp_generate_merge 'titles', @update_only_if_changed = 0

Example 4: To generate a MERGE statement for 'titles' table for only those titles 
 which contain the word 'Computer' in them:
 NOTE: Do not complicate the FROM or WHERE clause here. It's assumed that you are good with T-SQL if you are using this parameter

 EXEC sp_generate_merge 'titles', @from = "from titles where title like '%Computer%'"

Example 5: To specify that you want to include TIMESTAMP column's data as well in the MERGE statement:
 (By default TIMESTAMP column's data is not scripted)

 EXEC sp_generate_merge 'titles', @include_timestamp = 1

Example 6: To print the debug information:

 EXEC sp_generate_merge 'titles', @debug_mode = 1

Example 7: If the table is in a different schema to the default, use @schema parameter to specify the schema name
 To use this option, you must have SELECT permissions on that table

 EXEC sp_generate_merge 'Nickstable', @schema = 'Nick'

Example 8: To generate a MERGE statement for the rest of the columns excluding images

 EXEC sp_generate_merge 'imgtable', @ommit_images = 1

Example 9: To generate a MERGE statement excluding (omitting) IDENTITY columns:
 (By default IDENTITY columns are included in the MERGE statement)

 EXEC sp_generate_merge 'mytable', @ommit_identity = 1

Example 10: To generate a MERGE statement for the TOP 10 rows in the table:
 
 EXEC sp_generate_merge 'mytable', @top = 10

Example 11: To generate a MERGE statement with only those columns you want:
 
 EXEC sp_generate_merge 'titles', @cols_to_include = "'title','title_id','au_id'"

Example 12: To generate a MERGE statement by omitting certain columns:
 
 EXEC sp_generate_merge 'titles', @cols_to_exclude = "'title','title_id','au_id'"

Example 13: To avoid checking the foreign key constraints while loading data with a MERGE statement:
 
 EXEC sp_generate_merge 'titles', @disable_constraints = 1

Example 14: To exclude computed columns from the MERGE statement:

 EXEC sp_generate_merge 'MyTable', @ommit_computed_cols = 1
 
***********************************************************************************************************/

SET NOCOUNT ON


--Making sure user only uses either @cols_to_include or @cols_to_exclude
IF ((@cols_to_include IS NOT NULL) AND (@cols_to_exclude IS NOT NULL))
 BEGIN
 RAISERROR('Use either @cols_to_include or @cols_to_exclude. Do not use both the parameters at once',16,1)
 RETURN -1 --Failure. Reason: Both @cols_to_include and @cols_to_exclude parameters are specified
 END


--Making sure the @cols_to_include and @cols_to_exclude parameters are receiving values in proper format
IF ((@cols_to_include IS NOT NULL) AND (PATINDEX('''%''',@cols_to_include) = 0))
 BEGIN
 RAISERROR('Invalid use of @cols_to_include property',16,1)
 PRINT 'Specify column names surrounded by single quotes and separated by commas'
 PRINT 'Eg: EXEC sp_generate_merge titles, @cols_to_include = "''title_id'',''title''"'
 RETURN -1 --Failure. Reason: Invalid use of @cols_to_include property
 END

IF ((@cols_to_exclude IS NOT NULL) AND (PATINDEX('''%''',@cols_to_exclude) = 0))
 BEGIN
 RAISERROR('Invalid use of @cols_to_exclude property',16,1)
 PRINT 'Specify column names surrounded by single quotes and separated by commas'
 PRINT 'Eg: EXEC sp_generate_merge titles, @cols_to_exclude = "''title_id'',''title''"'
 RETURN -1 --Failure. Reason: Invalid use of @cols_to_exclude property
 END


--Checking to see if the database name is specified along wih the table name
--Your database context should be local to the table for which you want to generate a MERGE statement
--specifying the database name is not allowed
IF (PARSENAME(@table_name,3)) IS NOT NULL
 BEGIN
 RAISERROR('Do not specify the database name. Be in the required database and just specify the table name.',16,1)
 RETURN -1 --Failure. Reason: Database name is specified along with the table name, which is not allowed
 END


--Checking for the existence of 'user table' or 'view'
--This procedure is not written to work on system tables
--To script the data in system tables, just create a view on the system tables and script the view instead
IF @schema IS NULL
 BEGIN
 IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table_name AND (TABLE_TYPE = 'BASE TABLE' OR TABLE_TYPE = 'VIEW') AND TABLE_SCHEMA = SCHEMA_NAME())
 BEGIN
 RAISERROR('User table or view not found.',16,1)
 PRINT 'You may see this error if the specified table is not in your default schema (' + SCHEMA_NAME() + '). In that case use @schema parameter to specify the schema name.'
 PRINT 'Make sure you have SELECT permission on that table or view.'
 RETURN -1 --Failure. Reason: There is no user table or view with this name
 END
 END
ELSE
 BEGIN
 IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table_name AND (TABLE_TYPE = 'BASE TABLE' OR TABLE_TYPE = 'VIEW') AND TABLE_SCHEMA = @schema)
 BEGIN
 RAISERROR('User table or view not found.',16,1)
 PRINT 'Make sure you have SELECT permission on that table or view.'
 RETURN -1 --Failure. Reason: There is no user table or view with this name 
 END
 END


--Variable declarations
DECLARE @Column_ID int, 
 @Column_List varchar(8000), 
 @Column_List_For_Update varchar(8000), 
 @Column_List_For_Check varchar(8000), 
 @Column_Name varchar(128), 
 @Column_Name_Unquoted varchar(128), 
 @Data_Type varchar(128), 
 @Actual_Values nvarchar(max), --This is the string that will be finally executed to generate a MERGE statement
 @IDN varchar(128), --Will contain the IDENTITY column's name in the table
 @Target_Table_For_Output varchar(776),
 @Source_Table_Qualified varchar(776)
 
 

--Variable Initialization
SET @IDN = ''
SET @Column_ID = 0
SET @Column_Name = ''
SET @Column_Name_Unquoted = ''
SET @Column_List = ''
SET @Column_List_For_Update = ''
SET @Column_List_For_Check = ''
SET @Actual_Values = ''

--Variable Defaults
IF @schema IS NULL
 BEGIN
 SET @Target_Table_For_Output = QUOTENAME(COALESCE(@target_table, @table_name))
 END
ELSE
 BEGIN
 SET @Target_Table_For_Output = QUOTENAME(@schema) + '.' + QUOTENAME(COALESCE(@target_table, @table_name))
 END

SET @Source_Table_Qualified = QUOTENAME(COALESCE(@schema,SCHEMA_NAME())) + '.' + QUOTENAME(@table_name)

--To get the first column's ID
SELECT @Column_ID = MIN(ORDINAL_POSITION) 
FROM INFORMATION_SCHEMA.COLUMNS (NOLOCK) 
WHERE TABLE_NAME = @table_name
AND TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())


--Loop through all the columns of the table, to get the column names and their data types
WHILE @Column_ID IS NOT NULL
 BEGIN
 SELECT @Column_Name = QUOTENAME(COLUMN_NAME), 
 @Column_Name_Unquoted = COLUMN_NAME,
 @Data_Type = DATA_TYPE 
 FROM INFORMATION_SCHEMA.COLUMNS (NOLOCK) 
 WHERE ORDINAL_POSITION = @Column_ID
 AND TABLE_NAME = @table_name
 AND TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())

 IF @cols_to_include IS NOT NULL --Selecting only user specified columns
 BEGIN
 IF CHARINDEX( '''' + SUBSTRING(@Column_Name,2,LEN(@Column_Name)-2) + '''',@cols_to_include) = 0 
 BEGIN
 GOTO SKIP_LOOP
 END
 END

 IF @cols_to_exclude IS NOT NULL --Selecting only user specified columns
 BEGIN
 IF CHARINDEX( '''' + SUBSTRING(@Column_Name,2,LEN(@Column_Name)-2) + '''',@cols_to_exclude) <> 0 
 BEGIN
 GOTO SKIP_LOOP
 END
 END

 --Making sure to output SET IDENTITY_INSERT ON/OFF in case the table has an IDENTITY column
 IF (SELECT COLUMNPROPERTY( OBJECT_ID(@Source_Table_Qualified),SUBSTRING(@Column_Name,2,LEN(@Column_Name) - 2),'IsIdentity')) = 1 
 BEGIN
 IF @ommit_identity = 0 --Determing whether to include or exclude the IDENTITY column
 SET @IDN = @Column_Name
 ELSE
 GOTO SKIP_LOOP 
 END
 
 --Making sure whether to output computed columns or not
 IF @ommit_computed_cols = 1
 BEGIN
 IF (SELECT COLUMNPROPERTY( OBJECT_ID(@Source_Table_Qualified),SUBSTRING(@Column_Name,2,LEN(@Column_Name) - 2),'IsComputed')) = 1 
 BEGIN
 GOTO SKIP_LOOP 
 END
 END
 
 --Tables with columns of IMAGE data type are not supported for obvious reasons
 IF(@Data_Type in ('image'))
 BEGIN
 IF (@ommit_images = 0)
 BEGIN
 RAISERROR('Tables with image columns are not supported.',16,1)
 PRINT 'Use @ommit_images = 1 parameter to generate a MERGE for the rest of the columns.'
 RETURN -1 --Failure. Reason: There is a column with image data type
 END
 ELSE
 BEGIN
 GOTO SKIP_LOOP
 END
 END

 --Determining the data type of the column and depending on the data type, the VALUES part of
 --the MERGE statement is generated. Care is taken to handle columns with NULL values. Also
 --making sure, not to lose any data from flot, real, money, smallmomey, datetime columns
 SET @Actual_Values = @Actual_Values +
 CASE 
 WHEN @Data_Type IN ('char','nchar') 
 THEN 
 'COALESCE(''N'''''' + REPLACE(RTRIM(' + @Column_Name + '),'''''''','''''''''''')+'''''''',''NULL'')'
 WHEN @Data_Type IN ('varchar','nvarchar') 
 THEN 
 'COALESCE(''N'''''' + REPLACE(' + @Column_Name + ','''''''','''''''''''')+'''''''',''NULL'')'
 WHEN @Data_Type IN ('datetime','smalldatetime','datetime2','date') 
 THEN 
 'COALESCE('''''''' + RTRIM(CONVERT(char,' + @Column_Name + ',127))+'''''''',''NULL'')'
 WHEN @Data_Type IN ('uniqueidentifier') 
 THEN 
 'COALESCE(''N'''''' + REPLACE(CONVERT(char(36),RTRIM(' + @Column_Name + ')),'''''''','''''''''''')+'''''''',''NULL'')'
 WHEN @Data_Type IN ('text') 
 THEN 
 'COALESCE(''N'''''' + REPLACE(CONVERT(varchar(max),' + @Column_Name + '),'''''''','''''''''''')+'''''''',''NULL'')' 
 WHEN @Data_Type IN ('ntext') 
 THEN 
 'COALESCE('''''''' + REPLACE(CONVERT(nvarchar(max),' + @Column_Name + '),'''''''','''''''''''')+'''''''',''NULL'')' 
 WHEN @Data_Type IN ('xml') 
 THEN 
 'COALESCE('''''''' + REPLACE(CONVERT(nvarchar(max),' + @Column_Name + '),'''''''','''''''''''')+'''''''',''NULL'')' 
 WHEN @Data_Type IN ('binary','varbinary') 
 THEN 
 'COALESCE(RTRIM(CONVERT(varchar(max),' + @Column_Name + ', 1)),''NULL'')' 
 WHEN @Data_Type IN ('timestamp','rowversion') 
 THEN 
 CASE 
 WHEN @include_timestamp = 0 
 THEN 
 '''DEFAULT''' 
 ELSE 
 'COALESCE(RTRIM(CONVERT(char,' + 'CONVERT(int,' + @Column_Name + '))),''NULL'')' 
 END
 WHEN @Data_Type IN ('float','real','money','smallmoney')
 THEN
 'COALESCE(LTRIM(RTRIM(' + 'CONVERT(char, ' + @Column_Name + ',2)' + ')),''NULL'')' 
 WHEN @Data_Type IN ('hierarchyid')
 THEN 
  'COALESCE(''hierarchyid::Parse(''+'''''''' + LTRIM(RTRIM(' + 'CONVERT(char, ' + @Column_Name + ')' + '))+''''''''+'')'',''NULL'')' 
 ELSE 
 'COALESCE(LTRIM(RTRIM(' + 'CONVERT(char, ' + @Column_Name + ')' + ')),''NULL'')' 
 END + '+' + ''',''' + ' + '
 
 --Generating the column list for the MERGE statement
 SET @Column_List = @Column_List + @Column_Name + ',' 
 
 --Don't update Primary Key or Identity columns
 IF NOT EXISTS(
 SELECT 1
 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,
 INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
 WHERE pk.TABLE_NAME = @table_name
 AND pk.TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())
 AND CONSTRAINT_TYPE = 'PRIMARY KEY'
 AND c.TABLE_NAME = pk.TABLE_NAME
 AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA
 AND c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME
 AND c.COLUMN_NAME = @Column_Name_Unquoted 
 )
 BEGIN
 SET @Column_List_For_Update = @Column_List_For_Update + @Column_Name + ' = Source.' + @Column_Name + ', 
  ' 
 SET @Column_List_For_Check = @Column_List_For_Check +
 CASE @Data_Type 
 WHEN 'text' THEN CHAR(10) + CHAR(9) + 'NULLIF(CAST(Source.' + @Column_Name + ' AS VARCHAR(MAX)), CAST(Target.' + @Column_Name + ' AS VARCHAR(MAX))) IS NOT NULL OR NULLIF(CAST(Target.' + @Column_Name + ' AS VARCHAR(MAX)), CAST(Source.' + @Column_Name + ' AS VARCHAR(MAX))) IS NOT NULL OR '
 WHEN 'ntext' THEN CHAR(10) + CHAR(9) + 'NULLIF(CAST(Source.' + @Column_Name + ' AS NVARCHAR(MAX)), CAST(Target.' + @Column_Name + ' AS NVARCHAR(MAX))) IS NOT NULL OR NULLIF(CAST(Target.' + @Column_Name + ' AS NVARCHAR(MAX)), CAST(Source.' + @Column_Name + ' AS NVARCHAR(MAX))) IS NOT NULL OR ' 
 ELSE CHAR(10) + CHAR(9) + 'NULLIF(Source.' + @Column_Name + ', Target.' + @Column_Name + ') IS NOT NULL OR NULLIF(Target.' + @Column_Name + ', Source.' + @Column_Name + ') IS NOT NULL OR '
 END 
 END

 SKIP_LOOP: --The label used in GOTO

 SELECT @Column_ID = MIN(ORDINAL_POSITION) 
 FROM INFORMATION_SCHEMA.COLUMNS (NOLOCK) 
 WHERE TABLE_NAME = @table_name
 AND TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())
 AND ORDINAL_POSITION > @Column_ID

 END --Loop ends here!


--To get rid of the extra characters that got concatenated during the last run through the loop
IF LEN(@Column_List_For_Update) <> 0
 BEGIN
 SET @Column_List_For_Update = ' ' + LEFT(@Column_List_For_Update,len(@Column_List_For_Update) - 4)
 END

IF LEN(@Column_List_For_Check) <> 0
 BEGIN
 SET @Column_List_For_Check = LEFT(@Column_List_For_Check,len(@Column_List_For_Check) - 3)
 END

SET @Actual_Values = LEFT(@Actual_Values,len(@Actual_Values) - 6)

SET @Column_List = LEFT(@Column_List,len(@Column_List) - 1)
IF LEN(LTRIM(@Column_List)) = 0
 BEGIN
 RAISERROR('No columns to select. There should at least be one column to generate the output',16,1)
 RETURN -1 --Failure. Reason: Looks like all the columns are ommitted using the @cols_to_exclude parameter
 END


--Get the join columns ----------------------------------------------------------
DECLARE @PK_column_list VARCHAR(8000)
DECLARE @PK_column_joins VARCHAR(8000)
SET @PK_column_list = ''
SET @PK_column_joins = ''

SELECT @PK_column_list = @PK_column_list + '[' + c.COLUMN_NAME + '], '
, @PK_column_joins = @PK_column_joins + 'Target.[' + c.COLUMN_NAME + '] = Source.[' + c.COLUMN_NAME + '] AND '
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,
INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
WHERE pk.TABLE_NAME = @table_name
AND pk.TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())
AND CONSTRAINT_TYPE = 'PRIMARY KEY'
AND c.TABLE_NAME = pk.TABLE_NAME
AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA
AND c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME

IF IsNull(@PK_column_list, '') = '' 
 BEGIN
 RAISERROR('Table has no primary keys. There should at least be one column in order to have a valid join.',16,1)
 RETURN -1 --Failure. Reason: looks like table doesn't have any primary keys
 END

SET @PK_column_list = LEFT(@PK_column_list, LEN(@PK_column_list) -1)
SET @PK_column_joins = LEFT(@PK_column_joins, LEN(@PK_column_joins) -4)


--Forming the final string that will be executed, to output the a MERGE statement
SET @Actual_Values = 
 'SELECT ' + 
 CASE WHEN @top IS NULL OR @top < 0 THEN '' ELSE ' TOP ' + LTRIM(STR(@top)) + ' ' END + 
 '''' + 
 ' '' + CASE WHEN ROW_NUMBER() OVER (ORDER BY ' + @PK_column_list + ') = 1 THEN '' '' ELSE '','' END + ''(''+ ' + @Actual_Values + '+'')''' + ' ' + 
 COALESCE(@from,' FROM ' + @Source_Table_Qualified + ' (NOLOCK) ORDER BY ' + @PK_column_list)

 DECLARE @output VARCHAR(MAX) = ''
 DECLARE @b CHAR(1) = CHAR(13)

--Determining whether to ouput any debug information
IF @debug_mode =1
 BEGIN
 SET @output += @b + '/*****START OF DEBUG INFORMATION*****'
 SET @output += @b + ''
 SET @output += @b + 'The primary key column list:'
 SET @output += @b + @PK_column_list
 SET @output += @b + ''
 SET @output += @b + 'The INSERT column list:'
 SET @output += @b + @Column_List
 SET @output += @b + ''
 SET @output += @b + 'The UPDATE column list:'
 SET @output += @b + @Column_List_For_Update
 SET @output += @b + ''
 SET @output += @b + 'The SELECT statement executed to generate the MERGE:'
 SET @output += @b + @Actual_Values
 SET @output += @b + ''
 SET @output += @b + '*****END OF DEBUG INFORMATION*****/'
 SET @output += @b + ''
 END
 
IF (@include_use_db = 1)
BEGIN
	SET @output +=      'USE ' + DB_NAME()
	SET @output += @b + @batch_separator
	SET @output += @b + @b
END

IF (@nologo = 0)
BEGIN
 SET @output += @b + '--MERGE generated by ''sp_generate_merge'' stored procedure, Version 0.93'
 SET @output += @b + '--Originally by Vyas (http://vyaskn.tripod.com): sp_generate_inserts (build 22)'
 SET @output += @b + '--Adapted for SQL Server 2008/2012 by Daniel Nolan (http://danere.com)'
 SET @output += @b + ''
END

IF (@include_rowsaffected = 1) -- If the caller has elected not to include the "rows affected" section, let MERGE output the row count as it is executed.
 SET @output += @b + 'SET NOCOUNT ON'
 SET @output += @b + ''


--Determining whether to print IDENTITY_INSERT or not
IF (LEN(@IDN) <> 0)
 BEGIN
 SET @output += @b + 'SET IDENTITY_INSERT ' + @Target_Table_For_Output + ' ON'
 SET @output += @b + ''
 END


--Temporarily disable constraints on the target table
IF @disable_constraints = 1 AND (OBJECT_ID(@Source_Table_Qualified, 'U') IS NOT NULL)
 BEGIN
 SET @output += @b + 'ALTER TABLE ' + @Target_Table_For_Output + ' NOCHECK CONSTRAINT ALL' --Code to disable constraints temporarily
 END


--Output the start of the MERGE statement, qualifying with the schema name only if the caller explicitly specified it
SET @output += @b + 'MERGE INTO ' + @Target_Table_For_Output + ' AS Target'
SET @output += @b + 'USING (VALUES'


--All the hard work pays off here!!! You'll get your MERGE statement, when the next line executes!
DECLARE @tab TABLE (ID INT NOT NULL PRIMARY KEY IDENTITY(1,1), val NVARCHAR(max));
INSERT INTO @tab (val)
EXEC (@Actual_Values)

IF (SELECT COUNT(*) FROM @tab) <> 0 -- Ensure that rows were returned, otherwise the MERGE statement will get nullified.
BEGIN
 SET @output += CAST((SELECT @b + val FROM @tab ORDER BY ID FOR XML PATH('')) AS XML).value('.', 'VARCHAR(MAX)');
END

--Output the columns to correspond with each of the values above--------------------
SET @output += @b + ') AS Source (' + @Column_List + ')'


--Output the join columns ----------------------------------------------------------
SET @output += @b + 'ON (' + @PK_column_joins + ')'


--When matched, perform an UPDATE on any metadata columns only (ie. not on PK)------
IF LEN(@Column_List_For_Update) <> 0
BEGIN
 SET @output += @b + 'WHEN MATCHED ' + CASE WHEN @update_only_if_changed = 1 THEN 'AND (' + @Column_List_For_Check + ') ' ELSE '' END + 'THEN'
 SET @output += @b + ' UPDATE SET'
 SET @output += @b + '  ' + LTRIM(@Column_List_For_Update)
END


--When NOT matched by target, perform an INSERT------------------------------------
SET @output += @b + 'WHEN NOT MATCHED BY TARGET THEN';
SET @output += @b + ' INSERT(' + @Column_List + ')'
SET @output += @b + ' VALUES(' + REPLACE(@Column_List, '[', 'Source.[') + ')'


--When NOT matched by source, DELETE the row
IF @delete_if_not_matched=1 BEGIN
 SET @output += @b + 'WHEN NOT MATCHED BY SOURCE THEN '
 SET @output += @b + ' DELETE'
END;
SET @output += @b + ';'
SET @output += @b + @batch_separator

--Display the number of affected rows to the user, or report if an error occurred---
IF @include_rowsaffected = 1
BEGIN
 SET @output += @b + 'DECLARE @mergeError int'
 SET @output += @b + ' , @mergeCount int'
 SET @output += @b + 'SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT'
 SET @output += @b + 'IF @mergeError != 0'
 SET @output += @b + ' BEGIN'
 SET @output += @b + ' PRINT ''ERROR OCCURRED IN MERGE FOR ' + @Target_Table_For_Output + '. Rows affected: '' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected';
 SET @output += @b + ' END'
 SET @output += @b + 'ELSE'
 SET @output += @b + ' BEGIN'
 SET @output += @b + ' PRINT ''' + @Target_Table_For_Output + ' rows affected by MERGE: '' + CAST(@mergeCount AS VARCHAR(100));';
 SET @output += @b + ' END'
 SET @output += @b + @batch_separator
 SET @output += @b + @b
END

--Re-enable the previously disabled constraints-------------------------------------
IF @disable_constraints = 1 AND (OBJECT_ID(@Source_Table_Qualified, 'U') IS NOT NULL)
 BEGIN
 SET @output +=      'ALTER TABLE ' + @Target_Table_For_Output + ' CHECK CONSTRAINT ALL' --Code to enable the previously disabled constraints
 SET @output += @b + @batch_separator
 SET @output += @b
 END


--Switch-off identity inserting------------------------------------------------------
IF (LEN(@IDN) <> 0)
 BEGIN
 SET @output +=      'SET IDENTITY_INSERT ' + @Target_Table_For_Output + ' OFF'
 SET @output += @b + @batch_separator
 SET @output += @b
 END

IF (@include_rowsaffected = 1)
BEGIN
 SET @output +=      'SET NOCOUNT OFF'
 SET @output += @b + @batch_separator
 SET @output += @b
END

SET @output += @b + ''
SET @output += @b + ''

IF @results_to_text = 1
BEGIN
	--output the statement to the Grid/Messages tab
	SELECT @output;
END
ELSE
BEGIN
	--output the statement as xml (to overcome SSMS 4000/8000 char limitation)
	SELECT [processing-instruction(x)]=@output FOR XML PATH(''),TYPE;
	PRINT 'MERGE statement has been wrapped in an XML fragment and output successfully.'
	PRINT 'Ensure you have Results to Grid enabled and then click the hyperlink to copy the statement within the fragment.'
	PRINT ''
	PRINT 'If you would prefer to have results output directly (without XML) specify @results_to_text = 1, however please'
	PRINT 'note that the results may be truncated by your SQL client to 4000 nchars.'
END

SET NOCOUNT OFF
RETURN 0 --Success. We are done!
END










GO
/****** Object:  StoredProcedure [dbo].[sp_generate_merge2]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--execute ============================= exec sp_generate_merge2 'items', @schema='dbo'

--Turn system object marking on

CREATE PROCEDURE [dbo].[sp_generate_merge2]
(
@table_name varchar(776), -- The table/view for which the MERGE statement will be generated using the existing data
@target_table varchar(776) = NULL, -- Use this parameter to specify a different table name into which the data will be inserted/updated/deleted
@from nvarchar(max) = NULL, -- Use this parameter to filter the rows based on a filter condition (using WHERE)
@include_timestamp bit = 0, -- Specify 1 for this parameter, if you want to include the TIMESTAMP/ROWVERSION column's data in the MERGE statement
@debug_mode bit = 0, -- If @debug_mode is set to 1, the SQL statements constructed by this procedure will be printed for later examination
@schema varchar(64) = NULL, -- Use this parameter if you are not the owner of the table
@ommit_images bit = 0, -- Use this parameter to generate MERGE statement by omitting the 'image' columns
@ommit_identity bit = 0, -- Use this parameter to ommit the identity columns
@top int = NULL, -- Use this parameter to generate a MERGE statement only for the TOP n rows
@cols_to_include varchar(8000) = NULL, -- List of columns to be included in the MERGE statement
@cols_to_exclude varchar(8000) = NULL, -- List of columns to be excluded from the MERGE statement
@update_only_if_changed bit = 1, -- When 1, only performs an UPDATE operation if an included column in a matched row has changed.
@delete_if_not_matched bit = 1, -- When 1, deletes unmatched source rows from target, when 0 source rows will only be used to update existing rows or insert new.
@disable_constraints bit = 0, -- When 1, disables foreign key constraints and enables them after the MERGE statement
@ommit_computed_cols bit = 0, -- When 1, computed columns will not be included in the MERGE statement
@include_use_db bit = 1, -- When 1, includes a USE [DatabaseName] statement at the beginning of the generated batch
@results_to_text bit = 0, -- When 1, outputs results to grid/messages window. When 0, outputs MERGE statement in an XML fragment.
@include_rowsaffected bit = 1, -- When 1, a section is added to the end of the batch which outputs rows affected by the MERGE
@nologo bit = 0, -- When 1, the "About" comment is suppressed from output
@batch_separator VARCHAR(50) = 'GO' -- Batch separator to use
)
AS
BEGIN

/***********************************************************************************************************
Procedure: sp_generate_merge2 (Version 0.93)
(Adapted by Daniel Nolan for SQL Server 2008/2012)

Adapted from: sp_generate_inserts (Build 22) 
(Copyright © 2002 Narayana Vyas Kondreddi. All rights reserved.)

Purpose: To generate a MERGE statement from existing data, which will INSERT/UPDATE/DELETE data based
on matching primary key values in the source/target table.

The generated statements can be executed to replicate the data in some other location.

Typical use cases:
* Generate statements for static data tables, store the .SQL file in source control and use 
it as part of your Dev/Test/Prod deployment. The generated statements are re-runnable, so 
you can make changes to the file and migrate those changes between environments.

* Generate statements from your Production tables and then run those statements in your 
Dev/Test environments. Schedule this as part of a SQL Job to keep all of your environments 
in-sync.

* Enter test data into your Dev environment, and then generate statements from the Dev
tables so that you can always reproduce your test database with valid sample data.


Written by: Narayana Vyas Kondreddi
http://vyaskn.tripod.com

Daniel Nolan
http://danere.com
@dan3r3

Acknowledgements (sp_generate_merge2):
Nathan Skerl -- StackOverflow answer that provided a workaround for the output truncation problem
http://stackoverflow.com/a/10489767/266882

Bill Gibson -- Blog that detailed the static data table use case; the inspiration for this proc
http://blogs.msdn.com/b/ssdt/archive/2012/02/02/including-data-in-an-sql-server-database-project.aspx

Bill Graziano -- Blog that provided the groundwork for MERGE statement generation
http://weblogs.sqlteam.com/billg/archive/2011/02/15/generate-merge-statements-from-a-table.aspx 

Acknowledgements (sp_generate_inserts):
Divya Kalra -- For beta testing
Mark Charsley -- For reporting a problem with scripting uniqueidentifier columns with NULL values
Artur Zeygman -- For helping me simplify a bit of code for handling non-dbo owned tables
Joris Laperre -- For reporting a regression bug in handling text/ntext columns

Tested on: SQL Server 2008 (10.50.1600), SQL Server 2012 (11.0.2100)

Date created: January 17th 2001 21:52 GMT
Modified: May 1st 2002 19:50 GMT
Last Modified: September 27th 2012 10:00 AEDT

Email: dan@danere.com, vyaskn@hotmail.com

NOTE: This procedure may not work with tables with a large number of columns (> 500).
Results can be unpredictable with huge text columns or SQL Server 2000's sql_variant data types
IMPORTANT: This procedure has not been extensively tested with international data (Extended characters or Unicode). If needed
you might want to convert the datatypes of character variables in this procedure to their respective unicode counterparts
like nchar and nvarchar

Get Started: Ensure that your SQL client is configured to send results to grid (default SSMS behaviour).
This ensures that the generated MERGE statement can be output in full, getting around SSMS's 4000 nchar limit.
After running this proc, click the hyperlink within the single row returned to copy the generated MERGE statement.

Example 1: To generate a MERGE statement for table 'titles':

EXEC sp_generate_merge2 'titles'

Example 2: To generate a MERGE statement for 'titlesCopy' table from 'titles' table:

EXEC sp_generate_merge2 'titles', 'titlesCopy'

Example 3: To generate a MERGE statement for table 'titles' that will unconditionally UPDATE matching rows 
(ie. not perform a "has data changed?" check prior to going ahead with an UPDATE):

EXEC sp_generate_merge2 'titles', @update_only_if_changed = 0

Example 4: To generate a MERGE statement for 'titles' table for only those titles 
which contain the word 'Computer' in them:
NOTE: Do not complicate the FROM or WHERE clause here. It's assumed that you are good with T-SQL if you are using this parameter

EXEC sp_generate_merge2 'titles', @from = "from titles where title like '%Computer%'"

Example 5: To specify that you want to include TIMESTAMP column's data as well in the MERGE statement:
(By default TIMESTAMP column's data is not scripted)

EXEC sp_generate_merge2 'titles', @include_timestamp = 1

Example 6: To print the debug information:

EXEC sp_generate_merge2 'titles', @debug_mode = 1

Example 7: If the table is in a different schema to the default, use @schema parameter to specify the schema name
To use this option, you must have SELECT permissions on that table

EXEC sp_generate_merge2 'Nickstable', @schema = 'Nick'

Example 8: To generate a MERGE statement for the rest of the columns excluding images

EXEC sp_generate_merge2 'imgtable', @ommit_images = 1

Example 9: To generate a MERGE statement excluding (omitting) IDENTITY columns:
(By default IDENTITY columns are included in the MERGE statement)

EXEC sp_generate_merge2 'mytable', @ommit_identity = 1

Example 10: To generate a MERGE statement for the TOP 10 rows in the table:

EXEC sp_generate_merge2 'mytable', @top = 10

Example 11: To generate a MERGE statement with only those columns you want:

EXEC sp_generate_merge2 'titles', @cols_to_include = "'title','title_id','au_id'"

Example 12: To generate a MERGE statement by omitting certain columns:

EXEC sp_generate_merge2 'titles', @cols_to_exclude = "'title','title_id','au_id'"

Example 13: To avoid checking the foreign key constraints while loading data with a MERGE statement:

EXEC sp_generate_merge2 'titles', @disable_constraints = 1

Example 14: To exclude computed columns from the MERGE statement:

EXEC sp_generate_merge2 'MyTable', @ommit_computed_cols = 1

***********************************************************************************************************/

SET NOCOUNT ON


--Making sure user only uses either @cols_to_include or @cols_to_exclude
IF ((@cols_to_include IS NOT NULL) AND (@cols_to_exclude IS NOT NULL))
BEGIN
RAISERROR('Use either @cols_to_include or @cols_to_exclude. Do not use both the parameters at once',16,1)
RETURN -1 --Failure. Reason: Both @cols_to_include and @cols_to_exclude parameters are specified
END


--Making sure the @cols_to_include and @cols_to_exclude parameters are receiving values in proper format
IF ((@cols_to_include IS NOT NULL) AND (PATINDEX('''%''',@cols_to_include) = 0))
BEGIN
RAISERROR('Invalid use of @cols_to_include property',16,1)
PRINT 'Specify column names surrounded by single quotes and separated by commas'
PRINT 'Eg: EXEC sp_generate_merge2 titles, @cols_to_include = "''title_id'',''title''"'
RETURN -1 --Failure. Reason: Invalid use of @cols_to_include property
END

IF ((@cols_to_exclude IS NOT NULL) AND (PATINDEX('''%''',@cols_to_exclude) = 0))
BEGIN
RAISERROR('Invalid use of @cols_to_exclude property',16,1)
PRINT 'Specify column names surrounded by single quotes and separated by commas'
PRINT 'Eg: EXEC sp_generate_merge2 titles, @cols_to_exclude = "''title_id'',''title''"'
RETURN -1 --Failure. Reason: Invalid use of @cols_to_exclude property
END


--Checking to see if the database name is specified along wih the table name
--Your database context should be local to the table for which you want to generate a MERGE statement
--specifying the database name is not allowed
IF (PARSENAME(@table_name,3)) IS NOT NULL
BEGIN
RAISERROR('Do not specify the database name. Be in the required database and just specify the table name.',16,1)
RETURN -1 --Failure. Reason: Database name is specified along with the table name, which is not allowed
END


--Checking for the existence of 'user table' or 'view'
--This procedure is not written to work on system tables
--To script the data in system tables, just create a view on the system tables and script the view instead
IF @schema IS NULL
BEGIN
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table_name AND (TABLE_TYPE = 'BASE TABLE' OR TABLE_TYPE = 'VIEW') AND TABLE_SCHEMA = SCHEMA_NAME())
BEGIN
RAISERROR('User table or view not found.',16,1)
PRINT 'You may see this error if the specified table is not in your default schema (' + SCHEMA_NAME() + '). In that case use @schema parameter to specify the schema name.'
PRINT 'Make sure you have SELECT permission on that table or view.'
RETURN -1 --Failure. Reason: There is no user table or view with this name
END
END
ELSE
BEGIN
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table_name AND (TABLE_TYPE = 'BASE TABLE' OR TABLE_TYPE = 'VIEW') AND TABLE_SCHEMA = @schema)
BEGIN
RAISERROR('User table or view not found.',16,1)
PRINT 'Make sure you have SELECT permission on that table or view.'
RETURN -1 --Failure. Reason: There is no user table or view with this name 
END
END


--Variable declarations
DECLARE @Column_ID int, 
@Column_List varchar(8000), 
@Column_List_For_Update varchar(8000), 
@Column_List_For_Check varchar(8000), 
@Column_Name varchar(128), 
@Column_Name_Unquoted varchar(128), 
@Data_Type varchar(128), 
@Actual_Values nvarchar(max), --This is the string that will be finally executed to generate a MERGE statement
@IDN varchar(128), --Will contain the IDENTITY column's name in the table
@Target_Table_For_Output varchar(776),
@Source_Table_Qualified varchar(776)



--Variable Initialization
SET @IDN = ''
SET @Column_ID = 0
SET @Column_Name = ''
SET @Column_Name_Unquoted = ''
SET @Column_List = ''
SET @Column_List_For_Update = ''
SET @Column_List_For_Check = ''
SET @Actual_Values = ''

--Variable Defaults
IF @schema IS NULL
BEGIN
SET @Target_Table_For_Output = QUOTENAME(COALESCE(@target_table, @table_name))
END
ELSE
BEGIN
SET @Target_Table_For_Output = QUOTENAME(@schema) + '.' + QUOTENAME(COALESCE(@target_table, @table_name))
END

SET @Source_Table_Qualified = QUOTENAME(COALESCE(@schema,SCHEMA_NAME())) + '.' + QUOTENAME(@table_name)

--To get the first column's ID
SELECT @Column_ID = MIN(ORDINAL_POSITION) 
FROM INFORMATION_SCHEMA.COLUMNS (NOLOCK) 
WHERE TABLE_NAME = @table_name
AND TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())


--Loop through all the columns of the table, to get the column names and their data types
WHILE @Column_ID IS NOT NULL
BEGIN
SELECT @Column_Name = QUOTENAME(COLUMN_NAME), 
@Column_Name_Unquoted = COLUMN_NAME,
@Data_Type = DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS (NOLOCK) 
WHERE ORDINAL_POSITION = @Column_ID
AND TABLE_NAME = @table_name
AND TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())

IF @cols_to_include IS NOT NULL --Selecting only user specified columns
BEGIN
IF CHARINDEX( '''' + SUBSTRING(@Column_Name,2,LEN(@Column_Name)-2) + '''',@cols_to_include) = 0 
BEGIN
GOTO SKIP_LOOP
END
END

IF @cols_to_exclude IS NOT NULL --Selecting only user specified columns
BEGIN
IF CHARINDEX( '''' + SUBSTRING(@Column_Name,2,LEN(@Column_Name)-2) + '''',@cols_to_exclude) <> 0 
BEGIN
GOTO SKIP_LOOP
END
END

--Making sure to output SET IDENTITY_INSERT ON/OFF in case the table has an IDENTITY column
IF (SELECT COLUMNPROPERTY( OBJECT_ID(@Source_Table_Qualified),SUBSTRING(@Column_Name,2,LEN(@Column_Name) - 2),'IsIdentity')) = 1 
BEGIN
IF @ommit_identity = 0 --Determing whether to include or exclude the IDENTITY column
SET @IDN = @Column_Name
ELSE
GOTO SKIP_LOOP 
END

--Making sure whether to output computed columns or not
IF @ommit_computed_cols = 1
BEGIN
IF (SELECT COLUMNPROPERTY( OBJECT_ID(@Source_Table_Qualified),SUBSTRING(@Column_Name,2,LEN(@Column_Name) - 2),'IsComputed')) = 1 
BEGIN
GOTO SKIP_LOOP 
END
END

--Tables with columns of IMAGE data type are not supported for obvious reasons
IF(@Data_Type in ('image'))
BEGIN
IF (@ommit_images = 0)
BEGIN
RAISERROR('Tables with image columns are not supported.',16,1)
PRINT 'Use @ommit_images = 1 parameter to generate a MERGE for the rest of the columns.'
RETURN -1 --Failure. Reason: There is a column with image data type
END
ELSE
BEGIN
GOTO SKIP_LOOP
END
END

--Determining the data type of the column and depending on the data type, the VALUES part of
--the MERGE statement is generated. Care is taken to handle columns with NULL values. Also
--making sure, not to lose any data from flot, real, money, smallmomey, datetime columns
SET @Actual_Values = @Actual_Values +
CASE 
WHEN @Data_Type IN ('char','nchar') 
THEN 
'COALESCE(''N'''''' + REPLACE(RTRIM(' + @Column_Name + '),'''''''','''''''''''')+'''''''',''NULL'')'
WHEN @Data_Type IN ('varchar','nvarchar') 
THEN 
'COALESCE(''N'''''' + REPLACE(' + @Column_Name + ','''''''','''''''''''')+'''''''',''NULL'')'
WHEN @Data_Type IN ('datetime','smalldatetime','datetime2','date') 
THEN 
'COALESCE('''''''' + RTRIM(CONVERT(char,' + @Column_Name + ',127))+'''''''',''NULL'')'
WHEN @Data_Type IN ('uniqueidentifier') 
THEN 
'COALESCE(''N'''''' + REPLACE(CONVERT(char(36),RTRIM(' + @Column_Name + ')),'''''''','''''''''''')+'''''''',''NULL'')'
WHEN @Data_Type IN ('text') 
THEN 
'COALESCE(''N'''''' + REPLACE(CONVERT(varchar(max),' + @Column_Name + '),'''''''','''''''''''')+'''''''',''NULL'')' 
WHEN @Data_Type IN ('ntext') 
THEN 
'COALESCE('''''''' + REPLACE(CONVERT(nvarchar(max),' + @Column_Name + '),'''''''','''''''''''')+'''''''',''NULL'')' 
WHEN @Data_Type IN ('xml') 
THEN 
'COALESCE('''''''' + REPLACE(CONVERT(nvarchar(max),' + @Column_Name + '),'''''''','''''''''''')+'''''''',''NULL'')' 
WHEN @Data_Type IN ('binary','varbinary') 
THEN 
'COALESCE(RTRIM(CONVERT(varchar(max),' + @Column_Name + ', 1)),''NULL'')' 
WHEN @Data_Type IN ('timestamp','rowversion') 
THEN 
CASE 
WHEN @include_timestamp = 0 
THEN 
'''DEFAULT''' 
ELSE 
'COALESCE(RTRIM(CONVERT(char,' + 'CONVERT(int,' + @Column_Name + '))),''NULL'')' 
END
WHEN @Data_Type IN ('float','real','money','smallmoney')
THEN
'COALESCE(LTRIM(RTRIM(' + 'CONVERT(char, ' + @Column_Name + ',2)' + ')),''NULL'')' 
WHEN @Data_Type IN ('hierarchyid')
THEN 
'COALESCE(''hierarchyid::Parse(''+'''''''' + LTRIM(RTRIM(' + 'CONVERT(char, ' + @Column_Name + ')' + '))+''''''''+'')'',''NULL'')' 
ELSE 
'COALESCE(LTRIM(RTRIM(' + 'CONVERT(char, ' + @Column_Name + ')' + ')),''NULL'')' 
END + '+' + ''',''' + ' + '

--Generating the column list for the MERGE statement
SET @Column_List = @Column_List + @Column_Name + ',' 

--Don't update Primary Key or Identity columns
IF NOT EXISTS(
SELECT 1
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,
INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
WHERE pk.TABLE_NAME = @table_name
AND pk.TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())
AND CONSTRAINT_TYPE = 'PRIMARY KEY'
AND c.TABLE_NAME = pk.TABLE_NAME
AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA
AND c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME
AND c.COLUMN_NAME = @Column_Name_Unquoted 
)
BEGIN
SET @Column_List_For_Update = @Column_List_For_Update + @Column_Name + ' = s.' + @Column_Name + ', 
' 
SET @Column_List_For_Check = @Column_List_For_Check +
CASE @Data_Type 
WHEN 'text' THEN CHAR(10) + CHAR(9) + 'NULLIF(CAST(s.' + @Column_Name + ' AS VARCHAR(MAX)), CAST(t.' + @Column_Name + ' AS VARCHAR(MAX))) IS NOT NULL OR NULLIF(CAST(t.' + @Column_Name + ' AS VARCHAR(MAX)), CAST(s.' + @Column_Name + ' AS VARCHAR(MAX))) IS NOT NULL OR '
WHEN 'ntext' THEN CHAR(10) + CHAR(9) + 'NULLIF(CAST(s.' + @Column_Name + ' AS NVARCHAR(MAX)), CAST(t.' + @Column_Name + ' AS NVARCHAR(MAX))) IS NOT NULL OR NULLIF(CAST(t.' + @Column_Name + ' AS NVARCHAR(MAX)), CAST(s.' + @Column_Name + ' AS NVARCHAR(MAX))) IS NOT NULL OR ' 
ELSE CHAR(10) + CHAR(9) + 'NULLIF(s.' + @Column_Name + ', t.' + @Column_Name + ') IS NOT NULL OR NULLIF(t.' + @Column_Name + ', s.' + @Column_Name + ') IS NOT NULL OR '
END 
END

SKIP_LOOP: --The label used in GOTO

SELECT @Column_ID = MIN(ORDINAL_POSITION) 
FROM INFORMATION_SCHEMA.COLUMNS (NOLOCK) 
WHERE TABLE_NAME = @table_name
AND TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())
AND ORDINAL_POSITION > @Column_ID

END --Loop ends here!


--To get rid of the extra characters that got concatenated during the last run through the loop
IF LEN(@Column_List_For_Update) <> 0
BEGIN
SET @Column_List_For_Update = ' ' + LEFT(@Column_List_For_Update,len(@Column_List_For_Update) - 4)
END

IF LEN(@Column_List_For_Check) <> 0
BEGIN
SET @Column_List_For_Check = LEFT(@Column_List_For_Check,len(@Column_List_For_Check) - 3)
END

SET @Actual_Values = LEFT(@Actual_Values,len(@Actual_Values) - 6)

SET @Column_List = LEFT(@Column_List,len(@Column_List) - 1)
IF LEN(LTRIM(@Column_List)) = 0
BEGIN
RAISERROR('No columns to select. There should at least be one column to generate the output',16,1)
RETURN -1 --Failure. Reason: Looks like all the columns are ommitted using the @cols_to_exclude parameter
END


--Get the join columns ----------------------------------------------------------
DECLARE @PK_column_list VARCHAR(8000)
DECLARE @PK_column_joins VARCHAR(8000)
SET @PK_column_list = ''
SET @PK_column_joins = ''

SELECT @PK_column_list = @PK_column_list + '[' + c.COLUMN_NAME + '], '
, @PK_column_joins = @PK_column_joins + 't.[' + c.COLUMN_NAME + '] = s.[' + c.COLUMN_NAME + '] AND '
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,
INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
WHERE pk.TABLE_NAME = @table_name
AND pk.TABLE_SCHEMA = COALESCE(@schema, SCHEMA_NAME())
AND CONSTRAINT_TYPE = 'PRIMARY KEY'
AND c.TABLE_NAME = pk.TABLE_NAME
AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA
AND c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME

IF IsNull(@PK_column_list, '') = '' 
BEGIN
RAISERROR('Table has no primary keys. There should at least be one column in order to have a valid join.',16,1)
RETURN -1 --Failure. Reason: looks like table doesn't have any primary keys
END

SET @PK_column_list = LEFT(@PK_column_list, LEN(@PK_column_list) -1)
SET @PK_column_joins = LEFT(@PK_column_joins, LEN(@PK_column_joins) -4)


--Forming the final string that will be executed, to output the a MERGE statement
SET @Actual_Values = 
'SELECT ' + 
CASE WHEN @top IS NULL OR @top < 0 THEN '' ELSE ' TOP ' + LTRIM(STR(@top)) + ' ' END + 
'''' + 
' '' + CASE WHEN ROW_NUMBER() OVER (ORDER BY ' + @PK_column_list + ') = 1 THEN '' '' ELSE '','' END + ''(''+ ' + @Actual_Values + '+'')''' + ' ' + 
COALESCE(@from,' FROM ' + @Source_Table_Qualified + ' (NOLOCK) ORDER BY ' + @PK_column_list)

DECLARE @output VARCHAR(MAX) = ''
DECLARE @b CHAR(1) = CHAR(13)

--Determining whether to ouput any debug information
IF @debug_mode =1
BEGIN
SET @output += @b + '/*****START OF DEBUG INFORMATION*****'
SET @output += @b + ''
SET @output += @b + 'The primary key column list:'
SET @output += @b + @PK_column_list
SET @output += @b + ''
SET @output += @b + 'The INSERT column list:'
SET @output += @b + @Column_List
SET @output += @b + ''
SET @output += @b + 'The UPDATE column list:'
SET @output += @b + @Column_List_For_Update
SET @output += @b + ''
SET @output += @b + 'The SELECT statement executed to generate the MERGE:'
SET @output += @b + @Actual_Values
SET @output += @b + ''
SET @output += @b + '*****END OF DEBUG INFORMATION*****/'
SET @output += @b + ''
END

IF (@include_use_db = 1)
BEGIN
SET @output += 'USE ' + DB_NAME()
SET @output += @b + @batch_separator
SET @output += @b + @b
END

IF (@nologo = 0)
BEGIN
SET @output += @b + '--MERGE generated by ''sp_generate_merge2'' stored procedure, Version 0.93'
SET @output += @b + '--Originally by Vyas (http://vyaskn.tripod.com): sp_generate_inserts (build 22)'
SET @output += @b + '--Adapted for SQL Server 2008/2012 by Daniel Nolan (http://danere.com)'
SET @output += @b + ''
END

IF (@include_rowsaffected = 1) -- If the caller has elected not to include the "rows affected" section, let MERGE output the row count as it is executed.
SET @output += @b + 'SET NOCOUNT ON'
SET @output += @b + ''


--Determining whether to print IDENTITY_INSERT or not
IF (LEN(@IDN) <> 0)
BEGIN
SET @output += @b + 'SET IDENTITY_INSERT ' + @Target_Table_For_Output + ' ON'
SET @output += @b + ''
END


--Temporarily disable constraints on the target table
IF @disable_constraints = 1 AND (OBJECT_ID(@Source_Table_Qualified, 'U') IS NOT NULL)
BEGIN
SET @output += @b + 'ALTER TABLE ' + @Target_Table_For_Output + ' NOCHECK CONSTRAINT ALL' --Code to disable constraints temporarily
END


--Output the start of the MERGE statement, qualifying with the schema name only if the caller explicitly specified it
SET @output += @b + 'MERGE INTO ' + @Target_Table_For_Output + ' AS t'
SET @output += @b + 'USING (VALUES'


--All the hard work pays off here!!! You'll get your MERGE statement, when the next line executes!
DECLARE @tab TABLE (ID INT NOT NULL PRIMARY KEY IDENTITY(1,1), val NVARCHAR(max));
INSERT INTO @tab (val)
EXEC (@Actual_Values)

IF (SELECT COUNT(*) FROM @tab) <> 0 -- Ensure that rows were returned, otherwise the MERGE statement will get nullified.
BEGIN
SET @output += CAST((SELECT @b + val FROM @tab ORDER BY ID FOR XML PATH('')) AS XML).value('.', 'VARCHAR(MAX)');
END

--Output the columns to correspond with each of the values above--------------------
SET @output += @b + ') AS s (' + @Column_List + ')'


--Output the join columns ----------------------------------------------------------
SET @output += @b + 'ON (' + @PK_column_joins + ')'


--When matched, perform an UPDATE on any metadata columns only (ie. not on PK)------
IF LEN(@Column_List_For_Update) <> 0
BEGIN
SET @output += @b + 'WHEN MATCHED ' + CASE WHEN @update_only_if_changed = 1 THEN 'AND (' + @Column_List_For_Check + ') ' ELSE '' END + 'THEN'
SET @output += @b + ' UPDATE SET'
SET @output += @b + ' ' + LTRIM(@Column_List_For_Update)
END


--When NOT matched by target, perform an INSERT------------------------------------
SET @output += @b + 'WHEN NOT MATCHED BY TARGET THEN';
SET @output += @b + ' INSERT(' + @Column_List + ')'
SET @output += @b + ' VALUES(' + REPLACE(@Column_List, '[', 's.[') + ')'


--When NOT matched by source, DELETE the row
IF @delete_if_not_matched=1 BEGIN
SET @output += @b + 'WHEN NOT MATCHED BY SOURCE THEN '
SET @output += @b + ' DELETE'
END;
SET @output += @b + ';'
SET @output += @b + @batch_separator

--Display the number of affected rows to the user, or report if an error occurred---
IF @include_rowsaffected = 1
BEGIN
SET @output += @b + 'DECLARE @mergeError int'
SET @output += @b + ' , @mergeCount int'
SET @output += @b + 'SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT'
SET @output += @b + 'IF @mergeError != 0'
SET @output += @b + ' BEGIN'
SET @output += @b + ' PRINT ''ERROR OCCURRED IN MERGE FOR ' + @Target_Table_For_Output + '. Rows affected: '' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected';
SET @output += @b + ' END'
SET @output += @b + 'ELSE'
SET @output += @b + ' BEGIN'
SET @output += @b + ' PRINT ''' + @Target_Table_For_Output + ' rows affected by MERGE: '' + CAST(@mergeCount AS VARCHAR(100));';
SET @output += @b + ' END'
SET @output += @b + @batch_separator
SET @output += @b + @b
END

--Re-enable the previously disabled constraints-------------------------------------
IF @disable_constraints = 1 AND (OBJECT_ID(@Source_Table_Qualified, 'U') IS NOT NULL)
BEGIN
SET @output += 'ALTER TABLE ' + @Target_Table_For_Output + ' CHECK CONSTRAINT ALL' --Code to enable the previously disabled constraints
SET @output += @b + @batch_separator
SET @output += @b
END


--Switch-off identity inserting------------------------------------------------------
IF (LEN(@IDN) <> 0)
BEGIN
SET @output += 'SET IDENTITY_INSERT ' + @Target_Table_For_Output + ' OFF'
SET @output += @b + @batch_separator
SET @output += @b
END

IF (@include_rowsaffected = 1)
BEGIN
SET @output += 'SET NOCOUNT OFF'
SET @output += @b + @batch_separator
SET @output += @b
END

SET @output += @b + ''
SET @output += @b + ''

IF @results_to_text = 1
BEGIN
--output the statement to the Grid/Messages tab
SELECT @output;
END
ELSE
BEGIN
--output the statement as xml (to overcome SSMS 4000/8000 char limitation)
SELECT [processing-instruction(x)]=@output FOR XML PATH(''),TYPE;
PRINT 'MERGE statement has been wrapped in an XML fragment and output successfully.'
PRINT 'Ensure you have Results to Grid enabled and then click the hyperlink to copy the statement within the fragment.'
PRINT ''
PRINT 'If you would prefer to have results output directly (without XML) specify @results_to_text = 1, however please'
PRINT 'note that the results may be truncated by your SQL client to 4000 nchars.'
END

SET NOCOUNT OFF
RETURN 0 --Success. We are done!
END







GO
/****** Object:  StoredProcedure [dbo].[sp_get_custprice_for_item]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
CREATE PROCEDURE [dbo].[sp_get_custprice_for_item]
(@comp1 char(2), @lbarcode1 varchar(20),@glact1 bit,@cust nvarchar(10))

AS
BEGIN

declare @sql nvarchar(MAX)='',
    @dbc varchar(10),
    @c_l_price float=0,
	@lpkqty numeric(8,3),
	@mdate varchar(10),
    @paralist nvarchar(MAX)=''
	set @dbc=FILE_NAME(1) --DB_NAME()	

	select top 1 @c_l_price=price,@lpkqty=pkqty,@mdate=CONVERT(VARCHAR(10), CAST(a.invmdate as date), 105) from sales_dtl a join sales_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.slcenter=b.slcenter and a.ref=b.ref  where a.branch=@comp1 and 
		ltrim(rtrim(iif(@glact1=0,a.itemno,a.barcode)))=@lbarcode1 and a.invtype in ('04','05') and b.custno=@cust order by b.released desc

		--if isnull(@c_l_price,0)=0
		--begin
		--  declare @yrcode varchar(4)
		--  set @yrcode=substring(@dbc,6,4)
		--  --select substring(db_name(),6,4)
		-- -- set @dbc=substring(@dbc,1,4)+str(cast(@yrcode as int)-1,2)
		-- set @dbc=ltrim(rtrim(substring(@dbc,1,5)))+ltrim(rtrim(str(cast(@yrcode as int)-1)))
		--  if exists(SELECT name FROM sys.databases where name=@dbc)
		--  begin
  --           set @paralist='@c_l_price float output,@lpkqty numeric(8,3) output,@mdate varchar(10) output'
  --           set @sql='select top 1 @c_l_price=price ,@lpkqty=pkqty,@mdate=CONVERT(VARCHAR(10), CAST(a.invmdate as date), 105) from '+@dbc+'.dbo.sales_dtl a join '+@dbc+'.dbo.sales_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.slcenter=b.slcenter and a.ref=b.ref  where a.branch='''+@comp1+''' and 
  --                    ltrim(rtrim('+iif(@glact1=0,'a.itemno','a.barcode')+ '))='''+@lbarcode1+''' and a.invtype in (''04'',''05'') and b.custno='''+@cust+''' order by b.released desc'
		--			  --ltrim(rtrim('+iif(@glact1=0,'a.itemno','a.barcode')+ '))='''+@lbarcode1+''' and a.invtype in (''04'',''05'') and b.custno='''+@cust+''' 
  --           EXECUTE sp_executesql  @sql ,@paralist,@c_l_price=@c_l_price output,@lpkqty=@lpkqty output,@mdate=@mdate output
  --        end
  --     	end

set @c_l_price=isnull(@c_l_price,0)
	set @lpkqty=isnull(@lpkqty,0)
	--set @mdate=isnull(@mdate,'')
	if @c_l_price>0 and @lpkqty>1 set @c_l_price=@c_l_price/@lpkqty

	--insert into @t values(@c_l_price,@mdate)
	select round(@c_l_price,4) c_l_price  --(@c_l_price,@mdate)

END


GO
/****** Object:  StoredProcedure [dbo].[sp_get_lastprice_for_item]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_get_lastprice_for_item]
(@comp1 char(2), @lbarcode1 varchar(20),@glact1 bit)

AS
BEGIN

declare @sql nvarchar(MAX)='',
    @dbc varchar(10),
    @c_l_price float=0,
	@lpkqty numeric(8,3),
	@mdate varchar(10),
    @paralist nvarchar(MAX)=''
	set @dbc=DB_NAME()	

	select top 1 @c_l_price=(case when b.with_tax=1 then round((price-(tax_amt/qty)),2) else price end) ,@lpkqty=pkqty,@mdate=CONVERT(VARCHAR(10), CAST(a.invmdate as date), 105) from pu_dtl a join pu_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.pucenter=b.pucenter and a.ref=b.ref  where a.branch=@comp1 and 
		ltrim(rtrim(iif(@glact1=0,a.itemno,a.barcode)))=@lbarcode1 and a.invtype in ('06','07')  order by b.released desc

		--if isnull(@c_l_price,0)=0
		--begin
		--  declare @yrcode varchar(4)
		--  set @yrcode=substring(@dbc,6,4)
		--  --select substring(db_name(),6,4)
		-- -- set @dbc=substring(@dbc,1,4)+str(cast(@yrcode as int)-1,2)
		-- set @dbc=ltrim(rtrim(substring(@dbc,1,5)))+ltrim(rtrim(str(cast(@yrcode as int)-1)))
		--  if exists(SELECT name FROM sys.databases where name=@dbc)
		--  begin
  --           set @paralist='@c_l_price float output,@lpkqty numeric(8,3) output,@mdate varchar(10) output'
  --           set @sql='select top 1 @c_l_price=price ,@lpkqty=pkqty,@mdate=CONVERT(VARCHAR(10), CAST(a.invmdate as date), 105) from '+@dbc+'.dbo.pu_dtl a join '+@dbc+'.dbo.pu_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.pucenter=b.pucenter and a.ref=b.ref  where a.branch='''+@comp1+''' and 
		--				 ltrim(rtrim('+iif(@glact1=0,'a.itemno','a.barcode')+ '))='''+@lbarcode1+''' and a.invtype in (''06'',''07'') order by b.released desc'
		--	      --     ltrim(rtrim('+iif(@glact1=0,'a.itemno','a.barcode')+ '))='''+@lbarcode1+''' and a.invtype in (''06'',''07'') '
  --           EXECUTE sp_executesql  @sql ,@paralist,@c_l_price=@c_l_price output,@lpkqty=@lpkqty output,@mdate=@mdate output
  --        end
	 --   end

set @c_l_price=isnull(@c_l_price,0)
	set @lpkqty=isnull(@lpkqty,0)
	--set @mdate=isnull(@mdate,'')
	if @c_l_price>0 and @lpkqty>1 set @c_l_price=@c_l_price/@lpkqty

	--insert into @t values(@c_l_price,@mdate)
	select round(@c_l_price,4) c_l_price  --(@c_l_price,@mdate)

END

GO
/****** Object:  StoredProcedure [dbo].[update_acc_inv]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[update_acc_inv] 

@acc_hdr_tb tb_acc_hdr READONLY,
@acc_dtl_tb tb_acc_dtl READONLY,
@sales_hdr_tb tb_sales_hdr READONLY,
@pu_hdr_tb tb_pu_hdr READONLY

AS
BEGIN

  MERGE acc_hdr as t
  USING @acc_hdr_tb as s
  ON t.a_brno=s.a_brno and t.a_type=s.a_type and t.a_ref=s.a_ref and t.sl_no=s.sl_no and t.pu_no=s.pu_no
  WHEN MATCHED THEN
  UPDATE SET t.a_amt = s.a_amt,t.a_text = s.a_text,t.a_mdate=s.a_mdate, t.a_hdate=s.a_hdate,t.invsupno=s.invsupno, t.taxid=s.taxid,t.cc_no=s.cc_no,t.mainkey=s.mainkey
  WHEN NOT MATCHED THEN
  INSERT VALUES(s.a_brno, s.a_type, s.a_ref, s.a_mdate, s.a_hdate, s.a_text, s.a_amt, s.a_entries, s.jhsrc, s.a_sysdate, s.jhcurr, s.jhfcflag, s.jhrate
                , s.jhreleased, s.a_posted, s.lastupdt, s.jhlccrttl, s.jhlcdbttl, s.jhfccrttl, s.jhfcdbttl, s.jhimgrate, s.modified, s.serial_no, s.rcvdtrn
				, s.suspend, s.usrid, s.isbrtrx, s.brxref, s.brxfrm, s.jhcustno, s.hide_jv, s.rowguid, s.mainkey, s.sl_no, s.pu_no, s.aqd_no, s.cc_no
				, s.invsupno, s.taxid);


  END


 BEGIN

  MERGE acc_dtl as t
  USING @acc_dtl_tb as s
  ON t.a_brno=s.a_brno and t.a_type=s.a_type and t.a_ref=s.a_ref and t.a_folio=s.a_folio and t.sl_no=s.sl_no and t.pu_no=s.pu_no
  WHEN MATCHED THEN
  UPDATE SET t.a_camt = s.a_camt,t.a_damt = s.a_damt,t.a_text = s.a_text,t.a_mdate=s.a_mdate, t.a_hdate=s.a_hdate, t.cu_no=s.cu_no, t.su_no=s.su_no,t.cc_no=s.cc_no,t.a_key=s.a_key
  WHEN NOT MATCHED THEN
  INSERT VALUES(s.a_brno, s.a_type, s.a_ref, s.a_key, s.a_mdate, s.a_hdate, s.a_text, s.a_camt, s.a_damt, s.jddbcr, s.jdfcamt, s.jdcurr, s.a_folio
                , s.a_sysdate, s.jdsrc, s.jdfc_imgval, s.jdcstval, s.cstkey, s.brnno, s.bracc, s.brxref, s.match, s.rplct_post, s.rowguid, s.taxcatId
				, s.cu_no, s.su_no, s.sl_no, s.pu_no, s.cc_no,s.qst_ref,s.qst_sl);

  END

  BEGIN

  MERGE sales_hdr as t
  USING @sales_hdr_tb as s
  ON t.slcenter=s.slcenter and t.invtype=s.invtype and t.ref=s.ref and t.branch=s.branch
  WHEN MATCHED THEN
  UPDATE SET t.invttl = s.invttl,t.invdsvl = s.invdsvl,t.nettotal = s.nettotal,t.invdspc = s.invdspc,t.text = s.text,invmdate=s.invmdate, t.invhdate=s.invhdate, t.tax_amt_rcvd=s.tax_amt_rcvd, t.custno=s.custno,t.taxid=s.taxid,t.casher=s.casher
  WHEN NOT MATCHED THEN
  INSERT VALUES(s.branch, s.slcenter, s.invtype, s.ref, s.invmdate, s.invhdate, s.custno, s.text, s.glser, s.dsctype, s.pstmode, s.fcy, s.fcyrate, s.invttl, s.invcst, s.invdspc, s.invdsvl, s.nettotal, s.invpaid, 
                         s.casher, s.entries, s.released, s.posted, s.fixrate, s.extamt, s.extser, pricetp, s.ischeque, s.chkno, s.chkdate, s.lastupdt, s.jvgenrt, s.cccommsn, s.belowbal, s.fcy2, s.ccpayment, s.rplsamt, 
                         s.pdother, s.slcode, s.prpaidamt, s.instldays, s.instflag, s.slcmnd, s.inv_printed, s.bendit, s.modified, s.rqstorder, s.rqststld, s.carrier, s.rcvdtrn, s.usrid, s.address, s.suspend, s.rtnref, s.ispurchase, 
                         s.stkjvno, s.posinv, s.sanedcrd_amt, s.rtncash_dfrpl, s.invlocked, s.remarks, s.tax_amt_rcvd, s.with_tax, s.taxid, s.tax_percent, s.taxfree_amt, s.reref,s.note2,s.note3,s.inv_mpay,s.cust_mobil);

  END

  BEGIN

  MERGE pu_hdr as t
  USING @pu_hdr_tb as s
  ON t.pucenter=s.pucenter and t.invtype=s.invtype and t.ref=s.ref and t.branch=s.branch
  WHEN MATCHED THEN
  UPDATE SET t.invttl = s.invttl,t.invdsvl = s.invdsvl,t.nettotal = s.nettotal,t.invdspc = s.invdspc,t.text = s.text,invmdate=s.invmdate, t.invhdate=s.invhdate, t.tax_amt_paid=s.tax_amt_paid, t.supno=s.supno,t.taxid=s.taxid,t.casher=s.casher
  WHEN NOT MATCHED THEN
  INSERT VALUES(s.branch, s.pucenter, s.invtype, s.ref, s.invmdate, s.invhdate, s.supno, s.text, s.glser, s.dsctype, s.pstmode, s.cur, s.currate, s.invttl, s.invcst, s.invdspc, s.invdsvl, s.nettotal, s.invpaid, 
                         s.casher, s.entries, s.released, s.posted, s.fixrate, s.extamt, s.extser, s.pricetp, s.ischeque, s.chkno, s.chkdate, s.lastupdt, s.jvgenrt, s.cccommsn, s.belowbal, s.invsupno, s.ccpayment, s.rplsamt, 
                         s.pdother, s.slcode, s.prpaidamt, s.instldays, s.instflag, s.slcmnd, s.inv_printed, s.bendit, s.modified, s.rqstorder, s.rqststld, s.carrier, s.rcvdtrn, s.usrid, s.address, s.wst_key, s.wst_amt, s.gmark, 
                         s.tameen, s.shahan, s.msfr, s.mother, s.etax, s.remarks, s.tax_amt_paid, s.with_tax, s.taxid, s.tax_percent, s.taxfree_amt, s.reref,s.sabr);

  END


GO
/****** Object:  StoredProcedure [dbo].[update_items]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[update_items]

  @server nvarchar(50), 
  @db nvarchar(50),
  @NO_items_updated int output,
  @errstatus int = 0 output,
  @sl_man int,
  @br_sl_pr int,
  @br_no nvarchar(2),
  @with_offers bit


AS
BEGIN

declare   
 @sqlmsg nvarchar(max)

set @sqlmsg='MERGE items as t'
set @sqlmsg=@sqlmsg+' using'
set  @sqlmsg= @sqlmsg +' [main].[' + @db +'].dbo.items  as s'
    +' ON (T.item_no = S.item_no)'
    +' WHEN NOT MATCHED'
  --  +' THEN INSERT (item_no, item_name, item_cost,item_price,item_barcode,item_unit,item_obalance,item_cbalance,item_group,item_image,item_req)'
    +' THEN INSERT ([item_no],[item_name],[item_cost],[item_price],[item_barcode],[item_unit],[item_obalance],[item_cbalance],[item_group],[item_image],[item_req],[item_tax],[unit2],[uq2],[unit2p],[unit3],[uq3],[unit3p],[unit4],[uq4],[unit4p],[item_ename],[item_opencost],[item_curcost],[supno],[note],[last_updt],[sgroup],[price2], price3, min_price, static_cost, inactive,dunit) '
   -- +' VALUES (s.item_no, s.item_name, s.item_cost,s.item_price,s.item_barcode,s.item_unit,s.item_obalance,s.item_cbalance,s.item_group,s.item_image,s.item_req)'
    +' VALUES (s.[item_no],s.[item_name],s.[item_cost],s.[item_price],s.[item_barcode],s.[item_unit],s.[item_obalance],s.[item_cbalance],s.[item_group],s.[item_image],s.[item_req],s.[item_tax],s.[unit2],s.[uq2],s.[unit2p],s.[unit3],s.[uq3],s.[unit3p],s.[unit4],s.[uq4],s.[unit4p],s.[item_ename],s.[item_opencost],s.[item_curcost],s.[supno],s.[note],s.[last_updt],s.[sgroup],s.[price2], s.price3, s.min_price, s.static_cost, s.inactive,s.dunit)'
    +' WHEN MATCHED'
    +' THEN UPDATE SET t.item_price = s.item_price , t.item_name=s.item_name ,t.price2=s.price2,t.price3=s.price3,t.min_price=s.min_price, t.static_cost=s.static_cost, t.inactive=s.inactive,t.uq2=s.uq2,t.uq3=s.uq3'
    +' WHEN NOT MATCHED BY SOURCE'
    +' THEN DELETE;'
	begin transaction
	exec sp_executesql @sqlmsg
	set @No_items_updated=@@ROWCOUNT
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=1
		return 
	end
	commit transaction

-----------------------------------------------------------------------------


	set @sqlmsg='merge items_bc as t'
	set  @sqlmsg= @sqlmsg+' using'
	set  @sqlmsg= @sqlmsg +' [main].[' + @db +'].dbo.items_bc  as s'
                  +' ON t.barcode=s.barcode and t.br_no=s.br_no and t.sl_no=s.sl_no'
                  +' WHEN MATCHED THEN'
                  +' UPDATE SET t.price = s.price,t.pk_qty=s.pk_qty'
                  +' WHEN NOT MATCHED THEN'
                 -- +' INSERT VALUES(s.item_no, s.barcode, s.pack, s.pk_qty, s.price, s.note, s.pkorder)'
				  +' INSERT VALUES(s.[item_no],s.[barcode],s.[pack],s.[pk_qty],s.[price],s.[note],s.[pkorder], s.price2, s.price3, s.min_price,s.br_no,s.sl_no)'
				  +' WHEN NOT MATCHED BY SOURCE'
                  +' THEN DELETE;'	
	begin transaction
	exec sp_executesql @sqlmsg
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=2
		return 
	end
	commit transaction

	---------------------------------------------------------------------
if @with_offers = 1
begin
    set @sqlmsg='merge items_offer as t'
	set  @sqlmsg= @sqlmsg+' using'
	set  @sqlmsg= @sqlmsg +' [main].[' + @db +'].dbo.items_offer  as s'
                  +' ON t.br_no=s.br_no and t.sl_no= s.sl_no and t.barcode=s.barcode '
                  +' WHEN MATCHED THEN'
                  +' update set t.DiscountP=s.DiscountP,t.disctype=s.disctype,t.MinQty=s.MinQty,t.GivenAmt=s.GivenAmt,t.StartDate=s.StartDate,'
			      +' t.EndDate=s.EndDate,t.InActive=s.InActive,t.MaxQty=s.MaxQty,t.lprice1=s.lprice1,t.offer_price=s.offer_price'
                  +' WHEN NOT MATCHED THEN'
                 -- +' INSERT VALUES(s.item_no, s.barcode, s.pack, s.pk_qty, s.price, s.note, s.pkorder)'
				  +' INSERT VALUES(br_no, sl_no, itemno, unicode, barcode, DiscountP, disctype, MinQty, GivenAmt, StartDate, EndDate, InActive,'
				  +' lastupdt, MaxQty, lprice1, itemgroup,offer_price)'
				  +' WHEN NOT MATCHED BY SOURCE'
                  +' THEN DELETE;'	
	begin transaction
	exec sp_executesql @sqlmsg
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=3
		return 
	end
	commit transaction
end



	-----------------------------------------------------------------------

    set @sqlmsg='merge brprices as t'
	set  @sqlmsg= @sqlmsg+' using'
	set  @sqlmsg= @sqlmsg +' (select [branch],[slcenter],[itemno],[lprice1],[barcode] from [main].[' + @db +'].dbo.brprices where '+iif(@br_sl_pr=1,'branch','slcenter')+' ='+@br_no+') as s'
                  +' ON t.itemno=s.itemno'
                  +' WHEN MATCHED THEN'
                  +' UPDATE SET t.lprice1 = s.lprice1'
                  +' WHEN NOT MATCHED THEN'
				  +' INSERT([branch],[slcenter],[itemno],[lprice1],[barcode])'
				  +' VALUES(s.[branch],s.[slcenter],s.[itemno],s.[lprice1],s.[barcode])'
				  +' WHEN NOT MATCHED BY SOURCE'
                  +' THEN DELETE;'	
	begin transaction
	exec sp_executesql @sqlmsg
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=4
		return 
	end
	commit transaction

-----------------------------------------------------------------------

    exec bld_brprices_all @br_no = @br_no, @br_sl_pr = @br_sl_pr
	--begin transaction
	--exec sp_executesql @sqlmsg
	--if @@error<>0
	--begin
	--	rollback transaction
	--	set @errstatus=5
	--	return 
	--end
	--commit transaction

------------------------------------------------------------------------
if @sl_man=1
  begin
    set @sqlmsg='merge pos_salmen as t'
	set  @sqlmsg= @sqlmsg+' using'
	set  @sqlmsg= @sqlmsg +' (select * from [main].[' + @db +'].dbo.pos_salmen where sl_brno='+@br_no+') as s'
                  +' ON t.sl_brno=s.sl_brno and t.sl_id=s.sl_id'
                  +' WHEN MATCHED THEN'
                  +' UPDATE SET  t.[sl_name] = s.[sl_name], 
  t.[slpaddress] = s.[slpaddress], 
  t.[slptel] = s.[slptel], 
  t.[sl_password] = s.[sl_password], 
  t.[sl_chgqty] = s.[sl_chgqty], 
  t.[sl_chgprice] = s.[sl_chgprice], 
  t.[sl_delline] = s.[sl_delline], 
  t.[sl_delinv] = s.[sl_delinv], 
  t.[sl_return] = s.[sl_return], 
  t.[sl_admin] = s.[sl_admin], 
  t.[sl_alowdisc] = s.[sl_alowdisc], 
  t.[slpmaxdisc] = s.[slpmaxdisc], 
  t.[slppricetp] = s.[slppricetp], 
  t.[sl_alowcancel] = s.[sl_alowcancel], 
  t.[slpalowopdr] = s.[slpalowopdr], 
  t.[modified] = s.[modified], 
  t.[sl_alowexit] = s.[sl_alowexit], 
  t.[slprtwoinv] = s.[slprtwoinv], 
  t.[slpblowbal] = s.[slpblowbal], 
  t.[scr_open] = s.[scr_open], 
  t.[alwitmdsc] = s.[alwitmdsc], 
  t.[maxitmdsc] = s.[maxitmdsc], 
  t.[alwuserpls] = s.[alwuserpls], 
  t.[alwuseicrpls] = s.[alwuseicrpls], 
  t.[slpmagnetic] = s.[slpmagnetic], 
  t.[alwusebrrtn] = s.[alwusebrrtn], 
  t.[alwreprint] = s.[alwreprint], 
  t.[frcrplinv] = s.[frcrplinv], 
  t.[catch_thief] = s.[catch_thief], 
  t.[sold_once] = s.[sold_once], 
  t.[only_scan_bc] = s.[only_scan_bc], 
  t.[stop_item_column] = s.[stop_item_column], 
  t.[slpfpalw] = s.[slpfpalw], 
  t.[slpfpimage] = s.[slpfpimage], 
  t.[slcenter] = s.[slcenter], 
  t.[salesquota] = s.[salesquota], 
  t.[commissionP] = s.[commissionP], 
  t.[bonus] = s.[bonus], 
  t.[supervisor] = s.[supervisor], 
  t.[ReadInvByBarcd] = s.[ReadInvByBarcd], 
  t.[alwrtncash] = s.[alwrtncash], 
  t.[alwmtchrtn] = s.[alwmtchrtn], 
  t.[alwinvtrnsfr] = s.[alwinvtrnsfr], 
  t.[alwcshcardpay] = s.[alwcshcardpay], 
  t.[slp_hlldscamtMAX] = s.[slp_hlldscamtMAX], 
  t.[alwprnttmpinv] = s.[alwprnttmpinv], 
  t.[sl_inactive] = s.[sl_inactive],
  t.[sl_alwcrdit] = s.[sl_alwcrdit]'
                  +' WHEN NOT MATCHED THEN'
				  +' INSERT([sl_brno],[sl_id],[sl_name],[slpaddress],[slptel],[sl_password],[sl_chgqty],[sl_chgprice],[sl_delline],[sl_delinv],[sl_return],[sl_admin],[sl_alowdisc],[slpmaxdisc],[slppricetp],[sl_alowcancel],[slpalowopdr],[modified],[sl_alowexit],[slprtwoinv],[slpblowbal],[scr_open],[alwitmdsc],[maxitmdsc],[alwuserpls],[alwuseicrpls],[slpmagnetic],[alwusebrrtn],[alwreprint],[frcrplinv],[catch_thief],[sold_once],[only_scan_bc],[stop_item_column],[slpfpalw],[slpfpimage],[slcenter],[salesquota],[commissionP],[bonus],[supervisor],[ReadInvByBarcd],[alwrtncash],[alwmtchrtn],[alwinvtrnsfr],[alwcshcardpay],[slp_hlldscamtMAX],[alwprnttmpinv],[sl_inactive])'
				  +' VALUES(s.[sl_brno],s.[sl_id],s.[sl_name],s.[slpaddress],s.[slptel],s.[sl_password],s.[sl_chgqty],s.[sl_chgprice],s.[sl_delline],s.[sl_delinv],s.[sl_return],s.[sl_admin],s.[sl_alowdisc],s.[slpmaxdisc],s.[slppricetp],s.[sl_alowcancel],s.[slpalowopdr],s.[modified],s.[sl_alowexit],s.[slprtwoinv],s.[slpblowbal],s.[scr_open],s.[alwitmdsc],s.[maxitmdsc],s.[alwuserpls],s.[alwuseicrpls],s.[slpmagnetic],s.[alwusebrrtn],s.[alwreprint],s.[frcrplinv],s.[catch_thief],s.[sold_once],s.[only_scan_bc],s.[stop_item_column],s.[slpfpalw],s.[slpfpimage],s.[slcenter],s.[salesquota],s.[commissionP],s.[bonus],s.[supervisor],s.[ReadInvByBarcd],s.[alwrtncash],s.[alwmtchrtn],s.[alwinvtrnsfr],s.[alwcshcardpay],s.[slp_hlldscamtMAX],s.[alwprnttmpinv],s.[sl_inactive]);'
				--  +' WHEN NOT MATCHED BY SOURCE'
               --   +' THEN DELETE;'	
	begin transaction
	exec sp_executesql @sqlmsg
	if @@error<>0
	begin
		rollback transaction
		set @errstatus=6
		return 
	end
	commit transaction
 end

end


	--MERGE items AS T
--USING  @server_db +'.dbo.items'  AS S
--ON (T.item_barcode = S.item_barcode) 
--WHEN NOT MATCHED --BY TARGET AND S.EmployeeName LIKE 'S%' 
----THEN INSERT (select* from S)
--THEN INSERT (item_no, item_name, item_cost,item_price,item_barcode)
--VALUES (s.item_no, s.item_name, s.item_cost,s.item_price,s.item_barcode)
--WHEN MATCHED 
--THEN UPDATE SET T.item_price = S.item_price
--WHEN NOT MATCHED BY SOURCE --AND T.EmployeeName LIKE 'S%'
--THEN DELETE ;
----OUTPUT $action,inserted.*, deleted.*;
--END
--GO








GO
/****** Object:  StoredProcedure [dbo].[update_items_table]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[update_items_table] 
@br_no varchar(2),
@items_tb tb_items READONLY,
@items_bc_tb tb_items_bc READONLY,
@units_tb tb_units READONLY,
@brprices_tb tb_brprices READONLY,
@pos_salmen_tb tb_pos_salmen READONLY,
@items_offer_tb tb_items_offer READONLY,
@whbins_tb tb_whbins READONLY,
@sl_man int,
@br_sl_pr int,
@with_offer bit,
@with_stk bit,
@NO_items_updated int output

AS
BEGIN

  ----BEGIN TRANSACTION;  
  
  ----     BEGIN TRY 

  ----  delete from items;

    MERGE items AS T
    using @items_tb  AS S
    ON (T.item_barcode = S.item_barcode)
    WHEN NOT MATCHED
    THEN INSERT ([item_no],[item_name],[item_cost],[item_price],[item_barcode],[item_unit],[item_obalance],[item_cbalance],[item_group],[item_image],[item_req],[item_tax],[unit2],[uq2],[unit2p],[unit3],[uq3],[unit3p],[unit4],[uq4],[unit4p],[item_ename],[item_opencost],[item_curcost],[supno],[note],[last_updt],[sgroup],[price2],price3, min_price, static_cost, inactive,dunit)
    VALUES (s.[item_no],s.[item_name],s.[item_cost],s.[item_price],s.[item_barcode],s.[item_unit],s.[item_obalance],s.[item_cbalance],s.[item_group],s.[item_image],s.[item_req],s.[item_tax],s.[unit2],s.[uq2],s.[unit2p],s.[unit3],s.[uq3],s.[unit3p],s.[unit4],s.[uq4],s.[unit4p],s.[item_ename],s.[item_opencost],s.[item_curcost],s.[supno],s.[note],s.[last_updt],s.[sgroup],s.[price2], s.price3, s.min_price, s.static_cost, s.inactive,s.dunit)
    WHEN MATCHED
    THEN UPDATE SET t.item_price = s.item_price , t.item_name=s.item_name ,t.price2=s.price2,t.price3=s.price3,t.min_price=s.min_price, t.static_cost=s.static_cost, t.inactive=s.inactive,t.uq2=s.uq2,t.uq3=s.uq3;
    --WHEN NOT MATCHED BY SOURCE
    --THEN DELETE;
	set @No_items_updated=@@ROWCOUNT


    MERGE items_bc as t
    USING @items_bc_tb as s
    ON t.barcode=s.barcode and t.br_no=s.br_no and t.sl_no=s.sl_no
    WHEN MATCHED THEN
    UPDATE SET t.price = s.price
    WHEN NOT MATCHED THEN
   -- INSERT VALUES(s.item_no, s.barcode, s.pack, s.pk_qty, s.price, s.note,s.pkorder);
    INSERT VALUES(s.[item_no],s.[barcode],s.[pack],s.[pk_qty],s.[price],s.[note],s.[pkorder], s.price2, s.price3, s.min_price,s.br_no,s.sl_no);

	MERGE units as t
    USING @units_tb as s
    ON t.unit_id=s.unit_id
    WHEN MATCHED THEN
    UPDATE SET t.unit_name = s.unit_name
    WHEN NOT MATCHED THEN
   -- INSERT VALUES(s.item_no, s.barcode, s.pack, s.pk_qty, s.price, s.note,s.pkorder);
    INSERT VALUES(s.[unit_id],s.[unit_name],s.[unit_desc],s.[unit_order],s.[unit_qty]);

	   ----COMMIT;  	  
	   ----END TRY

    ----   BEGIN CATCH
	     
    ----     ROLLBACK; 
    ----   END CATCH

if @with_offer=1
begin

merge items_offer as t
 using
 @items_offer_tb  as s
                   ON t.br_no=s.br_no and t.sl_no= s.sl_no and t.barcode=s.barcode 
                   WHEN MATCHED THEN
                   update set t.DiscountP=s.DiscountP,t.disctype=s.disctype,t.MinQty=s.MinQty,t.GivenAmt=s.GivenAmt,t.StartDate=s.StartDate,
			       t.EndDate=s.EndDate,t.InActive=s.InActive,t.MaxQty=s.MaxQty,t.lprice1=s.lprice1,t.offer_price=s.offer_price
                   WHEN NOT MATCHED THEN
                 -- +' INSERT VALUES(s.item_no, s.barcode, s.pack, s.pk_qty, s.price, s.note, s.pkorder)'
				   INSERT VALUES(br_no, sl_no, itemno, unicode, barcode, DiscountP, disctype, MinQty, GivenAmt, StartDate, EndDate, InActive,
				   lastupdt, MaxQty, lprice1, itemgroup,offer_price);
end

	merge brprices as t
    using @brprices_tb as s
                  ON t.itemno=s.itemno and t.branch=@br_no 
                  WHEN MATCHED THEN
                  UPDATE SET t.lprice1 = s.lprice1
                  WHEN NOT MATCHED THEN
				  INSERT([branch],[slcenter],[itemno],[lprice1],[barcode])
				  VALUES(s.[branch],s.[slcenter],s.[itemno],s.[lprice1],s.[barcode]);
				  --WHEN NOT MATCHED BY SOURCE
      --            THEN DELETE;	

--------------------------------------------------------------------------------------------------------------

exec bld_brprices_all @br_no =@br_no, @br_sl_pr =@br_sl_pr;	

--UPDATE t1 SET 
--         t1.item_price = t2.lprice1
--  FROM   items t1 
--         INNER JOIN @brprices_tb t2 
--                   ON t1.item_no = t2.itemno and t1.item_price <> t2.lprice1
--  WHERE  iif(@br_sl_pr=1,t2.branch,t2.slcenter)=@br_no ; 

--  UPDATE t1 SET 
--         t1.price = t2.lprice1
--  FROM   items_bc t1 
--         INNER JOIN @brprices_tb t2 
--                   ON t1.item_no = t2.itemno and t1.price <> t2.lprice1
--  WHERE  iif(@br_sl_pr=1,t2.branch,t2.slcenter)=@br_no ; 

--------------------------------------------------------------------------------------------------------------

if @with_stk=1
begin

merge whbins as t
 using
 @whbins_tb  as s
                   on t.br_no=s.br_no and t.item_no=s.item_no and t.unicode=s.unicode and t.wh_no=s.wh_no and t.bin_no=s.bin_no and t.expdate=s.expdate 
                   WHEN MATCHED THEN
                   update set t.[qty]=s.[qty],t.openbal=s.[openbal],t.openlcost=s.lcost,t.openfcost=s.fcost
                   WHEN NOT MATCHED THEN
                   insert VALUES(s.[br_no],s.[item_no],s.[unicode],s.[wh_no],s.[bin_no],s.[qty],s.[rsvqty], s.[openbal],s.[lcost]
				   ,s.[fcost],s.[lcost],s.[openfcost],s.[expdate]);

UPDATE t1 SET 
         --t1.item_cbalance = t2.qty+ t2.[openbal],t1.item_curcost=((t2.qty*t2.lcost)+ (t2.[openbal]*t2.openlcost))/(t2.qty+ t2.[openbal])
		 t1.item_cbalance = t2.qty+ t2.[openbal],t1.item_curcost=t2.lcost
  FROM   items t1 
        INNER JOIN @whbins_tb t2 
                  ON t1.item_no = t2.item_no ;

end



	  if @sl_man=1
         BEGIN
		 merge pos_salmen as t
 using @pos_salmen_tb as s
   ON t.sl_brno=s.sl_brno and t.sl_id=s.sl_id
   WHEN MATCHED THEN
   UPDATE SET  t.[sl_name] = s.[sl_name], 
  t.[slpaddress] = s.[slpaddress], 
  t.[slptel] = s.[slptel], 
  t.[sl_password] = s.[sl_password], 
  t.[sl_chgqty] = s.[sl_chgqty], 
  t.[sl_chgprice] = s.[sl_chgprice], 
  t.[sl_delline] = s.[sl_delline], 
  t.[sl_delinv] = s.[sl_delinv], 
  t.[sl_return] = s.[sl_return], 
  t.[sl_admin] = s.[sl_admin], 
  t.[sl_alowdisc] = s.[sl_alowdisc], 
  t.[slpmaxdisc] = s.[slpmaxdisc], 
  t.[slppricetp] = s.[slppricetp], 
  t.[sl_alowcancel] = s.[sl_alowcancel], 
  t.[slpalowopdr] = s.[slpalowopdr], 
  t.[modified] = s.[modified], 
  t.[sl_alowexit] = s.[sl_alowexit], 
  t.[slprtwoinv] = s.[slprtwoinv], 
  t.[slpblowbal] = s.[slpblowbal], 
  t.[scr_open] = s.[scr_open], 
  t.[alwitmdsc] = s.[alwitmdsc], 
  t.[maxitmdsc] = s.[maxitmdsc], 
  t.[alwuserpls] = s.[alwuserpls], 
  t.[alwuseicrpls] = s.[alwuseicrpls], 
  t.[slpmagnetic] = s.[slpmagnetic], 
  t.[alwusebrrtn] = s.[alwusebrrtn], 
  t.[alwreprint] = s.[alwreprint], 
  t.[frcrplinv] = s.[frcrplinv], 
  t.[catch_thief] = s.[catch_thief], 
  t.[sold_once] = s.[sold_once], 
  t.[only_scan_bc] = s.[only_scan_bc], 
  t.[stop_item_column] = s.[stop_item_column], 
  t.[slpfpalw] = s.[slpfpalw], 
  t.[slpfpimage] = s.[slpfpimage], 
  t.[slcenter] = s.[slcenter], 
  t.[salesquota] = s.[salesquota], 
  t.[commissionP] = s.[commissionP], 
  t.[bonus] = s.[bonus], 
  t.[supervisor] = s.[supervisor], 
  t.[ReadInvByBarcd] = s.[ReadInvByBarcd], 
  t.[alwrtncash] = s.[alwrtncash], 
  t.[alwmtchrtn] = s.[alwmtchrtn], 
  t.[alwinvtrnsfr] = s.[alwinvtrnsfr], 
  t.[alwcshcardpay] = s.[alwcshcardpay], 
  t.[slp_hlldscamtMAX] = s.[slp_hlldscamtMAX], 
  t.[alwprnttmpinv] = s.[alwprnttmpinv], 
  t.[sl_inactive] = s.[sl_inactive],
  t.[sl_alwcrdit] = s.[sl_alwcrdit]
  WHEN NOT MATCHED THEN
  INSERT([sl_brno],[sl_id],[sl_name],[slpaddress],[slptel],[sl_password],[sl_chgqty],[sl_chgprice],[sl_delline],[sl_delinv],[sl_return],[sl_admin],[sl_alowdisc],[slpmaxdisc],[slppricetp],[sl_alowcancel],[slpalowopdr],[modified],[sl_alowexit],[slprtwoinv],[slpblowbal],[scr_open],[alwitmdsc],[maxitmdsc],[alwuserpls],[alwuseicrpls],[slpmagnetic],[alwusebrrtn],[alwreprint],[frcrplinv],[catch_thief],[sold_once],[only_scan_bc],[stop_item_column],[slpfpalw],[slpfpimage],[slcenter],[salesquota],[commissionP],[bonus],[supervisor],[ReadInvByBarcd],[alwrtncash],[alwmtchrtn],[alwinvtrnsfr],[alwcshcardpay],[slp_hlldscamtMAX],[alwprnttmpinv],[sl_inactive],[sl_alwcrdit])
  VALUES(s.[sl_brno],s.[sl_id],s.[sl_name],s.[slpaddress],s.[slptel],s.[sl_password],s.[sl_chgqty],s.[sl_chgprice],s.[sl_delline],s.[sl_delinv],s.[sl_return],s.[sl_admin],s.[sl_alowdisc],s.[slpmaxdisc],s.[slppricetp],s.[sl_alowcancel],s.[slpalowopdr],s.[modified],s.[sl_alowexit],s.[slprtwoinv],s.[slpblowbal],s.[scr_open],s.[alwitmdsc],s.[maxitmdsc],s.[alwuserpls],s.[alwuseicrpls],s.[slpmagnetic],s.[alwusebrrtn],s.[alwreprint],s.[frcrplinv],s.[catch_thief],s.[sold_once],s.[only_scan_bc],s.[stop_item_column],s.[slpfpalw],s.[slpfpimage],s.[slcenter],s.[salesquota],s.[commissionP],s.[bonus],s.[supervisor],s.[ReadInvByBarcd],s.[alwrtncash],s.[alwmtchrtn],s.[alwinvtrnsfr],s.[alwcshcardpay],s.[slp_hlldscamtMAX],s.[alwprnttmpinv],s.[sl_inactive],s.[sl_alwcrdit]);

       end

      
	   
  END











GO
/****** Object:  StoredProcedure [dbo].[update_pos_inv]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[update_pos_inv] 

@pos_hdr_tb tblpos_hdr READONLY,
@pos_dtl_tb tblpos_dtl READONLY

AS
BEGIN

  MERGE pos_hdr as t
  USING @pos_hdr_tb as s
  ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr
  WHEN MATCHED THEN
  UPDATE SET T.total = S.total,T.discount = S.discount,T.net_total = S.net_total,t.tax_amt=s.tax_amt
  WHEN NOT MATCHED THEN
  INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.date,s.total,s.count,s.payed,s.total_cost,s.saleman,s.req_no,s.cust_no,s.discount,s.net_total,s.sysdate,s.gen_ref,s.tax_amt,s.dscper,s.card_type,s.card_amt,s.rref,s.rcontr,s.taxfree_amt,s.note,s.mobile);

    --MERGE items AS T
    --using @items_tb  AS S
    --ON (T.item_barcode = S.item_barcode)
    --WHEN NOT MATCHED
    --THEN INSERT (item_no, item_name, item_cost,item_price,item_barcode,item_unit,item_obalance,item_cbalance,item_group,item_image,item_req)
    --VALUES (s.item_no, s.item_name, s.item_cost,s.item_price,s.item_barcode,s.item_unit,s.item_obalance,s.item_cbalance,s.item_group,s.item_image,s.item_req)
    --WHEN MATCHED
    --THEN UPDATE SET T.item_price = S.item_price
    --WHEN NOT MATCHED BY SOURCE
    --THEN DELETE;

  END


 BEGIN

  MERGE pos_dtl as t
  USING @pos_dtl_tb as s
  ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr and T.srno=S.srno
  WHEN MATCHED THEN
  UPDATE SET T.price = S.price,T.qty=S.qty,t.itemno=s.itemno,t.srno=s.srno
  WHEN NOT MATCHED THEN
  INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.barcode,s.name,s.unit,s.price,s.qty,s.cost,s.is_req,s.itemno,s.srno,s.pkqty,s.discpc,s.tax_id,s.tax_amt,s.rqty,s.offr_amt);
                 
    --MERGE items_bc as t
    --USING @items_bc_tb as s
    --ON t.barcode=s.barcode
    --WHEN MATCHED THEN
    --UPDATE SET t.price = s.price
    --WHEN NOT MATCHED THEN
    --INSERT VALUES(s.item_no, s.barcode, s.pack, s.pk_qty, s.price, s.note,s.pkorder);

  END



GO
/****** Object:  UserDefinedFunction [dbo].[get_balanace_for_item]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[get_balanace_for_item]
(@brno char(2),@whno char(2),@itemid varchar(20),@Glact bit)
-- @mode 
RETURNS numeric(8,2)
AS
BEGIN
    declare @dbc varchar(40),
    @c_l_cost float=0,
	@lpkqty numeric(8,2),
	@sql varchar(400)='',
	@paralist varchar(400)
	set @dbc=DB_NAME()	

	--if @glact=0
	--begin
   select @lpkqty =(coalesce(stp.openbal,0) + coalesce(pur.puqty,0) -coalesce(sal.salqty,0) + coalesce(sto.stoqty,0) - coalesce(sto.stoqty1,0) - coalesce(sto2.stoqty2,0)) from items a
	 left join
	 (select t.itemno,sum(case when t.invtype in ('06','07') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else -t.qty*t.pkqty end) as puqty 
      from pu_dtl t where t.whno=@whno
      group by t.itemno
     ) pur   on a.item_no = pur.itemno  left join
     (select t.itemno,  sum(case when t.invtype in ('04','05') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else -t.qty*t.pkqty end) as salqty  
      from sales_dtl t where t.whno=@whno
      group by t.itemno
     ) sal
     on a.item_no = sal.itemno left join
     (select t.itemno, sum(case when t.trtype in ('31','33','45','46','35') and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end) as stoqty 
	  ,sum(case when t.trtype in ('32') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end) as stoqty1 
	 -- ,sum(case when t.trtype in ('31','33','45','46') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end) as stoqty2
	 --,sum(case when t.trtype in ('33') and t.towhno=@whno and t.branch=@brno then t.qty*t.pkqty  when t.trtype in ('33') and t.whno=@whno and t.branch=@brno then -(t.qty*t.pkqty) else 0 end) as stoqty2
	  from sto_dtl t where t.towhno=@whno
      group by t.itemno
     ) sto
	  on a.item_no = sto.itemno  left join
     (select t.itemno,sum(case when t.trtype in ('33') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end) as stoqty2
	  from sto_dtl t where t.whno=@whno
      group by t.itemno
    ) sto2
	  on a.item_no = sto2.itemno left join
     (select t.item_no,t.wh_no,sum(t.openbal) openbal from whbins t where t.wh_no=@whno and t.br_no=@brno
      group by t.item_no,t.wh_no
     ) stp
	  on a.item_no = stp.item_no and stp.wh_no=@whno
	  where a.item_no = @itemid;
		
	--end
	set @c_l_cost=isnull(@c_l_cost,0)
	set @lpkqty=isnull(@lpkqty,0)
	if @c_l_cost>0 and @lpkqty>1 set @c_l_cost=@c_l_cost/@lpkqty
	--return @c_l_cost
	return @lpkqty

	--select [dbo].[get_balanace_for_item] ('01','01','114B',0)
end

GO
/****** Object:  UserDefinedFunction [dbo].[get_item_bal]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[get_item_bal]
(@itemno varchar(20)) 
RETURNS numeric(8,2)
AS
BEGIN 

    declare @dbc varchar(40),
    @c1 float,@c2 float,@c3 float, @c4 float,@c5 float,@c6 float
	set @dbc=DB_NAME()	

     set @c1 = (select sum(case when t.invtype in ('06','07')  then t.qty*t.pkqty else -t.qty*t.pkqty end)  from pu_dtl t where t.itemno = @itemno)

     set @c2 = (select sum(case when t.invtype in ('14','15') then t.qty*t.pkqty else -t.qty*t.pkqty end) from sales_dtl t where t.itemno = @itemno)


     set @c3 = (select sum(case when t.trtype in ('31','45','46','35',case when trtype='45' and whno='' and towhno<>'' then '45' else '' end)  then t.qty*t.pkqty else 0 end) from sto_dtl t where t.itemno = @itemno)

	 set @c4 = (select sum(case when t.trtype in ('32',case when trtype='45' and towhno='' and whno<>'' then '45' else '' end) then t.qty*t.pkqty else 0 end) from sto_dtl t where t.itemno = @itemno)

    -- set @c5 = (select sum(case when t.trtype in ('31','33','45','46') then t.qty*t.pkqty else 0 end) from sto_dtl t  where t.itemno = @itemno)
	-- set @c5 = (select sum(case when t.trtype in ('33')  then t.qty*t.pkqty else 0 end) from sto_dtl t where t.itemno = @itemno)
     
     set @c6 =(select sum(t.openbal) openbal from whbins t where t.item_no = @itemno)

	  
	
	 return coalesce(@c6,0)+ coalesce(@c1,0)+ coalesce(@c2,0)+ coalesce(@c3,0)- coalesce(@c4,0)

	--select [dbo].[get_item_bal] ('111')

end

GO
/****** Object:  UserDefinedFunction [dbo].[get_item_bal_by_whno]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[get_item_bal_by_whno]
(@itemno varchar(20),@whno varchar(2),@brno varchar(2)) 
RETURNS numeric(8,2)
AS
BEGIN 
 --   declare @string numeric(8,2)
	----set @string=@strnum
	--set @string = (select isnull(sum(qty + openbal),0) from whbins where br_no=@brno and wh_no=@whno and item_no=@itemno)
	--return @string

	----select  [dbo].[get_numbers_from_string] ('SA1234M30')

	  declare @dbc varchar(40),
    @c_l_cost float=0,
	@lpkqty numeric(8,2),
	@sql varchar(400)='',
	@paralist varchar(400)
	set @dbc=DB_NAME()	

	--if @glact=0
	--begin
   select @lpkqty =(coalesce(stp.openbal,0) + coalesce(pur.puqty,0) -coalesce(sal.salqty,0) + coalesce(sto1.stoqty1,0) - coalesce(sto2.stoqty2,0)) from items a
	 left join
	 (select t.itemno,isnull(sum(case when t.invtype in ('06','07') and t.whno=''+@whno+'' and t.branch=''+@brno+'' then t.qty*t.pkqty else -t.qty*t.pkqty end) ,0) as puqty
      from pu_dtl t where t.whno=''+@whno+'' and t.itemno=''+@itemno +''
      group by t.itemno
     ) pur   on a.item_no = pur.itemno  left join
     (select t.itemno,  isnull(sum(case when t.invtype in ('04','05') and t.whno=''+@whno+'' and t.branch=''+@brno+'' then t.qty*t.pkqty else -t.qty*t.pkqty end),0) as salqty
      from sales_dtl t where t.whno=''+@whno+'' and t.itemno=''+@itemno +''
      group by t.itemno
     ) sal
     on a.item_no = sal.itemno left join
     (select t.itemno, isnull(sum(case when t.trtype in ('31','45','46','35',case when trtype='45' and whno='' and towhno<>'' and t.branch=''+@brno+'' then '45' else '' end,case when t.trtype in ('33') and t.towhno=''+@whno+'' then '33' else '' end) and t.towhno=''+@whno+'' and t.branch=''+@brno+'' then t.qty*t.pkqty else 0 end) ,0) as stoqty1 
	  --,isnull(sum(case when t.trtype in ('31','33') and t.towhno=''+@whno+'' and t.branch=''+@brno+'' then t.qty*t.pkqty else 0 end),0) as stoqty1 
	 -- ,sum(case when t.trtype in ('31','33','45','46') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end) as stoqty2
	  from sto_dtl t where t.towhno=''+@whno+'' and t.itemno=''+@itemno +''
      group by t.itemno
     ) sto1
	  on a.item_no = sto1.itemno  left join
     (select t.itemno,isnull(sum(case when t.trtype in ('32',case when trtype='45' and towhno='' and whno<>'' and t.branch=''+@brno+'' then '45' else '' end,case when t.trtype in ('33') and t.whno=''+@whno+''  then '33' else '' end) and t.whno=''+@whno+'' and t.branch=''+@brno+'' then t.qty*t.pkqty else 0 end),0) as stoqty2
	   from sto_dtl t where t.whno=''+@whno+'' and t.itemno=''+@itemno +''
      group by t.itemno
     ) sto2
	  on a.item_no = sto2.itemno left join
     (select t.item_no,t.wh_no, isnull(sum(t.openbal),0) openbal  from whbins t where t.wh_no=''+@whno+'' and t.br_no=''+@brno+'' and t.item_no=''+@itemno +''
      group by t.item_no,t.wh_no
     ) stp
	  on a.item_no = stp.item_no and stp.wh_no=''+@whno+''
	  where a.item_no = ''+@itemno +'';
		
	--end
	set @c_l_cost=isnull(@c_l_cost,0)
	set @lpkqty=isnull(@lpkqty,0)
	if @c_l_cost>0 and @lpkqty>1 set @c_l_cost=@c_l_cost/@lpkqty
	--return @c_l_cost
	return @lpkqty

	--select [dbo].[get_item_bal_by_whno] ('2211','01','01')

end


GO
/****** Object:  UserDefinedFunction [dbo].[get_item_bal_old]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[get_item_bal_old]
(@itemno varchar(20)) 
RETURNS numeric(8,2)
AS
BEGIN 
 --   declare @string numeric(8,2)
	----set @string=@strnum
	--set @string = (select isnull(sum(qty + openbal),0) from whbins where br_no=@brno and wh_no=@whno and item_no=@itemno)
	--return @string

	----select  [dbo].[get_numbers_from_string] ('SA1234M30')

	  declare @dbc varchar(40),
    @c_l_cost float=0,
	@lpkqty numeric(8,2),
	@sql varchar(400)='',
	@paralist varchar(400)
	set @dbc=DB_NAME()	

	--if @glact=0
	--begin
   select @lpkqty =(coalesce(stp.openbal,0) + coalesce(pur.puqty,0) -coalesce(sal.salqty,0) + coalesce(sto.stoqty,0) - coalesce(sto.stoqty1,0) - coalesce(sto2.stoqty2,0)) from items a
	 left join
	 (select t.itemno,sum(case when t.invtype in ('06','07')  then t.qty*t.pkqty else -t.qty*t.pkqty end) as puqty 
      from pu_dtl t 
      group by t.itemno
     ) pur   on a.item_no = pur.itemno  left join
     (select t.itemno,  sum(case when t.invtype in ('04','05') then t.qty*t.pkqty else -t.qty*t.pkqty end) as salqty  
      from sales_dtl t 
      group by t.itemno
     ) sal
     on a.item_no = sal.itemno left join
     (select t.itemno, sum(case when t.trtype in ('31','33','45','46','35')  then t.qty*t.pkqty else 0 end) as stoqty 
	  ,sum(case when t.trtype in ('32') then t.qty*t.pkqty else 0 end) as stoqty1 
	 -- ,sum(case when t.trtype in ('31','33','45','46') and t.whno=@whno and t.branch=@brno then t.qty*t.pkqty else 0 end) as stoqty2
	  from sto_dtl t 
      group by t.itemno
     ) sto
	  on a.item_no = sto.itemno  left join
     (select t.itemno,sum(case when t.trtype in ('31','33','45','46') then t.qty*t.pkqty else 0 end) as stoqty2
	  from sto_dtl t 
      group by t.itemno
     ) sto2
	  on a.item_no = sto2.itemno left join
     (select t.item_no,t.wh_no,sum(t.openbal) openbal from whbins t
      group by t.item_no,t.wh_no
     ) stp
	  on a.item_no = stp.item_no 
	  where a.item_no = @itemno;
		
	--end
	set @c_l_cost=isnull(@c_l_cost,0)
	set @lpkqty=isnull(@lpkqty,0)
	if @c_l_cost>0 and @lpkqty>1 set @c_l_cost=@c_l_cost/@lpkqty
	--return @c_l_cost
	return @lpkqty

	--select [dbo].[get_item_bal] ('2211')

end


GO
/****** Object:  UserDefinedFunction [dbo].[get_lastcost_for_item]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[get_lastcost_for_item]
(@comp char(2),@itemid varchar(20), @lbarcode varchar(20),@Glact bit)
-- @mode 
RETURNS float
AS
BEGIN
    declare @dbc varchar(40),
    @c_l_cost float=0,
	@lpkqty numeric(8,3),
	@sql varchar(400)='',
	@paralist varchar(400)
	set @dbc=DB_NAME()	

	if @glact=0
	begin
		select top 1 @c_l_cost=price ,@lpkqty=pkqty from pu_dtl a join pu_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.pucenter=b.pucenter and a.ref=b.ref  where a.branch=@comp and ltrim(rtrim(a.itemno))=@itemid and 
		ltrim(rtrim(a.barcode))=@lbarcode and a.invtype in ('06','07') --order by b.released desc

		if isnull(@c_l_cost,0)=0
		begin
		  declare @yrcode char(4)
		  set @yrcode=substring(@dbc,6,4)
		  set @dbc=substring(@dbc,1,5)+str(cast(@yrcode as int)-1,2)
		  if exists(SELECT name FROM sys.databases where name=@dbc)
		  begin
			 set @paralist='@c_l_cost float output,@lpkqty numeric(8,3) output'
			 set @sql='select top 1 @c_l_cost=price,@lpkqty=pkqty from '+@dbc+'.dbo.pu_dtl where 
					   branch='''+@comp+''' and ltrim(rtrim(itemno))='''+@itemid+''' and 
					   ltrim(rtrim(barcode))='''+@lbarcode+''' and invtype in (''06'',''07'') '
					 --  ltrim(rtrim(barcode))='''+@lbarcode+''' and invtype in (''06'',''07'') order by invmdate desc'

			 EXECUTE sp_executesql @sql ,@paralist,@c_l_cost=@c_l_cost output,@lpkqty=@lpkqty output
		  end
		end
	end
	set @c_l_cost=isnull(@c_l_cost,0)
	set @lpkqty=isnull(@lpkqty,0)
	if @c_l_cost>0 and @lpkqty>1 set @c_l_cost=@c_l_cost/@lpkqty
	return @c_l_cost


	--select [dbo].[get_lastprice_for_clint] ('01','1', '10051',0)
end



GO
/****** Object:  UserDefinedFunction [dbo].[get_lastprice_for_clint]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[get_lastprice_for_clint]
(@comp char(2),@custid varchar(19), @lbarcode varchar(20),@Glact bit)
-- @mode 
RETURNS float
AS
BEGIN
    declare @dbc varchar(40),
    @c_l_price float=0,
	@lpkqty numeric(8,3),
	@sql varchar(400)='',
	@paralist varchar(400)
	set @dbc=DB_NAME()	

	if @glact=0
	begin
		select top 1 @c_l_price=price ,@lpkqty=pkqty from sales_dtl a join sales_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.slcenter=b.slcenter and a.ref=b.ref  where a.branch=@comp and ltrim(rtrim(b.custno))=@custid and 
		ltrim(rtrim(a.barcode))=@lbarcode and a.invtype in ('04','05') --order by b.released desc

		if isnull(@c_l_price,0)=0
		begin
		  declare @yrcode char(4)
		  set @yrcode=substring(@dbc,6,4)
		  set @dbc=substring(@dbc,1,5)+str(cast(@yrcode as int)-1,2)
		  if exists(SELECT name FROM sys.databases where name=@dbc)
		  begin
			 set @paralist='@c_l_price float output,@lpkqty numeric(8,3) output'
			 set @sql='select top 1 @c_l_price=price,@lpkqty=pkqty from '+@dbc+'.dbo.sales_dtl where 
					   branch='''+@comp+''' and ltrim(rtrim(custno))='''+@custid+''' and 
					   ltrim(rtrim(barcode))='''+@lbarcode+''' and invtype in (''04'',''05'') '
					   --ltrim(rtrim(barcode))='''+@lbarcode+''' and invtype in (''04'',''05'') order by invmdate desc'

			 EXECUTE sp_executesql @sql ,@paralist,@c_l_price=@c_l_price output,@lpkqty=@lpkqty output
		  end
		end
	end
	set @c_l_price=isnull(@c_l_price,0)
	set @lpkqty=isnull(@lpkqty,0)
	if @c_l_price>0 and @lpkqty>1 set @c_l_price=@c_l_price/@lpkqty
	return @c_l_price


	--select [dbo].[get_lastprice_for_clint] ('01','2', '111',0)
end


GO
/****** Object:  UserDefinedFunction [dbo].[get_lastprice_for_item]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[get_lastprice_for_item]
(@comp char(2), @lbarcode varchar(20),@Glact bit)
-- @mode 
RETURNS float
AS
BEGIN
    declare @dbc varchar(40),
    @c_l_price float=0,
	@lpkqty numeric(8,3),
	@mdate varchar(10),
	@sql varchar(400)='',
	@paralist varchar(400)
	set @dbc=DB_NAME()	

	if @glact=0
	begin
		select top 1 @c_l_price=price ,@lpkqty=pkqty,@mdate=CONVERT(VARCHAR(10), CAST(a.invmdate as date), 105) from pu_dtl a join pu_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.pucenter=b.pucenter and a.ref=b.ref  where a.branch=@comp and 
		ltrim(rtrim(a.itemno))=@lbarcode and a.invtype in ('06','07') --order by b.released desc

		if isnull(@c_l_price,0)=0
		begin
		  declare @yrcode char(4)
		  set @yrcode=substring(@dbc,6,4)
		 -- set @dbc=substring(@dbc,1,4)+str(cast(@yrcode as int)-1,2)
		 set @dbc=substring(@dbc,1,5)+str(cast(@yrcode as int)-1)
		  if exists(SELECT name FROM sys.databases where name=@dbc)
		  begin
			 set @paralist='@c_l_price float output,@lpkqty numeric(8,3) output'
			 set @sql='select top 1 @c_l_price=price,@lpkqty=pkqty from '+@dbc+'.dbo.pu_dtl where 
					   branch='''+@comp+''' and 
					   ltrim(rtrim(itemno))='''+@lbarcode+''' and invtype in (''06'',''07'') '
					   --ltrim(rtrim(itemno))='''+@lbarcode+''' and invtype in (''06'',''07'') order by released desc'

			 EXECUTE sp_executesql @sql ,@paralist,@c_l_price=@c_l_price output,@lpkqty=@lpkqty output
		  end
		end
	end
	else
	begin
	select top 1 @c_l_price=price ,@lpkqty=pkqty,@mdate=CONVERT(VARCHAR(10), CAST(a.invmdate as date), 105) from pu_dtl a join pu_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.pucenter=b.pucenter and a.ref=b.ref  where a.branch=@comp and 
		ltrim(rtrim(a.barcode))=@lbarcode and a.invtype in ('06','07') order by b.released desc


      if isnull(@c_l_price,0)=0
		begin
		  declare @yrcode1 char(4)
		  set @yrcode1=substring(@dbc,6,4)
		 -- set @dbc=substring(@dbc,1,4)+str(cast(@yrcode as int)-1,2)
		 set @dbc=substring(@dbc,1,5)+str(cast(@yrcode1 as int)-1)
		  if exists(SELECT name FROM sys.databases where name=@dbc)
		  begin
			 set @paralist='@c_l_price float output,@lpkqty numeric(8,3) output'
			 set @sql='select top 1 @c_l_price=price,@lpkqty=pkqty from '+@dbc+'.dbo.pu_dtl where 
					   branch='''+@comp+''' and 
					   ltrim(rtrim(barcode))='''+@lbarcode+''' and invtype in (''06'',''07'') order by released desc'

			 EXECUTE sp_executesql @sql ,@paralist,@c_l_price=@c_l_price output,@lpkqty=@lpkqty output
		  end
		end
	end

	set @c_l_price=isnull(@c_l_price,0)
	set @lpkqty=isnull(@lpkqty,0)
	--set @mdate=isnull(@mdate,'')
	if @c_l_price>0 and @lpkqty>1 set @c_l_price=@c_l_price/@lpkqty

	--insert into @t values(@c_l_price,@mdate)
	return round(@c_l_price,4) --(@c_l_price,@mdate)


	--select [dbo].[get_lastprice_for_item] ('01','555',0)

end


GO
/****** Object:  UserDefinedFunction [dbo].[get_lastprice_for_item1]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create FUNCTION [dbo].[get_lastprice_for_item1]
(@comp char(2), @lbarcode varchar(20),@Glact bit)
-- @mode 
RETURNS @t table (price float, pudate varchar(10))
AS
BEGIN
    declare @dbc varchar(40),
    @c_l_price float=0,
	@lpkqty numeric(8,3),
	@mdate varchar(10),
	@sql varchar(400)='',
	@paralist varchar(400)
	set @dbc=DB_NAME()	

	if @glact=0
	begin
		select top 1 @c_l_price=price ,@lpkqty=pkqty,@mdate=CONVERT(VARCHAR(10), CAST(a.invmdate as date), 105) from pu_dtl a join pu_hdr b on a.branch=b.branch and a.invtype=b.invtype and a.pucenter=b.pucenter and a.ref=b.ref  where a.branch=@comp and 
		ltrim(rtrim(a.itemno))=@lbarcode and a.invtype in ('06','07') order by b.released desc

		--if isnull(@c_l_price,0)=0
		--begin
		--  declare @yrcode char(4)
		--  set @yrcode=substring(@dbc,6,4)
		--  set @dbc=substring(@dbc,1,5)+str(cast(@yrcode as int)-1,2)
		--  if exists(SELECT name FROM sys.databases where name=@dbc)
		--  begin
		--	 set @paralist='@c_l_price float output,@lpkqty numeric(8,3) output'
		--	 set @sql='select top 1 @c_l_price=price,@lpkqty=pkqty from '+@dbc+'.dbo.pu_dtl where 
		--			   branch='''+@comp+''' and 
		--			   ltrim(rtrim(barcode))='''+@lbarcode+''' and invtype in (''06'',''07'') order by invmdate desc'

		--	 EXECUTE sp_executesql @sql ,@paralist,@c_l_price=@c_l_price output,@lpkqty=@lpkqty output
		--  end
		--end
	end
	set @c_l_price=isnull(@c_l_price,0)
	set @lpkqty=isnull(@lpkqty,0)
	set @mdate=isnull(@mdate,'')
	if @c_l_price>0 and @lpkqty>1 set @c_l_price=@c_l_price/@lpkqty

	insert into @t values(@c_l_price,@mdate)
	return  --(@c_l_price,@mdate)


	--select [dbo].[get_lastprice_for_item] ('01','555',0)

end


GO
/****** Object:  UserDefinedFunction [dbo].[get_numbers_from_string]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[get_numbers_from_string]
(@strnum varchar(100)) 
RETURNS varchar(100)
AS
BEGIN 
    declare @string varchar(100)
	set @string=@strnum
	WHILE PATINDEX('%[^0-9]%',@string) <> 0	SET @string = STUFF(@string,PATINDEX('%[^0-9]%',@string),1,'')
	return @string

	--select  [dbo].[get_numbers_from_string] ('SA1234M30')
end

GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetNumeric]    Script Date: 24-02-2025 7:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[udf_GetNumeric]
(
  @strAlphaNumeric VARCHAR(256)
)
RETURNS VARCHAR(256)
AS
BEGIN
  DECLARE @intAlpha INT
  SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)
  BEGIN
    WHILE @intAlpha > 0
    BEGIN
      SET @strAlphaNumeric = STUFF(@strAlphaNumeric, @intAlpha, 1, '' )
      SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric )
    END
  END
  RETURN ISNULL(@strAlphaNumeric,0)
END


GO

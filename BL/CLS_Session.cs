using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO.Ports;
//using System.Windows.Forms;

namespace POS.BL
{
    class CLS_Session
    {
       // public static Form lastfrm;
        public static DataTable dttrtype, dtsalman, dtbranch, dtdescs, dtsalbind, dtemail, dtyrper, dtunits, dtcomp, dtfieldnm;
        public static SerialPort sp;
        public static double tax_per;

        public static string temp_lang = "ar", temp_bcorno = "no";

        public static string sqlserver, sqldb, sqluser, sqluserpass,sqlcontimout;

        public static string lang, sl_prices, cmp_id, cmp_name, cmp_ename, whatsapptokn, whatsappmsg, einv_p2_synctim, minlastlgin, minstart, cmpschem, tax_no, start_dt, end_dt, bkpdir, comp_logo, sysno, sal_nobal, isshamltax, is_dorymost, iscashir, cmp_color, font_t = "Tahoma", font_s = "9", is_bold = "1", fore_color = "#000000", cmp_type, price_type, item_sw, mnu_type, autobaktime, mizansart, rowhight, einv_p2_date, z_certpem, z_keypem, z_secrettxt, daycount = "";
       // public static float font_s;
        public static string contr_id, port_no, cd_msg, sl_no,prnt_type;
        public static string UserID, UserName, UseEdit;
        public static string msg1;
        public static string msg2;
        public static string ownrmob,cust_mob;
        public static string formkey, slkey, pukey, stkey, acckey, trkey, cstkey, brkey, inv_dsc, item_dsc;

        public static string cur;
        public static int scount = 0, rscount = 0, mizanitemlen, mizanpricelen, mizantype, madatype, madaporttyp, madaportno;

        public static bool up_changprice, up_use_dsc, up_ch_itmpr, up_us_post, up_chang_dt, up_edit_othr, up_sal_icost, use_cd, istuch, imp_itm, isopprt = false, showwin, up_editop, up_stopsrch, up_suspend, up_pricelowcost, up_belwbal, islight = false, autobak, iscofi, nocahopen, is_einv = false, whatsappactv, notxchng;
        public static bool chk_qty1=false, is_sal_login = false, autoitemno = false, printrtn, autotax, autoposupdat, issections = false, multi_bc, mustcost, ismain, system_down, is_taqsit = false, mustequal, msrofqid, oneslserial, showqtysrch, edititm, is_einv_p2 = false, is_production = false, allwzerop, einv_p2_syncactv=false;
        public static int posatrtday, up_prtfrm;

        public static string brno, brname, brcash,brstkin,brstkout;

        public static string slno, ctrno;
        //public static string Pass;
        //public static string Type;
        //public static string Poob;

        public string st()
        {
            return "This Function";
        }

      //  public static string cur { get; set; }
    }
}

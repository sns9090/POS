using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace POS
{
    class GetCurUser
    {
        public DataTable GetCurUserF()
        {
            var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");

          //  string userid = lines.First().ToString();
            string userid = lines.Skip(5).First().ToString();
            int uid = Convert.ToInt32(userid);


            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
            param[0].Value = uid;

            //param[1] = new SqlParameter("@PWD", SqlDbType.VarChar, 50);
            //param[1].Value = PWD;

            DataTable Dt = new DataTable();
            Dt = DAL.SelectData("GetCurUser", param);
            DAL.Close();
            return Dt;
        }
    }
}

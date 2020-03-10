using GiaCongThienStore.Database;
using GiaCongThienStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaCongThienStore.Helper
{
    public static class Helper
    {
        public static string GetNewID(string idTable, string lastID)
        {
            var arrStr = lastID.Replace(idTable,"");
            if (IsNumberHelper.IsNumeric(arrStr))
            {
                var _last = Convert.ToInt32(arrStr);
                var id = (Convert.ToInt32(_last) + 1).ToString();
                string temp = "";
                for (int i = 0; i < 10 - idTable.Length - id.Length; i++)
                {
                    temp += "0";
                }
                return idTable + temp + id;
            }
            return null;
        }


        public static bool WriteLog(string message, string code)
        {
            try
            {
                LOGHISTORY log = new LOGHISTORY();
                log.MLOG = Helper.GetNewID("LOG", DataProvider.Ins.DB.LOGHISTORies.ToList()[DataProvider.Ins.DB.LOGHISTORies.Count() - 1].MLOG);
                log.CODE = code;
                log.CONTENT = message; 
                DataProvider.Ins.DB.LOGHISTORies.Add(log);
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            } 
            return true;
        }
    }
}

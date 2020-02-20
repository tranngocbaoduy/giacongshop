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
    }
}

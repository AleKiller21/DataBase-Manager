using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace DataBaseLayer
{
    public static class DBUtilities
    {
        public static DataTable ProjectData(string query)
        {
            try
            {
                var db2Command = new DB2Command(query, Connection.CurrentConnection);
                var result = db2Command.ExecuteReader();
                var data = new DataTable();
                data.Load(result);

                return data;
            }
            catch (Exception e)
            {
                Connection.Disconnect();
                throw;
            }
        }
    }
}

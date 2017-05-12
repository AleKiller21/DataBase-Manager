using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace DataBaseLayer
{
    public static class View
    {
        public static string GenerateDDL(string schema, string name)
        {
            var query = $"SELECT TEXT FROM SYSCAT.VIEWS WHERE VIEWSCHEMA = '{schema}' AND VIEWNAME = '{name}'";
            var reader = new DB2Command(query, Connection.CurrentConnection).ExecuteReader();

            reader.Read();
            var ddl = reader.GetString(0);
            reader.Close();

            return ddl;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace DataBaseLayer
{
    public static class Trigger
    {
        public static string GenerateDDL(string schema, string name)
        {
            var query = $"SELECT TEXT FROM SYSCAT.TRIGGERS WHERE TRIGSCHEMA = '{schema}' AND TRIGNAME = '{name}'";
            var reader = new DB2Command(query, Connection.CurrentConnection).ExecuteReader();

            reader.Read();
            var ddl = reader.GetString(0);

            reader.Close();
            return ddl;
        }

        public static string GenerateCreateTemplate()
        {
            return @"CREATE OR REPLACE TRIGGER <TRIGGER NAME>
<AFTER || BEFORE || INSTEAD OF> <TRIGGER EVENT> ON <TABLE NAME>
<FOR EACH ROW || FOR EACH STATEMENT || REFERENCING>
<TRIGGERED ACTION>";
        }
    }
}

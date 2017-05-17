using System;
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

        public static string GenerateDropDDL(string name)
        {
            return $"DROP VIEW {name}";
        }

        public static DB2DataReader GetData(string schema, string name)
        {
            var ddl = GenerateDDL(schema, name);
            var index = ddl.IndexOf("select", StringComparison.OrdinalIgnoreCase);
            var query = ddl.Substring(index, ddl.Length - index);
            var reader = new DB2Command(query, Connection.CurrentConnection).ExecuteReader();

            return reader;
        }
    }
}

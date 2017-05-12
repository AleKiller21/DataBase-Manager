using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace DataBaseLayer
{
    public static class Index
    {
        public static string GenerateDDL(string schema, string name)
        {
            var query =
                $"SELECT UNIQUERULE FROM SYSCAT.INDEXES WHERE INDSCHEMA = '{schema}' AND INDNAME = '{name}'";

            var result = new DB2Command(query, Connection.CurrentConnection).ExecuteReader();
            result.Read();
            var indexType = result.GetString(0);

            return indexType.Equals("P") ? GeneratePrimaryDDL(schema, name) : GenerateNormalDDL(schema, name);
        }

        public static string GenerateDropDDL(string schema, string name)
        {
            return $"DROP INDEX {schema}.{name}";
        }

        private static string GenerateNormalDDL(string schema, string name)
        {
            var indexQuery =
                $"SELECT TABNAME, UNIQUERULE FROM SYSCAT.INDEXES WHERE INDSCHEMA = '{schema}' AND INDNAME = '{name}'";
            var indexColQuery =
                $"SELECT COLNAME FROM SYSCAT.INDEXCOLUSE WHERE INDSCHEMA = '{schema}' AND INDNAME = '{name}'";

            var ddl = "";
            var indexReader = new DB2Command(indexQuery, Connection.CurrentConnection).ExecuteReader();
            var indexColReader = new DB2Command(indexColQuery, Connection.CurrentConnection).ExecuteReader();

            indexReader.Read();
            var indexTableName = indexReader.GetString(0);
            var indexUniqueRule = indexReader.GetString(1);
            indexReader.Close();

            ddl = indexUniqueRule.Equals("U") ? 
                $"CREATE UNIQUE INDEX {name} ON {indexTableName} (" : $"CREATE INDEX {name} ON {indexTableName} (";

            while (indexColReader.Read())
            {
                ddl += indexColReader.GetString(0) + ", ";
            }

            ddl = ddl.Substring(0, ddl.Length - 2);
            ddl += ");";

            return ddl;
        }

        private static string GeneratePrimaryDDL(string schema, string name)
        {
            var queryColname = $"SELECT COLNAME FROM SYSCAT.INDEXCOLUSE WHERE INDSCHEMA = '{schema}' AND INDNAME = '{name}'";
            var queryTabname = $"SELECT TABNAME FROM SYSCAT.INDEXES WHERE INDNAME = '{name}'";
            var colNameReader = new DB2Command(queryColname, Connection.CurrentConnection).ExecuteReader();
            var tabNameReader = new DB2Command(queryTabname, Connection.CurrentConnection).ExecuteReader();

            tabNameReader.Read();

            var tableName = tabNameReader.GetString(0);
            var ddl = $"ALTER TABLE {tableName}\nADD CONSTRAINT {name} PRIMARY KEY ";

            while (colNameReader.Read())
            {
                ddl += $"( {colNameReader["COLNAME"]}, ";
            }

            ddl = ddl.Substring(0, ddl.Length - 2);
            ddl += ");";

            return ddl;
        }
    }
}

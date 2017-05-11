﻿using System;
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
            try
            {
                var query =
                    $"SELECT UNIQUERULE FROM SYSCAT.INDEXES WHERE INDSCHEMA = '{schema}' AND INDNAME = '{name}'";

                var result = new DB2Command(query, Connection.CurrentConnection).ExecuteReader();
                result.Read();
                var indexType = result.GetString(0);

                return indexType.Equals("P") ? GeneratePrimaryDDL(schema, name) : GenerateNormalDDL(schema, name);
            }
            catch (Exception e)
            {
                Connection.Disconnect();
                throw;
            }
        }

        private static string GenerateNormalDDL(string schema, string name)
        {
            throw new NotImplementedException();
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

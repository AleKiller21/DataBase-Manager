﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace DataBaseLayer
{
    public class PrimaryKey : Constraints
    {
        private readonly string _schema;
        private readonly string _name;
        private readonly string _table;

        public PrimaryKey(string schema, string name, string table)
        {
            _schema = schema;
            _name = name;
            _table = table;
        }

        public override string GenerateDDL()
        {
            return Index.GenerateDDL(_schema, _name);
        }

        public override string GenerateDropDDL()
        {
            var query = $"SELECT CONSTNAME, TABNAME FROM SYSCAT.REFERENCES WHERE REFKEYNAME = '{_name}'";
            var reader = new DB2Command(query, Connection.CurrentConnection).ExecuteReader();
            var ddl = "";

            while (reader.Read())
            {
                ddl += $"ALTER TABLE {reader["TABNAME"]} DROP FOREIGN KEY {reader["CONSTNAME"]};\n";
            }

            ddl += $"ALTER TABLE {_table} DROP PRIMARY KEY;";

            reader.Close();
            return ddl;

        }

        public override string GenerateAlterTemplate()
        {
            throw new NotImplementedException();
        }

        public static string GenerateCreateTemplate()
        {
            return "ALTER TABLE <TABLE_NAME>\nADD CONSTRAINT <NAME> PRIMARY KEY (COLUMN);";
        }
    }
}

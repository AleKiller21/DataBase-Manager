using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace DataBaseLayer
{
    public class Check : Constraints
    {
        private readonly string _schema;
        private readonly string _name;
        private readonly string _table;

        public Check(string schema, string name, string table)
        {
            _schema = schema;
            _name = name;
            _table = table;
        }

        public override string GenerateDDL()
        {
            var query =
                $"SELECT TABNAME, CONSTNAME, TEXT FROM SYSCAT.CHECKS WHERE TABSCHEMA = '{_schema}' AND CONSTNAME = '{_name}'";

            var reader = new DB2Command(query, Connection.CurrentConnection).ExecuteReader();

            reader.Read();
            var ddl = $"ALTER TABLE {reader["TABNAME"]}\nADD CONSTRAINT {reader["CONSTNAME"]} CHECK ({reader["TEXT"]});";

            reader.Close();
            return ddl;
        }

        public override string GenerateDropDDL()
        {
            return $"ALTER TABLE {_table} DROP CHECK {_name};";
        }

        public override string GenerateAlterTemplate()
        {
            return $"{GenerateDropDDL()}\n{GenerateDDL()}";
        }

        public static string GenerateCreateTemplate()
        {
            return "ALTER TABLE <TABLE_NAME>\nADD CONSTRAINT <NAME> CHECK (<PREDICATE>);";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace DataBaseLayer
{
    public class ForeignKey : Constraints
    {
        private readonly string _schema;
        private readonly string _name;
        private readonly string _table;

        public ForeignKey(string schema, string name, string table)
        {
            _schema = schema;
            _name = name;
            _table = table;
        }

        public override string GenerateDDL()
        {
            var queryRefTable =
                $"SELECT REFTABNAME FROM SYSCAT.REFERENCES WHERE TABSCHEMA = '{_schema}' AND CONSTNAME = '{_name}'";

            var queryRefKey =
                $"SELECT REFKEYNAME FROM SYSCAT.REFERENCES WHERE TABSCHEMA = '{_schema}' AND CONSTNAME = '{_name}'";

            var queryColName = $"SELECT COLNAME FROM SYSCAT.KEYCOLUSE WHERE CONSTNAME = '{_name}'";

            var refTableReader = new DB2Command(queryRefTable, Connection.CurrentConnection).ExecuteReader();
            var refKeyReader = new DB2Command(queryRefKey, Connection.CurrentConnection).ExecuteReader();
            var colNameReader = new DB2Command(queryColName, Connection.CurrentConnection).ExecuteReader();

            refTableReader.Read();
            refKeyReader.Read();

            var refColNameQuery = $"SELECT COLNAME FROM SYSCAT.KEYCOLUSE WHERE CONSTNAME = '{refKeyReader.GetString(0)}'";
            var refColReader = new DB2Command(refColNameQuery, Connection.CurrentConnection).ExecuteReader();

            var fkDll = $"ALTER TABLE {_table}\nADD CONSTRAINT {_name} FOREIGN KEY(";

            while (colNameReader.Read())
            {
                fkDll += colNameReader["COLNAME"] + ", ";
            }

            fkDll = fkDll.Substring(0, fkDll.Length - 2);
            fkDll += $") REFERENCES {refTableReader.GetString(0)} (";

            while (refColReader.Read())
            {
                fkDll += refColReader["COLNAME"] + ", ";
            }

            fkDll = fkDll.Substring(0, fkDll.Length - 2);
            fkDll += ");";

            refTableReader.Close();
            refKeyReader.Close();
            refColReader.Close();
            colNameReader.Close();

            return fkDll;
        }

        public override string GenerateDropDDL()
        {
            return $"ALTER TABLE {_table} DROP FOREIGN KEY {_name};";
        }

        public override string GenerateAlterTemplate()
        {
            throw new NotImplementedException();
        }

        public static string GenerateCreateTemplate()
        {
            return "ALTER TABLE <TABLE_NAME>\nADD CONSTRAINT <NAME> FOREIGN KEY (<TABLE_COLUMN>) REFERENCES <REFERENCED TABLE>(<REFERENCED TABLE COLUMN>);";
        }
    }
}

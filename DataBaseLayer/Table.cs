using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2;

namespace DataBaseLayer
{
    public static class Table
    {
        public static string GenerateDDL(string tableName, string tableSchema)
        {
            var columnsProjectionCommand =
                $"SELECT * FROM SYSCAT.COLUMNS WHERE TABNAME = '{tableName}' AND TABSCHEMA = '{tableSchema}'";

            var columnsProjection = new DB2Command(columnsProjectionCommand, Connection.CurrentConnection);
            var dataReader = columnsProjection.ExecuteReader();
            var tableDDL = $"CREATE TABLE {tableName} (\n";

            while (dataReader.Read())
            {
                tableDDL += dataReader["COLNAME"] as string + " " + dataReader["TYPENAME"];
                if (dataReader["STRINGUNITSLENGTH"] != null) tableDDL += "(" + dataReader["LENGTH"] + ")";
                if (dataReader["NULLS"] as string == "N") tableDDL += " NOT NULL,\n";
                else tableDDL += ",\n";
            }

            var primaryKeyCommand = $"SELECT COLNAMES FROM SYSCAT.INDEXES WHERE TABSCHEMA = '{tableSchema}'" +
                                    $"AND TABNAME = '{tableName}' AND UNIQUERULE = 'P'";

            var primaryKeyReader = new DB2Command(primaryKeyCommand, Connection.CurrentConnection).ExecuteReader();

            primaryKeyReader.Read();
            if (primaryKeyReader.HasRows)
            {
                var key = primaryKeyReader.GetString(0).Substring(1);
                tableDDL += "PRIMARY KEY ( " + key + " )\n";
            }

            tableDDL += ")\n" + "ORGANIZE BY ROW;";

            //TODO Show the foreign key too
            return tableDDL;
        }
    }
}
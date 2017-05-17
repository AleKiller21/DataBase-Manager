using IBM.Data.DB2;

namespace DataBaseLayer
{
    public static class Routine
    {
        public static string GenerateDDL(string schema, string name)
        {
            var query =
                $"SELECT TEXT FROM SYSCAT.ROUTINES WHERE ROUTINESCHEMA = '{schema}' AND ROUTINENAME = '{name}'";

            var reader = new DB2Command(query, Connection.CurrentConnection).ExecuteReader();
            reader.Read();

            var ddl = reader.GetString(0);
            reader.Close();
            return ddl;
        }

        public static string GenerateDropDDL(string name, string type, string schema)
        {
            return type.Equals("F") ? $"DROP FUNCTION {schema.Trim()}.{name}" : $"DROP PROCEDURE {schema.Trim()}.{name}";
        }
    }
}

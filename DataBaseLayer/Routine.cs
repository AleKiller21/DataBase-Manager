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

            return reader.GetString(0);
        }

        public static string GenerateDropDDL(string name, string type)
        {
            return type.Equals("F") ? $"DROP FUNCTION {name}" : $"DROP PROCEDURE {name}";
        }
    }
}

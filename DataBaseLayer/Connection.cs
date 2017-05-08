using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace DataBaseLayer
{
    public static class Connection
    {
        public static void CreateConnection(string databaseName, string host, string username, string password, string port)
        {
            var conn = new SqlConnectionStringBuilder
            {
                DataSource = $"Server={host}:{port};Database={databaseName};UID={username};PWD={password};",
                InitialCatalog = databaseName,
                UserID = username,
                Password = password
            };

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connString = config.ConnectionStrings.ConnectionStrings[host];

            if (connString == null)
            {
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings
                {
                    Name = host,
                    ConnectionString = conn.ConnectionString
                });
            }
            else
            {
                var currentConn = new SqlConnectionStringBuilder(connString.ConnectionString);
                if (databaseName == currentConn.InitialCatalog && username == currentConn.UserID)
                    throw new ConnectionException("A connection with same credentials and host already exist.");

                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings
                {
                    Name = host,
                    ConnectionString = conn.ConnectionString
                });
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");

            CreateDatabase(conn.ConnectionString, databaseName);
        }

        private static void CreateDatabase(string connString, string databaseName)
        {
            if (Directory.Exists($"C:\\DB2\\NODE0000\\{databaseName}")) return;

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "db2cmd.exe";
            startInfo.UseShellExecute = false;
            startInfo.Arguments = $"/C \"db2 CREATE DATABASE {databaseName} AUTOMATIC STORAGE YES ON \'C:\\\' DBPATH ON \'C:\\\'\"";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}

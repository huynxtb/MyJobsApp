using System;
using System.Linq;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using SMO = Microsoft.SqlServer.Management.Smo;

namespace MyJobsApp.Helper
{
    public class SqlBackupHelper
    {
        public static string SaveFileBackUpToServer(string path, string connectionString, string databaseName)
        {
            var schemaName = "dbo";
            var fileName = StringHelper.UniqueSqlBackupFileName(databaseName);
            var fileNamePath = path + fileName;

            if (File.Exists(fileNamePath))
                File.Delete(fileNamePath);

            var server = new SMO.Server(new ServerConnection(new SqlConnection(connectionString)));
            var options = new SMO.ScriptingOptions();
            var databases = server.Databases[databaseName];

            options.FileName = fileNamePath;
            options.ScriptDrops = false;
            options.AppendToFile = true;
            options.ScriptSchema = true;
            options.ScriptData = true;
            options.Indexes = true;

            var tableEnum = databases.Tables.Cast<SMO.Table>().Where(i => i.Schema == schemaName);
            var viewEnum = databases.Views.Cast<SMO.View>().Where(i => i.Schema == schemaName);
            var procedureEnum = databases.StoredProcedures.Cast<SMO.StoredProcedure>()
                .Where(i => i.Schema == schemaName);

            Console.WriteLine("SQL Script Generator");

            Console.WriteLine("\nTable Scripts:");
            foreach (SMO.Table table in tableEnum)
            {
                databases.Tables[table.Name, schemaName].EnumScript(options);
                Console.WriteLine(table.Name);
            }

            options.ScriptData = false;
            options.WithDependencies = false;


            Console.WriteLine("\nView Scripts:");
            foreach (SMO.View view in viewEnum)
            {
                databases.Views[view.Name, schemaName].Script(options);
                Console.WriteLine(view.Name);
            }

            Console.WriteLine("\nStored Procedure Scripts:");
            foreach (SMO.StoredProcedure procedure in procedureEnum)
            {
                databases.StoredProcedures[procedure.Name, schemaName].Script(options);
                Console.WriteLine(procedure.Name);
            }

            return fileName;
        }
    }
}
namespace MS.Database
{
    using DbUp;
    using DbUp.ScriptProviders;
    using System;
    using System.IO;
    class Program
    {
        protected static NLog.Logger _nlog = NLog.LogManager.GetCurrentClassLogger();
        static int Main()
        {
            var connectionString = "Server=DESKTOP-I055E4A\\SQL;Database=test;User ID=sa;Password=adminadmin";
            var scriptsPath = "Scripts";
            Console.WriteLine("Start executing migration scripts");
            _nlog.Trace("Start executing migration scripts");
            var migrationScriptsPath = Path.Combine(scriptsPath, "Migrations");
            if (Directory.Exists(migrationScriptsPath))
            {
                var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(migrationScriptsPath, new FileSystemScriptOptions
                    {
                        IncludeSubDirectories = true
                    })
                    .LogToConsole()
                    .WithExecutionTimeout(TimeSpan.FromMinutes(20))
                    .JournalToSqlTable("migra", "MigrationsJournal")
                    .Build();
                var result = upgrader.PerformUpgrade();
                if (!result.Successful)
                {
                    Console.ReadKey();
                    return ReturnError(result.Error.ToString());
                }
                ShowSuccess();
            }
            else
            {
                Console.WriteLine("Migrations path not found");
                _nlog.Trace("Migrations path not found for");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("DONE!");
            _nlog.Trace("DONE");
            Console.ResetColor();
            Console.ReadKey();
            return 0;
        }
        private static void ShowSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            _nlog.Trace("Success!");
            Console.ResetColor();
        }
        private static int ReturnError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            _nlog.Error(error);
            Console.ResetColor();
            return -1;
        }
    }
}
﻿using DbUp;
using System.Reflection;

namespace SalaryManagement.Infrastructure.Migrations;

public class DbInitializer
{
    public static void Initialize(string connectionString)
    {
        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .WithTransaction()
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine(result.Error);
        }

        Console.WriteLine("Success upgrading database");
    }
}

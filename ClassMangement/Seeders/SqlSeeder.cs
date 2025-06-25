using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Mock;

namespace ClassMangement.Seeders
{
    public static class SqlSeeder
    {
        public static void RunSqlFromFile(Database context, string relativePath)
        {
            // יוצר נתיב מוחלט מהנתיב היחסי (תיקיית bin היא נקודת ההתחלה)
            var fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"SQL file not found: {fullPath}");

            var sql = File.ReadAllText(fullPath);
            context.Database.ExecuteSqlRaw(sql);
        }
    }


}


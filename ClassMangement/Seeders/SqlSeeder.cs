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

			context.Database.OpenConnection();
			try
			{
				context.Database.ExecuteSqlRaw(sql);
			}
			finally
			{
				context.Database.CloseConnection(); // תמיד ייסגר, גם אם נזרקה חריגה
			}
			//context.Database.ExecuteSqlRaw(sql);

			//context.Database.CloseConnection();
		}

        public static void CheckSeederWorks(Database context, string relativePath)
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);
            Console.WriteLine($"📍 [CheckSeederWorks] Checking file path: {fullPath}");

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("❌ SQL file not found.");
                return;
            }

            try
            {
                var sql = File.ReadAllText(fullPath);
                Console.WriteLine($"📄 SQL Content Preview (first 300 chars): {sql.Substring(0, Math.Min(300, sql.Length))}");

                using var transaction = context.Database.BeginTransaction();
                context.Database.ExecuteSqlRaw(sql);
                transaction.Rollback();

                Console.WriteLine("✅ SQL executed successfully (in test mode with rollback).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Exception occurred while executing SQL:");
                Console.WriteLine(ex.Message);
            }
        }

    }


}


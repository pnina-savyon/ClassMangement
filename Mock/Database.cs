using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;

namespace Mock
{
    public class Database : DbContext, IContext
    {
        public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<DailyAttendance> DailyAttendances { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ירושה של Student ו-Teacher מ-User
            //modelBuilder.Entity<User>()
            //    .HasDiscriminator<string>("UserType")
            //    .HasValue<Student>("Student")
            //    .HasValue<Teacher>("Teacher");
            modelBuilder.Entity<User>()
    .ToTable("Users")  // ← הנה ההוספה המומלצת
    .HasDiscriminator<string>("UserType")
    .HasValue<Student>("Student")
    .HasValue<Teacher>("Teacher");


            // הגדרת הקשרים בין תלמיד לכיתה 
            // מניעת מחיקת שרשרת (Cascade) בין Student ל-Class
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students) // אם Class כולל ICollection<Student>
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict); // או DeleteBehavior.NoAction

            // הגדרת הקשרים בין תלמיד לכיסא
            // מניעת מחיקת שרשרת עם Chair
            modelBuilder.Entity<Student>()
                .HasOne(s => s.CurrentChair)
                .WithMany()
                .HasForeignKey(s => s.ChairId)
                .OnDelete(DeleteBehavior.Restrict);

            // יצירת טבלאות בשביל למפות את הקשרים בין תלמיד לחברים
            modelBuilder.Entity<Student>()
                .HasMany(s => s.FavoriteFriends)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
            "StudentFavoriteFriends",
            j => j.HasOne<Student>()
                  .WithMany()
                  .HasForeignKey("FriendId")
                  .OnDelete(DeleteBehavior.Restrict),
            j => j.HasOne<Student>()
                  .WithMany()
                  .HasForeignKey("StudentId")
                  .OnDelete(DeleteBehavior.Cascade)
        );

            // קשר רבים-לרבים בין תלמידים לחברים לא מועדפים
            modelBuilder.Entity<Student>()
                .HasMany(s => s.NonFavoriteFriends)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "StudentNonFavoriteFriends",
                    j => j.HasOne<Student>()
                          .WithMany()
                          .HasForeignKey("NonFriendId")
                          .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<Student>()
                          .WithMany()
                          .HasForeignKey("StudentId")
                          .OnDelete(DeleteBehavior.Cascade)
                );


            // הגדרת הקשר בין כיתה למורה
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // הגדרת הקשר בין כיסא לכיתה 
            modelBuilder.Entity<Chair>()
                .HasOne(ch => ch.Class)
                .WithMany(c => c.Chairs)
                .HasForeignKey(ch => ch.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // יצרירת מחלקה למפות את הקשר בין כיסא לכיסאות שכנים
            //modelBuilder.Entity<Chair>()
            //    .HasMany(c => c.NearbyChairs)
            //    .WithMany()
            //    .UsingEntity<Dictionary<string, object>>(
            //        "ChairNearbyChairs",
            //        j => j.HasOne<Chair>()
            //              .WithMany()
            //              .HasForeignKey("NearbyChairId")
            //              .OnDelete(DeleteBehavior.Restrict), // לא למחוק את השכן בטעות
            //        j => j.HasOne<Chair>()
            //              .WithMany()
            //              .HasForeignKey("ChairId")
            //              .OnDelete(DeleteBehavior.Cascade) // כשמוחקים את הכיסא – מוחקים את הקשר בלבד
            //    );

            modelBuilder.Entity<Chair>()
                .HasMany(c => c.NearbyChairs)
                .WithMany(c => c.NearbyOfChairs) // הצד ההפוך
                .UsingEntity<Dictionary<string, object>>(
                    "ChairNearbyChairs",
                    j => j.HasOne<Chair>()
                          .WithMany()
                          .HasForeignKey("NearbyChairId")
                          .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<Chair>()
                          .WithMany()
                          .HasForeignKey("ChairId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.ToTable("ChairNearbyChairs")
                );
        }

        public async Task Save()
        {
            await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=WX1097573;Database=ClassManagementDB3;trusted_connection=true;TrustServerCertificate=true");
        }
        //WX1097573
        //DESKTOP-1VUANBN
    }
}

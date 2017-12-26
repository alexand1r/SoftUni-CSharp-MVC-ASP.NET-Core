namespace LearningSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using LearningSystem.Data.Models;

    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Course> Courses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UsersCourses>()
                .HasKey(uc => new {uc.UserId, uc.CourseId});

            builder.Entity<UsersCourses>()
                .HasOne<User>(uc => uc.User)
                .WithMany(uc => uc.Courses)
                .HasForeignKey(uc => uc.UserId);

            builder.Entity<UsersCourses>()
                .HasOne<Course>(uc => uc.Course)
                .WithMany(uc => uc.Users)
                .HasForeignKey(uc => uc.CourseId);

            builder.Entity<Article>()
                .HasOne<User>(a => a.Author)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.AuthorId);

            builder.Entity<Course>()
                .HasOne<User>(c => c.Trainer)
                .WithMany(u => u.Trainings)
                .HasForeignKey(c => c.TrainerId);

            base.OnModelCreating(builder);
        }
    }
}

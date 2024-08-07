using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure;

public class SchoolManagementDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurations spécifiques pour les relations many-to-many

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Roles)
            .WithMany(r => r.Persons)
            .UsingEntity(j => j.ToTable("PersonRole"));

        modelBuilder.Entity<Person>()
            .HasMany(p => p.StudentGroups)
            .WithMany(g => g.Students)
            .UsingEntity(j => j.ToTable("StudentGroup"));

        modelBuilder.Entity<Person>()
            .HasMany(p => p.TeacherClassrooms)
            .WithMany(c => c.Teachers)
            .UsingEntity(j => j.ToTable("TeacherClassroom"));

        modelBuilder.Entity<Lesson>()
            .HasMany(l => l.Teachers)
            .WithMany(t => t.TeacherLessons)
            .UsingEntity(j => j.ToTable("TeacherLesson"));

        modelBuilder.Entity<Lesson>()
            .HasMany(l => l.Groups)
            .WithMany(g => g.Lessons)
            .UsingEntity(j => j.ToTable("LessonGroup"));

        modelBuilder.Entity<Group>()
            .HasOne(g => g.Classroom)
            .WithMany(c => c.Groups)
            .HasForeignKey(g => g.ClassroomId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}

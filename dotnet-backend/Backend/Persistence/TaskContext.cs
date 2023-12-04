using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence;

public partial class TaskContext : DbContext
{
    public TaskContext()
    {
    }

    public TaskContext(DbContextOptions<TaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=taskdb.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Priority>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.HasData(
                new Priority { Id = 1, Name = "Low" },
                new Priority { Id = 2, Name = "Medium" },
                new Priority { Id = 3, Name = "High" });
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.HasData(
                new Type { Id = 1, Name = "Personal" },
                new Type { Id = 2, Name = "Work" });
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DueDate);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDate);

            entity.HasOne(d => d.FkPriority).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.FkPriorityId);

            entity.HasOne(d => d.FkType).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.FkTypeId);

            entity.HasData(
               new Task
               {
                   Id = 1,
                   Name = "Small",
                   Description = "job a",
                   StartDate = DateTime.UtcNow,
                   DueDate = DateTime.UtcNow,
                   FkPriorityId = 1,
                   FkTypeId = 1,
               });
        });
    }
}

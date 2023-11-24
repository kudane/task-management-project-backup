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

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("DemoDB");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Priority>(entity =>
        {
            //entity.ToTable("Priority");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            //entity.ToTable("Size");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            //entity.ToTable("Task");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DueDate);//.HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Startdate);//.HasColumnType("datetime");

            entity.HasOne(d => d.FkPriority).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.FkPriorityId);
            //.HasConstraintName("FK_Task_Priority");

            entity.HasOne(d => d.FkSize).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.FkSizeId);
                //.HasConstraintName("FK_Task_Size");
        });

        Seeding.AddData(modelBuilder);
    }
}

using Microsoft.EntityFrameworkCore;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    } 

    // DbSets representing tables in the database
    public DbSet<User> Users { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    public DbSet<ExerciseEquipment> ExerciseEquipments { get; set; } // Add this

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExerciseEquipment>()
            .HasKey(ee => new { ee.ExerciseId, ee.EquipmentId });

        modelBuilder.Entity<ExerciseEquipment>()
            .HasOne(ee => ee.Exercise)
            .WithMany(e => e.ExerciseEquipments)
            .HasForeignKey(ee => ee.ExerciseId);

        modelBuilder.Entity<ExerciseEquipment>()
            .HasOne(ee => ee.Equipment)
            .WithMany(e => e.ExerciseEquipments)
            .HasForeignKey(ee => ee.EquipmentId);

        modelBuilder.Entity<WorkoutExercise>()
            .HasKey(we => new { we.WorkoutId, we.ExerciseId });

        modelBuilder.Entity<WorkoutExercise>()
            .HasOne(we => we.Workout)
            .WithMany(w => w.WorkoutExercises)
            .HasForeignKey(we => we.WorkoutId);

        modelBuilder.Entity<WorkoutExercise>()
            .HasOne(we => we.Exercise)
            .WithMany(e => e.WorkoutExercises)
            .HasForeignKey(we => we.ExerciseId);
    }
}
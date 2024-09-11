using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WorkoutTracker_api.DBContext;
using System.Collections.Generic;

using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext
{
    public  class Seed
{
    public static void SeedData(DataContext context)
{
    context.Database.EnsureCreated();

    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User { Id = 1, Name = "John Doe", Email = "john@example.com", Password = "hashed_password_1" },
            new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Password = "hashed_password_2" }
        );
        context.SaveChanges();
    }

    if (!context.Equipments.Any())
    {
        context.Equipments.AddRange(
            new Equipment { Id = 1, Name = "Dumbbell", Description = "Hand-held weights" },
            new Equipment { Id = 2, Name = "Barbell", Description = "Long bar for weightlifting" },
            new Equipment { Id = 3, Name = "Treadmill", Description = "Running machine" }
        );
        context.SaveChanges();
    }

    if (!context.Exercises.Any())
    {
        context.Exercises.AddRange(
            new Exercise { Id = 1, Name = "Bench Press", Description = "Chest exercise using a barbell", PrimaryMuscleGroup = "Chest" },
            new Exercise { Id = 2, Name = "Squat", Description = "Leg exercise using a barbell", PrimaryMuscleGroup = "Legs" },
            new Exercise { Id = 3, Name = "Bicep Curl", Description = "Arm exercise using dumbbells", PrimaryMuscleGroup = "Arms" }
        );
        context.SaveChanges();
    }

    // Seed ExerciseEquipment data - Linking Exercises with Equipments
    if (!context.ExerciseEquipments.Any())
    {
        context.ExerciseEquipments.AddRange(
            new ExerciseEquipment { ExerciseId = 1, EquipmentId = 2 }, // Bench Press uses Barbell
            new ExerciseEquipment { ExerciseId = 2, EquipmentId = 2 }, // Squat uses Barbell
            new ExerciseEquipment { ExerciseId = 3, EquipmentId = 1 }  // Bicep Curl uses Dumbbell
        );
        context.SaveChanges();
    }

    if (!context.Workouts.Any())
    {
        context.Workouts.AddRange(
            new Workout { Id = 1, UserId = 1, Date = DateTime.Now, Name = "Upper Body Strength", Notes = "Focus on chest and arms" },
            new Workout { Id = 2, UserId = 2, Date = DateTime.Now, Name = "Leg Day", Notes = "Heavy squats today" }
        );
        context.SaveChanges();
    }

    if (!context.WorkoutExercises.Any())
    {
        context.WorkoutExercises.AddRange(
            new WorkoutExercise { Id = 1, WorkoutId = 1, ExerciseId = 1, Sets = 4, Reps = 8, Weight = 80, RestTimeSeconds = 90 },
            new WorkoutExercise { Id = 2, WorkoutId = 1, ExerciseId = 3, Sets = 3, Reps = 10, Weight = 15, RestTimeSeconds = 60 },
            new WorkoutExercise { Id = 3, WorkoutId = 2, ExerciseId = 2, Sets = 5, Reps = 5, Weight = 100, RestTimeSeconds = 120 }
        );
        context.SaveChanges();
    }
}

}

}


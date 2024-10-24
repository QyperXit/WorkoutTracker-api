using Microsoft.EntityFrameworkCore;
using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.DBContext.Interfaces;
using WorkoutTracker_api.DBContext.Mapper;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext.Repository;

public class WorkoutRepository : IWorkoutRepository
{
    private readonly DataContext _context;

    public WorkoutRepository(DataContext _context)
    {
        this._context = _context;
        _context = _context;
    }

    // Get workouts for a specific user
    public IEnumerable<WorkoutDto> GetWorkoutsByUserId(int userId)
    {
        if (_context == null)
        {
            throw new InvalidOperationException("DbContext is not initialized.");
        }

        return _context.Workouts
            .Where(w => w.UserId == userId)  // Filter by user ID
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise)
            .Select(w => WorkoutMapper.ToDto(w))
            .ToList();
    }
    public IEnumerable<WorkoutDto> GetWorkouts()
    {
        if (_context == null)
        {
            throw new InvalidOperationException("DbContext is not initialized.");
        }
        //
        return _context.Workouts
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise) 
            .Select(w => WorkoutMapper.ToDto(w))
            .ToList();
    }

    public Workout GetWorkout(int id)
    {
        return _context.Workouts
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise)
            .FirstOrDefault(w => w.Id == id);
    }

    public Workout CreateWorkout(Workout workout)
    {
        // Ensure the workout has a UserId set
        if (workout.UserId <= 0)
        {
            throw new ArgumentException("UserId must be set when creating a workout");
        }

        _context.Workouts.Add(workout);
        SaveChanges();
        return workout;
    }

    public bool UpdateWorkout(WorkoutUpdateDto workoutUpdateDto)
    {
        var existingWorkout = _context.Workouts
            .Include(w => w.WorkoutExercises)
            .FirstOrDefault(w => w.Id == workoutUpdateDto.Id && w.UserId == workoutUpdateDto.UserId);

        if (existingWorkout == null)
        {
            return false;  // Workout not found
        }

        // Use the mapper to update the existing workout entity with new data from the DTO
        WorkoutMapper.UpdateModelFromDto(workoutUpdateDto, existingWorkout);

        // Save changes and return true if any changes were successfully saved
        return SaveChanges();
    }

    public bool DeleteWorkout(int id, int userId)
    {
        var workout = _context.Workouts
            .Include(w => w.WorkoutExercises)
            .FirstOrDefault(w => w.Id == id && w.UserId == userId);

        if (workout == null)
        {
            return false; // Workout not found or doesn't belong to user
        }

        _context.Workouts.Remove(workout);
        _context.WorkoutExercises.RemoveRange(workout.WorkoutExercises);

        return SaveChanges();
    }
    

    public bool WorkoutExists(int id)
    {
        return _context.Workouts.Any(w => w.Id == id);
    }

    public bool WorkoutExistsForUser(int id, int userId)
    {
        return _context.Workouts
            .Any(w => w.Id == id && w.UserId == userId);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }
}
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

    public IEnumerable<WorkoutDto> GetWorkouts()
    {
        if (_context == null)
        {
            throw new InvalidOperationException("DbContext is not initialized.");
        }
        //
        return _context.Workouts
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise) // Load Exercise for WorkoutExercises
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
        _context.Workouts.Add(workout);
        SaveChanges();
        return workout;
    }

    public bool UpdateWorkout(WorkoutUpdateDto workoutUpdateDto)
    {
        var existingWorkout = _context.Workouts
            .Include(w => w.WorkoutExercises)
            .FirstOrDefault(w => w.Id == workoutUpdateDto.Id);

        if (existingWorkout == null)
        {
            return false;  // Workout not found
        }

        // Use the mapper to update the existing workout entity with new data from the DTO
        WorkoutMapper.UpdateModelFromDto(workoutUpdateDto, existingWorkout);

        // Save changes and return true if any changes were successfully saved
        return SaveChanges();
    }

    public bool DeleteWorkout(Workout workout)
    {
        throw new NotImplementedException();
    }

    public bool WorkoutExists(int id)
    {
        return _context.Workouts.Any(w => w.Id == id);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }
}
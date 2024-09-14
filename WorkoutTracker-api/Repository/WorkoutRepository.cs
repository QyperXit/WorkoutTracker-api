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
        throw new NotImplementedException();
    }

    public bool CreateWorkout(Workout workout)
    {
        throw new NotImplementedException();
    }

    public bool UpdateWorkout(Workout workout)
    {
        throw new NotImplementedException();
    }

    public bool DeleteWorkout(Workout workout)
    {
        throw new NotImplementedException();
    }

    public bool WorkoutExists(int id)
    {
        throw new NotImplementedException();
    }

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }
}
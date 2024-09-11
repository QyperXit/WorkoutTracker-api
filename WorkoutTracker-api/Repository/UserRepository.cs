using WorkoutTracker_api.DBContext.Interfaces;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext _context)
    {
        this._context = _context;
        _context = _context;
    }

    public IEnumerable<User> GetUsers()
    {
        return _context.Users.OrderBy(u => u.Id).ToList();
    }

    public User GetUserById(int id)
    {
        return _context.Users.Where(u => u.Id == id).FirstOrDefault();
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.Where(u => u.Email == email).FirstOrDefault();
    }

    public bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }

    public bool UserExists(string email)
    {
        return _context.Users.Any(e => e.Email == email);
    }
}
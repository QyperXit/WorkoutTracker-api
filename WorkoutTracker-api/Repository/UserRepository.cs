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

    public bool CreateUser(User user)
    {
        
        // Repository-level check for email uniqueness
        if (UserExists(user.Email))
        {
            return false; // Or throw an exception if preferred
        }
        
        _context.Users.Add(user);
        return SaveChanges();
    }

    public bool UpdateUser(User user)
    {
        _context.Users.Update(user);
        return SaveChanges();
    }

    public bool DeleteUser(int id)
    {
        var user = GetUserById(id);
        if (user == null)
        {
            return false;
        }
        
        _context.Users.Remove(GetUserById(id));
        return SaveChanges();
    }

    public bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }

    public bool UserExists(string email)
    {
        return _context.Users.Any(e => e.Email == email);
    }

    public bool SaveChanges()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
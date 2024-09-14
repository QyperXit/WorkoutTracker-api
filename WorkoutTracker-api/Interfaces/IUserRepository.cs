using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetUsers();
    User GetUserById(int id);
    User GetUserByEmail(string email);
    bool CreateUser(User user);
    bool UpdateUser(User user);
    bool DeleteUser(int id);
    bool UserExists(int id);
    bool UserExists(string email);
    bool SaveChanges();

}
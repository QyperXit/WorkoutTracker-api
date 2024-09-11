using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetUsers();
    User GetUserById(int id);
    User GetUserByEmail(string email);
    // void CreateUser(User user);
    // void UpdateUser(User user);
    // void DeleteUser(int id);
    bool UserExists(int id);
    bool UserExists(string email);
    // bool SaveChanges();

}
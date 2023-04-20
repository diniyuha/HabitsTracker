using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<int> CreateUser(User user);
        Task DeleteUser(int id);
        Task UpdateUser(int id, User user);
    }
}
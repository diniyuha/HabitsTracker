using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(Guid id);
        User GetAuthUser(string email, string password);
        Task<Guid> CreateUser(User user);
        Task DeleteUser(Guid id);
        Task UpdateUser(Guid id, User user);
    }
}
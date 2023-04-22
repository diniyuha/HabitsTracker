using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IUserService
    {
        User GetUserById(Guid id);
        User GetAuthUser(string email, string password);
        Guid CreateUser(string email, string password);
        void DeleteUser(Guid id);
        void UpdateUser(Guid id, User user);
        bool CheckEmailForMatches(string email);
        Task SendConfirmationEmailAsync(string email);
    }
}
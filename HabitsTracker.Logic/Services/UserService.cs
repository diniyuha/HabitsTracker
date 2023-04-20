using System;
using System.Threading.Tasks;
using AutoMapper;
using HabitsTracker.Data;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public class UserService: IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        
        //TODO
        public UserService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        //TODO
        public Task<User> GetUserById(Guid id)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public Task<Guid> CreateUser(User user)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public Task DeleteUser(Guid id)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public Task UpdateUser(Guid id, User user)
        {
            throw new  NotImplementedException();
        }
    }
}
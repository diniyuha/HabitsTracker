using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HabitsTracker.Data;
using HabitsTracker.Data.Entities;
using HabitsTracker.Logic.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace HabitsTracker.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        //TODO
        public UserService(AppDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public User GetUserById(Guid id)
        {
            var userEntity = _dbContext.Users.Find(id);
            if (userEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            return _mapper.Map<User>(userEntity);
        }

        public User GetAuthUser(string email, string password)
        {
            UserEntity user = _dbContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            return _mapper.Map<User>(user);
        }

        public Guid CreateUser(string email, string password)
        {
            var user = new UserEntity()
            {
                Id = new Guid(),
                Email = email,
                Password = password
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public void UpdateUser(Guid id, User user)
        {
            var userEntity = _dbContext.Users.Find(id);
            if (userEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _mapper.Map(user, userEntity);
            _dbContext.SaveChangesAsync();
        }

        public void DeleteUser(Guid id)
        {
            var userEntity = _dbContext.Users.Find(id);
            if (userEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _dbContext.Users.Remove(userEntity);
            _dbContext.SaveChangesAsync();
        }

        public bool CheckEmailForMatches(string email)
        {
            if (_dbContext.Users.Any(u => u.Email == email))
            {
                return true;
            }

            return false;
        }

        public async Task SendConfirmationEmailAsync(string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("YourApp", "yourapp@example.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Confirm your email address";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
                "<p>Please confirm your email address by clicking the following link:</p><a href='https://yourapp.com/confirm-email'>Confirm Email</a>";
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                // Get SMTP settings from configuration
                var smtpHost = _configuration["Smtp:Host"];
                var smtpPort = int.Parse(_configuration["Smtp:Port"]);
                var smtpUsername = _configuration["Smtp:Username"];
                var smtpPassword = _configuration["Smtp:Password"];

                // Connect to SMTP server and authenticate with provided credentials
                await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpUsername, smtpPassword);

                // Send the email and disconnect from the server
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
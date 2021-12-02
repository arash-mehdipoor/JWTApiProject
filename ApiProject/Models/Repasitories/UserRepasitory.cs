using ApiProject.Models.Context;
using ApiProject.Models.Entity;
using System;
using System.Linq;

namespace ApiProject.Models.Repasitories
{
    public class UserRepasitory
    {
        private readonly ToDoDbContext _dbContext;

        public UserRepasitory(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(User User)
        {
            _dbContext.Users.Add(User);
            _dbContext.SaveChanges();
        }

        public User GetUser(Guid Id)
        {
            User user = _dbContext.Users.SingleOrDefault(u => u.Id == Id);
            return user;
        }

        public bool ValidateUser(string UserName, string Password)
        {
            var user = _dbContext.Users.FirstOrDefault();
            return user != null ? true : false;
        }
    }
}

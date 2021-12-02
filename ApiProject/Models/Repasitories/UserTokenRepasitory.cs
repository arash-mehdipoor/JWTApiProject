using ApiProject.Models.Context;
using ApiProject.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiProject.Models.Repasitories
{
    public class UserTokenRepasitory
    {
        private readonly ToDoDbContext _dbContext;

        public UserTokenRepasitory(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveToken(UserToken userToken)
        {
            _dbContext.UserTokens.Add(userToken);
            _dbContext.SaveChanges();
        }
    }
}

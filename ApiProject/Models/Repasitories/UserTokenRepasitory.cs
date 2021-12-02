using ApiProject.Helpers;
using ApiProject.Models.Context;
using ApiProject.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public UserToken FindRefreshToken(string refreshToken)
        {
            string refreshtokenHash = SecurityHelper.GetSHa256Hash(refreshToken);
            var uerToken = _dbContext.UserTokens.Include(u => u.User)
                .SingleOrDefault(u => u.RefreshToken == refreshtokenHash);
            return uerToken;
        }

        public void DeleteToken(string RefreshToken)
        {
            var token = FindRefreshToken(RefreshToken);
            if (token != null)
            {
                _dbContext.UserTokens.Remove(token);
                _dbContext.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Models.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UserToken> UserTokens { get; set; }
    }

    public class UserToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime TokenExpier { get; set; }
        public string MobileModel { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}

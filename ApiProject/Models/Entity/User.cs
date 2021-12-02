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
        public string PhoneNumber { get; set; }
        public ICollection<UserToken> UserTokens { get; set; }
    }
}

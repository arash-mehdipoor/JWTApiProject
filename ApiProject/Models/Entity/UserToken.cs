using System;

namespace ApiProject.Models.Entity
{
    public class UserToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime TokenExpier { get; set; }
        public string MobileModel { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpier { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}

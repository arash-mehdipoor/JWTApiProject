using ApiProject.Models.Repasitories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiProject.Models.Services
{
    public class TokenValidator : ITokenValidator
    {
        private readonly UserRepasitory _userRepasitory;

        public TokenValidator(UserRepasitory userRepasitory)
        {
            _userRepasitory = userRepasitory;
        }

        public async Task Execute(TokenValidatedContext context)
        {
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail("Claims Not Found");
                return;
            }
            var userId = claimsIdentity.FindFirst("UserId").Value;

            if (!Guid.TryParse(userId, out Guid userGuid))
            {
                context.Fail("Claims Not Found");
                return;
            }
            var user = _userRepasitory.GetUser(userGuid);
            if (!user.IsActive)
            {
                context.Fail("User Not Active");
                return;
            }
        }
    }

}

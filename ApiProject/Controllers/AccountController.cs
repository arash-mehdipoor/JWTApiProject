using ApiProject.Helpers;
using ApiProject.Models.Entity;
using ApiProject.Models.Repasitories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration Configuration { get; }
        private UserTokenRepasitory _userTokenRepasitory { get; set; }
        private UserRepasitory _userRepasitory { get; set; }

        public AccountController(IConfiguration configuration,
            UserTokenRepasitory userTokenRepasitory,
            UserRepasitory userRepasitory
            )
        {
            Configuration = configuration;
            _userTokenRepasitory = userTokenRepasitory;
            _userRepasitory = userRepasitory;
        }

        /// <summary>
        /// JTW توکنهااااا
        /// </summary>
        /// <param name="userName">نام کاربری</param>
        /// <param name="password">رمز عبور</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(string userName, string password)
        {
            if (_userRepasitory.ValidateUser(userName, password))
            {
                var user = _userRepasitory.GetUser(Guid.Parse("76bea2ad-726e-40c3-a59c-6b56515fa46f"));

                var claims = new List<Claim>()
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("Name",user.Name),
                };

                var key = Configuration["JWTConfig:key"];
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenExpier = DateTime.Now.AddDays(2);

                var token = new JwtSecurityToken(
                    issuer: Configuration["JWTConfig:issuer"],
                    audience: Configuration["JWTConfig:audience"],
                    expires: tokenExpier,
                    notBefore: DateTime.Now,
                    claims: claims,
                    signingCredentials: credentials
                    );


                var JWTToken = new JwtSecurityTokenHandler().WriteToken(token);

                var tokenHash = SecurityHelper.GetSHa256Hash(JWTToken);
                _userTokenRepasitory.SaveToken(new UserToken()
                {
                    MobileModel = "IPone SX",
                    User = user,
                    TokenExpier = tokenExpier,
                    TokenHash = tokenHash
                });

                return Ok(JWTToken);
            }
            return Unauthorized();
        }
    }
}

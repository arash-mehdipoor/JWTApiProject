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
        public IActionResult Post(string PhoneNumber, string SmsCode)
        {
            var loginResult = _userRepasitory.Login(PhoneNumber, SmsCode);
            if (!loginResult.IsSuccess)
            {
                return Unauthorized(loginResult.Message);
            }

            CreateToken(loginResult.User);

            return Ok();
        }

        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefreshToken(string refreshToken)
        {
            var userToken = _userTokenRepasitory.FindRefreshToken(refreshToken);
            if (userToken == null)
            {
                return Unauthorized();
            }
            if (userToken.RefreshTokenExpier < DateTime.Now)
            {
                return Unauthorized("Token Expier");
            }
            var token = CreateToken(userToken.User);
            _userTokenRepasitory.DeleteToken(refreshToken);
            return Ok(token);
        }

        [HttpGet]
        [Route("GetSmsCode")]
        public IActionResult GetSms(string PhoneNumber)
        {
            var code = _userRepasitory.GetSmsCode(PhoneNumber);

            //send sms
            return Ok();
        }

        private LoginResultDto CreateToken(User user)
        {

 
            var claims = new List<Claim>()
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("Name",user.Name??""),
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
            var refreshToken = Guid.NewGuid();


            var tokenHash = SecurityHelper.GetSHa256Hash(JWTToken);
            var refreshTokenHash = SecurityHelper.GetSHa256Hash(refreshToken.ToString());

            _userTokenRepasitory.SaveToken(new UserToken()
            {
                MobileModel = "IPone SX",
                User = user,
                TokenExpier = tokenExpier,
                TokenHash = tokenHash,
                RefreshToken = refreshTokenHash,
                RefreshTokenExpier = DateTime.Now.AddDays(30)
            });

            return new LoginResultDto()
            {
                Token = JWTToken,
                RefreshToken = refreshToken.ToString()
            };
        }
    }
}

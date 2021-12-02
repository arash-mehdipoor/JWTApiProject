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

        public string GetSmsCode(string PhoneNumber)
        {
            Random random = new Random();
            var code = random.Next(10000, 99999).ToString();
            SmsCode smsCode = new SmsCode()
            {
                Code = code,
                ExpierDate = DateTime.Now,
                PhoneNumber = PhoneNumber,
                RequestCount = 0,
                Used = false
            };
            _dbContext.smsCodes.Add(smsCode);
            _dbContext.SaveChanges();
            return code;
        }

        public LoginDto Login(string PhoneNumber, string code)
        {
            var SmsCode = _dbContext.smsCodes.Where(s => s.PhoneNumber == PhoneNumber
            && s.Code == code).FirstOrDefault();
            if (SmsCode == null)
            {
                return new LoginDto()
                {
                    IsSuccess = false,
                    Message = "کد وارد شده صحیح نیست",
                };
            }
            else
            {
                if (SmsCode.Used)
                {
                    return new LoginDto()
                    {
                        IsSuccess = false,
                        Message = "این کد قبلا استفاده شده است"
                    };
                }
            }
            SmsCode.RequestCount++;
            SmsCode.Used = true;
            _dbContext.SaveChanges();

            var user = FindUserByPhoneNumber(PhoneNumber);
            if (user != null) 
            {
                return new LoginDto()
                {
                    IsSuccess = true, 
                    User = user
                };
            }
            else
            {
                user = RegisterUser(PhoneNumber);
                return new LoginDto()
                {
                    IsSuccess = true,
                    User = user
                };
            }
        }

        public User FindUserByPhoneNumber(string PhoneNumber)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.PhoneNumber == PhoneNumber);
            return user; 
        }

        public User RegisterUser(string PhoneNumber)
        {
            User user = new User()
            {
                IsActive = true,
                PhoneNumber = PhoneNumber
            };
            return user;
        }
    }

    public class LoginDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }


}
